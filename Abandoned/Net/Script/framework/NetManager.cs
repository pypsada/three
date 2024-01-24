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
    //���ջ�����
    static ByteArray readBuff;
    //д�����
    static Queue<ByteArray> writeQueue;

    //�¼�
    public enum NetEvent
    {
        ConnectSucc=1,
        ConnectFail,
        Close
    }

    //�¼�ί������  
    public delegate void EventListener(string err);
    //�¼������б�
    private static Dictionary<NetEvent, EventListener> eventListener = new();

    //����¼�����
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
    //ɾ���¼�����
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

    //�ַ��¼�
    private static void FireEvent(NetEvent netEvent,string err)
    {
        if(eventListener.ContainsKey(netEvent))
        {
            eventListener[netEvent](err);
        }
    }

    //��ʼ��״̬
    private static void InitState()
    {
        //Socket
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //���ջ�����
        readBuff = new();
        //�Ƿ���������
        isConnecting = false;
        //�Ƿ����ڹر�
        isClosing = false;
        //��Ϣ�б�
        msgList = new();
        //��Ϣ�б���
        msgCount = 0;
        //����ʱ��
        lastPingTime = Time.time;
        lastPongTime = Time.time;
        //����PongЭ��
        if(!msgListeners.ContainsKey("MsgPong"))
        {
            AddMsgListener("MsgPong", OnMsgPong);
        }
    }

    //�Ƿ���������
    static bool isConnecting = false;

    //����
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
            //��ʼ����
            socket.BeginReceive(readBuff.bytes, readBuff.writeIdx, readBuff.Remain, 0, ReceiveCallback, socket);
        }
        catch(SocketException ex)
        {
            Debug.Log("Socket Conn fail " + ex.ToString());
            FireEvent(NetEvent.ConnectFail, ex.ToString());
            isConnecting = false;
        }
    }

    //�Ƿ����ڹر�
    static bool isClosing = false;

    //�ر�����
    public static void Close()
    {
        //״̬�ж�
        if(socket!=null&&!socket.Connected)
        {
            return;
        }
        if(isConnecting)
        {
            return;
        }
        //���������ڷ���
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

    //��������
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

        //���ݱ���
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

    //��Ϣί���¼�
    public delegate void MsgListener(MsgBase msgBase);
    //��Ϣ�����б�
    private static Dictionary<string, MsgListener> msgListeners = new();

    //�����Ϣ����
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

    //ɾ����Ϣ����
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
    //�ַ���Ϣ
    private static void FireMsg(string msgName, MsgBase msgBase)
    {
        if (msgListeners.ContainsKey(msgName))
        {
            msgListeners[msgName](msgBase);
        }
    }

    //��Ϣ�б�
    static List<MsgBase> msgList = new();
    //��Ϣ�б���
    static int msgCount = 0;
    //����ȡ��Ϣ����
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

    //�Ƿ�ʹ������
    public static bool isUsePing = true;
    //�������ʱ��
    public static int pingInterval = 30;
    //��һ�η���pingʱ��
    static float lastPingTime = 0;
    //��һ���ܵ�pongʱ��
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
        //���pongʱ��
        if(Time.time-lastPongTime>pingInterval*4)
        {
            Debug.Log("PingUpdate Close Connection:Too long time has no MsgPong from server");
            Close();
        }
    }

    //����PongЭ��
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
