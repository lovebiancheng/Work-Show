using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RunData 
{
    [field: SerializeField][field: Range(1f, 2f)] public float NormalSpeedModifier { get; private set; } = 1f;
}
