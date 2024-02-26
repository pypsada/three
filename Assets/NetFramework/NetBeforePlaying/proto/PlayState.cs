//服务端发给胜利玩家
using NetGame;

public class MsgYouWin : MsgBase
{
    public MsgYouWin() { protoName = "MsgYouWin"; }
    public int victualTimes;
    public int failTimes;
}

//服务端发给失败玩家
public class MsgYouLost : MsgBase
{
    public MsgYouLost() { protoName = "MsgYouLost"; }
    public int victualTimes;
    public int failTimes;
}

//服务端发给玩家表示继续
public class MsgGameContinue:MsgBase
{
    public MsgGameContinue() { protoName = "MsgGameContinue"; }
}


//客户端发服务端表示玩家已经行动
public class MsgPlayerAct : MsgBase
{
    public MsgPlayerAct() { protoName = "MsgPlayerAct"; }

    public string tmpData;
}

//服务端发客户端传递远程玩家消息
public class MsgRemoteInfo : MsgBase
{
    public MsgRemoteInfo() { protoName = "MsgRemoteInfo"; }

    public string tmpData;
}

//服务端发客户端传递当地玩家消息
public class MsgLocalInfo : MsgBase
{
    public MsgLocalInfo() { protoName = "MsgLocalInfo"; }

    public string tmpData;
}