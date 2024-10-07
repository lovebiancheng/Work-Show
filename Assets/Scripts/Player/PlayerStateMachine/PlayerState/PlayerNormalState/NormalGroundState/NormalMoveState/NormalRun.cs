using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NormalRun : NormalGroundState
{
    public NormalRun(PlayerStateMachine playerStateMachie) : base(playerStateMachie)
    {
    }
    #region ״̬����
    public override void Enter()
    {
        stateMachine.ResuableDataR.MovementSpeedModifier = groundData.runData.NormalSpeedModifier;
        base.Enter();
        stateMachine.ResuableDataR.currentJumpForce = airData.JumpData.StrongForce;
    }
    public override void Exit()
    {
        base.Exit();

    }
    public override void Update()
    {
        base.Update();
    }
    #endregion

    #region ��Ҫ����
    //private void StopRun()
    //{
    //    if(stateMachine.ResuableDataR.MovementInput==Vector2.zero)
    //    {
    //        stateMachine.ChangeState(stateMachine.NormalIdleN);
    //        return;
    //    }
    //    stateMachine.ChangeState(stateMachine.NormalWalkN);
    //}
    #endregion
    #region ���뷽��
    protected override void OnMovementCancelded(InputAction.CallbackContext context)
    {
        stateMachine.ChangeState(stateMachine.NormalHardStopN);
        base.OnMovementCancelded(context);
    }
    protected override void OnWalkToggle(InputAction.CallbackContext context)
    {
        base.OnWalkToggle(context);
        stateMachine.ChangeState(stateMachine.NormalWalkN);
    }

    #endregion
}
