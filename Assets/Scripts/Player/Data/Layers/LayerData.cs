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
        //32λ���룬1��������ĵ�һ��λ�ã�<<��λ��λ
        //�������0��˵��LayerMasks��û�����layer
    }
    public bool IsGroundLayer(int layer)
    {
        return ContainsLayer(groundlayer, layer);
    }
}
