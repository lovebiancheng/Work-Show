using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.XR.OpenVR;
using UnityEngine;
using UnityEngine.UI;

public class FiveStarRedFlag : Graphic
{
    [field: SerializeField] public float Length = 300f;
    [field: SerializeField] public float together = 0f;
    [field: SerializeField] public int Radius = 20;
    private Vector3 center;
    private List<Vector2> points1;
    private List<Vector2> points2;
    protected override void OnPopulateMesh(VertexHelper vh)
    {
        base.OnPopulateMesh(vh);
        
        vh.Clear();
        
        DrawSqure(vh);
        
    }
    private void DrawSqure(VertexHelper vh)
    {
        float length = Length;
        float width = length / 3f * 2;
        Vector2 first = new Vector2(length / 2, width / 2);
        Vector2 second = new Vector2(-length / 2, width / 2);
        Vector2 third = new Vector2(-length / 2, -width / 2);
        Vector2 fourth = new Vector2(length / 2, -width / 2);
        Fill(vh, first, second, third, UnityEngine.Color.red);
        Fill(vh, first, fourth, third, UnityEngine.Color.red);
    }
    
    private void Fill(VertexHelper vh, Vector2 first, Vector2 second, Vector2 three,UnityEngine.Color temp)
    {
        vh.AddVert(first, temp, Vector2.zero);
        vh.AddVert(second, temp, Vector2.zero);
        vh.AddVert(three, temp, Vector2.zero);

        int startIndex = vh.currentVertCount - 3;
        vh.AddTriangle(startIndex, startIndex + 1, startIndex + 2);
    }
    private void Caculate(int radius,float rotation)
    {
        center= Vector2.zero;
        points1 = new List<Vector2>();
        points2 = new List<Vector2>();
        float single = Mathf.PI / 180;
        float everysingle = 2 * Mathf.PI / 5;
        float start1Single = 18 * single+rotation;
        for (int i = 0; i < 5; i++)
        {
            float x = center.x + radius * Mathf.Cos(start1Single);
            float y = center.x + radius * Mathf.Sin(start1Single);
            points1.Add(new Vector2(x, y));
            start1Single += everysingle;
        }
        float innerRadius = (radius * Mathf.Sin(18 * single)) / Mathf.Cos(36 * single);
        float start2Single = 54 * single+rotation;
        for(int i=0;i<5;i++)
        {
            float x = center.x + innerRadius * Mathf.Cos(start2Single);
            float y=center.x+innerRadius * Mathf.Sin(start2Single);
            points2.Add(new Vector2(x,y));
            start2Single += everysingle;
        }
    }
    
}
