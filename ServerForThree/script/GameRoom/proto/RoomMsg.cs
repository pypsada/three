
//开始匹配
//客户端发出，服务端不发出
public class MsgBgmatch : MsgBase
{
    public MsgBgmatch() { protoName = "MsgBgmatch"; }
}

//结束匹配
//服务端发出，客户端不发出
public class MsgMatched : MsgBase
{
    public MsgMatched() { protoName = "MsgMatched"; }

    public string remotePlayerData;
    public string localPlayerData;

    public string remotePlayerId = "";
    public string localPlayerId = "";
}

//退出匹配。
//客户端发出，服务端不发出
public class MsgMatchQuit : MsgBase
{
    public MsgMatchQuit() { protoName = "MsgMatchQuit"; }
}
