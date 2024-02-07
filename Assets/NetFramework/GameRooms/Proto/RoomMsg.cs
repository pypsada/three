using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ø™ º∆•≈‰
public class MsgBgmatch : MsgBase
{
    MsgBgmatch() { protoName = "MsgBgmatch"; }
}

//Ω· ¯∆•≈‰
public class MsgMatched : MsgBase
{
    MsgMatched() { protoName = "MsgMatched"; }
    public NetGame.PlayerData? remotePlayer;
    public string remotePlayerId = "";
}
