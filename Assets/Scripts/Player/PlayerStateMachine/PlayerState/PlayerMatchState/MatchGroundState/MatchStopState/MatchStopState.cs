using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MatchStopState : MatchGroundState
{
    public MatchStopState(PlayerStateMachine playerStateMachie) : base(playerStateMachie)
    {
    }

    #region  ‰»Î∑Ω∑®
    protected override void AddInputActionsCallBacks()
    {
        base.AddInputActionsCallBacks();
        stateMachine.Player.InputActions.playerActions.Movement.started += OnMovementStarted;

    }


    protected override void RemoveInputActionsCallBacks()
    {
        base.RemoveInputActionsCallBacks();
    }
    private void OnMovementStarted(InputAction.CallbackContext context)
    {
        DicideDirection();
    }
    #endregion
}
