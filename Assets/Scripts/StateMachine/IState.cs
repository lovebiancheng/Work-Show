using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState 
{
    //状态的三种状态，进入，退出，更新
    void Enter();
    void Exit();
    void Update();
    void HandleInput();
    void PhysicsUpdate();
}
