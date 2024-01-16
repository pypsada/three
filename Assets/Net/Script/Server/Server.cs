using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System;
using System.Globalization;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Server : MonoBehaviour
{
    [HideInInspector]
    public bool isServer;
    private string localIpAdress;
    private int localPort;

    public InputField inputField;

    private Socket listenfd;
    List<Socket> clients = new();
    List<Socket> check = new();

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        isServer = false;
    }

    public void BeginServer()
    {
        string[] input = inputField.text.Split(":");
        localIpAdress = input[0];
        localPort = int.Parse(input[1]);
        isServer = true;
        NetManager.Connect(localIpAdress, localPort);

        SceneManager.LoadScene(1);
        return;
        //string[] input = inputField.text.Split(":");
        //localIpAdress = input[0];
        //localPort = int.Parse(input[1]);

        //isServer = true;

        //listenfd = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //IPAddress ip = IPAddress.Parse(localIpAdress);
        //IPEndPoint ipEnd = new(ip, localPort);
        //listenfd.Bind(ipEnd);
        //listenfd.Listen(0);
        //Debug.Log("Listening...");
    }

    public void BeginClient()
    {
        string[] input = inputField.text.Split(":");
        localIpAdress = input[0];
        localPort = int.Parse(input[1]);
        isServer = false;
        NetManager.Connect(localIpAdress, localPort);

        SceneManager.LoadScene(1);
    }

    // Update is called once per frame
    void Update()
    {
        return;
        if (!isServer) return;
        Debug.Log("ServerUpdate");
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
