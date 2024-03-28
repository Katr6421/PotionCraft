using System.Collections.Generic;
using UnityEngine;

public class LineSegment {
    public Transform startPoint;
    public Transform endPoint;
}

public class LineController : MonoBehaviour
{
    // Start is called before the first frame update
    private LineRenderer lineRenderer;
    private List<Transform> points = new List<Transform>();

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
       
    }

    // Set points so the linrender knows how many lines it need to draw 

    public void SetUpLine(List<Transform> points)
    {
        Debug.Log("Setting up line!!!!!");
        lineRenderer.positionCount = points.Count;
        this.points = points;
           
    }

    public void Update(){
        Debug.Log("Updating line!!!!!");
        for (int i = 0; i < points.Count; i++)
        {
            lineRenderer.SetPosition(i, points[i].position);
        }
    }
}

