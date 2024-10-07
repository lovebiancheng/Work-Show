using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NormalGroundState : PlayerNormalState
{
    public NormalGroundState(PlayerStateMachine playerStateMachie) : base(playerStateMachie)
    {
    }
    #region ״̬����
    public override void Enter()
    {
        base.Enter();
    }
    public override void Exit() 
    { 
        base.Exit();
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    #endregion

    #region ��������
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
    protected virtual void OnMove()
    {
        if (!stateMachine.ResuableDataR.ShouldMatch&&stateMachine.ResuableDataR.ShouldRun)
        {
            
            stateMachine.ChangeState(stateMachine.NormalRunN);
        }
        if (!stateMachine.ResuableDataR.ShouldMatch&&!stateMachine.ResuableDataR.ShouldRun)
        {
            stateMachine.ChangeState(stateMachine.NormalWalkN);
        }
        
    }
    #endregion

    #region ���뷽��
    
    protected virtual void OnJumpStarted(InputAction.CallbackContext context)
    {
        stateMachine.ChangeState(stateMachine.NormalJumpN);
    }
    


    #endregion

}
