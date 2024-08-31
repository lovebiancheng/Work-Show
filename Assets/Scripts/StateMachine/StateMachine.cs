using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine 
{
    protected IState currentState;
    public void ChangeState(IState state)
    {
        //先退出上一次状态
        currentState?.Exit();
        currentState = state;
        currentState.Enter();
    }
    public void Update()
    {
        currentState?.Update();
    }
    public void PhysicsUpdate()
    {
        currentState?.PhysicsUpdate();
    }
}
