namespace NetGame
{
    public class Room
    {
        //玩家a和玩家b
        public Player player_a;
        public Player player_b;

        //两个玩家进入房间
        public Room(Player a,Player b)
        {
            player_a = a;
            player_b = b;
            a.room = this;
            b.room = this;
        }

        //判定与结算。给双方玩家发送消息：一方胜利一方失败 或者 继续
    }
}