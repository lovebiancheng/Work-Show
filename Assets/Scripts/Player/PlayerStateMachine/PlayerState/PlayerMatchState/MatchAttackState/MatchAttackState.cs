using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchAttackState : PlayerMatchState
{
    public MatchAttackState(PlayerStateMachine playerStateMachie) : base(playerStateMachie)
    {
    }
    public override void Enter()
    {
        base.Enter();
        stateMachine.ResuableDataR.RemoveNumber = 0;
        
        
    }
    public override void Exit()
    {
        
        base.Exit();
    }
   
}
