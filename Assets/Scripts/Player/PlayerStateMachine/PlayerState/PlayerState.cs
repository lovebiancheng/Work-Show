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


    

    #region ״̬����
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
        
        //�����жϵ����Ƿ��������ײ
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
    //�������ײ
    protected virtual void OnContactWithGround(Collider collider)
    {

    }


    protected virtual void OnContactWithGroundExited(Collider collider)
    {

    }

}
