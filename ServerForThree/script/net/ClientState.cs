using System.Net.Sockets;

public class ClientState
{
    //Socket
    public Socket socket;
    //接收缓冲区
    public ByteArray readBuff = new();
    //上次ping时间
    public long lastPingTime = 0;
    //Player
    public Player? player;
    //为了避免非法客户端发送奇怪的东西，只有客户端在40s内发送了ping过后才能正常发送。
    public bool hasPing = false;
}