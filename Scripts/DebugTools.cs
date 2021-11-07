using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class DebugTools
{

    public static LineRenderer AddLineRenderer(GameObject gameObject)
    {
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.material = new Material(Shader.Find("Diffuse"));
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;

        return lineRenderer;
    }

    public static void DrawLine(LineRenderer lr, Vector3 start, Vector3 end)
    {
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
    }

}