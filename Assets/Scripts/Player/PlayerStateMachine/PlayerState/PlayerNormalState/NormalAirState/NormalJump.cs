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

    #region ״̬����
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
        //����������״̬  �ж��Ƿ����� �ж���Ҵ�ֱ�ٶ��Ƿ�Ϊ0��   ��Ϊ�������
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

    #region ��Ҫ����
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
    #region ���뷽��
    protected override void OnMovementCancelded(InputAction.CallbackContext context)
    {
        
    }
    #endregion


}
