//using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.UI;

public class ChatFeild:MonoBehaviour
{
    [Header("ref")]
    public Text[] texts;
    public InputField inputField;

    void Start()
    {
        AddMsgListener();
    }

    void Update()
    {
        //OnClickEnter();
        return;
    }

    private void OnDestroy()
    {
        RemoveMsgListener();
    }

    private void AddMsgListener()
    {
        NetManager.AddMsgListener("MsgChat", RecvRemoteMassage);
    }

    private void RemoveMsgListener()
    {
        NetManager.RemoveMsgListener("MsgChat", RecvRemoteMassage);
    }

    //收到对方玩家消息
    private void RecvRemoteMassage(MsgBase msgBase)
    {
        NetMain.actions.Enqueue(() =>
        {
            MsgChat msg = (MsgChat)msgBase;
            TextRoll("对手:"+msg.chatStr);
        });
    }

    //己方玩家发消息(点击发送按钮)
    public void OnClickSend()
    {
        if (inputField.text.Replace(" ", "") == "") return;

        TextRoll("自己:"+inputField.text);

        MsgChat msg = new();
        msg.chatStr = inputField.text;
        NetManager.Send(msg);

        inputField.text = "";
    }

    //Used by update
    //按下Enter键相当于点击发送键
    //private void OnClickEnter()
    //{
    //    if(Input.GetKeyDown(KeyCode.KeypadEnter))
    //    {
    //        OnClickSend();
    //    }
    //}

    private void TextRoll(string text)
    {
        for (int i = 0; i < texts.Length - 1; i++)
        {
            texts[i].text = texts[i + 1].text;
        }
        Text lastText = texts[texts.Length - 1];
        lastText.text = text;
    }
}