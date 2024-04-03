using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class Utilities
{
    public static Vector3 WorldToCanvasPosition(Canvas canvas, Vector3 worldPosition)
    {
        // Calculate the position of the world position on the canvas
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(worldPosition);
        Vector3 canvasSize = canvas.GetComponent<RectTransform>().sizeDelta;

        // Convert the viewport position to be relative to the canvas
        return new Vector3((viewportPosition.x * canvasSize.x) - (canvasSize.x * 0.5f),
                        (viewportPosition.y * canvasSize.y) - (canvasSize.y * 0.5f),
                        0);
    }
    

    
}