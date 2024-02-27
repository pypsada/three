//using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.UI;

public class ChatFeild:MonoBehaviour
{
    [Header("ref")]
    public Text[] texts;
    public InputField inputField;
    [Header("set")]
    [Tooltip("对方消息颜色")]
    public Color remoteColor;
    [Tooltip("我方消息颜色")]
    public Color localColor;

    void Start()
    {
        AddMsgListener();
    }

    void Update()
    {
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
            TextRoll(msg.chatStr, remoteColor);
        });
    }

    //己方玩家发消息(点击发送按钮)
    public void OnClickSend()
    {
        TextRoll(inputField.text, localColor);
        inputField.text = "";
    }

    private void TextRoll(string text,Color color)
    {
        for (int i = 0; i < texts.Length - 1; i++)
        {
            texts[i] = texts[i + 1];
        }
        Text lastText = texts[texts.Length - 1];
        lastText.color = color;
        lastText.text = text;
    }
}