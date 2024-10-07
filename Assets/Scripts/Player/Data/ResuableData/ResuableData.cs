using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResuableData 
{
    public Vector2 MovementInput { get; set; }
    public float MovementSpeedModifier { get; set; } = 1f;
    public float MovementOnSlopeSpeedModifier { get; set; } = 1f;
    public float MovementDecelerationForce { get; set; } = 1f;
    //public List<PlayerCamerRecentteringData> sidewayCameraRecenteringData { get; set; }
    //public List<PlayerCamerRecentteringData> backwardCameraRecenteringData { get; set; }

    public int RemoveNumber { get; set; } = -1;


    public bool ShouldRun { get; set; }
    //public bool ShouldSprint { get; set; }

    public bool ShouldMatch { get; set; }

    private Vector3 currentTargetRotation;
    private Vector3 timeToReachTargetRotation;
    private Vector3 dampedTargetRotationCurrentVelocity;
    private Vector3 dampedTargetRotationPassedTime;


    public ref Vector3 CurrentTargetRotation
    {
        get { return ref currentTargetRotation; }
    }
    public ref Vector3 TimeToReachTargetRotation
    {
        get { return ref timeToReachTargetRotation; }
    }
    public ref Vector3 DampedTargetRotationCurrentVelocity
    {
        get { return ref dampedTargetRotationCurrentVelocity; }
    }
    public ref Vector3 DampedTargetRotationPassedTime
    {
        get { return ref dampedTargetRotationPassedTime; }
    }

    public Vector3 currentJumpForce { get; set; }


    public RotationData rotationData { get; set; }
}
