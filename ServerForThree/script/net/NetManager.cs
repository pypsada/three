using System.Net;
using System.Net.Sockets;
using System.Reflection;

class NetManager
{
    //监听Socket
    public static Socket listenfd;
    //客户端Socket及状态信息
    public static Dictionary<Socket, ClientState> clients = new();

    //Socket的检查列表
    static List<Socket> checkRead = new();

    public static void StartLoop(int listenPort)
    {
        //Socket
        listenfd = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //Bind
        IPAddress ipAdr = IPAddress.Parse("0.0.0.0");
        IPEndPoint ipEd = new IPEndPoint(ipAdr, listenPort);
        listenfd.Bind(ipEd);
        //Listen
        listenfd.Listen(0);
        LogManager.Log("[Server]Listening...");
        //Loop
        while(true)
        {
            ResetCheckRead();
            Socket.Select(checkRead, null, null, 1000000);
            //检查可读对象
            for(int i=checkRead.Count-1;i>=0;i--)
            {
                Socket s = checkRead[i];
                if(s==listenfd)
                {
                    try
                    {
                        ReadListenfd(s);
                    }
                    catch(Exception ex)
                    {
                        LogManager.Log("ReadListenfd err:"+ex.Message);
                    }
                }
                else
                {
                    try
                    {
                        ReadClientfd(s);
                    }
                    catch(Exception ex)
                    {
                        LogManager.Log("ReadClientfd " + s.RemoteEndPoint.ToString() + " err:" + ex.Message);
                        Close(clients[s]);
                    }
                }
            }
            //超时
            try
            {
                Timer();
            }
            catch(Exception ex)
            {
                LogManager.Log("Timer err:" + ex.Message);
            }
        }
    }

    //Reset the checkRead list
    public static void ResetCheckRead()
    {
        checkRead.Clear();
        checkRead.Add(listenfd);
        foreach(ClientState s in clients.Values)
        {
            checkRead.Add(s.socket);
        }
    }

    public static void ReadListenfd(Socket listenfd)
    {
        try
        {
            Socket clientfd = listenfd.Accept();
            LogManager.Log("Accept " + clientfd.RemoteEndPoint.ToString());
            ClientState state = new();
            state.socket = clientfd;
            state.lastPingTime = GetTimeStamp();
            clients.Add(clientfd, state);
        }
        catch(SocketException ex)
        {
            LogManager.Log("Accept fail: " + ex.ToString());
        }
    }

    public static void ReadClientfd(Socket clientfd)
    {
        ClientState state = clients[clientfd];
        ByteArray readBuff = state.readBuff;
        //接收
        int count = 0;
        //缓冲区长度问题
        if(readBuff.Remain<=0)
        {
            OnReceiveData(state);
            readBuff.MoveBytes();
        }
        if(readBuff.Remain<=0)
        {
            LogManager.Log("Recv fail, maybe msg length > buff capacity");
            Close(state);
            return;
        }

        try
        {
            count = clientfd.Receive(readBuff.bytes, readBuff.writeIdx, readBuff.Remain, 0);
        }
        catch(SocketException ex)
        {
            LogManager.Log("Recv socketException " + ex.ToString());
            Close(state);
            return;
        }

        //Client Close
        if (count <= 0)
        {
            LogManager.Log("Socket Close " + clientfd.RemoteEndPoint.ToString());
            Close(state);
            return;
        }
        LogManager.Log("Recv " + count.ToString() + " from " + clientfd.RemoteEndPoint.ToString() + ":\n" +
            System.Text.Encoding.UTF8.GetString(readBuff.bytes, readBuff.writeIdx, count), false, true);
        LogManager.Log("Recv " + count.ToString() + " from " + clientfd.RemoteEndPoint.ToString(), true, false);
        readBuff.writeIdx += count;
        OnReceiveData(state);
        readBuff.CheckAndMoveBytes();
    }

    public static void Close(ClientState state)
    {
        //Fire event
        MethodInfo mei = typeof(EventHandler).GetMethod("OnDisconnect");
        object[] ob = {state};
        mei.Invoke(null, ob);
        //Close
        state.socket.Close();
        clients.Remove(state.socket);
    }

    public static void OnReceiveData(ClientState state)
    {
        ByteArray readBuff = state.readBuff;
        byte[] bytes = readBuff.bytes;
        //Msg length
        if (readBuff.Length < 2) return;
        Int16 bodyLength = (Int16)((bytes[readBuff.readIdx + 1] << 8) | bytes[readBuff.readIdx]);
        if (readBuff.Length < bodyLength) return;
        readBuff.readIdx += 2;
        //Decode proto name
        int nameCount = 0;
        string protoName = MsgBase.DecodeName(readBuff.bytes, readBuff.readIdx, out nameCount);
        if (protoName != "MsgPing" && !state.hasPing)
        {
            LogManager.Log("Unallowed clients:" + state.socket.RemoteEndPoint.ToString());
            Close(state);
            return;
        }
        if(protoName=="")
        {
            LogManager.Log("OnReceiveData MsgBase.DecodeName fail");
            Close(state);
            return;
        }
        readBuff.readIdx += nameCount;
        //Decode proto body
        int bodyCount = bodyLength - nameCount;
        MsgBase msgBase = MsgBase.Decode(protoName, readBuff.bytes, readBuff.readIdx, bodyCount);
        readBuff.readIdx += bodyCount;
        readBuff.CheckAndMoveBytes();
        //Fire msg
        MethodInfo mi = typeof(MsgHandler).GetMethod(protoName);
        object[] o = { state, msgBase };
        LogManager.Log("OnReceive:" + protoName + " from " + state.socket.RemoteEndPoint.ToString());
        if(mi!=null)
        {
            mi.Invoke(null, o);
        }
        else
        {
            LogManager.Log("OnReceiveData Invoke fail:" + protoName);
        }
        if(readBuff.Length>2)
        {
            OnReceiveData(state);
        }
    }

    public static void Timer()
    {
        //Msg fire
        MethodInfo mei = typeof(EventHandler).GetMethod("OnTimer");
        object[] ob = { };
        mei.Invoke(null, ob);
    }

    public static void Send(ClientState cs,MsgBase msg)
    {
        if (cs == null) return;
        if (!cs.socket.Connected) return;
        //Encode
        byte[] nameBytes = MsgBase.EncodeName(msg);
        byte[] bodyBytes = MsgBase.Encode(msg);
        int len = nameBytes.Length + bodyBytes.Length;
        byte[] sendBytes = new byte[2 + len];
        //pack len
        sendBytes[0] = (byte)(len % 256);
        sendBytes[1] = (byte)(len / 256);
        //pack
        Array.Copy(nameBytes, 0, sendBytes, 2, nameBytes.Length);
        Array.Copy(bodyBytes, 0, sendBytes, 2 + nameBytes.Length, bodyBytes.Length);
        //为了简化代码，暂时不使用回调
        try
        {
            cs.socket.BeginSend(sendBytes, 0, sendBytes.Length, 0, SendCallback, cs.socket);
        }
        catch(SocketException ex)
        {
            LogManager.Log("Socket Close on BeginSend:" + ex.ToString());
        }
    }

    private static void SendCallback(IAsyncResult ar)
    {
        Socket socket = (Socket)ar.AsyncState;
        int count = socket.EndSend(ar);
        LogManager.Log("Send to " + socket.RemoteEndPoint.ToString() + " " + count.ToString());
    }

    //获取时间戳
    public static long GetTimeStamp()
    {
        TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
        return Convert.ToInt64(ts.TotalSeconds);
    }

    //Ping间隔
    public static long pingInterval = 30;
}