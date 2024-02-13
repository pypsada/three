public partial class MsgHandler
{
    public void MsgMatchQuit(ClientState c, MsgBase msgBase)
    {
        RoomManager.MathingQuit(c.player);
    }

    public void MsgMatched(ClientState c, MsgBase msgBase)
    {
        LogManager.Log("[MsgHandler Recv MsgMatched in RoomMsgHandler]" +
            "Clients do not send this Msg, there must be something wrong!");
    }

    public void MsgBgmatch(ClientState c, MsgBase msgBase)
    {
        RoomManager.AddPlayerMatching(c.player);
        RoomManager.PlayerMatch();
    }
}