namespace NetGame
{
    public class PlayerTmpData
    {
        //public string zhaoshi;
        public int health;  //Ѫ��
        public int Energy;
        //private Animator myAnim;
        public int Priority = 0;  //���ȼ�
        public bool Rebounding = false;  //�Ƿ񷴵�
        public bool Defensing = false;  //�Ƿ����
        //public AI AI;
        //public GameObject AIgameobject;
        public bool Chose = false;    //�Ƿ�ѡ���˼���
        public int Career;  //���ܵ���(ְҵ���ܵ���)
        public string StringCareer;  //ְҵ����
        public int Ground;  //�غ���
        public int RascallyNumber;  //�������ܻ�ȡ������
        public int ArroganceNumber;  //��������
        public bool Thiefing;  //��͵�ж�
        public int PangolinNumber; //��ɽ�׵��Ӳ���
        public bool IsPangolin;  //�����ж�
        public bool Continue;

        //public Text countDownText; // ����ʱ�ı�
        //public float countDownTimer = 10f; // ����ʱʱ��
        //public Text GroundText; //�غ����ı�

        public bool Win = true;  //�Ƿ��ʤ

        public PlayerTmpData()
        {
            InitData();
        }

        //��ʼ������
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
            Energy = 0;   //��ʼ��
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

        //public void RubbingEnergy()   //����
        //{
        //    Chose = true;
        //    zhaoshi = "RubbingEnergy";
        //    countDownTimer = 2f;
        //}
        public void RubbingEnergy()   //����
        {
            Energy += 1;
            //Debug.Log("You:RubbingEnergy");
            Priority = 0;
            //myAnim.SetTrigger("Cuo");
        }
        //public void Gun()  //ǹ
        //{
        //    Chose = true;
        //    zhaoshi = "Gun";
        //    countDownTimer = 2f;
        //}
        public void Gun()  //ǹ
        {
            Priority = 1;
            Energy -= 1;
            //Debug.Log("You:Gun");
            //myAnim.SetTrigger("Gun");
        }
        //public void Rebound()   //����
        //{
        //    Chose = true;
        //    zhaoshi = "Rebound";
        //    countDownTimer = 2f;
        //}
        public void Rebound()   //����
        {
            Priority = 100;
            Rebounding = true;
            Energy -= 2;
            //Debug.Log("You:Rebound");
            //myAnim.SetTrigger("Rebound");
        }
        //public void Defense()   //����
        //{
        //    Chose = true;
        //    zhaoshi = "Defense";
        //    countDownTimer = 2f;
        //}
        public void Defense()   //����
        {
            Priority = 1;
            Defensing = true;
            //Debug.Log("You:Defense");
            //myAnim.SetTrigger("Defense");
        }
        //public void HolyGrail()   //����
        //{
        //    Chose = true;
        //    zhaoshi = "HolyGrail1";
        //    countDownTimer = 2f;
        //}
        public void HolyGrail()   //����
        {
            Priority = 2;
            Energy -= 4;
            //Debug.Log("You:HolyGrail");
            //myAnim.SetTrigger("King");
        }
        //public void Assassinate()  // �̿ͼ��ܣ���ɱ
        //{
        //    Chose = true;
        //    zhaoshi = "Assassinate";
        //    countDownTimer = 2f;
        //}
        public void Assassinate()  // �̿ͼ��ܣ���ɱ
        {
            Priority = 1;
            Career -= 1;
            //Debug.Log("You:Assassinate");
            //myAnim.SetTrigger("King");
        }

        //public void Steal()   // �������ܣ�͵ȡ
        //{
        //    Chose = true;
        //    countDownTimer = 2f;
        //    Priority = 1;
        //}
        //public void King() // �������ܣ���Ȩ
        //{
        //    Chose = true;
        //    zhaoshi = "King";
        //    countDownTimer = 2f;
        //}
        public void King() // �������ܣ���Ȩ
        {
            Priority = 2;
            Energy -= 2;
            //Debug.Log("You:King");
            //myAnim.SetTrigger("King");
        }
        //public void Guard()  // �������ܣ��ܷ�
        //{
        //    Chose = true;
        //    zhaoshi = "Guard";
        //    countDownTimer = 2f;
        //}
        public void Guard()  // �������ܣ��ܷ�
        {
            Priority = 1;
            Defensing = true;
            Energy += 1;
            //Debug.Log("You:Guard");
            //myAnim.SetTrigger("King");
        }
        //public void Turtle()  //�ڹ꼼�ܣ�����
        //{
        //    Chose = true;
        //    zhaoshi = "Turtle";
        //    countDownTimer = 2f;
        //}
        public void Turtle()  //�ڹ꼼�ܣ�����
        {
            Energy -= 1;
            Priority = 100;
            Rebounding = true;
            //Debug.Log("You:Turtle");
            //myAnim.SetTrigger("King");
        }
        //public void Rascally()  //�������ܣ�����
        //{
        //    Chose = true;
        //    zhaoshi = "Rascally";
        //    countDownTimer = 2f;
        //}
        public void Rascally()  //�������ܣ�����
        {
            Priority = 0;
            Energy += RascallyNumber;
            RascallyNumber += 1;
            //myAnim.SetTrigger("King");
        }
        //public void Arrogance()  //�������ܣ�����
        //{
        //    Chose = true;
        //    zhaoshi = "Arrogance";
        //    countDownTimer = 2f;
        //}
        public void Arrogance()  //�������ܣ�����
        {
            ArroganceNumber += 1;
            Priority = 0;
            //myAnim.SetTrigger("Arrogant");
        }
        //public void Thief()  //�������ܣ���͵
        //{
        //    Chose = true;
        //    zhaoshi = "Thief";
        //    countDownTimer = 2f;
        //}
        public void Thief()  //�������ܣ���͵
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