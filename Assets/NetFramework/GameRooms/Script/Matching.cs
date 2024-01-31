using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Matching : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        matching.SetActive(true);
        matched.SetActive(false);
        return;
    }

    // Update is called once per frame
    void Update()
    {
        matchingFace();
        return;
    }

    [Header("ref")]
    public GameRoomMenu gameRoomMenu;
    public GameObject matching;
    public GameObject matched;
    public GameObject begingMatch;

    //如果正在查找就显示matching，如果找到就显示mathed界面.
    [Header("符号变化间隔")]
    public float signInterval;
    private string str = "匹配中";
    private float lastUpdate = 0;
    private int signNum = 1;

    public void OnClickBeginMatching()
    {

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
}
