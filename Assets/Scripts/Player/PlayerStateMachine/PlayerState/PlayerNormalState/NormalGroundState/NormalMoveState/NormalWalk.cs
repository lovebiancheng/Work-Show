using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NormalWalk : NormalGroundState
{
    public NormalWalk(PlayerStateMachine playerStateMachie) : base(playerStateMachie)
    {
    }

    #region ״̬����
    public override void Enter()
    {
        stateMachine.ResuableDataR.MovementSpeedModifier = groundData.walkData.NormalSpeedModifier;
        base.Enter();
        stateMachine.ResuableDataR.currentJumpForce = airData.JumpData.WeakForce;


    }
    public override void Exit()
    {
        base.Exit();

    }
    public override void Update()
    {
        base.Update();
        if(stateMachine.ResuableDataR.ShouldRun)
        {
            stateMachine.ChangeState(stateMachine.NormalRunN);
        }
    }
    #endregion

    #region ���뷽��
    protected override void OnMovementCancelded(InputAction.CallbackContext context)
    {
        stateMachine.ChangeState(stateMachine.NormalLightStopN);
        base.OnMovementCancelded(context);
    }
    protected override void OnWalkToggle(InputAction.CallbackContext context)
    {
        base.OnWalkToggle(context);
        stateMachine.ChangeState(stateMachine.NormalRunN);
    }
    #endregion


}
