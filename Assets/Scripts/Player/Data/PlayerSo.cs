using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="PlayerStateMachineData",menuName ="Tools/Player/PlayerStateMachineData")]
public class PlayerSo : ScriptableObject
{
    [field: SerializeField] public GroundData groundedData { get; private set; }
    [field: SerializeField] public AirData airborneData { get; private set; }
}
