using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class SBaseRadarMap : Graphic
{
    private RadarData radarData;
    protected override void OnPopulateMesh(VertexHelper vh)
    {
        base.OnPopulateMesh(vh);
        radarData = new RadarData();
        radarData.center = Vector3.zero;
        
        vh.Clear();
        CalculatePoints();

        for (int j = 0 ; j < radarData.sides; j++)
        {
            Fill(vh, radarData.points[j], radarData.points[j + 1], radarData.center);
        }
        Fill(vh, radarData.points[0], radarData.points[radarData.sides-1], radarData.center);
        for (int j = 0; j < radarData.points.Count-1; j+=radarData.sides)
        {
            int begain = j;
            for (int i = j; i < begain+radarData.sides-1; i++)
            {
                Vector2 start = radarData.points[i];
                Vector2 end = radarData.points[i + 1];
                DrawLine(vh, start, end, radarData.linewidth);
            }
            DrawLine(vh, radarData.points[begain], radarData.points[begain+radarData.sides-1], radarData.linewidth);
        }
        for (int j = 0; j <= radarData.sides-1; j++)
        {
            DrawLine(vh, radarData.points[j], radarData.center, radarData.linewidth);
        }
        

    }

    public  void Creat(VertexHelper vh, float[] array)
    {
        if (array == null)
            return;
        float max = 100;
        float fireBallPercent = array[0] / max;
        float skillPercent = array[1] / max;
        float speedPercent = array[2] / max;
        float defensivePercent = array[3] / max;
        float strengthPercent = array[4] / max;
        float experiencePercent = array[5] / max;
        List<Vector2> arrayList= new List<Vector2>();
        Vector2 fireBall= new Vector2();
        Vector2 skill= new Vector2();
        Vector2 speed= new Vector2();
        Vector2 defense= new Vector2();
        Vector2 strength= new Vector2();
        Vector2 experience= new Vector2();
        if(fireBallPercent > 1f)
        {
            fireBall = radarData.points[0];
        }
        else
        {
            fireBall= radarData.points[0] * fireBallPercent;
        }
        if (skillPercent > 1f)
        {
            skill = radarData.points[1];
        }
        else
        {
            skill = radarData.points[1] * speedPercent;
        }
        if (speedPercent > 1f)
        {
           speed = radarData.points[2];
        }
        else
        {
            speed = radarData.points[2] *speedPercent;
        }
        if (defensivePercent > 1f)
        {
           defense = radarData.points[3];
        }
        else
        {
            defense = radarData.points[3] * defensivePercent;
        }
        if(strengthPercent > 1f)
        {
            strength = radarData.points[4];
        }
        else
        {
            strength= radarData.points[4] * strengthPercent;
        }
        if(experiencePercent > 1f)
        {
            experience = radarData.points[5];
        }
        else
        {
            experience= radarData.points[5] *experiencePercent;
        }
        
        //0 发球 ；1技巧； 2速度；3防守；4 力量；5 经验
    }


    private void DrawLine(VertexHelper vh, Vector2 start, Vector2 end, float width)
    {
        // 计算线段的方向向量，并归一化
        Vector2 direction = (end - start).normalized;
        // 计算垂直于方向向量的向量，并根据线宽进行缩放
        Vector2 perpendicular = new Vector2(-direction.y, direction.x) * width / 2;

        // 计算线段的四个顶点
        Vector3 v0 = start - perpendicular;
        Vector3 v1 = start + perpendicular;
        Vector3 v2 = end + perpendicular;
        Vector3 v3 = end - perpendicular;

        // 将顶点添加到VertexHelper中
        vh.AddVert(v0, color, Vector2.zero);
        vh.AddVert(v1, color, Vector2.zero);
        vh.AddVert(v2, color, Vector2.zero);
        vh.AddVert(v3, color, Vector2.zero);

        // 添加两个三角形，组成一个矩形
        int startIndex = vh.currentVertCount - 4;
        vh.AddTriangle(startIndex, startIndex + 1, startIndex + 2);
        vh.AddTriangle(startIndex, startIndex + 2, startIndex + 3);
    }
    private void Fill(VertexHelper vh,Vector2 first,Vector2 second,Vector2 three)
    {
        vh.AddVert(first,UnityEngine.Color.black, Vector2.zero);
        vh.AddVert(second, UnityEngine.Color.black, Vector2.zero);
        vh.AddVert(three, UnityEngine.Color.black, Vector2.zero);

        int startIndex = vh.currentVertCount - 3;
        vh.AddTriangle(startIndex,startIndex + 1, startIndex+2);
    }


    private void CalculatePoints()
    {
        float layers = radarData.radius / radarData.layerNumbers;
        for (float j = radarData.radius; j >= layers; j -= layers)
        {
            float singleAngle = 2 * Mathf.PI / radarData.sides;
            float startAngle = 0;
            for (int i = 0; i < radarData.sides; i++)
            {
                float x = radarData.center.x + j * Mathf.Cos(startAngle);
                float y = radarData.center.y + j * Mathf.Sin(startAngle);
                radarData.points.Add(new Vector2(x, y));
                startAngle += singleAngle;

            }
        }
        radarData.points.Add(radarData.center);
        
        
    }
}
