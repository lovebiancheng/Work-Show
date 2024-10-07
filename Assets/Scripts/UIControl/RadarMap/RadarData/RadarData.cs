using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RadarData 
{
    public int sides { get; set; } = 6;
    public Vector2 center { get; set; }
    public float radius { get; set; } = 100f;
    public float linewidth { get; set; } = 2f;
    public int layerNumbers { get; set; } = 5;
    public List<Vector2> points { get; set; }



    public RadarData() 
    {
        points = new List<Vector2>();
        center = new Vector2();
    }

}
