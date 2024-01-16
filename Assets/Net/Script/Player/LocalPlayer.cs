using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalPlayer : PlayerNetBase
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void RubbingEnergy()
    {
        base.RubbingEnergy();
        MsgAct msgAct = new();
        msgAct.act = MsgAct.Act.RubbingEnergy;
        NetManager.Send(msgAct);
        Debug.Log("You:Rubbing");
    }

    public override void Gun()
    {
        base.Gun();
        MsgAct msgAct = new();
        msgAct.act = MsgAct.Act.Gun;
        NetManager.Send(msgAct);
        Debug.Log("You:Gun");
    }

    public override void Defense()
    {
        base.Defense();
        MsgAct msgAct = new();
        msgAct.act = MsgAct.Act.Defense;
        NetManager.Send(msgAct);
        Debug.Log("You:Defense");
    }

    public override void HolyGrail()
    {
        base.HolyGrail();
        MsgAct msgAct = new();
        msgAct.act = MsgAct.Act.HolyGrail;
        NetManager.Send(msgAct);
        Debug.Log("You:HolyGrail");
    }
    public override void Rebound()
    {
        base.Rebound();
        MsgAct msgAct = new();
        msgAct.act = MsgAct.Act.Rebound;
        NetManager.Send(msgAct);
        Debug.Log("You:HolyGrail");
    }
}
