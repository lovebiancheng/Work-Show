using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchFall : MatchAirState
{
    private FallData fallData;
    private Vector3 playerPositionEnter;
    public MatchFall(PlayerStateMachine playerStateMachie) : base(playerStateMachie)
    {
        fallData = airData.FallData;
    }
    #region 状态方法
    public override void Enter()
    {
        base.Enter();
        playerPositionEnter = stateMachine.Player.transform.position;
        stateMachine.ResuableDataR.MovementSpeedModifier = 0f;
        ResetVirticalVelocity();
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

    #region 反复方法
    protected override void OnContactWithGround(Collider collider)
    {
        float fallDistance = playerPositionEnter.y - stateMachine.Player.transform.position.y;
        if (fallDistance < fallData.MinimumDistanceToBeConsideredHardFall)
        {
            stateMachine.ChangeState(stateMachine.MatchLightLandM);
            return;
        }
        //if (stateMachine.ResuableDataR.ShouldRun || stateMachine.ResuableDataR.MovementInput == Vector2.zero)
        //{
        //    stateMachine.ChangeState(stateMachine.MatchHardLandM);
        //    return;
        //}
    }
    #endregion

}
