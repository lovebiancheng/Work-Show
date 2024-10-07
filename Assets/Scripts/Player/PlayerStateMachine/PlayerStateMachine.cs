using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public Player Player { get; }


    public ResuableData ResuableDataR { get; }


    public NormalWalk NormalWalkN { get; }
    public NormalRun NormalRunN { get; }
    public NormalJump NormalJumpN { get; }
    public NormalFall NormalFall { get; }
    public NormalLightLand NormalLightLandN { get; }
    public NormalHardLand NormalHardLandN { get; }
    public NormalLightStop NormalLightStopN { get; }
    public NormalHardStop NormalHardStopN { get; }
    public NormalIlde NormalIdleN { get; }


    public MatchForWardMove MatchForWardMove { get; }
    public MatchBackMove MatchBackMove { get; }
    public MatchLeftMove MatchLeftMove { get; }
    public MatchRightMove MatchRightMove { get; }
    public MatchJump MatchJumpM { get; }
    public MatchFall MatchFallM { get; }
    public MatchLightLand MatchLightLandM { get; }
    public MatchHardLand MatchHardLandM { get; }
    public MatchLightStop MatchLightStopM { get; }
    public MatchHardStop MatchHardStopM { get; }
    public MatchIdle MatchIldeM { get; }
    
    public MatchLightAttack  MatchLightAttackM{get;}
    public MatchMediumAttack MatchMediumAttackM { get; }
    public MatchHardAttack   MatchHardAttackM { get; }


    public PlayerStateMachine(Player player)
    {
        Player = player;
        ResuableDataR = new ResuableData();


        NormalWalkN = new NormalWalk(this);
        NormalRunN =new NormalRun(this);
        NormalJumpN =new NormalJump(this);
        NormalFall =new NormalFall(this);
        NormalLightLandN =new NormalLightLand(this);
        NormalHardLandN = new NormalHardLand(this);
        NormalLightStopN=new NormalLightStop(this);
        NormalHardStopN=new NormalHardStop(this);
        NormalIdleN=new NormalIlde(this);


        MatchForWardMove = new MatchForWardMove(this);
        MatchBackMove = new MatchBackMove(this);
        MatchLeftMove = new MatchLeftMove(this);
        MatchRightMove = new MatchRightMove(this);
        MatchJumpM =new MatchJump(this);
        MatchFallM =new MatchFall(this);
        MatchLightLandM =new MatchLightLand(this);
        MatchHardLandM =new MatchHardLand(this);
        MatchLightStopM =new MatchLightStop(this);
        MatchHardStopM =new MatchHardStop(this);
        MatchIldeM=new MatchIdle(this);
        MatchIldeM=new MatchIdle(this);
        MatchLightAttackM=new MatchLightAttack(this);
        MatchMediumAttackM=new MatchMediumAttack(this);
        MatchHardAttackM=new MatchHardAttack(this);

    }








}
