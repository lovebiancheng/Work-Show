using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Product
{
    void DrawRadarChart(GameObject gameObject);
    Vector2[] DrawLine(Vector2 start, Vector2 end);
}

