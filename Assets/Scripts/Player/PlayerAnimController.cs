using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerAnimController : MonoBehaviour
{
    public enum PlayerStates
    {
        Idle,
        Walk,
        Run,
        Jump,
        Attack,
        Sit
    }
    public PlayerStates playerState;
    public Animator playerAnimator;
    // Start is called before the first frame update
    void Start()
    {
        playerAnimator=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(playerState)
        {
            case PlayerStates.Idle:
                
                break;
            case PlayerStates.Walk:
                playerAnimator.SetInteger("SpeedA", 1);
                break;
            case PlayerStates.Run:
                playerAnimator.SetInteger("SpeedA", 3);
                break;
            case PlayerStates.Jump:
                
                break;
            case PlayerStates.Attack:
                break;
            case PlayerStates.Sit:
                break;
            default:
                break;
                
        }
    }
}
