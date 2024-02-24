//服务端发给胜利玩家
public class MsgYouWin:MsgBase
{
    public MsgYouWin() { protoName = "MsgYouWin"; }
}

//服务端发给失败玩家
public class MsgYouLost:MsgBase
{
    public MsgYouLost() { protoName = "MsgYouLost"; }
}

//服务端发给玩家表示继续
public class MsgGameContinue:MsgBase
{
    public MsgGameContinue() { protoName = "MsgGameContinue"; }
}