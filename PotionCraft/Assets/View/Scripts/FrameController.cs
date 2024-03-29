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

    public Vector3 PlaceRoot(Canvas canvas)
    {
        var x = (minX + maxX) / 2;
        Vector3 v = Utilities.WorldToCanvasPosition(canvas, new Vector3(x, 0, 0));
        return new Vector3(v.x, 239, 0);
    }
    
    public Vector3 FindNewPos(Canvas canvas, GameObject parentNodePos, bool isLeft)
    {
        //Debug.Log("frame coord " + transform.position);
        //Debug.Log("parent coord " +parentNodePos.transform.position);
        //Debug.Log("minX og maxX " + minX + " " + maxX);
        if (isLeft)
        {
            var x = (parentNodePos.transform.position.x + minX) / 2;
            var y = parentNodePos.transform.position.y - 1;
            //Debug.Log("nye coord " + x + " " + y);
            return Utilities.WorldToCanvasPosition(canvas, new Vector3(x, y, 0));
        }
        else
        {
            var x = (parentNodePos.transform.position.x + maxX) / 2;
            var y = parentNodePos.transform.position.y - 1;
            return Utilities.WorldToCanvasPosition(canvas, new Vector3(x, y, 0));
        }
    }
}
