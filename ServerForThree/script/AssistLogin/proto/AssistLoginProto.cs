//客户端发服务端：询问新的UID
//服务端发客户端: 回答新的UID
public class MsgAskNewUid:MsgBase
{
    public MsgAskNewUid() { protoName = "MsgAskNewUid"; }

    public int newUid;
}