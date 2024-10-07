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
    


    #region ״̬����
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
    #region ��Ҫ����
    private void ReadMovementInput()
    {
        stateMachine.ResuableDataR.MovementInput = stateMachine.Player.InputActions.playerActions.Movement.ReadValue<Vector2>();
        

    }
    private void Move()
    {

        //�ж��Ƿ��ƶ�������δ����/�ƶ��ٶ�Ϊ0
        if (stateMachine.ResuableDataR.MovementInput == Vector2.zero || stateMachine.ResuableDataR.MovementSpeedModifier == 0f)
        {
            return;
        }
        Vector3 movementDirection = GetMovementInputDirection();
        float targetRotationYAngle = Rotate(movementDirection);

        Vector3 targetRotationDirection = GetTargetRotationDirection(targetRotationYAngle);

        float movementSpeed = GetMovementSpeed();

        Vector3 currentPlayerHorizontalVelocity = GetPlayerHorizontalVelocity();//���������Ϊ�˽����������֮�����һֱ�ƶ�����ֹͣ,����ǰ�ٶȱ�������

        stateMachine.Player.Rigidbody.AddForce(targetRotationDirection * movementSpeed - currentPlayerHorizontalVelocity, ForceMode.VelocityChange);

    }
    private float Rotate(Vector3 direction)//Player����ת�ķ���
    {
        float directionAngle = UpdateTargetRotation(direction);

        RotateTowardsTargetRotation();
        return directionAngle;
    }

    //��ȡ��ת�Ƕ�
    private float GetDirectionAngle(Vector3 direction)
    {
        float directionAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;/*Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;//����תΪ�Ƕ�*/
        if (directionAngle < 0)
        {
            directionAngle += 360f;
        }
        return directionAngle;
    }
    //������ת����
    private void UpdateTargetRotationData(float targetAngle)
    {
        stateMachine.ResuableDataR.CurrentTargetRotation.y = targetAngle;
        stateMachine.ResuableDataR.DampedTargetRotationPassedTime.y = 0f;
    }
    //����������ת�Ƕ�
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



    #region ��������
    //���ذ������µ�ֵ
    protected Vector3 GetMovementInputDirection()
    {
        return new Vector3(stateMachine.ResuableDataR.MovementInput.x, 0f, stateMachine.ResuableDataR.MovementInput.y);
    }

    //��ȡ�µ�ǰ������
    protected Vector3 GetTargetRotationDirection(float targetAngle)
    {
        return Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;//��Ԫ����������˿��Ա�ʾ����������������Ԫ����ת֮��õ����µ�����-���µ�ǰ������,��˵�˳�����Ҫ
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
    //��ȡ���ˮƽ����ٶ�
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
    //����Ŀ����ת
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
    //ģ����ת
    protected void RotateTowardsTargetRotation()
    {
        float currentYAngle = stateMachine.Player.Rigidbody.rotation.eulerAngles.y;//��ȡ��ǰ�Ƕ�
        if (currentYAngle == stateMachine.ResuableDataR.CurrentTargetRotation.y)
        {
            return;//                                                                                                                              
        }
        float smoothedYAngle = Mathf.SmoothDampAngle(currentYAngle, stateMachine.ResuableDataR.CurrentTargetRotation.y, ref stateMachine.ResuableDataR.DampedTargetRotationCurrentVelocity.y, stateMachine.ResuableDataR.TimeToReachTargetRotation.y - stateMachine.ResuableDataR.DampedTargetRotationPassedTime.y);
        stateMachine.ResuableDataR.DampedTargetRotationPassedTime.y += Time.deltaTime;

        Quaternion targetRotation = Quaternion.Euler(0f, smoothedYAngle, 0f);
        stateMachine.Player.Rigidbody.MoveRotation(targetRotation);
    }
    //ÿ�α仯Ϊ˲ʱ�仯
    protected void ResetVelocity()
    {
        stateMachine.Player.Rigidbody.velocity = Vector3.zero;//ȷ��˲ʱ�仯
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
        //����д�������

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
