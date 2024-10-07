using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MatchForWardMove : MatchMoveState
{
    public MatchForWardMove(PlayerStateMachine playerStateMachie) : base(playerStateMachie)
    {
    }
    #region 状态方法
    public override void Enter()
    {
        stateMachine.ResuableDataR.MovementSpeedModifier = groundData.walkData.MatchSpeedModifier;
        base.Enter();

    }
    public override void Exit()
    {
        base.Exit();
    }
    #endregion
    #region 输入方法
    //protected override void OnMovementCancled(InputAction.CallbackContext context)
    //{
    //    base.OnMovementCancled(context);
    //}
    #endregion
}
