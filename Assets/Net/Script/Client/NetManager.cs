using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public static class NetManager
{
    //Socket
    static Socket socket;
    //Read buffer
    static ByteArray readBuff;
    //Write Queue
    static Queue<ByteArray> writeQueue;
    //�Ƿ���������
    static bool isConnecting = false;
    //����Ƿ����ڹر�
    static bool isClosing = false;
    //��Ϣί������
    public delegate void MsgListener(MsgBase msgBase);
    //��Ϣ�����б�
    private static Dictionary<string, MsgListener> msgListeners = new();
    //��Ϣ�б�
    static List<MsgBase> msgList = new();
    //��Ϣ�б���
    static int msgCount = 0;
    //ÿһ��Update�������Ϣ��
    readonly static int MAX_MESSAGE_FIRE = 10;

    //����
    //�Ƿ���������(����ûд�꣬��Ҫ��������
    [Tooltip("����ûд�꣬����������")]
    public static bool isUsePing = false;
    //�������ʱ��
    public static int pingInterval = 30;
    //��һ�η���Pingʱ��
    static float lastPingTime = 0;
    //��һ���ܵ�Pongʱ��
    static float lastPongTime = 0;

    //Net event
    public enum NetEvent
    {
        ConnectSucc = 1,
        ConnectFail,
        Close
    }

    //�¼�ί������
    public delegate void EventListener(string err);
    //�¼������б�
    private static Dictionary<NetEvent, EventListener> eventListeners = new();

    //��ʼ��״̬
    private static void InitState()
    {
        //Socket
        socket = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //Read buffer
        readBuff = new();
        //д�����
        writeQueue = new();
        //�Ƿ�����
        isConnecting = false;
        //�Ƿ����ڹر�
        isClosing = false;
        //��Ϣ�б�
        msgList = new();
        //��Ϣ�б���
        msgCount = 0;
        //��һ�η���Pingʱ��
        lastPingTime = Time.time;
        //��һ���ܵ�Pongʱ��
        lastPongTime = Time.time;
        if (!msgListeners.ContainsKey("MsgPong"))
        {
            AddMsgListener("MsgPong", OnMsgPong);
        }
    }

    //����
    //����PingЭ��
    private static void PingUpdate()
    {
        if (!isUsePing) return;
        //Send Ping
        if (Time.time - lastPingTime > pingInterval)
        {
            /*
             * �����������ƻ�δ���ƣ������ʱ��Ҫ��
            MsgPing msgPing = new();
            Send(msgPing);
            */
            lastPingTime = Time.time;
        }
        //���ʱ��
        if (Time.time - lastPongTime > pingInterval * 4)
        {
            Close();
        }
    }
    //����Pong
    private static void OnMsgPong(MsgBase msgBase)
    {
        lastPongTime = Time.time;
    }

    //����¼�����
    public static void AddEventListener(NetEvent netEvent, EventListener listener)
    {
        if (eventListeners.ContainsKey(netEvent))
        {
            eventListeners[netEvent] += listener;
        }
        else
        {
            eventListeners[netEvent] = listener;
        }
    }

    //ɾ���¼�����
    public static void RemoveEventListener(NetEvent netEvent, EventListener listener)
    {
        if (eventListeners.ContainsKey(netEvent))
        {
            eventListeners[netEvent] -= listener;
            if (eventListeners[netEvent] == null)
            {
                eventListeners.Remove(netEvent);
            }
        }
    }

    //�ַ��¼�
    private static void FireEvent(NetEvent netEvent, string err)
    {
        if (eventListeners.ContainsKey(netEvent))
        {
            eventListeners[netEvent](err);
        }
    }

    //����
    public static void Connect(string ip, int port)
    {
        //�Ƿ��Ѿ����ӣ�
        if (socket != null && socket.Connected)
        {
            Debug.Log("Connect fail, already connected!");
            return;
        }
        //�Ƿ��������ӣ�
        if (isConnecting)
        {
            Debug.Log("Connect fail, isConnecting!");
            return;
        }

        //init
        InitState();
        //��������
        socket.NoDelay = true;
        //Connect
        isConnecting = true;
        socket.BeginConnect(ip, port, ConnectCallBack, socket);
    }

    private static void ConnectCallBack(IAsyncResult ar)
    {
        try
        {
            Socket socket = (Socket)ar.AsyncState;
            socket.EndConnect(ar);
            Debug.Log("Socket Connect Succ");
            FireEvent(NetEvent.ConnectSucc, "");
            isConnecting = false;
            //��ʼ����
            socket.BeginReceive(readBuff.bytes, readBuff.writeIdx, readBuff.remain,
               0, ReceiveCallBack, socket);
        }
        catch (SocketException ex)
        {
            Debug.Log("Socket Connect fail " + ex.ToString());
            FireEvent(NetEvent.ConnectFail, ex.ToString());
            isConnecting = false;
        }
    }

    public static void Close()
    {
        if (socket == null || !socket.Connected) return;
        if (isClosing) return;
        //�Ƿ��������ڷ���
        if (writeQueue.Count > 0)
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
        //״̬�ж�
        if (socket == null || !socket.Connected) return;
        if (isConnecting) return;
        if (isClosing) return;
        byte[] sendBytes = msg.Encode();
        //д�����
        ByteArray ba = new ByteArray(sendBytes);
        int count = 0;  //���еĳ���

        lock (writeQueue)
        {
            writeQueue.Enqueue(ba);
            count = writeQueue.Count;
        }
        //Send
        if (count == 1)
        {
            socket.BeginSend(sendBytes, 0, sendBytes.Length, 0, SendCallBack, socket);
        }
    }

    private static void SendCallBack(IAsyncResult ar)
    {
        Socket socket = (Socket)ar.AsyncState;
        //״̬�ж�
        if (socket == null || !socket.Connected) return;
        //End
        int count = socket.EndSend(ar);
        //��ȡ����������
        ByteArray ba;
        lock (writeQueue)
        {
            ba = writeQueue.First();
        }
        //��������
        ba.readIdx += count;
        if (ba.length == 0)
        {
            lock (writeQueue)
            {
                writeQueue.Dequeue();
                ba = writeQueue.First();
            }
        }
        //��������
        if (ba != null)
        {
            socket.BeginSend(ba.bytes, ba.readIdx, ba.length, 0, SendCallBack, socket);
        }
        else if (isClosing)
        {
            socket.Close();
        }
    }

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

    private static void ReceiveCallBack(IAsyncResult ar)
    {
        try
        {
            Socket socket = (Socket)ar.AsyncState;
            int count = socket.EndReceive(ar);
            if (count == 0)
            {
                Close();
                return;
            }
            readBuff.writeIdx += count;
            //��Ϣ����
            OnReceiveData();
            //������������
            if (readBuff.remain < 8)
            {
                readBuff.MoveBytes();
                readBuff.ReSize(readBuff.length * 2);
            }
            socket.BeginReceive(readBuff.bytes, readBuff.writeIdx, readBuff.remain,
                0, ReceiveCallBack, socket);
        }
        catch (SocketException ex)
        {
            Debug.Log("Socket Receive fail " + ex.ToString());
        }
    }


    //��Ϣ����
    public static void OnReceiveData()
    {
        if (readBuff.length <= 2) return;

        int readIdx = readBuff.readIdx;
        byte[] bytes = readBuff.bytes;
        Int16 bodyLength = (Int16)((bytes[readIdx + 1] << 8) | bytes[readIdx]);
        if (bytes.Length < 2 + bodyLength) return;
        readBuff.readIdx += 2;
        //����Э����
        int msgCount = 0;
        MsgBase msgBase = MsgBase.Decode(readBuff.bytes,readBuff.readIdx,out msgCount);
        if (msgBase==null)
        {
            Debug.Log("OnReceiveData MsgBase.DecodeName fail");
            return;
        }
        readBuff.readIdx += msgCount;
        //��ӵ���Ϣ����
        lock (msgList)
        {
            msgList.Add(msgBase);
        }
        msgCount++;
        //������ȡ��Ϣ
        if (readBuff.length > 2)
        {
            OnReceiveData();
        }
    }

    //������Ϣ
    public static void MsgUpdate()
    {
        if (msgCount == 0) return;
        Debug.Log("MsgFire");
        for (int i = 0; i < MAX_MESSAGE_FIRE; i++)
        {
            MsgBase msgBase = null;
            //��ȡ��һ����Ϣ
            lock (msgList)
            {
                if (msgList.Count > 0)
                {
                    msgBase = msgList[0];
                    msgList.RemoveAt(0);
                    msgCount--;
                }
            }

            //�������Ϣ��ת��
            if (msgBase != null)
            {
                FireMsg(msgBase.protoName, msgBase);
            }
            else
            {
                break;
            }
        }
    }
}
