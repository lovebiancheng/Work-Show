using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMatchState : PlayerState
{
    protected PlayerStateMachine stateMachine;
    protected GroundData groundData;
    protected AirData airData;
    public PlayerMatchState(PlayerStateMachine playerStateMachie) : base(playerStateMachie)
    {
        stateMachine = playerStateMachie;
        groundData = stateMachine.Player.Data.groundedData;
        airData = stateMachine.Player.Data.airborneData;

    }
    #region 状态方法
    public override void Enter()
    {
        
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
        if (!stateMachine.ResuableDataR.ShouldMatch)
        {
            stateMachine.ChangeState(stateMachine.NormalIdleN);
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
    protected void Move()
    {
        if(stateMachine.ResuableDataR.MovementInput==Vector2.zero||stateMachine.ResuableDataR.MovementSpeedModifier==0f)
        {
            return;
        }
        Vector3 inputDirection = new Vector3(stateMachine.ResuableDataR.MovementInput.x, 0f, stateMachine.ResuableDataR.MovementInput.y);
        float movementSpeed = stateMachine.ResuableDataR.MovementSpeedModifier;
        Vector3 playerDirection=stateMachine.Player.transform.forward;
        //Debug.Log(playerDirection+"        =");
        Vector3 rotateionAxis=Vector3.Cross(Vector3.forward,playerDirection).normalized;
        float angle=Mathf.Acos(Vector3.Dot(playerDirection,Vector3.forward))*Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, rotateionAxis);
        Vector3 newDirection = rotation * inputDirection;

        //Vector3 currentPlayerHorizontalVelocity =new Vector3( stateMachine.Player.Rigidbody.velocity.x,0f,stateMachine.Player.Rigidbody.velocity.y);
        stateMachine.Player.Rigidbody.AddForce(newDirection*movementSpeed, ForceMode.VelocityChange);
        
    }
    #endregion

    #region 反复方法
    protected Vector3 GetPlayerVerticalVelocity()
    {
        return new Vector3(0f, stateMachine.Player.Rigidbody.velocity.y, 0f);
    }

    protected void ResetVelocity()
    {
        stateMachine.Player.Rigidbody.velocity = Vector3.zero;
    }
    protected void ResetVirticalVelocity()
    {
        Vector3 velocity = stateMachine.Player.Rigidbody.velocity;
        velocity.y = 0f;
    }

    protected bool IsMovingUp(float miniumVelocity = 0.1f)
    {
        return GetPlayerVerticalVelocity().y > miniumVelocity;
    }
    protected bool IsMovingDown(float miniumVelocity = 0.1f)
    {
        return GetPlayerVerticalVelocity().y < -miniumVelocity;
    }

    //protected virtual void OnContactWithGround(Collider collider)
    //{

    //}

    //与地面碰撞
    //protected virtual void OnContactWithGroundExited(Collider collider)
    //{

    //}






    #endregion






    #region 输入方法
    protected virtual void AddInputActionsCallBacks()
    {
        stateMachine.Player.InputActions.playerActions.NormalOrMatch.started += OnNorMalOrMatch;
        stateMachine.Player.InputActions.playerActions.Look.started += OnMouseMovementStarted;
        stateMachine.Player.InputActions.playerActions.Movement.performed += OnMovementPerformed;
        stateMachine.Player.InputActions.playerActions.Movement.canceled += OnMovementCancled;
    }

    

    protected virtual void RemoveInputActionsCallBacks()
    {
        stateMachine.Player.InputActions.playerActions.NormalOrMatch.started -= OnNorMalOrMatch;
        stateMachine.Player.InputActions.playerActions.Look.started -= OnMouseMovementStarted;
        stateMachine.Player.InputActions.playerActions.Movement.performed -= OnMovementPerformed;
        stateMachine.Player.InputActions.playerActions.Movement.canceled -= OnMovementCancled;
    }
    protected void OnNorMalOrMatch(InputAction.CallbackContext context)
    {
        stateMachine.ResuableDataR.ShouldMatch = !stateMachine.ResuableDataR.ShouldMatch;
       // Debug.Log("Match--------"+stateMachine.ResuableDataR.ShouldMatch);
    }
    private void OnMouseMovementStarted(InputAction.CallbackContext context)
    {
        
    }
    private void OnMovementPerformed(InputAction.CallbackContext context)
    {

    }
    protected virtual void  OnMovementCancled(InputAction.CallbackContext context)
    {

    }
    #endregion
}
