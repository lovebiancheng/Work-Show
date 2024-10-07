using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private bool isDrawing=false;
    // Start is called before the first frame update
    void Start()
    {
        CreateNewLine();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            isDrawing = false;
            CreateNewLine();
        }

        if (isDrawing && Input.GetMouseButton(0))
        {
            Vector3 mousePos = GetMouseWorldPosition();
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, mousePos);
        }

        if (Input.GetMouseButtonDown(0))
        {
            isDrawing = true;
        }
    }
    private void CreateNewLine()
    {
        GameObject lineObject = new GameObject("Line");
        lineRenderer = lineObject.AddComponent<LineRenderer>();

        lineRenderer.positionCount = 0;
        lineRenderer.material=new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = Color.blue;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;

    }
    public Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePos=Input.mousePosition;
        mousePos.z=-Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}
