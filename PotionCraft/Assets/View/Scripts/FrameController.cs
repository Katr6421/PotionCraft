using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameController : MonoBehaviour
{
    private float minX;
    private float maxX;

    public void Start()
    {
        minX = transform.TransformPoint(GetComponent<RectTransform>().rect.min).x;
        maxX = transform.TransformPoint(GetComponent<RectTransform>().rect.max).x;
    }

    public float PlaceRoot(Canvas canvas)
    {
        return FindMiddleX(canvas, maxX, true);
    }
    
    public float FindMiddleX(Canvas canvas, float parentNodePosX, bool isLeft)
    {
        if (isLeft)
        {
            var x = (parentNodePosX + minX) / 2;
            return Utilities.WorldToCanvasPosition(canvas, new Vector3(x, 0, 0)).x;
        }
        else
        {
            var x = (parentNodePosX + maxX) / 2;
            return Utilities.WorldToCanvasPosition(canvas, new Vector3(x, 0, 0)).x;
        }
    }
}
