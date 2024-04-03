using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    // Move an ingredient to a new position 
    public static IEnumerator MoveNode(GameObject ingredient, Vector3 newPosition, float duration, NullCircle nullCircle, Action onComplete)
    {
        Vector3 startingPos = ingredient.transform.position;
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            ingredient.transform.position = Vector3.Lerp(startingPos, newPosition, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        ingredient.transform.position = newPosition;
        
        yield return null;
        onComplete?.Invoke();
    }

}