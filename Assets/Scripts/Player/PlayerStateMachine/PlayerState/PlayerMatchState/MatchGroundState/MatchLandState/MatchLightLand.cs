using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MatchLightLand : MatchLandState
{
    public MatchLightLand(PlayerStateMachine playerStateMachie) : base(playerStateMachie)
    {
    }
    public override void Enter()
    {
        stateMachine.ResuableDataR.MovementSpeedModifier = 0f;
        base.Update();
        stateMachine.ResuableDataR.currentJumpForce = airData.JumpData.StationaryForce;
    }
    public override void Update()
    {
        base.Update();
        if(stateMachine.ResuableDataR.MovementInput==Vector2.zero)
        {
            return;
        }
        DicideDirection();
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if(IsMovingDown())
        {
            return;
        }
        ResetVelocity();
    }
}
