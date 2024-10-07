using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Star : Graphic
{
    [field: SerializeField] public int Radius = 10;
    [field: SerializeField] public float together = 0f;
    private Vector3 center;
    private List<Vector2> points1;
    private List<Vector2> points2;
    protected override void OnPopulateMesh(VertexHelper vh)
    {
        base.OnPopulateMesh(vh);
        vh.Clear();

        Caculate(Radius, together);
        DrawStar(vh);
    }
    private void DrawStar(VertexHelper vh)
    {

        for (int i = 0; i < points1.Count; i++)
        {
            Fill(vh, points1[i], points2[i], center, UnityEngine.Color.yellow);
            if (i < points1.Count - 1)
            {
                Fill(vh, points1[i + 1], points2[i], center, UnityEngine.Color.yellow);
            }
        }
        Fill(vh, points1[0], points2[points2.Count - 1], center, UnityEngine.Color.yellow);
        for (int i = 0; i < points2.Count - 1; i++)
        {

        }

    }

    private void Fill(VertexHelper vh, Vector2 first, Vector2 second, Vector2 three, UnityEngine.Color temp)
    {
        vh.AddVert(first, temp, Vector2.zero);
        vh.AddVert(second, temp, Vector2.zero);
        vh.AddVert(three, temp, Vector2.zero);

        int startIndex = vh.currentVertCount - 3;
        vh.AddTriangle(startIndex, startIndex + 1, startIndex + 2);
    }
    private void Caculate(int radius, float rotation)
    {
        center = Vector2.zero;
        points1 = new List<Vector2>();
        points2 = new List<Vector2>();
        float single = Mathf.PI / 180;
        float everysingle = 2 * Mathf.PI / 5;
        float start1Single = 18 * single + rotation;
        for (int i = 0; i < 5; i++)
        {
            float x = center.x + radius * Mathf.Cos(start1Single);
            float y = center.x + radius * Mathf.Sin(start1Single);
            points1.Add(new Vector2(x, y));
            start1Single += everysingle;
        }
        float innerRadius = (radius * Mathf.Sin(18 * single)) / Mathf.Cos(36 * single);
        float start2Single = 54 * single + rotation;
        for (int i = 0; i < 5; i++)
        {
            float x = center.x + innerRadius * Mathf.Cos(start2Single);
            float y = center.x + innerRadius * Mathf.Sin(start2Single);
            points2.Add(new Vector2(x, y));
            start2Single += everysingle;
        }
    }
}
