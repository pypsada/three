
public class MsgBgmatch : MsgBase
{
    MsgBgmatch() { protoName = "MsgBgmatch"; }
}

public class MsgMatched:MsgBase
{
    MsgMatched() { protoName = "MsgMatched"; }
    public PlayerData? remotePlayer;
    public string remotePlayerId = "";
}
