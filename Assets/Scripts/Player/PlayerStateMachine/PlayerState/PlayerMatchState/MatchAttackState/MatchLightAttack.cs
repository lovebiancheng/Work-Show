using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchLightAttack : MatchAttackState
{
    public MatchLightAttack(PlayerStateMachine playerStateMachie) : base(playerStateMachie)
    {

    }
    public override void Enter()
    {
        base.Enter();
        stateMachine.ChangeState(stateMachine.MatchIldeM);
    }
    public override void Exit()
    {
        
        base.Exit();
    }
}
