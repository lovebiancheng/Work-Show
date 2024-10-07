using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NormalStopState : NormalGroundState
{
    public NormalStopState(PlayerStateMachine playerStateMachie) : base(playerStateMachie)
    {
    }
    #region 状态方法
    public override void Enter()
    {
        stateMachine.ResuableDataR.MovementSpeedModifier = 0f;
        base.Enter();
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }


    #endregion


    #region 反复方法
    protected override void AddInputActionsCallBacks()
    {
        base.AddInputActionsCallBacks();
        stateMachine.Player.InputActions.playerActions.Movement.started += OnMovementStared;
    }

    protected override void RemoveInputActionsCallBacks()
    {
        base.RemoveInputActionsCallBacks();
        stateMachine.Player.InputActions.playerActions.Movement.started += OnMovementStared;
    }

    #endregion





    #region 输入方法
    private  void OnMovementStared(InputAction.CallbackContext context)
    {
        OnMove();
    }

    #endregion

}
