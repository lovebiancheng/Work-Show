using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerNormalState : PlayerState
{

    protected PlayerStateMachine stateMachine;
    protected GroundData groundData;
    protected AirData airData;
    public PlayerNormalState(PlayerStateMachine playerStateMachie) : base(playerStateMachie)
    {
        stateMachine = playerStateMachie;
        groundData = stateMachine.Player.Data.groundedData;
        airData = stateMachine.Player.Data.airborneData;
    }
    


    #region 状态方法
    public override void Enter()
    {
        //if (stateMachine.ResuableDataR.ShouldMatch)
        //{
            
        //    return;
        //}
            
        base.Enter();
        AddInputActionsCallBacks();
    }
    public override void Exit() 
    {
        RemoveInputActionsCallBacks();
    }
    public override void PhysicsUpdate()
    {
        Move();
    }
    public override void HandleInput()
    {
        if (stateMachine.ResuableDataR.ShouldMatch)
        {
            stateMachine.ChangeState(stateMachine.MatchIldeM);
            return;
        }
        ReadMovementInput();
        
        
    }
    #endregion
    #region 主要方法
    private void ReadMovementInput()
    {
        stateMachine.ResuableDataR.MovementInput = stateMachine.Player.InputActions.playerActions.Movement.ReadValue<Vector2>();
        

    }
    private void Move()
    {

        //判断是否移动，按键未按下/移动速度为0
        if (stateMachine.ResuableDataR.MovementInput == Vector2.zero || stateMachine.ResuableDataR.MovementSpeedModifier == 0f)
        {
            return;
        }
        Vector3 movementDirection = GetMovementInputDirection();
        float targetRotationYAngle = Rotate(movementDirection);

        Vector3 targetRotationDirection = GetTargetRotationDirection(targetRotationYAngle);

        float movementSpeed = GetMovementSpeed();

        Vector3 currentPlayerHorizontalVelocity = GetPlayerHorizontalVelocity();//这个方法是为了解决按键按下之后玩家一直移动，不停止,将当前速度保存下来

        stateMachine.Player.Rigidbody.AddForce(targetRotationDirection * movementSpeed - currentPlayerHorizontalVelocity, ForceMode.VelocityChange);

    }
    private float Rotate(Vector3 direction)//Player将旋转的方向
    {
        float directionAngle = UpdateTargetRotation(direction);

        RotateTowardsTargetRotation();
        return directionAngle;
    }

    //获取旋转角度
    private float GetDirectionAngle(Vector3 direction)
    {
        float directionAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;/*Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;//弧度转为角度*/
        if (directionAngle < 0)
        {
            directionAngle += 360f;
        }
        return directionAngle;
    }
    //更新旋转数据
    private void UpdateTargetRotationData(float targetAngle)
    {
        stateMachine.ResuableDataR.CurrentTargetRotation.y = targetAngle;
        stateMachine.ResuableDataR.DampedTargetRotationPassedTime.y = 0f;
    }
    //摄像机添加旋转角度
    private float AddCameraRotationToAngle(float angle)
    {
        angle += stateMachine.Player.MmainCameraTransform.eulerAngles.y;
        if (angle > 360f)
        {
            angle -= 360f;
        }
        return angle;
    }
#endregion



    #region 反复方法
    //返回按键按下的值
    protected Vector3 GetMovementInputDirection()
    {
        return new Vector3(stateMachine.ResuableDataR.MovementInput.x, 0f, stateMachine.ResuableDataR.MovementInput.y);
    }

    //获取新的前进方向
    protected Vector3 GetTargetRotationDirection(float targetAngle)
    {
        return Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;//四元数和向量相乘可以表示这个向量按照这个四元数旋转之后得到的新的向量-即新的前进方向,相乘的顺序很重要
    }
    protected float GetMovementSpeed(bool shouldConsiderSlopes = true)
    {
        float movementSpeed = groundData.baseSpeed * stateMachine.ResuableDataR.MovementOnSlopeSpeedModifier;
        if (shouldConsiderSlopes)
        {
            movementSpeed *= stateMachine.ResuableDataR.MovementSpeedModifier;

        }
        return movementSpeed;
    }
    //获取玩家水平向的速度
    protected Vector3 GetPlayerHorizontalVelocity()
    {
        Vector3 playerHorizontalVelocity = stateMachine.Player.Rigidbody.velocity;
        playerHorizontalVelocity.y = 0f;
        return playerHorizontalVelocity;
    }
    protected Vector3 GetPlayerVerticalVelocity()
    {
        return new Vector3(0f,stateMachine.Player.Rigidbody.velocity.y, 0f);
    }
    //更新目标旋转
    protected float UpdateTargetRotation(Vector3 direction, bool shouldConsiderCamerRotation = true)
    {
        float directionAngle = GetDirectionAngle(direction);

        if (shouldConsiderCamerRotation)
        {
            directionAngle = AddCameraRotationToAngle(directionAngle);
        }



        if (directionAngle != stateMachine.ResuableDataR.CurrentTargetRotation.y)
        {
            UpdateTargetRotationData(directionAngle);
        }

        return directionAngle;
    }
    //模型旋转
    protected void RotateTowardsTargetRotation()
    {
        float currentYAngle = stateMachine.Player.Rigidbody.rotation.eulerAngles.y;//获取当前角度
        if (currentYAngle == stateMachine.ResuableDataR.CurrentTargetRotation.y)
        {
            return;//                                                                                                                              
        }
        float smoothedYAngle = Mathf.SmoothDampAngle(currentYAngle, stateMachine.ResuableDataR.CurrentTargetRotation.y, ref stateMachine.ResuableDataR.DampedTargetRotationCurrentVelocity.y, stateMachine.ResuableDataR.TimeToReachTargetRotation.y - stateMachine.ResuableDataR.DampedTargetRotationPassedTime.y);
        stateMachine.ResuableDataR.DampedTargetRotationPassedTime.y += Time.deltaTime;

        Quaternion targetRotation = Quaternion.Euler(0f, smoothedYAngle, 0f);
        stateMachine.Player.Rigidbody.MoveRotation(targetRotation);
    }
    //每次变化为瞬时变化
    protected void ResetVelocity()
    {
        stateMachine.Player.Rigidbody.velocity = Vector3.zero;//确保瞬时变化
    }
    protected void ResetVirticalVelocity()
    {
        Vector3 playerHorizontalVelocity=GetPlayerHorizontalVelocity();
        stateMachine.Player.Rigidbody.velocity=playerHorizontalVelocity;
    }
    protected bool IsMovingUp(float miniumVelocity=0.1f)
    {
        return GetPlayerVerticalVelocity().y > miniumVelocity;
    }
    protected bool IsMovingDown(float miniumVelocity = 0.1f)
    {
        return GetPlayerVerticalVelocity().y < -miniumVelocity;
    }

    
    protected void DecelerateVertically()
    {
        Vector3 playerVerticalVelocity = GetPlayerVerticalVelocity();
        stateMachine.Player.Rigidbody.AddForce(-playerVerticalVelocity * stateMachine.ResuableDataR.MovementDecelerationForce, ForceMode.Acceleration);
    }
    #endregion







    protected virtual void AddInputActionsCallBacks()
    {
        stateMachine.Player.InputActions.playerActions.Look.started += OnMouseMovementStarted;
        stateMachine.Player.InputActions.playerActions.WalkToogle.started += OnWalkToggle;
        stateMachine.Player.InputActions.playerActions.Movement.performed += OnMovementPerformed;
        stateMachine.Player.InputActions.playerActions.Movement.canceled += OnMovementCancelded;
        stateMachine.Player.InputActions.playerActions.NormalOrMatch.started += OnNormalOrMatch;
    }

    protected virtual void RemoveInputActionsCallBacks()
    {
        stateMachine.Player.InputActions.playerActions.Look.started -= OnMouseMovementStarted;
        stateMachine.Player.InputActions.playerActions.WalkToogle.started -= OnWalkToggle;
        stateMachine.Player.InputActions.playerActions.Movement.performed -= OnMovementPerformed;
        stateMachine.Player.InputActions.playerActions.Movement.canceled -= OnMovementCancelded;
        stateMachine.Player.InputActions.playerActions.NormalOrMatch.started -= OnNormalOrMatch;
    }


    private void OnMouseMovementStarted(InputAction.CallbackContext context)
    {
        //这里写相机跟随

    }
    private void OnMovementPerformed(InputAction.CallbackContext context)
    {

    }
    protected virtual void OnWalkToggle(InputAction.CallbackContext context)
    {
        stateMachine.ResuableDataR.ShouldRun = !stateMachine.ResuableDataR.ShouldRun;
    }
    protected virtual void OnMovementCancelded(InputAction.CallbackContext context)
    {

    }
    protected virtual void OnNormalOrMatch(InputAction.CallbackContext context)
    {
        stateMachine.ResuableDataR.ShouldMatch=!stateMachine.ResuableDataR.ShouldMatch;
        //Debug.Log("Normal--------" + stateMachine.ResuableDataR.ShouldMatch);
    }

    







}
