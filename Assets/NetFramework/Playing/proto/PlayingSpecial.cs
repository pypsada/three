//客户端发服务端表示认输
public class MsgAdmitDefeat:MsgBase
{
    public MsgAdmitDefeat() { protoName = "MsgAdmitDefeat"; }
}

//催促客户端方发服务端，
//服务端方发被催促客户端
public class MsgUrge:MsgBase
{
    public MsgUrge() { protoName = "MsgUrge"; }
    //大于零仅仅为提醒，小于等于零则被催促客户端强制认输
    public int leftTime;
}