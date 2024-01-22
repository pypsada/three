using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using UnityEngine;

public static class NetManager
{
    //Socket
    static Socket socket;
    //接收缓冲区
    static ByteArray readBuff;
    //写入队列
    static Queue<ByteArray> writeQueue;

    //事件
    public enum NetEvent
    {
        ConnectSucc=1,
        ConnectFail,
        Close
    }

    //事件委托类型  
    public delegate void EventListener(string err);
    //事件监听列表
    private static Dictionary<NetEvent, EventListener> eventListener = new();

    //添加事件监听
    public static void AddEventListener(NetEvent netEvent,EventListener listener)
    {
        if(eventListener.ContainsKey(netEvent))
        {
            eventListener[netEvent] += listener;
        }
        else
        {
            eventListener[netEvent] = listener;
        }
    }
    //删除事件监听
    public static void RemoveEventListener(NetEvent netEvent,EventListener listener)
    {
        if (eventListener.ContainsKey(netEvent))
        {
            eventListener[netEvent] -= listener;
            if (eventListener[netEvent] == null)
            {
                eventListener.Remove(netEvent);
            }
        }
    }

    //分发事件
    private static void FireEvent(NetEvent netEvent,string err)
    {
        if(eventListener.ContainsKey(netEvent))
        {
            eventListener[netEvent](err);
        }
    }

    //初始化状态
    private static void InitState()
    {
        //Socket
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //接收缓冲区
        readBuff = new();
        //是否正在连接
        isConnecting = false;
        //是否正在关闭
        isClosing = false;
        //消息列表
        msgList = new();
        //消息列表长度
        msgCount = 0;
        //心跳时间
        lastPingTime = Time.time;
        lastPongTime = Time.time;
        //监听Pong协议
        if(!msgListeners.ContainsKey("MsgPong"))
        {
            AddMsgListener("MsgPong", OnMsgPong);
        }
    }

    //是否正在连接
    static bool isConnecting = false;

    //连接
    public static void Connect(string ip,int port)
    {
        if(socket!=null&&socket.Connected)
        {
            Debug.Log("Conn fail,already connected!");
            return;
        }
        if(isConnecting)
        {
            Debug.Log("Connect fail, is connecting");
            return;
        }
        InitState();
        socket.NoDelay = true;
        isConnecting = true;
        socket.BeginConnect(ip, port, ConnectCallBack, socket);
    }

    private static void ConnectCallBack(IAsyncResult ar)
    {
        try
        {
            Socket socket = (Socket)ar.AsyncState;
            socket.EndConnect(ar);
            Debug.Log("Socket Conn Succ");
            FireEvent(NetEvent.ConnectSucc, "");
            isConnecting = false;
            //开始接收
            socket.BeginReceive(readBuff.bytes, readBuff.writeIdx, readBuff.Remain, 0, ReceiveCallback, socket);
        }
        catch(SocketException ex)
        {
            Debug.Log("Socket Conn fail " + ex.ToString());
            FireEvent(NetEvent.ConnectFail, ex.ToString());
            isConnecting = false;
        }
    }

    //是否正在关闭
    static bool isClosing = false;

    //关闭连接
    public static void Close()
    {
        //状态判断
        if(socket!=null&&!socket.Connected)
        {
            return;
        }
        if(isConnecting)
        {
            return;
        }
        //还有数据在发送
        if(writeQueue.Count>0)
        {
            isClosing = true;
        }
        else
        {
            socket.Close();
            FireEvent(NetEvent.Close, "");
        }
    }

    //发送数据
    public static void Send(MsgBase msg)
    {
        if(socket==null||!socket.Connected)
        {
            return;
        }
        if (isConnecting)
        {
            return;
        }
        if (isClosing)
        {
            return;
        }

        //数据编码
        byte[] nameBytes = MsgBase.EncodeName(msg);
        byte[] bodyBytes = MsgBase.Encode(msg);
        int len = nameBytes.Length + bodyBytes.Length;
        byte[] sendBytes = new byte[len + 2];
        sendBytes[0] = (byte)(len % 256);
        sendBytes[1] = (byte)(len / 256);
        Array.Copy(nameBytes, 0, sendBytes, 2, nameBytes.Length);
        Array.Copy(bodyBytes, 0, sendBytes, 2 + nameBytes.Length, bodyBytes.Length);
        ByteArray ba = new ByteArray(sendBytes);
        int count = 0;
        if (writeQueue == null) writeQueue = new();
        lock(writeQueue)
        {
            writeQueue.Enqueue(ba);
            count = writeQueue.Count;
        }
        //Send
        if(count==1)
        {
            socket.BeginSend(sendBytes, 0, sendBytes.Length, 0, SendCallback, socket);
        }
    }
    private static void SendCallback(IAsyncResult ar)
    {
        Socket socket = (Socket)ar.AsyncState;
        if(socket==null||!socket.Connected)
        {
            return;
        }
        int count = socket.EndSend(ar);
        Debug.Log("Send " + count.ToString());
        ByteArray ba;
        lock(writeQueue)
        {
            ba = writeQueue.First();
        }
        ba.readIdx += count;
        if (ba.Length == 0)
        {
            lock(writeQueue)
            {
                writeQueue.Dequeue();
                ba = writeQueue.First();
            }
        }
        if(ba!=null)
        {
            socket.BeginSend(ba.bytes, ba.readIdx, ba.Length,0,SendCallback,socket);
        }
        else if(isClosing)
        {
            socket.Close();
        }
    }

    //消息委托事件
    public delegate void MsgListener(MsgBase msgBase);
    //消息监听列表
    private static Dictionary<string, MsgListener> msgListeners = new();

    //添加消息监听
    public static void AddMsgListener(string msgName, MsgListener listener)
    {
        if (msgListeners.ContainsKey(msgName))
        {
            msgListeners[msgName] += listener;
        }
        else
        {
            msgListeners[msgName] = listener;
        }
    }

    //删除消息监听
    public static void RemoveMsgListener(string msgName, MsgListener listener)
    {
        if (msgListeners.ContainsKey(msgName))
        {
            msgListeners[msgName] -= listener;
            if (msgListeners[msgName] == null)
            {
                msgListeners.Remove(msgName);
            }
        }
    }
    //分发消息
    private static void FireMsg(string msgName, MsgBase msgBase)
    {
        if (msgListeners.ContainsKey(msgName))
        {
            msgListeners[msgName](msgBase);
        }
    }

    //消息列表
    static List<MsgBase> msgList = new();
    //消息列表长度
    static int msgCount = 0;
    //最多读取消息数量
    readonly static int MAX_MESSAGE_FIRE = 10;

    private static void ReceiveCallback(IAsyncResult ar)
    {
        try
        {
            Debug.Log("Receive callback.");
            Socket socket = (Socket)ar.AsyncState;
            int count = socket.EndReceive(ar);
            Debug.Log("Receive "+count.ToString());
            if(count==0)
            {
                Close();
                return;
            }
            readBuff.writeIdx += count;
            OnReceiveData();
            if(readBuff.Remain<8)
            {
                readBuff.MoveBytes();
                readBuff.ReSize(readBuff.Length * 2);
            }
            socket.BeginReceive(readBuff.bytes, readBuff.writeIdx, readBuff.Remain, 0, ReceiveCallback, socket);
        }
        catch(SocketException ex)
        {
            Debug.Log("Socket Receive fail " + ex.ToString());
        }
    }

    public static void OnReceiveData()
    {
        if (readBuff.Length <= 2) return;
        Debug.Log("More than 2, begin Receive Data.");
        int readIdx = readBuff.readIdx;
        byte[] bytes = readBuff.bytes;
        Int16 bodyLength = (Int16)((bytes[readIdx + 1] << 8) | bytes[readIdx]);
        if (readBuff.Length < bodyLength + 2) return;
        Debug.Log("Length is enough, continuing receive data.");
        readBuff.readIdx += 2;
        int nameCount = 0;
        string protoName = MsgBase.DecodeName(readBuff.bytes, readBuff.readIdx, out nameCount);
        Debug.Log("Have decoded the protoName. Lengh of nameCount: "+nameCount.ToString());
        Debug.Log("The bodylengh:" + bodyLength.ToString());
        if(protoName=="")
        {
            Debug.Log("OnReceiveData MsgBase.DecodeName fail");
            return;
        }
        readBuff.readIdx += nameCount;
        int bodyCount = bodyLength - nameCount;
        MsgBase msgBase = MsgBase.Decode(protoName, readBuff.bytes, readBuff.readIdx,bodyCount);
        Debug.Log("Have decoded the protoBody. Lengh of protoBody:" + bodyCount);
        readBuff.readIdx += bodyCount;
        readBuff.CheckAndMoveBytes();
        lock(msgList)
        {
            msgList.Add(msgBase);
        }
        msgCount++;
        if(readBuff.Length>2)
        {
            OnReceiveData();
        }
    }

    //是否使用心跳
    public static bool isUsePing = true;
    //心跳间隔时间
    public static int pingInterval = 30;
    //上一次发送ping时间
    static float lastPingTime = 0;
    //上一次受到pong时间
    static float lastPongTime = 0;

    //Send proto ping
    private static void PingUpdate()
    {
        if (!isUsePing) return;
        //Send ping
        if(Time.time-lastPingTime>pingInterval)
        {
            MsgPing msgPing = new();
            Send(msgPing);
            lastPingTime = Time.time;
        }
        //检测pong时间
        if(Time.time-lastPongTime>pingInterval*4)
        {
            Debug.Log("PingUpdate Close Connection:Too long time has no MsgPong from server");
            Close();
        }
    }

    //监听Pong协议
    private static void OnMsgPong(MsgBase msgBase)
    {
        lastPongTime = Time.time;
    }

    //MsgUpdate
    private static void MsgUpdate()
    {
        if (msgCount == 0) return;
        for(int i=0;i<MAX_MESSAGE_FIRE;i++)
        {
            MsgBase msgBase = null;
            lock(msgList)
            {
                if(msgList.Count>0)
                {
                    msgBase = msgList[0];
                    msgList.RemoveAt(0);
                    msgCount--;
                }
            }
            if(msgBase!=null)
            {
                FireMsg(msgBase.protoName, msgBase);
            }
            else
            {
                break;
            }
        }
    }

    public static void Update()
    {
        MsgUpdate();
        PingUpdate();
    }
}
