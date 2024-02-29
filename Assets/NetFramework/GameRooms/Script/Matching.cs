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
    //匹配中动画页面
    public GameObject matching;
    //匹配成功页面（显示对手和自己的胜率以及对手id）
    public GameObject matched;
    //开始匹配按钮
    public GameObject begingMatch;

    //如果正在查找就显示matching，如果找到就显示mathed界面.
    [Header("符号变化相关")]
    public float signInterval;
    private string str = "匹配中";
    private float lastUpdate = 0;
    private int signNum = 1;

    [Header("Mathced界面引用")]
    public Text localPlayerId;
    public Text remotePlayerId;
    public Text localPlayerData;
    public Text remotePlayerData;

    //收到MsgMatched协议
    public void Matched(MsgBase msgBase)
    {
        NetManager.RemoveMsgListener("MsgMatched", Matched);
        matching.SetActive(false);
        matched.SetActive(true);
        MsgMatched msg = (MsgMatched)msgBase;
        PlayerData local = (PlayerData)JsonUtility.FromJson(msg.localPlayerData, typeof(PlayerData));
        PlayerData remote = (PlayerData)JsonUtility.FromJson(msg.remotePlayerData, typeof(PlayerData));
        localPlayerId.text = "ID:"+local.nickName;
        remotePlayerId.text = "对手ID:" + remote.nickName;

        localPlayerData.text = "数据：" + local.victoryTimes + "胜" + local.failTimes + "败\n" +
            "胜率" + (float)local.victoryTimes / (float)(local.victoryTimes + local.failTimes);
        remotePlayerData.text = "对手数据：" + remote.victoryTimes + "胜" + remote.failTimes + "败\n" +
            "胜率" + (float)remote.victoryTimes / (float)(remote.victoryTimes + remote.failTimes);
    }

    //按下开始匹配按钮
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

        Debug.Log("登出");
        NetManager.Close();
        NetManager.ping = -1;
        NetManager.RemoveMsgListener("MsgMatched", Matched);
        SceneManager.LoadScene("FightPrepare");
    }

    //匹配等待中的字符串动画
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
        Debug.Log("登出");
        NetManager.Close();
        NetManager.ping = -1;
        SceneManager.LoadScene("FightPrepare");
    }
}
