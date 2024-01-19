using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MsgAct : MsgBase
{
    public MsgAct()
    {
        this.protoName = "MsgAct";
    }

    public enum Act
    {
        RubbingEnergy,
        Gun,
        Rebound,
        Defense,
        HolyGrail
    }

    public Act act;
}
