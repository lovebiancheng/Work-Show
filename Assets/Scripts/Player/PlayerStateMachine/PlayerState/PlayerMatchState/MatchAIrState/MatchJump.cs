using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MatchJump : MatchAirState
{
    private JumpData jumpData;
    public MatchJump(PlayerStateMachine playerStateMachie) : base(playerStateMachie)
    {
        jumpData = airData.JumpData;
    }
    #region 状态方法
    public override void Enter()
    {
        stateMachine.ResuableDataR.MovementSpeedModifier = 0;
        base.Enter();
        stateMachine.ResuableDataR.MovementDecelerationForce = jumpData.DecelerationForce;
        Jump();
    }
    #endregion

    #region 主要方法
    private void Jump()
    {
        Vector3 jumpForce = stateMachine.ResuableDataR.currentJumpForce;
        Vector3 jumpDirection = stateMachine.Player.transform.forward;
        jumpForce.x *= jumpDirection.x;
        jumpForce.z *= jumpDirection.z;
        ResetVelocity();
        stateMachine.Player.Rigidbody.AddForce(jumpForce, ForceMode.VelocityChange);
    }
    #endregion


    #region 输入方法
    protected override void OnMovementCancled(InputAction.CallbackContext context)
    {
       //base.OnMovementCancled(context);
    }
    #endregion

}

