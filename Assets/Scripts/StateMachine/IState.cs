using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState 
{
    //״̬������״̬�����룬�˳�������
    void Enter();
    void Exit();
    void Update();
    void HandleInput();
    void PhysicsUpdate();
}
