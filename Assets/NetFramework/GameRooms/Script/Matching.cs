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
        MakeSure();
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
    public Text makeSure;

    private float mathedTime = 0;

    //10秒自动点击确认
    //Used by Update
    private void MakeSure()
    {
        if (!matched.activeSelf) return;
        int bdiff = (int)(Time.time - mathedTime);
        int diff = 10 - bdiff;
        if(diff>0)
        {
            makeSure.text = "确认(" + diff.ToString() + ")";
        }
        else
        {
            OnClickSure();
        }
    }

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
        NetMain.actions.Enqueue(() =>
        {
            mathedTime = Time.time;
            matching.SetActive(false);
            matched.SetActive(true);
            NetManager.RemoveMsgListener("MsgMatched", Matched);
            MsgMatched msg = (MsgMatched)msgBase;
            PlayerData local = (PlayerData)JsonUtility.FromJson(msg.localPlayerData, typeof(PlayerData));
            PlayerData remote = (PlayerData)JsonUtility.FromJson(msg.remotePlayerData, typeof(PlayerData));
            localPlayerId.text = "ID:" + local.nickName;
            remotePlayerId.text = "对手ID:" + remote.nickName;

            Whole.remoteNickName = remote.nickName;
            Whole.localNickName = local.nickName;


            localPlayerData.text = "数据：" + local.victoryTimes + "胜" + local.failTimes + "败\n" +
                "胜率" + OutPutRate(local.victoryTimes, local.victoryTimes + local.failTimes);
            remotePlayerData.text = "对手数据：" + remote.victoryTimes + "胜" + remote.failTimes + "败\n" +
                "胜率" + OutPutRate(remote.victoryTimes, remote.victoryTimes + remote.failTimes);
        });
    }

    //胜率显示
    private string OutPutRate(int win,int all)
    {
        float rate = 100f * win / all;
        if(all!=0)
        {
            float rate1 = Mathf.Round(rate * 100) / 100;
            return rate1.ToString("F2") + "%";
        }
        else
        {
            return "NaN%";
        }
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
