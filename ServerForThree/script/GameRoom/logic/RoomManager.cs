using NetGame;
using Newtonsoft.Json;

//管理匹配中和游戏中的房间
public class RoomManager
{
    //正式游玩房间
    static List<Room> rooms = new();
    //匹配中玩家房间
    public static List<Player> matching = new();

    //添加匹配玩家
    public static void AddPlayerMatching(Player player)
    {
        matching.Add(player);
    }

    //当匹配中玩家多于两个时，就放在一个房间里。
    public static void PlayerMatch()
    {
        if(matching.Count>=2)
        {
            Room room = new(matching[0], matching[1]);
            rooms.Add(room);

            MsgMatched msg_0 = new();
            msg_0.localPlayerData = JsonConvert.SerializeObject(matching[0].data);
            msg_0.localPlayerId = matching[0].id;
            msg_0.remotePlayerId = matching[1].id;
            msg_0.remotePlayerData = JsonConvert.SerializeObject(matching[1].data);
            matching[0].Send(msg_0);

            MsgMatched msg_1 = new();
            msg_1.localPlayerData = JsonConvert.SerializeObject(matching[1].data);
            msg_1.localPlayerId = matching[1].id;
            msg_1.remotePlayerId = matching[0].id;
            msg_1.remotePlayerData = JsonConvert.SerializeObject(matching[0].data);
            matching[1].Send(msg_1);

            matching.RemoveAt(0);
            matching.RemoveAt(0);
        }

        if(matching.Count>=2)
        {
            PlayerMatch();
        }
    }

    //玩家取消匹配
    public static void MathingQuit(Player player)
    {
        matching.Remove(player);
    }

    //清除已经结束的房间
    public static void ClearRooms()
    {
        foreach(Room room in rooms)
        {
            if(room.over==true)
            {
                rooms.Remove(room);
            }
        }
    }
}