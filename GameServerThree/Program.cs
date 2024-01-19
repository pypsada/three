using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System;
using System.Globalization;
using System.Diagnostics;

public class MainClass
{
    private static string localIpAdress;
    private static int localPort;

    private static  Socket listenfd;
    static List<Socket> clients = new();
    static List<Socket> check = new();
    static void Main(string[] args)
    {
        string buff = Console.ReadLine();
        string[] input = buff.Split(":");
        localIpAdress = input[0];
        localPort = int.Parse(input[1]);

        listenfd = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        IPAddress ip = IPAddress.Parse(localIpAdress);
        IPEndPoint ipEnd = new(ip, localPort);
        listenfd.Bind(ipEnd);
        listenfd.Listen(0);
        Console.WriteLine("Listening...");
        while(true)
        {
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
    }

    static void ReadListenfd(Socket socket)
    {
        Socket newClient = socket.Accept();
        clients.Add(newClient);
        Console.WriteLine("Accept " + newClient.RemoteEndPoint.ToString());
    }

    static void ReadClientfd(Socket socket)
    {
        byte[] buff = new byte[1024];
        int count = socket.Receive(buff, 0, 1024, 0);
        if(count==0)
        {
            socket.Close();
        }
        BroadCast(socket, buff, count);
    }

    //给clients中除sn外所有客户端分发消息
    static void BroadCast(Socket sn, byte[] bytes,int count)
    {
        foreach (Socket s in clients)
        {
            if (s != sn)
            {
                s.Send(bytes, 0, count, 0);
                Console.WriteLine("Send to "+s.RemoteEndPoint.ToString());
            }
        }
        Console.WriteLine("BroadCast " + count);
    }
}
