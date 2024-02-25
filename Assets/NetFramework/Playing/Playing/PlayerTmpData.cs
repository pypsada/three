namespace NetGame
{
    public class PlayerTmpData
    {
        //public string zhaoshi;
        public int health;  //血量
        public int Energy;
        //private Animator myAnim;
        public int Priority = 0;  //优先级
        public bool Rebounding = false;  //是否反弹
        public bool Defensing = false;  //是否防御
        //public AI AI;
        //public GameObject AIgameobject;
        public bool Chose = false;    //是否选择了技能
        public int Career;  //技能点数(职业技能点数)
        public string StringCareer;  //职业名字
        public int Ground;  //回合数
        public int RascallyNumber;  //老赖技能获取能量数
        public int ArroganceNumber;  //傲慢点数
        public bool Thiefing;  //神偷判定
        public int PangolinNumber; //穿山甲叠加层数
        public bool IsPangolin;  //叠加判定
        public bool Continue;

        //public Text countDownText; // 倒计时文本
        //public float countDownTimer = 10f; // 倒计时时间
        //public Text GroundText; //回合数文本

        public bool Win = true;  //是否获胜

        public PlayerTmpData()
        {
            InitData();
        }

        //初始化数据
        public void InitData()
        {
            Continue = true;
            Win = true;
            health = 1;
            Ground = 1;
            RascallyNumber = 1;
            ArroganceNumber = 0;
            Thiefing = false;
            PangolinNumber = 0;
            StringCareer = Whole.PlayerCareer;
            Career = 0;
            Energy = 0;   //初始化
            if (StringCareer == "Thief" || StringCareer == "Assassin" || StringCareer == "Guard" ||
                StringCareer == "Rascally" || StringCareer == "Arrogance" || StringCareer == "Pangolin")
            {
                Career = 1;
            }
            if (StringCareer == "Turtle")
            {
                Energy = 1;
                Career = 1;
            }
        }

        //public void RubbingEnergy()   //能量
        //{
        //    Chose = true;
        //    zhaoshi = "RubbingEnergy";
        //    countDownTimer = 2f;
        //}
        public void RubbingEnergy()   //能量
        {
            Energy += 1;
            //Debug.Log("You:RubbingEnergy");
            Priority = 0;
            //myAnim.SetTrigger("Cuo");
        }
        //public void Gun()  //枪
        //{
        //    Chose = true;
        //    zhaoshi = "Gun";
        //    countDownTimer = 2f;
        //}
        public void Gun()  //枪
        {
            Priority = 1;
            Energy -= 1;
            //Debug.Log("You:Gun");
            //myAnim.SetTrigger("Gun");
        }
        //public void Rebound()   //反弹
        //{
        //    Chose = true;
        //    zhaoshi = "Rebound";
        //    countDownTimer = 2f;
        //}
        public void Rebound()   //反弹
        {
            Priority = 100;
            Rebounding = true;
            Energy -= 2;
            //Debug.Log("You:Rebound");
            //myAnim.SetTrigger("Rebound");
        }
        //public void Defense()   //防御
        //{
        //    Chose = true;
        //    zhaoshi = "Defense";
        //    countDownTimer = 2f;
        //}
        public void Defense()   //防御
        {
            Priority = 1;
            Defensing = true;
            //Debug.Log("You:Defense");
            //myAnim.SetTrigger("Defense");
        }
        //public void HolyGrail()   //大招
        //{
        //    Chose = true;
        //    zhaoshi = "HolyGrail1";
        //    countDownTimer = 2f;
        //}
        public void HolyGrail()   //大招
        {
            Priority = 2;
            Energy -= 4;
            //Debug.Log("You:HolyGrail");
            //myAnim.SetTrigger("King");
        }
        //public void Assassinate()  // 刺客技能：暗杀
        //{
        //    Chose = true;
        //    zhaoshi = "Assassinate";
        //    countDownTimer = 2f;
        //}
        public void Assassinate()  // 刺客技能：暗杀
        {
            Priority = 1;
            Career -= 1;
            //Debug.Log("You:Assassinate");
            //myAnim.SetTrigger("King");
        }

        //public void Steal()   // 盗贼技能：偷取
        //{
        //    Chose = true;
        //    countDownTimer = 2f;
        //    Priority = 1;
        //}
        //public void King() // 国王技能：王权
        //{
        //    Chose = true;
        //    zhaoshi = "King";
        //    countDownTimer = 2f;
        //}
        public void King() // 国王技能：王权
        {
            Priority = 2;
            Energy -= 2;
            //Debug.Log("You:King");
            //myAnim.SetTrigger("King");
        }
        //public void Guard()  // 护卫技能：能防
        //{
        //    Chose = true;
        //    zhaoshi = "Guard";
        //    countDownTimer = 2f;
        //}
        public void Guard()  // 护卫技能：能防
        {
            Priority = 1;
            Defensing = true;
            Energy += 1;
            //Debug.Log("You:Guard");
            //myAnim.SetTrigger("King");
        }
        //public void Turtle()  //乌龟技能：龟缩
        //{
        //    Chose = true;
        //    zhaoshi = "Turtle";
        //    countDownTimer = 2f;
        //}
        public void Turtle()  //乌龟技能：龟缩
        {
            Energy -= 1;
            Priority = 100;
            Rebounding = true;
            //Debug.Log("You:Turtle");
            //myAnim.SetTrigger("King");
        }
        //public void Rascally()  //老赖技能：汲能
        //{
        //    Chose = true;
        //    zhaoshi = "Rascally";
        //    countDownTimer = 2f;
        //}
        public void Rascally()  //老赖技能：汲能
        {
            Priority = 0;
            Energy += RascallyNumber;
            RascallyNumber += 1;
            //myAnim.SetTrigger("King");
        }
        //public void Arrogance()  //傲慢技能：嘲讽
        //{
        //    Chose = true;
        //    zhaoshi = "Arrogance";
        //    countDownTimer = 2f;
        //}
        public void Arrogance()  //傲慢技能：嘲讽
        {
            ArroganceNumber += 1;
            Priority = 0;
            //myAnim.SetTrigger("Arrogant");
        }
        //public void Thief()  //盗贼技能：神偷
        //{
        //    Chose = true;
        //    zhaoshi = "Thief";
        //    countDownTimer = 2f;
        //}
        public void Thief()  //盗贼技能：神偷
        {
            Career -= 1;
            Thiefing = true;
            Priority = 1;
            //myAnim.SetTrigger("Steal");
        }

        //public void Pangolin()
        //{
        //    Chose = true;
        //    zhaoshi = "Pangolin";
        //    countDownTimer = 2f;
        //}
        public void Pangolin()
        {
            PangolinNumber += 1;
            Priority = 0;
            IsPangolin = true;
            //myAnim.SetTrigger("King");
        }
    }
}