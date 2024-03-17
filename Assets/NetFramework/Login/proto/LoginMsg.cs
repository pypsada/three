using System.Security.Cryptography.X509Certificates;

//注册
public class MsgRegister:MsgBase
{
    public MsgRegister()
    {
        this.protoName = "MsgRegister";
    }
    //客户端发
    public string id = "";
    public string pw = "";
    //服务端回(0成功，1失败)
    public int result = 0;
}

//登陆
public class MsgLogin : MsgBase
{
    public MsgLogin()
    {
        this.protoName = "MsgLogin";
    }
    //客户端发
    public string id = "";
    public string pw = "";
    public string nickName = "";
    public int winTimes;
    public int lostTimes;
    //版本号
    public int version;
    //服务端回（0成功，1失败）
    public int result = 0;
}

//踢下线
public class MsgKick:MsgBase
{
    public MsgKick() { protoName = "MsgKick"; }
    //原因（0其他人登陆同一账号）
    public int reason = 0;
}