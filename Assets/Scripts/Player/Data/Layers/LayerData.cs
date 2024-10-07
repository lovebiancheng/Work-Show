using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LayerData 
{
    [field: SerializeField] public LayerMask groundlayer { get; private set; }

    public bool ContainsLayer(LayerMask layerMask, int layer)
    {
        return (1 << layer & layerMask) != 0;
        //32位掩码，1代表掩码的第一个位置，<<按位移位
        //如果返回0，说明LayerMasks中没有这个layer
    }
    public bool IsGroundLayer(int layer)
    {
        return ContainsLayer(groundlayer, layer);
    }
}
