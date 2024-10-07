using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalLandState : NormalGroundState
{
    public NormalLandState(PlayerStateMachine playerStateMachie) : base(playerStateMachie)
    {
    }
    public override void Enter()
    {
        base.Enter();
        
    }
    public override void Exit()
    {
        base.Exit();
    }
    protected override void OnContactWithGround(Collider collider)
    {
        stateMachine.ChangeState(stateMachine.NormalIdleN);
    }
}
