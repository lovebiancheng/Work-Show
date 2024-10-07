using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalLightLand : NormalLandState
{
    public NormalLightLand(PlayerStateMachine playerStateMachie) : base(playerStateMachie)
    {
    }
    #region ×´Ì¬·½·¨
    public override void Enter()
    {
        stateMachine.ResuableDataR.MovementSpeedModifier = 0f;
        base.Enter();
        stateMachine.ResuableDataR.currentJumpForce = airData.JumpData.StationaryForce;
        ResetVelocity();
    }
    public override void Update()
    {
        base.Update();
        if (stateMachine.ResuableDataR.MovementInput == Vector2.zero)
        {
            return;
        }
        OnMove();
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
