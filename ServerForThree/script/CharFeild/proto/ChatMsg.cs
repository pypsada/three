//客户端发服务端：传递给对方玩家的消息
//服务端发客户端：更新对方玩家的消息
public class MsgChat : MsgBase
{
    public MsgChat() { protoName = "MsgChat"; }

    public string chatStr = "";
}