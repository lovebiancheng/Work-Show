using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalFall : NormalAirState
{
    private FallData fallData;
    private Vector3 playerPositionEnter;
    
    public NormalFall(PlayerStateMachine playerStateMachie) : base(playerStateMachie)
    {
        fallData = airData.FallData;
    }
    #region 状态方法
    public override void Enter()
    {
        base.Enter();
        playerPositionEnter=stateMachine.Player.transform.position;
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
    public override void Update()
    {
        base.Update();
        

    }
    #endregion

    #region 主要方法
    private void LimitVerticalVelocity()
    {
        Vector3 playerVerticalVelocity=GetPlayerVerticalVelocity();
        if (playerVerticalVelocity.y>=-fallData.FallSpeedLimt)
        {
            return;
        }
        Vector3 limitedVelocity=new Vector3(0f,-fallData.FallSpeedLimt-playerVerticalVelocity.y,0f);
        stateMachine.Player.Rigidbody.AddForce(limitedVelocity,ForceMode.VelocityChange);
    }
    #endregion

    #region 反复方法
    protected override void OnContactWithGround(Collider collider)
    {
        float fallDistance=playerPositionEnter.y-stateMachine.Player.transform.position.y;
        //Debug.Log(fallDistance+"这是掉落距离");
        if(fallDistance <fallData.MinimumDistanceToBeConsideredHardFall)
        {
            stateMachine.ChangeState(stateMachine.NormalLightLandN);
            return;
        }
        if(stateMachine.ResuableDataR.ShouldRun||stateMachine.ResuableDataR.MovementInput==Vector2.zero)
        {
            stateMachine.ChangeState(stateMachine.NormalHardLandN);
            return;
        }
    }
    #endregion

}
