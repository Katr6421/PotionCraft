using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    // Start is called before the first frame update
    private LineRenderer lineRenderer;
    private List<Transform> points;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        points = new List<Transform>();
    }

    // Set points so the linrender knows how many lines it need to draw 

    public void SetUpLine(List<Transform> points)
    {
        lineRenderer.positionCount = points.Count;
        this.points = points;
    }

    public void Update(){
        for (int i = 0; i < points.Count; i++)
        {
            lineRenderer.SetPosition(i, points[i].position);
        }
    }
}
