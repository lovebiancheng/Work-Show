using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class GroundData
{
    [field: SerializeField][field: Range(0f, 25f)] public float baseSpeed { get; private set; } = 5f;
    [field: SerializeField][field: Range(0f, 5f)] public float groundToFallRayDistance { get; private set; }
    //[field: SerializeField] public List<PlayerCamerRecentteringData> sidewaysCameraRecenteringData { get; private set; }/////
    //[field: SerializeField] public List<PlayerCamerRecentteringData> backwardCameraRecenteringData { get; private set; }
    //[field: SerializeField] public AnimationCurve slopeSpeedAngles { get; private set; }//¶¯»­ÇúÏß
    [field: SerializeField] public RotationData baseRotationData { get; private set; }
    //[field: SerializeField] public PlayerIdleData idleData { get; private set; }
    [field: SerializeField] public WalkData walkData { get; private set; }
    [field: SerializeField] public RunData runData { get; private set; }
    [field: SerializeField] public StopData stopData { get; private set; }
}
