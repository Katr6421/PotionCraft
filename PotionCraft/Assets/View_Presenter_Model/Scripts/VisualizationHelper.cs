using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TreeEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Splines;

public class VisualizationHelper : MonoBehaviour
{
    [SerializeField] private  NullCircleSpawner _nullCircleSpawner;
    [SerializeField] private  TreeManager _treeManager;
    [SerializeField] private Spline _spline;


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
        //Debug.Log("Calling MoveNodeAndAllDescendants");
        NullCircle nullCircle = nodeToMove.GetComponent<NullCircle>();

        // Recursively move the left subtree if it exists.
        if (nullCircle.LeftChild.GetComponent<NullCircle>().Ingredient != null) {
            //Debug.Log("Jeg har et venstre barn");
            GameObject leftChild = nullCircle.LeftChild;
            //Debug.Log("Leftchild index: " + leftChild.GetComponent<NullCircle>().Index);
            Vector3 newLeftChildPosition = leftChild.GetComponent<NullCircle>().LeftChild.transform.position;
            //Debug.Log("newleftchildPosistion index:" + leftChild.GetComponent<NullCircle>().LeftChild.GetComponent<NullCircle>().Index);

            //Vector3 leftChildNewPosition = newPosition - (nodeToMove.transform.position - leftChild.transform.position);
            //leftChild.transform.position
            yield return StartCoroutine(MoveNodeAndAllDescendants(leftChild, newLeftChildPosition, duration, () => {}));
        }

        // Recursively move the right subtree if it exists.
        if (nullCircle.RightChild.GetComponent<NullCircle>().Ingredient != null) {
            GameObject rightChild = nullCircle.RightChild;
            Vector3 newRightChildPosition = rightChild.GetComponent<NullCircle>().RightChild.transform.position;
            //Vector3 rightChildNewPosition = newPosition - (nodeToMove.transform.position - rightChild.transform.position);
            // SHOULD MAYBE BE A DIFFERENT METHOD
            yield return StartCoroutine(MoveNodeAndAllDescendants(rightChild, newRightChildPosition, duration, () => {}));
        }


        // If the node has an ingredient, move it.
        if (nullCircle.Ingredient != null) {
            yield return StartCoroutine(MoveNode(nullCircle.Ingredient, newPosition, duration, nullCircle, () => {
                _nullCircleSpawner.DeactivateAllNullCirclesInSubtree(nullCircle);
                UpdateNullCircleWithIngredient(newPosition, nullCircle);
            }));
           
        }

        
        onComplete?.Invoke();
    }


////////////////////****************************************************************
  /// CHECK THIS TOMMORROW <summary>
    /// CHECK THIS TOMMORROW////////////////////****************************************************************
    /// ////////////////////****************************************************************
    /// ////////////////////****************************************************************
    /// ////////////////////****************************************************************
    /// ////////////////////****************************************************************
    ///     WHEN WE ROTATE WITH SUBTREES IT GOES WRONG
    /// </summary>
    /// <param name="newPosition"></param>
    /// <param name="nullCircle"></param>  


    public void UpdateNullCircleWithIngredient(Vector3 newPosition, NullCircle nullCircle) {
        //Debug.Log("I am in UpdateNullCircleWithIngredient");
        NullCircle foundNullCircle = _nullCircleSpawner.FindNullCircleBasedOnPosition(newPosition);
        if (foundNullCircle != null)
        {
            //Debug.Log("Found NullCircle with index: " + foundNullCircle.Index + "and updated it with ingredient " + nullCircle.Ingredient.GetComponentInChildren<TextMeshProUGUI>().text);
            // Update the found null circle with its new ingredient
            foundNullCircle.Ingredient = nullCircle.Ingredient;
            // Update the value of the found null circle with the value of the ingredient. 
            foundNullCircle.Value = int.Parse(nullCircle.Ingredient.GetComponentInChildren<TextMeshProUGUI>().text);

            // Update the color based on the nodes in our tree. We look up in our RedBlaackBST to get the node that the ingredients value corresponds to. Then we return the nodes color
            //Debug.Log("Now I am chaning the colors of the foundnullcircle at index" + foundNullCircle.Index);

            //Debug.Log("FoundNullCircleValue " + foundNullCircle.Value + " | FoundNullCircleIndex " + foundNullCircle.Index);
            
            foundNullCircle.IsRed = _treeManager.GetColor(foundNullCircle.Value);            

            // Only set null circle to null if the child is also null. Then we know that there will not be any more ingredients in the subtree.
            if (nullCircle.LeftChild.GetComponent<NullCircle>().Ingredient == null && nullCircle.RightChild.GetComponent<NullCircle>().Ingredient == null ) {
                //Debug.Log("Setting nullcircle ingredient to null" + nullCircle.Index);
                nullCircle.Ingredient = null; // the null circle where the ingredient was earlier now has no ingredient
                nullCircle.Value = 0; // the value of the null circle where the ingredient was earlier now has no value
                nullCircle.IsRed = false; // the color of the null circle where the ingredient was earlier now has no color
            }

            //Debug.Log("Now we update the lines");
            _nullCircleSpawner.UpdateLineRenderers();
        }
        else
        {
            //Debug.Log("NullCircle not found at position: " + newPosition);
        }
        
        
        //yield return null;
        //onComplete?.Invoke();
    }

    public void MoveSubtreeToNewPosition(NullCircle rootOfSubtree, GameObject rootToPlaceSubtree) {
        // flyt root til ny position
        // recursive flyt 
    }
 

}