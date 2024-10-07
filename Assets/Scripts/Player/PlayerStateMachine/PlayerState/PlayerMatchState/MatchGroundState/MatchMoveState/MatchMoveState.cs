using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MatchMoveState : MatchGroundState
{
    public MatchMoveState(PlayerStateMachine playerStateMachie) : base(playerStateMachie)
    {
    }
    #region 状态方法

    public override void Enter()
    {
        base.Enter();
        stateMachine.ResuableDataR.MovementSpeedModifier = groundData.walkData.MatchSpeedModifier;
        stateMachine.ResuableDataR.currentJumpForce = airData.JumpData.MatchWeakForce;
    }
    public override void Update()
    {
        base.Update();
        //DicideDirection();
    }
    #endregion





    #region 主要方法


    #endregion
    #region 输入方法
    protected override void OnMovementCancled(InputAction.CallbackContext context)
    {
        stateMachine.ChangeState(stateMachine.MatchIldeM);
        base.OnMovementCancled(context);
    }
    #endregion
}
