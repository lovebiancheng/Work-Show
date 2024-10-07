using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchIdle : MatchGroundState
{
    public MatchIdle(PlayerStateMachine playerStateMachie) : base(playerStateMachie)
    {
    }
    #region ×´Ì¬·½·¨
    public override void Enter()
    {
        stateMachine.ResuableDataR.MovementSpeedModifier = 0f;
        stateMachine.ResuableDataR.currentJumpForce = airData.JumpData.StationaryForce;
        base.Enter();
        ResetVelocity();
    }
    public override void Update()
    {
        base.Update();
        
        DicideDirection();
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (!IsMovingDown())
        {
            return;
        }
        ResetVelocity();
    }

    #endregion
}
