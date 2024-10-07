using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class FallData 
{
    [field: SerializeField][field: Range(1f, 15f)] public float FallSpeedLimt { get; private set; } = 15f;
    [field: SerializeField][field: Range(0f, 100f)] public float MinimumDistanceToBeConsideredHardFall { get; private set; } = 3f;
}
