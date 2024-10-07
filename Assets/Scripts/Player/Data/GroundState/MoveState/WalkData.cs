using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WalkData
{
    [field: SerializeField][field: Range(0f, 1f)] public float NormalSpeedModifier { get; private set; } = 0.225f;
    [field: SerializeField][field: Range(0f, 1f)] public float MatchSpeedModifier { get; private set; } = 0.2f;
    //[field: SerializeField] public List<PlayerCamerRecentteringData> backwardCameraRecenteringData { get; private set; }
}
