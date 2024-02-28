
using System.Runtime.Intrinsics.Arm;

public partial class MsgHandler
{
    //注册
    public static void MsgRegister(ClientState c, MsgBase msgBase)
    {
        MsgRegister msg = (MsgRegister)msgBase;
        //注册
        if(DbManager.Register(msg.id,msg.pw))
        {
            DbManager.CreatePlayer(msg.id);
            msg.result = 0;
        }
        else
        {
            msg.result = 1;
        }
        NetManager.Send(c,msg);
    }

    //登陆协议处理
    public static void MsgLogin(ClientState c,MsgBase msgBase)
    {
        MsgLogin msg = (MsgLogin)msgBase;
        //账号检验
        if (!DbManager.CheckPassword(msg.id, msg.pw))
        {
            msg.result = 1;
            NetManager.Send(c,msg);
            return;
        }
        //不允许重新登陆
        if(c.player!=null)
        {
            msg.result = 1;
            NetManager.Send(c, msg);
            return;
        }
        //如果已经登陆，踢下线
        if(PlayerManager.IsOnline(msg.id))
        {
            Player? other = PlayerManager.GetPlayer(msg.id);
            MsgKick msgKick = new();
            msgKick.reason = 0;
            if (other != null)
            {
                other.Send(msgKick);
                NetManager.Close(other.state);
            }
        }
        //获取玩家数据
        PlayerData? playerData = DbManager.GetPlayerData(msg.id);
        if(playerData==null)
        {
            msg.result = 1;
            NetManager.Send(c, msg);
            return;
        }
        //构建Player
        Player player = new(c);
        player.id = msg.id;
        player.data = playerData;
        PlayerManager.AddPlayer(msg.id, player);
        c.player = player;
        //修改Player的nickName
        c.player.data.nickName = msg.nickName;
        DbManager.UpdatePlayerData(c.player.id, c.player.data);
        //修改发出协议中的胜场败场
        msg.winTimes = playerData.victoryTimes;
        msg.lostTimes = playerData.failTimes;
        //返回协议
        msg.result = 0;
        player.Send(msg);
    }
}