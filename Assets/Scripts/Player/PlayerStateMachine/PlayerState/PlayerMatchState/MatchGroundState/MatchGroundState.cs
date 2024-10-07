using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MatchGroundState : PlayerMatchState
{
    public MatchGroundState(PlayerStateMachine playerStateMachie) : base(playerStateMachie)
    {
    }

    #region ״̬����
    public override void Enter()
    {
        base.Enter();
    }
    #endregion

    public override void Exit()
    {
        base.Exit();
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    #region ��Ҫ����
    //protected virtual void OnMove()
    //{
        
    //}
    protected void DicideDirection()
    {
        //if (stateMachine.ResuableDataR.MovementInput == Vector2.zero)
        //{
        //    return;
        //}
        if (stateMachine.ResuableDataR.MovementInput.x > 0f)
        {
            stateMachine.ChangeState(stateMachine.MatchRightMove);
            return;
        }
        if (stateMachine.ResuableDataR.MovementInput.x < 0f)
        {
            stateMachine.ChangeState(stateMachine.MatchLeftMove);
            return;
        }
        if (stateMachine.ResuableDataR.MovementInput.y > 0f)
        {
            stateMachine.ChangeState(stateMachine.MatchForWardMove);
            return;
        }
        if (stateMachine.ResuableDataR.MovementInput.y < 0f)
        {
            stateMachine.ChangeState(stateMachine.MatchBackMove);
            return;
        }
        if (stateMachine.ResuableDataR.RemoveNumber == 2)
        {
            //Debug.Log("ִ������΢����");
            stateMachine.ChangeState(stateMachine.MatchLightAttackM);

            return;
        }
        if (stateMachine.ResuableDataR.RemoveNumber >= 3 && stateMachine.ResuableDataR.RemoveNumber <= 5)
        {
            //Debug.Log("ִ�������͹���");
            stateMachine.ChangeState(stateMachine.MatchMediumAttackM);
            return;
        }
        if (stateMachine.ResuableDataR.RemoveNumber > 5)
        {
            //Debug.Log("ִ�������͹���");
            stateMachine.ChangeState(stateMachine.MatchHardAttackM);
            return;
        }

    }
    #endregion




    #region ���뷽��

    protected override void AddInputActionsCallBacks()
    {
        base.AddInputActionsCallBacks();
        stateMachine.Player.InputActions.playerActions.Jump.started += OnJumpStarted;
    }


    protected override void RemoveInputActionsCallBacks()
    {
        base.RemoveInputActionsCallBacks();
        stateMachine.Player.InputActions.playerActions.Jump.started -= OnJumpStarted;
    }

    private void OnJumpStarted(InputAction.CallbackContext context)
    {
        stateMachine.ChangeState(stateMachine.MatchJumpM);
    }

    #endregion
}
