using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��ʼƥ��
//�ͻ��˷���������˲�����
public class MsgBgmatch : MsgBase
{
    public MsgBgmatch() { protoName = "MsgBgmatch"; }
}

//����ƥ��
//����˷������ͻ��˲�����
public class MsgMatched : MsgBase
{
    public MsgMatched() { protoName = "MsgMatched"; }
    public NetGame.PlayerData remotePlayerData;
    public NetGame.PlayerData localPlayerData;
    public string remotePlayerId = "";
    public string localPlayerId = "";
}

//�˳�ƥ�䡣
//�ͻ��˷���������˲�����
public class MsgMatchQuit:MsgBase
{
    public MsgMatchQuit() { protoName = "MsgMatchQuit"; }
}
