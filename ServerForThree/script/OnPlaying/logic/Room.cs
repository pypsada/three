using System.Diagnostics;
using System.Text.RegularExpressions;

namespace NetGame
{
    public class Room
    {
        //玩家a和玩家b
        public Player player_a;
        public Player player_b;

        //房间是否已经结束
        public bool over = false;

        //两个玩家进入房间
        public Room(Player a,Player b)
        {
            player_a = a;
            player_b = b;
            a.room = this;
            b.room = this;

            over = false;
        }

        //准备开始
        public int readyClient = 0;

        //行动玩家数量 
        public int actPlayer = 0;

        public void ClientReady()
        {
            readyClient += 1;
            if(readyClient>=2)
            {
                player_a.Send(new MsgAllReady());
                player_b.Send(new MsgAllReady());
            }
        }

        //判定与结算。给双方玩家发送消息：一方胜利一方失败 或者 继续
        public void Despare()   //判定
        {
            player_a.tmpData.Ground += 1;
            //if (zhaoshi == "Thief")
            //{
            //    Thief1();
            //}
            //else if (zhaoshi == "RubbingEnergy")
            //{
            //    RubbingEnergy1();
            //}
            //else if (zhaoshi == "Gun")
            //{
            //    Gun1();
            //}
            //else if (zhaoshi == "Guard")
            //{
            //    Guard1();
            //}
            //else if (zhaoshi == "HolyGrail")
            //{
            //    HolyGrail1();
            //}
            //else if (zhaoshi == "Rebound")
            //{
            //    Rebound1();
            //}
            //else if (zhaoshi == "Defense")
            //{
            //    Defense1();
            //}
            //else if (zhaoshi == "Assassinate")
            //{
            //    Assassinate1();
            //}
            //else if (zhaoshi == "King")
            //{
            //    King1();
            //}
            //else if (zhaoshi == "Pangolin")
            //{
            //    Pangolin1();
            //}
            //else if (zhaoshi == "Turtle")
            //{
            //    Turtle1();
            //}
            //else if (zhaoshi == "Rascally")
            //{
            //    Rascally1();
            //}
            //else if (zhaoshi == "Arrogance")
            //{
            //    Arrogance1();
            //}
            if (player_a.tmpData.Thiefing)
            {
                if (player_b.tmpData.Defensing == true || player_b.tmpData.Rebounding == true)
                {
                    player_a.tmpData.Career -= 1;
                }
                else if (player_b.tmpData.Defensing == false && player_b.tmpData.Priority > 0)
                {
                    player_b.tmpData.Win = false;
                }
                else if (player_b.tmpData.Priority == 0)
                {
                    if (player_b.tmpData.Thiefing == true)
                    {

                    }
                    else
                    {
                        if (player_b.tmpData.StringCareer == "Rascally")
                        {
                            player_b.tmpData.Energy -= player_b.tmpData.RascallyNumber;
                            player_a.tmpData.Energy += player_b.tmpData.RascallyNumber;
                            player_a.tmpData.Career += 1;
                        }
                        else
                        {
                            player_b.tmpData.Energy -= 1;
                            player_a.tmpData.Energy += 1;
                            player_a.tmpData.Career += 1;
                        }
                    }
                }
                player_a.tmpData.Thiefing = false;
            }
            else
            {
                if (player_a.tmpData.Priority < player_b.tmpData.Priority && player_b.tmpData.Rebounding == false && player_b.tmpData.Defensing == false)  //AI攻击优先级更高
                {
                    player_a.tmpData.Win = false;
                }
                else if (player_a.tmpData.Priority < player_b.tmpData.Priority && player_b.tmpData.Rebounding == true && player_a.tmpData.Defensing == false && player_a.tmpData.Priority != 0) //AI反弹成功
                {
                    player_a.tmpData.Win = false;
                }
                else if (player_a.tmpData.Priority == player_b.tmpData.Priority)  //优先级一样，相互抵消
                {
                    if (player_b.tmpData.Thiefing == true)
                    {
                        if (player_a.tmpData.StringCareer == "Rascally")
                        {
                            player_a.tmpData.Energy -= player_a.tmpData.RascallyNumber;
                            player_b.tmpData.Energy += player_a.tmpData.RascallyNumber;
                            player_b.tmpData.Career += 1;
                        }
                        else
                        {
                            player_a.tmpData.Energy -= 1;
                            player_b.tmpData.Energy += 1;
                            player_b.tmpData.Career += 1;
                        }
                    }

                }
                else if (player_a.tmpData.Defensing == true && player_b.tmpData.Priority != 2)  //玩家防御，AI不用大招，继续游戏
                {
                    if (player_b.tmpData.Thiefing == true)
                    {
                        player_b.tmpData.Career -= 1;

                    }

                }
                else if (player_a.tmpData.Rebounding == true && (player_b.tmpData.Priority == 0 || (player_b.tmpData.Priority != 0 && player_b.tmpData.Defensing == true)))  //玩家反弹失败
                {
                    if (player_b.tmpData.Thiefing == true)
                    {
                        player_b.tmpData.Career -= 1;
                    }


                }
                else if (player_a.tmpData.Priority == 0 && (player_b.tmpData.Defensing == true || player_b.tmpData.Priority == 0 || player_b.tmpData.Rebounding == true))  //玩家搓能量，AI不攻击
                {
                    if (player_b.tmpData.Thiefing == true)
                    {
                        player_b.tmpData.Career += 1;
                        player_b.tmpData.Energy += 1;
                    }


                }
                else  //否则就是玩家赢
                {
                    if (player_b.tmpData.Thiefing == true)
                    {
                        player_a.tmpData.Win = false;
                    }
                    else
                    {
                        player_b.tmpData.Win = false;
                    }

                }
            }

        }

        public void Sum()  //结算
        {
            if (player_b.tmpData.Win == false)
            {
                if (player_a.tmpData.Thiefing)
                {
                    if (player_b.tmpData.Priority == 1)
                    {
                        if (player_b.tmpData.StringCareer == "Assassin")
                        {
                            player_b.tmpData.health -= (int)(30 + 1.1 * player_b.tmpData.level);
                        }
                        else
                        {
                            player_b.tmpData.health -= 20 + 1 * player_b.tmpData.level;
                        }

                    }
                    else if (player_b.tmpData.Priority == 2)
                    {
                        if (player_b.tmpData.StringCareer == "King")
                        {
                            player_b.tmpData.health -= (int)(40 + 1.8 * player_b.tmpData.level);
                        }
                        else
                        {
                            player_b.tmpData.health -= (int)(50 + 2 * player_b.tmpData.level);
                        }

                    }
                }
                else if (player_a.tmpData.Rebounding == true)
                {
                    if (player_b.tmpData.Priority == 1)
                    {
                        if (player_b.tmpData.StringCareer == "Assassin")
                        {
                            player_b.tmpData.health -= (int)(30 + 1.1 * player_b.tmpData.level);
                        }
                        else
                        {
                            player_b.tmpData.health -= 50 + 2 * player_b.tmpData.level;
                        }

                    }
                    else if (player_b.tmpData.Priority == 2)
                    {
                        if (player_b.tmpData.StringCareer == "King")
                        {
                            player_b.tmpData.health -= (int)(40 + 1.8 * player_b.tmpData.level);
                        }
                        else
                        {
                            player_b.tmpData.health -= 50 + 2 * player_b.tmpData.level;
                        }
                    }
                }
                else
                {
                    if (player_a.tmpData.Priority == 1)
                    {
                        if (player_a.tmpData.StringCareer == "Assassin")
                        {
                            player_b.tmpData.health -= (int)(30 + 1.1 * player_a.tmpData.level);
                        }
                        else
                        {
                            player_b.tmpData.health -= 20 + 1 * player_a.tmpData.level;
                        }

                    }
                    else if (player_a.tmpData.Priority == 2)
                    {
                        if (player_a.tmpData.StringCareer == "King")
                        {
                            player_b.tmpData.health -= (int)(40 + 1.8 * player_a.tmpData.level);
                        }
                        else
                        {
                            player_b.tmpData.health -= 50 + 2 * player_a.tmpData.level;
                        }

                    }
                }
            }
            else if (player_a.tmpData.Win == false)
            {
                if (player_b.tmpData.Thiefing)
                {
                    if (player_a.tmpData.Priority == 1)
                    {
                        if (player_a.tmpData.StringCareer == "Assassin")
                        {
                            player_a.tmpData.health -= (int)(30 + 1.1 * player_a.tmpData.level);
                        }
                        else
                        {
                            player_a.tmpData.health -= 20 + 1 * player_a.tmpData.level;
                        }

                    }
                    else if (player_a.tmpData.Priority == 2)
                    {
                        if (player_a.tmpData.StringCareer == "King")
                        {
                            player_a.tmpData.health -= (int)(40 + 1.8 * player_a.tmpData.level);
                        }
                        else
                        {
                            player_a.tmpData.health -= 50 + 2 * player_a.tmpData.level;
                        }

                    }
                }
                else if (player_b.tmpData.Rebounding == true)
                {
                    if (player_a.tmpData.Priority == 1)
                    {
                        if (player_a.tmpData.StringCareer == "Assassin")
                        {
                            player_a.tmpData.health -= (int)(30 + 1.1 * player_a.tmpData.level);
                        }
                        else
                        {
                            player_a.tmpData.health -= 20 + 1 * player_a.tmpData.level;
                        }

                    }
                    else if (player_a.tmpData.Priority == 2)
                    {
                        if (player_a.tmpData.StringCareer == "King")
                        {
                            player_a.tmpData.health -= (int)(40 + 1.8 * player_a.tmpData.level);
                        }
                        else
                        {
                            player_a.tmpData.health -= 50 + 2 * player_a.tmpData.level;
                        }

                    }
                }
                else
                {
                    if (player_b.tmpData.Priority == 1)
                    {
                        if (player_b.tmpData.StringCareer == "Assassin")
                        {
                            player_a.tmpData.health -= (int)(30 + 1.1 * player_b.tmpData.level);
                        }
                        else
                        {
                            player_a.tmpData.health -= 20 + 1 * player_b.tmpData.level;
                        }

                    }
                    else if (player_b.tmpData.Priority == 2)
                    {
                        if (player_a.tmpData.StringCareer == "Kings")
                        {
                            player_a.tmpData.health -= (int)(40 + 1.8 * player_b.tmpData.level);
                        }
                        else
                        {
                            player_a.tmpData.health -= 50 + 2 * player_b.tmpData.level;
                        }

                    }
                }
            }

            if (player_b.tmpData.health <= 0)
            {
                //TanChuang.SetActive(true);
                //Time.timeScale = 0;
                //WinOrLose.text = "Win";
                //Destroy(AIgameobject);
                //Debug.Log("Win");
                AWin();
            }
            else if (player_a.tmpData.health <= 0)
            {
                //TanChuang.SetActive(true);
                //Time.timeScale = 0;
                //WinOrLose.text = "Lose";
                //Destroy(gameObject);
                //Debug.Log("LOST");
                ////时间暂停
                BWin();
            }
            else
            {
                //Debug.Log("Continue");
                GameContinue();
            }

            player_a.tmpData.Win = true;
            player_b.tmpData.Win = true;
            player_a.tmpData.Chose = false;  //初始化
            player_a.tmpData.Priority = 0;
            player_a.tmpData.Defensing = false;
            player_a.tmpData.Rebounding = false;
            player_a.tmpData.Thiefing = false;
            player_b.tmpData.Rebounding = false;
            player_b.tmpData.Priority = 0;
            player_b.tmpData.Defensing = false;
            player_b.tmpData.Thiefing = false;

            //AI职业点数判定
            if (player_b.tmpData.StringCareer == "Assassin" || player_b.tmpData.StringCareer == "Thief")   //刺客,盗贼职业点数判定
            {
                if (player_a.tmpData.Ground % 2 == 1)
                {
                    player_b.tmpData.Career += 1;
                }
            }
            else if (player_b.tmpData.StringCareer == "King")  //国王职业点数判定
            {
                if (player_b.tmpData.Energy % 2 == 0)
                {
                    player_b.tmpData.Career = player_b.tmpData.Energy / 2;
                }
                else
                {
                    player_b.tmpData.Career = (player_b.tmpData.Energy - 1) / 2;
                }
            }
            else if (player_b.tmpData.StringCareer == "Guard" || player_b.tmpData.StringCareer == "Rascally" || player_b.tmpData.StringCareer == "Arrogance")  //卫士等职业点数判定
            {
                player_b.tmpData.Career = 1;
            }
            else if (player_b.tmpData.StringCareer == "Turtle")  //乌龟职业点数判定
            {
                player_b.tmpData.Career = player_b.tmpData.Energy;
            }


            if (player_a.tmpData.StringCareer == "Assassin" || player_a.tmpData.StringCareer == "Thief")   //刺客,盗贼职业点数判定
            {
                if (player_a.tmpData.Ground % 2 == 1)
                {
                    player_a.tmpData.Career += 1;
                }
            }
            else if (player_a.tmpData.StringCareer == "King")  //国王职业点数判定
            {
                if (player_a.tmpData.Energy % 2 == 0)
                {
                    player_a.tmpData.Career = player_a.tmpData.Energy / 2;
                }
                else
                {
                    player_a.tmpData.Career = (player_a.tmpData.Energy - 1) / 2;
                }
            }
            else if (player_a.tmpData.StringCareer == "Guard" || player_a.tmpData.StringCareer == "Rascally" || player_a.tmpData.StringCareer == "Arrogance")  //卫士等职业点数判定
            {
                player_a.tmpData.Career = 1;
            }
            else if (player_a.tmpData.StringCareer == "Turtle")  //乌龟职业点数判定
            {
                player_a.tmpData.Career = player_a.tmpData.Energy;
            }
            //myAnim.SetTrigger("Idle");
            //countDownTimer = 10f;
            player_a.tmpData.Continue = true;

            if (/*gameObject != null && AIgameobject != null*/!over)
            {
                if (player_a.tmpData.ArroganceNumber >= 3 && player_b.tmpData.ArroganceNumber >= 3)
                {
                    player_b.tmpData.health -= (int)(1.5 * player_a.tmpData.level + 45);
                    player_a.tmpData.health -= (int)(1.5 * player_b.tmpData.level + 45);
                    player_a.tmpData.ArroganceNumber -= 3;
                    player_b.tmpData.ArroganceNumber -= 3;
                    player_a.tmpData.Energy += 1;
                    player_b.tmpData.Energy += 1;
                    ArroganceSum();
                }
                else if (player_a.tmpData.ArroganceNumber >= 3)
                {
                    player_b.tmpData.health -= (int)(1.5 * player_a.tmpData.level + 45);
                    player_a.tmpData.ArroganceNumber -= 3;
                    player_a.tmpData.Energy += 1;
                    ArroganceSum();
                }
                else if (player_b.tmpData.ArroganceNumber >= 3)
                {
                    player_a.tmpData.health -= (int)(1.5 * player_b.tmpData.level + 45);
                    player_b.tmpData.ArroganceNumber -= 3;
                    player_b.tmpData.Energy += 1;
                    ArroganceSum();
                }
            }
        }

        public void ArroganceSum()
        {
            if (player_b.tmpData.health <= 0)
            {
                //TanChuang.SetActive(true);
                //Time.timeScale = 0;
                //WinOrLose.text = "Win";
                //Destroy(AIgameobject);
                //Debug.Log("Win");
                AWin();
            }
            if (player_a.tmpData.health <= 0)
            {
                //TanChuang.SetActive(true);
                //Time.timeScale = 0;
                //WinOrLose.text = "Lose";
                //Destroy(gameObject);
                //Debug.Log("LOST");
                BWin();
            }
        }

        private void AWin()
        {
            player_a.Send(new MsgYouWin());
            player_b.Send(new MsgYouLost());
            player_b.data.failTimes++;
            player_a.data.victoryTimes++;
            DbManager.UpdatePlayerData(player_b.id, player_b.data);
            DbManager.UpdatePlayerData(player_a.id, player_a.data);
            player_a.room = null;
            player_b.room = null;
            over = true;
            RoomManager.ClearRooms();
        }

        private void BWin()
        {
            player_a.Send(new MsgYouLost());
            player_b.Send(new MsgYouWin());
            player_a.data.failTimes++;
            player_b.data.victoryTimes++;
            DbManager.UpdatePlayerData(player_a.id, player_a.data);
            DbManager.UpdatePlayerData(player_b.id, player_b.data);
            player_a.room = null;
            player_b.room = null;
            over = true;
            RoomManager.ClearRooms();
        }

        private void GameContinue()
        {
            player_a.Send(new MsgGameContinue());
            player_b.Send(new MsgGameContinue());
        }

        //玩家逃跑算作失败
        public void PlayerEscape(Player esc)
        {
            LogManager.Log("[Room.PlayerEscape]" + esc.id + " escape");
            if(esc==player_a)
            {
                BWin();
            }
            else if(esc==player_b)
            {
                AWin();
            }
            else
            {
                LogManager.Log("[Room.PlayerEscape] esc is not in this room!");
                return;
            }
        }
    }
}




