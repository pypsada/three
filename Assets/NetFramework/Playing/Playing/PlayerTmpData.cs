using UnityEngine;

namespace NetGame
{
    public class PlayerTmpData
    {
        public string skillName;
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

        //��ҵȼ�
        public int level;

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


            //countDownTimer = 10f;
            Ground = 1;
            RascallyNumber = 1;
            ArroganceNumber = 0;
            Thiefing = false;
            PangolinNumber = 0;
            StringCareer = Whole.PlayerCareer;
            level = Whole.Characterlevel;
            Career = 0;
            Energy = 0;   //��ʼ��
            //myAnim = GetComponent<Animator>();
            //countDownText = countDownText.GetComponent<Text>();
            //GroundText = GroundText.GetComponent<Text>();
            //AI = FindObjectOfType<AI>();
            health = (int)0.5 * level + 1;
            if (StringCareer == "Thief" || StringCareer == "Assassin" || StringCareer == "Guard" ||
                StringCareer == "Rascally" || StringCareer == "Arrogance" || StringCareer == "Pangolin")
            {
                Career = 1;
            }
            if (StringCareer == "Turtle")
            {
                Energy = 1;
                Career = 1;
                health = 1 * level + 1;
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
            skillName = "Cuo";
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
            skillName = "Gun";
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
            skillName = "Rebound";
            //Debug.Log("You:Rebound");
            //myAnim.SetTrigger();
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
            skillName = "Defense";
            //Debug.Log("You:Defense");
            //myAnim.SetTrigger();
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
            skillName = "King";
            //Debug.Log("You:HolyGrail");
            //myAnim.SetTrigger();
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
            skillName = "King";
            //Debug.Log("You:Assassinate");
            //myAnim.SetTrigger();
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
            skillName = "King";
            //Debug.Log("You:King");
            //myAnim.SetTrigger();
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
            skillName = "King";
            //Debug.Log("You:Guard");
            //myAnim.SetTrigger();
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
            skillName = "Turtle";
            //Debug.Log("You:Turtle");
            //myAnim.SetTrigger();
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
            skillName = "King";
            //myAnim.SetTrigger();
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
            skillName = "Arrogant";
            //myAnim.SetTrigger();
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
            skillName = "Steal";
            //myAnim.SetTrigger();
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
            skillName = "King";
            //myAnim.SetTrigger();
        }
    }
}