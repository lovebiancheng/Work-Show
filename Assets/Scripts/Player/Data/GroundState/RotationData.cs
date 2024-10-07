using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RotationData 
{
    [field: SerializeField] public Vector3 targetRotationReachTime { get; private set; }
}
