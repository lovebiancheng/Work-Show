using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalHardStop : NormalStopState
{
    public NormalHardStop(PlayerStateMachine playerStateMachie) : base(playerStateMachie)
    {
    }

    #region ״̬����
    public override void Enter()
    {
        base.Enter();
        stateMachine.ResuableDataR.MovementDecelerationForce = groundData.stopData.HardDecelerationForce;
        stateMachine.ResuableDataR.currentJumpForce = airData.JumpData.StrongForce;
    }
    public override void Exit()
    {
        base.Exit();
    }
    #endregion

    #region ��������
    protected override void OnMove()
    {
        base.OnMove();
    }
    #endregion



}
