using NetGame;

public partial class MsgHandler
{
    public static void MsgChat(ClientState c,MsgBase msgBase)
    {
        MsgChat msg = (MsgChat)msgBase;

        if (c.player == null || c.player.room == null)
        {
            LogManager.Log("[MsgHandler:MsgChat] Recv from "+c.socket.RemoteEndPoint.ToString()+
                ": player or room is null!");
            return;
        }

        Room room = c.player.room;
        Player anotherPlayer;
        if(c.player==room.player_a)
        {
            anotherPlayer = room.player_b;
        }
        else if(c.player==room.player_b)
        {
            anotherPlayer = room.player_a;
        }
        else
        {
            LogManager.Log("[MsgHandler:MsgChat] Recv from " + c.socket.RemoteEndPoint.ToString() +
                ": player is not in this room!");
            return;
        }

        anotherPlayer.Send(msg);
        LogManager.Log("[MsgHandler:MsgChat] Recv from " + c.socket.RemoteEndPoint.ToString() +
                ": ChatMsg has send to "+anotherPlayer.state.socket.RemoteEndPoint.ToString());
    }
}