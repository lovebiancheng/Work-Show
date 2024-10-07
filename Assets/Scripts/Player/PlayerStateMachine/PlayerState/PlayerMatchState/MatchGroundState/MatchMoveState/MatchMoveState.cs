using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MatchMoveState : MatchGroundState
{
    public MatchMoveState(PlayerStateMachine playerStateMachie) : base(playerStateMachie)
    {
    }
    #region ״̬����

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





    #region ��Ҫ����


    #endregion
    #region ���뷽��
    protected override void OnMovementCancled(InputAction.CallbackContext context)
    {
        stateMachine.ChangeState(stateMachine.MatchIldeM);
        base.OnMovementCancled(context);
    }
    #endregion
}
