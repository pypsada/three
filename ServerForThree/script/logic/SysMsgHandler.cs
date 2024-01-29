public partial class MsgHandler
{
    //心跳：恢复ping协议
    public static void MsgPing(ClientState c, MsgBase msgBase)
    {
        c.lastPingTime = NetManager.GetTimeStamp();
        MsgPong msgPong = new();
        NetManager.Send(c, msgPong);
        c.hasPing = true;
    }

}