using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchLightStop : MatchStopState
{
    public MatchLightStop(PlayerStateMachine playerStateMachie) : base(playerStateMachie)
    {
    }


    #region ×´Ì¬·½·¨
    public override void Enter()
    {
        base.Enter();
        stateMachine.ResuableDataR.MovementDecelerationForce = 0;
        stateMachine.ResuableDataR.currentJumpForce = airData.JumpData.MatchWeakForce;
    }
    #endregion

}
