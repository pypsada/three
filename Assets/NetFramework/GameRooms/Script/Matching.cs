using NetGame;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Matching : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        matching.SetActive(false);
        matched.SetActive(false);
        begingMatch.SetActive(true);
        NetManager.AddMsgListener("MsgMatched", Matched);

        OnClickBeginMatching();
        return;
    }

    // Update is called once per frame
    void Update()
    {
        matchingFace();
        return;
    }

    [Header("ref")]
    //public GameRoomMenu gameRoomMenu;
    //ƥ���ж���ҳ��
    public GameObject matching;
    //ƥ��ɹ�ҳ�棨��ʾ���ֺ��Լ���ʤ���Լ�����id��
    public GameObject matched;
    //��ʼƥ�䰴ť
    public GameObject begingMatch;

    //������ڲ��Ҿ���ʾmatching������ҵ�����ʾmathed����.
    [Header("���ű仯���")]
    public float signInterval;
    private string str = "ƥ����";
    private float lastUpdate = 0;
    private int signNum = 1;

    [Header("Mathced��������")]
    public Text localPlayerId;
    public Text remotePlayerId;
    public Text localPlayerData;
    public Text remotePlayerData;

    //�յ�MsgMatchedЭ��
    public void Matched(MsgBase msgBase)
    {
        NetManager.RemoveMsgListener("MsgMatched", Matched);
        matching.SetActive(false);
        matched.SetActive(true);
        MsgMatched msg = (MsgMatched)msgBase;
        PlayerData local = (PlayerData)JsonUtility.FromJson(msg.localPlayerData, typeof(PlayerData));
        PlayerData remote = (PlayerData)JsonUtility.FromJson(msg.remotePlayerData, typeof(PlayerData));
        localPlayerId.text = "ID:"+local.nickName;
        remotePlayerId.text = "����ID:" + remote.nickName;

        localPlayerData.text = "���ݣ�" + local.victoryTimes + "ʤ" + local.failTimes + "��\n" +
            "ʤ��" + (float)local.victoryTimes / (float)(local.victoryTimes + local.failTimes);
        remotePlayerData.text = "�������ݣ�" + remote.victoryTimes + "ʤ" + remote.failTimes + "��\n" +
            "ʤ��" + (float)remote.victoryTimes / (float)(remote.victoryTimes + remote.failTimes);
    }

    //���¿�ʼƥ�䰴ť
    public void OnClickBeginMatching()
    {
        begingMatch.SetActive(false);
        matching.SetActive(true);
        NetManager.Send(new MsgBgmatch());
    }

    public void OnClickQuiteMatch()
    {
        begingMatch.SetActive(true);
        matching.SetActive(false);
        NetManager.Send(new MsgMatchQuit());

        Debug.Log("�ǳ�");
        NetManager.Close();
        NetManager.ping = -1;
        NetManager.RemoveMsgListener("MsgMatched", Matched);
        SceneManager.LoadScene("FightPrepare");
    }

    //ƥ��ȴ��е��ַ�������
    private void matchingFace()
    {
        if (matching.activeSelf == true)
        {
            Text text = matching.GetComponent<Text>();
            if (Time.time - lastUpdate > signInterval)
            {
                lastUpdate = Time.time;
                signNum = signNum % 6 + 1;
                string buff = "";
                for (int i = 0; i < signNum; i++)
                {
                    buff += ".";
                }
                text.text = str + buff;
            }
        }
    }

    public void OnClickSure()
    {
        SceneManager.LoadScene("NetBeforePlaying");
    }

    public void OnClickExit()
    {
        Debug.Log("�ǳ�");
        NetManager.Close();
        NetManager.ping = -1;
        SceneManager.LoadScene("FightPrepare");
    }
}
