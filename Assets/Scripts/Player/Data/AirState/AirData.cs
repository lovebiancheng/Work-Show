using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class AirData 
{
    [field: SerializeField] public JumpData JumpData { get; private set; }
    [field: SerializeField] public FallData FallData { get; private set; }
}
