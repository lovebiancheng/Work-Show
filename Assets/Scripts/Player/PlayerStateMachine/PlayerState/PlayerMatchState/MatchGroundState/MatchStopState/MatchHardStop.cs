using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchHardStop : MatchStopState
{
    public MatchHardStop(PlayerStateMachine playerStateMachie) : base(playerStateMachie)
    {
    }
    public override void Enter()
    {
        base.Enter();
        stateMachine.ResuableDataR.MovementDecelerationForce = 0;
        stateMachine.ResuableDataR.MovementDecelerationForce = groundData.stopData.LightDecelerationForce;
        //stateMachine.ResuableDataR.currentJumpForce = airData.JumpData.WeakForce;
    }
}
