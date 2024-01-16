using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MsgResult : MsgBase
{
    public MsgResult()
    {
        this.protoName = "MsgResult";
    }

    public enum Result
    {
        thisWin,
        thisLose,
        thisContinue
    }

    public Result result;
}
