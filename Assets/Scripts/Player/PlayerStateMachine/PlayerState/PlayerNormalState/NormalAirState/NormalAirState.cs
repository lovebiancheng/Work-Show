using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAirState : PlayerNormalState
{
    public NormalAirState(PlayerStateMachine playerStateMachie) : base(playerStateMachie)
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
    #endregion

    #region ��������
    protected override void OnContactWithGround(Collider collider)
    {
        stateMachine.ChangeState(stateMachine.NormalLightLandN);
    }

    #endregion





}
