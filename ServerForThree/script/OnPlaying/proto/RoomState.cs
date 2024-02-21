﻿using Org.BouncyCastle.Tls;

//某一个客户端已经选择职业完毕并进入游戏
public class MsgClientReady:MsgBase
{
    public MsgClientReady() { protoName = "MsgClientReady"; }
}

//两个客户端都已经准备完毕
public class MsgAllReady:MsgBase
{
    public MsgAllReady() { protoName = "MsgAllReady"; }
}