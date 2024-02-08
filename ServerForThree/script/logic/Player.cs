public class Player
{
    //id
    public string id = "";
    //指向ClientState
    public ClientState state;
    //临时数据
    public PlayerTmpData tmpData;
    //房间信息。方便找到对应房间
    public NetGame.Room room;
    //数据库数据
    public PlayerData data;

    //构造函数
    public Player(ClientState state)
    {
        this.state = state;
    }
    //发送信息
    public void Send(MsgBase msgBase)
    {
        NetManager.Send(state, msgBase);
    }
}