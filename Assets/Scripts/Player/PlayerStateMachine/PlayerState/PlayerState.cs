using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerState : IState
{

    
    public PlayerState(PlayerStateMachine playerStateMachie) 
    { 
       
    }


    

    #region 状态方法
    public virtual void Enter()
    {
        Debug.Log("State"+GetType().Name);
        
        
    }

    public virtual void Exit()
    {
       
    }

    public virtual void HandleInput()
    {
        
    }

    public virtual void PhysicsUpdate()
    {
        
    }

    public virtual void Update()
    {
        
    }
    public virtual void OnTriggerEnter(Collider collider)
    {
        
        //这里判断的是是否与地面碰撞
        if (collider.gameObject.name == "Ground")
        {
            OnContactWithGround(collider);
        }

    }

    public virtual void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.name == "Ground")
        {
            OnContactWithGroundExited(collider);
        }
    }
    #endregion
    //与地面碰撞
    protected virtual void OnContactWithGround(Collider collider)
    {

    }


    protected virtual void OnContactWithGroundExited(Collider collider)
    {

    }

}
