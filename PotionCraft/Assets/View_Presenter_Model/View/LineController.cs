using System.Collections.Generic;
using UnityEngine;

/*
public class LineSegment
{
    public Transform startPoint;
    public Transform endPoint;
}
*/

public class LineController : MonoBehaviour
{
    // Start is called before the first frame update
    private LineRenderer _lineRenderer;
    public List<Transform> Points { get; set; } = new List<Transform>();

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();

    }

    // Set Points so the linerender knows how many lines it need to draw 
    public void SetUpLine(List<Transform> Points)
    {
        _lineRenderer.positionCount = Points.Count;
        this.Points = Points;
    }

    public void Update()
    {
        for (int i = 0; i < Points.Count; i++)
        {
            _lineRenderer.SetPosition(i, Points[i].position);
        }
    }
}

