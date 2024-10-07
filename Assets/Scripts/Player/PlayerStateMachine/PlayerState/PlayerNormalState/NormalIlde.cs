using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class NormalIlde : NormalGroundState
{
    public NormalIlde(PlayerStateMachine playerStateMachie) : base(playerStateMachie)
    {
    }

    #region ×´Ì¬·½·¨
    public override void Enter()
    {
         stateMachine.ResuableDataR.MovementSpeedModifier = 0f;
        //stateMachine.ResuableDataR.
        base.Enter();
        stateMachine.ResuableDataR.currentJumpForce = airData.JumpData.StationaryForce;
        ResetVelocity();
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void Update()
    {
        base.Update();
        if(stateMachine.ResuableDataR.MovementInput==Vector2.zero)
        {
            return;
        }
        OnMove();
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if(!IsMovingDown())
        {
            return;
        }
        ResetVelocity();
    }
    #endregion



}
