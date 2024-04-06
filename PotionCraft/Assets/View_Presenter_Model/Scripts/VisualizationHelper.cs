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
    [SerializeField] private LineRendererManager _lineRendererManager;


    public Vector3 WorldToCanvasPosition(Canvas canvas, Vector3 worldPosition)
    {
        /*********************************************
        Calculate the position of the world position on the canvas
        *********************************************/
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(worldPosition);
        Vector3 canvasSize = canvas.GetComponent<RectTransform>().sizeDelta;


        /*********************************************
        Convert the viewport position to be relative to the canvas
        *********************************************/
        return new Vector3((viewportPosition.x * canvasSize.x) - (canvasSize.x * 0.5f),
                        (viewportPosition.y * canvasSize.y) - (canvasSize.y * 0.5f),
                        0);
    }

    
    /*********************************************
    Move an ingredient to a new position
    *********************************************/
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


    public IEnumerator MoveNodeAndAllDescendants(NullCircle nullCircle, Vector3 newPosition, float duration, Action onComplete) {    
        /*********************************************
        Recursively move the left subtree if it exists.
        *********************************************/
        if (nullCircle.LeftChild.GetComponent<NullCircle>().Ingredient != null) {
            NullCircle leftChild = nullCircle.LeftChild.GetComponent<NullCircle>();
            Vector3 newLeftChildPosition = leftChild.GetComponent<NullCircle>().LeftChild.transform.position;
            
            yield return StartCoroutine(MoveNodeAndAllDescendants(leftChild, newLeftChildPosition, duration, () => {}));
        }


        /*********************************************
        Recursively move the right subtree if it exists.
        *********************************************/
        if (nullCircle.RightChild.GetComponent<NullCircle>().Ingredient != null) {
            NullCircle rightChild = nullCircle.RightChild.GetComponent<NullCircle>();
            Vector3 newRightChildPosition = rightChild.GetComponent<NullCircle>().RightChild.transform.position;
            //Vector3 rightChildNewPosition = newPosition - (nodeToMove.transform.position - rightChild.transform.position);
            // SHOULD MAYBE BE A DIFFERENT METHOD
            yield return StartCoroutine(MoveNodeAndAllDescendants(rightChild, newRightChildPosition, duration, () => {}));
        }


        /*********************************************
        If the node has an ingredient, move it.
        *********************************************/
        if (nullCircle.Ingredient != null) {
            yield return StartCoroutine(MoveNode(nullCircle.Ingredient, newPosition, duration, nullCircle, () => {
                _nullCircleSpawner.DeactivateAllNullCirclesInSubtree(nullCircle);
                UpdateNullCircleWithIngredient(newPosition, nullCircle);
            }));
           
        }
        onComplete?.Invoke();
    }


    public void UpdateNullCircleWithIngredient(Vector3 newPosition, NullCircle nullCircle) {
        NullCircle foundNullCircle = _nullCircleSpawner.FindNullCircleBasedOnPosition(newPosition);
        if (foundNullCircle != null)
        {
            /*********************************************
            Update the found null circle with its new ingredient
            *********************************************/
            foundNullCircle.Ingredient = nullCircle.Ingredient;

            /*********************************************
            Update the value of the found null circle with the value of the ingredient. 
            *********************************************/
            foundNullCircle.Value = int.Parse(nullCircle.Ingredient.GetComponentInChildren<TextMeshProUGUI>().text);


            /*********************************************
            Update the color based on the nodes in our tree.
            We look up in our RedBlaackBST to get the node that the ingredients value corresponds to.
            Then we return the node's color
            *********************************************/
            foundNullCircle.IsRed = _treeManager.GetColor(foundNullCircle.Value);       


            /*********************************************
            Only set null circle to null if the child is also null.
            Then we know that there will not be any more ingredients in the subtree.
            *********************************************/
            if (nullCircle.LeftChild.GetComponent<NullCircle>().Ingredient == null && nullCircle.RightChild.GetComponent<NullCircle>().Ingredient == null ) {
                nullCircle.Ingredient = null; // the null circle where the ingredient was earlier now has no ingredient
                nullCircle.Value = 0; // the value of the null circle where the ingredient was earlier now has no value
                nullCircle.IsRed = false; // the color of the null circle where the ingredient was earlier now has no color
            }

            _lineRendererManager.UpdateLineRenderers();
        }
        else
        {
            //Debug.Log("NullCircle not found at position: " + newPosition);
        }
    }

   

    public IEnumerator RotateTree(bool isLeft, bool isDown, GameObject startingNullCircle, GameObject EndingNullCircle, float duration, Action onComplete)
    {
        NullCircle startNullCircle = startingNullCircle.GetComponent<NullCircle>();
        GameObject childNullCircle = isLeft ? startNullCircle.LeftChild
                                            : startNullCircle.RightChild;

        if (isDown)
        {
            /*********************************************
            Move down by the defined (left-right) subtree
            *********************************************/
            if (childNullCircle.GetComponent<NullCircle>().Ingredient != null)
            {
                GameObject newChildPosition = isLeft ? childNullCircle.GetComponent<NullCircle>().LeftChild
                                                     : childNullCircle.GetComponent<NullCircle>().RightChild;

                yield return StartCoroutine(MoveSubTree(isLeft, isDown, childNullCircle, newChildPosition, duration, () => { }));
            }

            /*********************************************
            Move down the starting node itself
            *********************************************/
            yield return StartCoroutine(MoveNode(startNullCircle.Ingredient, EndingNullCircle.transform.position, duration, startNullCircle, () =>
            {
                _nullCircleSpawner.DeactivateAllNullCirclesInSubtree(startNullCircle);
                UpdateNullCircleWithIngredient(EndingNullCircle.transform.position, startNullCircle);
            }));
            onComplete?.Invoke();

        }
        else
        {
            /*********************************************
            Move up the starting node itself
            *********************************************/
            yield return StartCoroutine(MoveNode(startNullCircle.Ingredient, EndingNullCircle.transform.position, duration, startNullCircle, () =>
            {
                _nullCircleSpawner.DeactivateAllNullCirclesInSubtree(startNullCircle);
                UpdateNullCircleWithIngredient(EndingNullCircle.transform.position, startNullCircle);
            }));


            /*********************************************
            Move up by the defined (left-right) subtree
            *********************************************/
            if (childNullCircle.GetComponent<NullCircle>().Ingredient != null)
            {
                GameObject newChildPosition = startNullCircle.gameObject;
                yield return StartCoroutine(MoveSubTree(isLeft, isDown, childNullCircle, newChildPosition, duration, () => { }));
            }
            onComplete?.Invoke();
        }

        onComplete?.Invoke();
    }



    private IEnumerator MoveSubTree(bool isLeft, bool isDown, GameObject nodeToMove, GameObject EndingNullCircle, float duration, Action onComplete)
    {
        NullCircle nullCircle = nodeToMove.GetComponent<NullCircle>();
        GameObject childNullCircle = isLeft ? nullCircle.LeftChild
                                            : nullCircle.RightChild;

        if (isDown)
        {
            /*********************************************
            Check if the node has a left/right child and move it along with its subtree
            *********************************************/
            yield return StartCoroutine(MoveSubSubTree(isLeft, isDown, nullCircle, duration, () => { }));


            /*********************************************
            Go recursively through the left/right subtree
            *********************************************/
            if (childNullCircle.GetComponent<NullCircle>().Ingredient != null)
            {
                GameObject newChildPosition = isLeft ? childNullCircle.GetComponent<NullCircle>().LeftChild
                                                    : childNullCircle.GetComponent<NullCircle>().RightChild;

                yield return StartCoroutine(MoveSubTree(isLeft, isDown, childNullCircle, newChildPosition, duration, () =>
                {}));
            }

            /*********************************************
            Move the node down
            *********************************************/
            yield return StartCoroutine(MoveNode(nullCircle.Ingredient, EndingNullCircle.transform.position, duration, nullCircle, () =>
            {
                _nullCircleSpawner.DeactivateAllNullCirclesInSubtree(nullCircle);
                UpdateNullCircleWithIngredient(EndingNullCircle.transform.position, nullCircle);
            }));

            onComplete?.Invoke();
        }

        else
        {
            /*********************************************
            Check if the node has a left/right child and move it along with its subtree
            *********************************************/
            yield return StartCoroutine(MoveSubSubTree(isLeft, isDown, nullCircle, duration, () => { }));


            /*********************************************
            Move the node up
            *********************************************/
            yield return StartCoroutine(MoveNode(nullCircle.Ingredient, EndingNullCircle.transform.position, duration, nullCircle, () =>
            {
                _nullCircleSpawner.DeactivateAllNullCirclesInSubtree(nullCircle);
                UpdateNullCircleWithIngredient(EndingNullCircle.transform.position, nullCircle);
            }));


            /*********************************************
            Go recursively through the left/right subtree
            *********************************************/
            if (childNullCircle.GetComponent<NullCircle>().Ingredient != null)
            {
                GameObject newChildPosition = nodeToMove;
                yield return StartCoroutine(MoveSubTree(isLeft, isDown, childNullCircle, newChildPosition, duration, () =>
                {}));
            }
            onComplete?.Invoke();
        }
        onComplete?.Invoke();
    }


    private IEnumerator MoveSubSubTree(bool isLeft, bool isDown, NullCircle nodeToMove, float duration, Action onComplete)
    {
        GameObject childNullCircle = isLeft ? nodeToMove.RightChild
                                            : nodeToMove.LeftChild;


        if (childNullCircle.GetComponent<NullCircle>().Ingredient != null)
        {
            GameObject rootOfSubtree = childNullCircle;
            
            /*********************************************
            Copy the subtree of the right child.
            *********************************************/
            NullCircle copyRootOfSubTree = _nullCircleSpawner.CopyNullCircleSubtree(rootOfSubtree.GetComponent<NullCircle>());

            /*********************************************
            Find the position of the root where we want to place the copied subtree.
            *********************************************/
            GameObject rootToPlaceSubtree = GetRootOfSubtreeToPlace(isLeft, isDown, nodeToMove);


            yield return StartCoroutine(MoveNodeAndAllDescendants(copyRootOfSubTree, rootToPlaceSubtree.transform.position, duration, () =>
            {
                _nullCircleSpawner.setNullCircleToDefault(rootOfSubtree.GetComponent<NullCircle>());
                _lineRendererManager.UpdateLineRenderers();
                _nullCircleSpawner.destroyNullCircleAndAllDescendants(copyRootOfSubTree.gameObject);
            }));
        }
        onComplete?.Invoke();
    }


    private GameObject GetRootOfSubtreeToPlace(bool isLeft, bool isDown, NullCircle StartingNullCircle)
    {
        GameObject resultNode;

        if (isLeft)
        {
            resultNode = isDown ? StartingNullCircle.LeftChild.GetComponent<NullCircle>().RightChild
                                : StartingNullCircle.Parent.GetComponent<NullCircle>().RightChild;
        }
        else
        {
            resultNode = isDown ? StartingNullCircle.RightChild.GetComponent<NullCircle>().LeftChild
                                : StartingNullCircle.Parent.GetComponent<NullCircle>().LeftChild;
        }

        return resultNode.gameObject;
    }

}
 

