using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitGameState : State
{
    public override void Enter(GlobalStateMachine host)
    {
        base.Enter(host);
        host.ChangeState(nextState);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
