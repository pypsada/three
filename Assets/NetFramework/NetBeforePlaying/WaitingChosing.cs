using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WaitingChosing : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        lastUpdate = 0;
        signNum = 1;

        NetManager.Send(new MsgClientReady());
        NetManager.AddMsgListener("MsgAllReady", OnLoad);
    }

    public void OnLoad(MsgBase msgBase)
    {
        NetManager.RemoveMsgListener("MsgAllReady", OnLoad);
        NetMain.actions.Enqueue(() =>
        {
            SceneManager.LoadScene("OnPlaying");
        });
    }

    // Update is called once per frame
    void Update()
    {
        OnWaiting();
    }

    [Header("ref")]
    public Text text;
    [Header("Setting")]
    [TextArea]
    public string str;
    public float signInterval;

    private float lastUpdate;
    private int signNum;

    //Æ¥ÅäµÈ´ýÖÐµÄ×Ö·û´®¶¯»­
    private void OnWaiting()
    {
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
