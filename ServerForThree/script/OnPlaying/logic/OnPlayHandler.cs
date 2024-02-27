﻿using Mysqlx.Connection;
using Newtonsoft.Json;

public partial class MsgHandler
{
    public static void MsgYouWin(ClientState c, MsgBase msgBase)
    {
        LogManager.Log("[MsgHandler Recv MsgYouWin in RoomMsgHandler]" +
            "Clients do not send this Msg, there must be something wrong!");
    }
    public static void MsgYouLost(ClientState c, MsgBase msgBase)
    {
        LogManager.Log("[MsgHandler Recv MsgYouLost in RoomMsgHandler]" +
            "Clients do not send this Msg, there must be something wrong!");
    }
    public static void MsgGameContinue(ClientState c, MsgBase msgBase)
    {
        LogManager.Log("[MsgHandler Recv MsgGameContinue in RoomMsgHandler]" +
             "Clients do not send this Msg, there must be something wrong!");
    }
    public static void MsgAllReady(ClientState c, MsgBase msgBase)
    {
        LogManager.Log("[MsgHandler Recv MsgAllReady in RoomMsgHandler]" +
            "Clients do not send this Msg, there must be something wrong!");
    }
    public static void MsgRemoteInfo(ClientState c,MsgBase msgBase)
    {
        LogManager.Log("[MsgHandler Recv MsgRemoteInfo in RoomMsgHandler]" +
            "Clients do not send this Msg, there must be something wrong!");
    }
    public static void MsgLocalInfo(ClientState c, MsgBase msgBase)
    {
        LogManager.Log("[MsgHandler Recv MsgLocalInfo in RoomMsgHandler]" +
            "Clients do not send this Msg, there must be something wrong!");
    }

    //收到某个客户端发过来的准备协议
    public static void MsgClientReady(ClientState c, MsgBase msgBase)
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
    public static void MsgPlayerAct(ClientState c,MsgBase msgBase)
    {
        MsgPlayerAct msg = (MsgPlayerAct)msgBase;
        c.player.tmpData = (PlayerTmpData)JsonConvert.DeserializeObject(msg.tmpData, typeof(PlayerTmpData));
        NetGame.Room room = c.player.room;
        room.actPlayer++;

        if(room.actPlayer>=2)
        {
            room.actPlayer = 0;
            room.Despare();
            room.Sum();


            MsgRemoteInfo msg_a = new();
            msg_a.tmpData = JsonConvert.SerializeObject(room.player_a.tmpData);
            MsgRemoteInfo msg_b = new();
            msg_b.tmpData = JsonConvert.SerializeObject(room.player_b.tmpData);

            room.player_a.Send(msg_b);
            room.player_b.Send(msg_a);


            MsgLocalInfo msg_aa = new();
            msg_aa.tmpData = JsonConvert.SerializeObject(room.player_a.tmpData);
            MsgLocalInfo msg_bb = new();
            msg_bb.tmpData = JsonConvert.SerializeObject(room.player_b.tmpData);

            room.player_a.Send(msg_aa);
            room.player_b.Send(msg_bb);


        }
    }
}