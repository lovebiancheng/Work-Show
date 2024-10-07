using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MatchLeftMove : MatchMoveState
{
    public MatchLeftMove(PlayerStateMachine playerStateMachie) : base(playerStateMachie)
    {
    }
    #region ×´Ì¬·½·¨
    public override void Enter()
    {
        stateMachine.ResuableDataR.MovementSpeedModifier = groundData.walkData.MatchSpeedModifier;
        base.Enter();

    }
   
    #endregion
}
