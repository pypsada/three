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
    //是否正在连接
    static bool isConnecting = false;
    //检测是否正在关闭
    static bool isClosing = false;
    //消息委托类型
    public delegate void MsgListener(MsgBase msgBase);
    //消息监听列表
    private static Dictionary<string, MsgListener> msgListeners = new();
    //消息列表
    static List<MsgBase> msgList = new();
    //消息列表长度
    static int msgCount = 0;
    //每一次Update处理的消息量
    readonly static int MAX_MESSAGE_FIRE = 10;

    //心跳
    //是否启用心跳(心跳没写完，不要启用心跳
    [Tooltip("心跳没写完，别启用心跳")]
    public static bool isUsePing = false;
    //心跳间隔时间
    public static int pingInterval = 30;
    //上一次发送Ping时间
    static float lastPingTime = 0;
    //上一次受到Pong时间
    static float lastPongTime = 0;

    //Net event
    public enum NetEvent
    {
        ConnectSucc = 1,
        ConnectFail,
        Close
    }

    //事件委托类型
    public delegate void EventListener(string err);
    //事件监听列表
    private static Dictionary<NetEvent, EventListener> eventListeners = new();

    //初始化状态
    private static void InitState()
    {
        //Socket
        socket = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //Read buffer
        readBuff = new();
        //写入队列
        writeQueue = new();
        //是否连接
        isConnecting = false;
        //是否正在关闭
        isClosing = false;
        //消息列表
        msgList = new();
        //消息列表长度
        msgCount = 0;
        //上一次发送Ping时间
        lastPingTime = Time.time;
        //上一次受到Pong时间
        lastPongTime = Time.time;
        if (!msgListeners.ContainsKey("MsgPong"))
        {
            AddMsgListener("MsgPong", OnMsgPong);
        }
    }

    //心跳
    //发送Ping协议
    private static void PingUpdate()
    {
        if (!isUsePing) return;
        //Send Ping
        if (Time.time - lastPingTime > pingInterval)
        {
            /*
             * 由于心跳机制还未完善，这段暂时不要。
            MsgPing msgPing = new();
            Send(msgPing);
            */
            lastPingTime = Time.time;
        }
        //检测时间
        if (Time.time - lastPongTime > pingInterval * 4)
        {
            Close();
        }
    }
    //监听Pong
    private static void OnMsgPong(MsgBase msgBase)
    {
        lastPongTime = Time.time;
    }

    //添加事件监听
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

    //删除事件监听
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

    //分发事件
    private static void FireEvent(NetEvent netEvent, string err)
    {
        if (eventListeners.ContainsKey(netEvent))
        {
            eventListeners[netEvent](err);
        }
    }

    //连接
    public static void Connect(string ip, int port)
    {
        //是否已经连接？
        if (socket != null && socket.Connected)
        {
            Debug.Log("Connect fail, already connected!");
            return;
        }
        //是否正在连接？
        if (isConnecting)
        {
            Debug.Log("Connect fail, isConnecting!");
            return;
        }

        //init
        InitState();
        //参数设置
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
            //开始接收
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
        //是否还有数据在发送
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

    //发送数据
    public static void Send(MsgBase msg)
    {
        //状态判断
        if (socket == null || !socket.Connected) return;
        if (isConnecting) return;
        if (isClosing) return;
        byte[] sendBytes = msg.Encode();
        //写入队列
        ByteArray ba = new ByteArray(sendBytes);
        int count = 0;  //队列的长度

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
        //状态判断
        if (socket == null || !socket.Connected) return;
        //End
        int count = socket.EndSend(ar);
        //获取队列中数据
        ByteArray ba;
        lock (writeQueue)
        {
            ba = writeQueue.First();
        }
        //完整发送
        ba.readIdx += count;
        if (ba.length == 0)
        {
            lock (writeQueue)
            {
                writeQueue.Dequeue();
                ba = writeQueue.First();
            }
        }
        //继续发送
        if (ba != null)
        {
            socket.BeginSend(ba.bytes, ba.readIdx, ba.length, 0, SendCallBack, socket);
        }
        else if (isClosing)
        {
            socket.Close();
        }
    }

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
            //消息处理
            OnReceiveData();
            //继续接受数据
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


    //消息处理
    public static void OnReceiveData()
    {
        if (readBuff.length <= 2) return;

        int readIdx = readBuff.readIdx;
        byte[] bytes = readBuff.bytes;
        Int16 bodyLength = (Int16)((bytes[readIdx + 1] << 8) | bytes[readIdx]);
        if (bytes.Length < 2 + bodyLength) return;
        readBuff.readIdx += 2;
        //解析协议名
        int msgCount = 0;
        MsgBase msgBase = MsgBase.Decode(readBuff.bytes,readBuff.readIdx,out msgCount);
        if (msgBase==null)
        {
            Debug.Log("OnReceiveData MsgBase.DecodeName fail");
            return;
        }
        readBuff.readIdx += msgCount;
        //添加到消息队列
        lock (msgList)
        {
            msgList.Add(msgBase);
        }
        msgCount++;
        //继续读取消息
        if (readBuff.length > 2)
        {
            OnReceiveData();
        }
    }

    //更新消息
    public static void MsgUpdate()
    {
        if (msgCount == 0) return;
        Debug.Log("MsgFire");
        for (int i = 0; i < MAX_MESSAGE_FIRE; i++)
        {
            MsgBase msgBase = null;
            //获取第一条消息
            lock (msgList)
            {
                if (msgList.Count > 0)
                {
                    msgBase = msgList[0];
                    msgList.RemoveAt(0);
                    msgCount--;
                }
            }

            //如果有消息则转发
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
