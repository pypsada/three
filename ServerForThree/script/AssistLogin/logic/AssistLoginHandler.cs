public partial class MsgHandler
{
    public static void MsgAskNewUid(ClientState c, MsgBase msgBase)
    {
        MsgAskNewUid msg = new();
        msg.newUid = DbManager.GetNewUid();
        NetManager.Send(c, msg);
    }
}