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
    public void MsgRemoteInfo(ClientState c,MsgBase msgBase)
    {
        LogManager.Log("[MsgHandler Recv MsgRemoteInfo in RoomMsgHandler]" +
            "Clients do not send this Msg, there must be something wrong!");
    }

    //收到某个客户端发过来的准备协议
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

    //收到某个客户端发来的玩家已经行动协议
    public void MsgPlayerAct(ClientState c,MsgBase msgBase)
    {
        MsgPlayerAct msg = (MsgPlayerAct)msgBase;
        c.player.tmpData = msg.tmpData;
        NetGame.Room room = c.player.room;

        if(room.actPlayer>=2)
        {
            MsgRemoteInfo msg_a = new();
            msg_a.tmpData = room.player_a.tmpData;
            MsgRemoteInfo msg_b = new();
            msg_b.tmpData = room.player_b.tmpData;

            room.player_a.Send(msg_b);
            room.player_b.Send(msg_a);


            room.actPlayer = 0;
            room.Despare();
            room.Sum();
        }
    }
}