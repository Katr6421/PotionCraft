using System.Collections.Generic;
using UnityEngine;



public class Line : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private LineRenderer _lineRenderer;
    public List<Transform> Points { get; set; } = new List<Transform>();
    public bool IsRed { get; set; } = false;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();

    }

    public void Start() {
        _lineRenderer.material = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply"));
        _lineRenderer.startWidth = 0.05f;
        _lineRenderer.endWidth = 0.05f;
        _lineRenderer.startColor = Color.black;
        _lineRenderer.endColor = Color.black;
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

    public void SetColor(Color color)
    {
        _lineRenderer.startColor = color;
        _lineRenderer.endColor = color;
    }

    public void ChangeEndPoint(Transform newEndPoint)
    {
        Points[Points.Count - 1] = newEndPoint;
    }

}

