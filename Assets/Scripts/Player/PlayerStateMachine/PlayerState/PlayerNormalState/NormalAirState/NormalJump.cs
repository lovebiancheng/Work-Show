using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NormalJump : NormalAirState
{
    private bool shouldKeepRotating;
    private JumpData jumpData;
    private bool canStartFalling;
    public NormalJump(PlayerStateMachine playerStateMachie) : base(playerStateMachie)
    {
        jumpData = airData.JumpData;
        stateMachine.ResuableDataR.rotationData = jumpData.rotationData;
    }

    #region 状态方法
    public override void Enter()
    {
        base.Enter();
        stateMachine.ResuableDataR.MovementSpeedModifier = 0;
        stateMachine.ResuableDataR.MovementDecelerationForce = jumpData.DecelerationForce;
        shouldKeepRotating = stateMachine.ResuableDataR.MovementInput != Vector2.zero;
        Jump();
    }
    public override void Exit()
    {
        base.Exit();
        canStartFalling = false;
    }
    public override void Update()
    {
        base.Update();
        if (!canStartFalling && IsMovingUp())
        {
            canStartFalling = true;
        }
        if (!canStartFalling || GetPlayerVerticalVelocity().y > 0)
        {
            return;
        }
        stateMachine.ChangeState(stateMachine.NormalFall);
        //这里填充掉落状态  判断是否上升 判断玩家垂直速度是否为0，   都为否，则跌落
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (shouldKeepRotating)
        {
            RotateTowardsTargetRotation();
        }
        if(IsMovingUp())
        {
            DecelerateVertically();
        }
    }

    #endregion

    #region 主要方法
    private void Jump()
    {
        Vector3 jumpForce = stateMachine.ResuableDataR.currentJumpForce;
        Vector3 jumpDirection=stateMachine.Player.transform.forward;
        if(shouldKeepRotating)
        {
            UpdateTargetRotation(GetMovementInputDirection());
            jumpDirection = GetTargetRotationDirection(stateMachine.ResuableDataR.CurrentTargetRotation.y);
            
        }
        jumpForce.x *= jumpDirection.x;
        jumpForce.z *= jumpDirection.z;

        //Vector3 capsuleColliderCenterInWorldSpace = stateMachine.Player.transform.position;
        //Ray dowanwardRayFromPlayer = new Ray(capsuleColliderCenterInWorldSpace, Vector3.down);

        //if (Physics.Raycast(dowanwardRayFromPlayer, out RaycastHit hit, jumpData.JumpToGroundRayDistance, stateMachine.Player.LayerData.groundlayer, QueryTriggerInteraction.Ignore)) ;
        //{

        //}
        ResetVelocity();
        stateMachine.Player.Rigidbody.AddForce(jumpForce,ForceMode.VelocityChange);
    }


    #endregion
    #region 输入方法
    protected override void OnMovementCancelded(InputAction.CallbackContext context)
    {
        
    }
    #endregion


}
