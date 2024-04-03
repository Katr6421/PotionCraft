using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VisualizationHelper : MonoBehaviour
{
    [SerializeField] private  NullCircleSpawner _nullCircleSpawner;


    public Vector3 WorldToCanvasPosition(Canvas canvas, Vector3 worldPosition)
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
    public IEnumerator MoveNode(GameObject ingredient, Vector3 newPosition, float duration, NullCircle nullCircle, Action onComplete)
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


    public IEnumerator MoveNodeAndAllDescendants(GameObject nodeToMove, Vector3 newPosition, float duration, Action onComplete) {
        NullCircle nullCircle = nodeToMove.GetComponent<NullCircle>();

        // If the node has an ingredient, move it.
        if (nullCircle.Ingredient != null) {
            yield return StartCoroutine(MoveNode(nullCircle.Ingredient, newPosition, duration, nullCircle, () => {
                _nullCircleSpawner.DeactivateAllNullCirclesInSubtree(nullCircle);
                UpdateNullCircleWithIngredient(newPosition, nullCircle);
            }));
        }

        // Recursively move the left subtree if it exists.
        if (nullCircle.LeftChild.GetComponent<NullCircle>().Ingredient != null) {
            GameObject leftChild = nullCircle.LeftChild;
            Vector3 leftChildNewPosition = newPosition - (nodeToMove.transform.position - leftChild.transform.position);
            yield return StartCoroutine(MoveNodeAndAllDescendants(leftChild, leftChildNewPosition, duration, () => {}));
        }

        // Recursively move the right subtree if it exists.
        if (nullCircle.RightChild.GetComponent<NullCircle>().Ingredient != null) {
            GameObject rightChild = nullCircle.RightChild;
            Vector3 rightChildNewPosition = newPosition - (nodeToMove.transform.position - rightChild.transform.position);
            yield return StartCoroutine(MoveNodeAndAllDescendants(rightChild, rightChildNewPosition, duration, () => {}));
        }
        onComplete?.Invoke();
    }

    public void UpdateNullCircleWithIngredient(Vector3 newPosition, NullCircle nullCircle) {
        NullCircle foundNullCircle = _nullCircleSpawner.FindNullCircleBasedOnPosition(newPosition);
        if (foundNullCircle != null)
        {
            //Debug.Log("null" + nullCircle.Index);
            //Debug.Log("Found NullCircle with index: " + foundNullCircle.Index + "and updated it with ingredient" + nullCircle.Ingredient.GetComponentInChildren<TextMeshProUGUI>().text);
            foundNullCircle.Ingredient = nullCircle.Ingredient;
        }
        else
        {
            Debug.Log("NullCircle not found at position: " + newPosition);
        }
        nullCircle.Ingredient = null; // the null circle where the ingredient was earlier now has no ingredient
    }

}