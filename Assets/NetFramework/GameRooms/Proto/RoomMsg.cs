using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��ʼƥ��
public class MsgBgmatch : MsgBase
{
    MsgBgmatch() { protoName = "MsgBgmatch"; }
}

//����ƥ��
public class MsgMatched : MsgBase
{
    MsgMatched() { protoName = "MsgMatched"; }
    public NetGame.PlayerData? remotePlayer;
    public string remotePlayerId = "";
}
