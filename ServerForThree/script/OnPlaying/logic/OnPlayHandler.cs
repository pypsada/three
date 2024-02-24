using Mysqlx.Connection;

public partial class MsgHandler
{
    public void MsgYouWin(ClientState c, MsgBase msgBase)
    {
        LogManager.Log("[MsgHandler Recv MsgYouWin in RoomMsgHandler]" +
            "Clients do not send this Msg, there must be something wrong!");
    }
    public void MsgYouLost(ClientState c, MsgBase msgBase)
    {
        LogManager.Log("[MsgHandler Recv MsgYouLost in RoomMsgHandler]" +
            "Clients do not send this Msg, there must be something wrong!");
    }
    public void MsgGameContinue(ClientState c, MsgBase msgBase)
    {
        LogManager.Log("[MsgHandler Recv MsgGameContinue in RoomMsgHandler]" +
             "Clients do not send this Msg, there must be something wrong!");
    }
    public void MsgAllReady(ClientState c, MsgBase msgBase)
    {
        LogManager.Log("[MsgHandler Recv MsgAllReady in RoomMsgHandler]" +
            "Clients do not send this Msg, there must be something wrong!");
    }

    //收到某个客户端发过来的协议
    public void MsgClientReady(ClientState c, MsgBase msgBase)
    {
        if(c.player==null)
        {
            LogManager.Log("[MsgHandler Recv]Client's player is null.");
            NetManager.Close(c);
            return;
        }
        NetGame.Room room = c.player.room;
        room.readyClient++;
        if(room.readyClient>=2)
        {
            room.player_a.Send(new MsgAllReady());
            room.player_b.Send(new MsgAllReady());
        }
    }
}