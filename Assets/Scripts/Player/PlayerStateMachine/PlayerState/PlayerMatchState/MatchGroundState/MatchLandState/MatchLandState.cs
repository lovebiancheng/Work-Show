using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchLandState : MatchGroundState
{
    public MatchLandState(PlayerStateMachine playerStateMachie) : base(playerStateMachie)
    {
    }
    #region ×´Ì¬·½·¨
    public override void Enter()
    {
        base.Enter();
    }
    public override void Exit()
    {
        base.Exit();

    }
    protected override void OnContactWithGround(Collider collider)
    {
        stateMachine.ChangeState(stateMachine.MatchIldeM);
    }

    #endregion

}
