public partial class EventHandler
{
    //断开连接事件
    public static void OnDisconnect(ClientState c)
    {
        
        LogManager.Log("Close:" + c.socket.RemoteEndPoint.ToString());

        //玩家逃跑算作失败
        if(c.player!=null&&c.player.room!=null)
        {
            c.player.room.PlayerEscape(c.player);
        }

        //玩家下线时保存玩家数据
        if(c.player!=null)
        {
            DbManager.UpdatePlayerData(c.player.id, c.player.data);
            PlayerManager.RemovePlayer(c.player.id);
        }
    }

    //Select超时事件
    public static void OnTimer()
    {
        CheckPing();
    }

    //ping检查
    public static void CheckPing()
    {
        //现在的时间戳
        long timeNow = NetManager.GetTimeStamp();
        //遍历，删除
        foreach(ClientState s in NetManager.clients.Values)
        {
            if(timeNow-s.lastPingTime>NetManager.pingInterval*4)
            {
                LogManager.Log("Too long time out of ping Close " + s.socket.RemoteEndPoint.ToString());
                NetManager.Close(s);
                return;
            }
        }
    }
}