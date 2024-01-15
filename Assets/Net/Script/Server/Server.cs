using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System;
using UnityEditor.PackageManager;
using System.Globalization;

public class Server : MonoBehaviour
{
    public bool isServer;
    public string localIpAdress;
    public int localPort;

    private Socket listenfd;
    List<Socket> clients = new();
    List<Socket> check = new();

    // Start is called before the first frame update
    void Start()
    {
        if (!isServer)
        {
            gameObject.GetComponent<Server>().enabled = false;
            return;
        }

        listenfd = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        IPAddress ip = IPAddress.Parse(localIpAdress);
        IPEndPoint ipEnd = new(ip, localPort);
        listenfd.Bind(ipEnd);
        listenfd.Listen(0);
        Debug.Log("Listening...");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isServer) return;

        check.Clear();
        check.Add(listenfd);
        foreach (Socket s in clients)
        {
            check.Add(s);
        }

        Socket.Select(check, null, null, 1000000);

        foreach (Socket s in check)
        {
            if (s == listenfd)
            {
                ReadListenfd(s);
            }
            else
            {
                ReadClientfd(s);
            }
        }
    }

    void ReadListenfd(Socket socket)
    {
        Socket newClient = socket.Accept();
        clients.Add(newClient);
    }

    void ReadClientfd(Socket socket)
    {
        byte[] buff = new byte[1024];
        socket.Receive(buff, 0, 1024, 0);
        BroadCast(socket, buff);
    }

    //给clients中除sn外所有客户端分发消息
    void BroadCast(Socket sn,byte[] bytes)
    {
        foreach (Socket s in clients)
        {
            if (s != sn)
            {
                s.Send(bytes);
            }
        }
        Debug.Log("BroadCast");
    }
}
