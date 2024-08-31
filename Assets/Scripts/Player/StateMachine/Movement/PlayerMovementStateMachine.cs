using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementStateMachine : StateMachine
{
    public Player player { get; }
    public PlayerIdleState idleState { get; }
    public PlayerWalkState walkState { get; }
    public PlayerRunState runState { get; }
    public PlayerSpriteState spriteState { get; }

    public PlayerMovementStateMachine(Player player1)
    {
        player = player1;
        idleState=new PlayerIdleState(this);
        walkState=new PlayerWalkState(this);
        runState=new PlayerRunState(this);
        spriteState=new PlayerSpriteState(this);
        //这种写法只是为了安全，并无具体作用
    }



}
