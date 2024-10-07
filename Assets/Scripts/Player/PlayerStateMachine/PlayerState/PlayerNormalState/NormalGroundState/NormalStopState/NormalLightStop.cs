using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalLightStop : NormalStopState
{
    public NormalLightStop(PlayerStateMachine playerStateMachie) : base(playerStateMachie)
    {
    }
    #region ×´Ì¬·½·¨

    public override void Enter()
    {
        base.Enter();
        stateMachine.ResuableDataR.MovementDecelerationForce = groundData.stopData.LightDecelerationForce;
        stateMachine.ResuableDataR.currentJumpForce = airData.JumpData.WeakForce;
    }
    #endregion

}
