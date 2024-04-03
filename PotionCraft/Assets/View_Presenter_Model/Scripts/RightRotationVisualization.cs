using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RightRotationVisualization : MonoBehaviour
{
    [SerializeField] private NullCircleSpawner _nullCircleSpawner; 

    void Start() {
    }

    public IEnumerator RotateRightAnimation(GameObject leftChild, GameObject parent, GameObject grandparent, GameObject parentNullCircle) {
        /* 
            Move grandparent to grandparent.rightChild
            Move parent to grandparent
            Move leftChild to parent
            Update lines, nullcircles' ingredient, and visibility
        */


        // Calculate the positions based on your tree's structure.
        Vector3 grandParentNewPosition = parentNullCircle.GetComponent<NullCircle>().Parent.GetComponent<NullCircle>().RightChild.transform.position;
        Vector3 parentNewPosition = grandparent.transform.position;
        Vector3 leftChildNewPosition = parent.transform.position;

        //Translate our ingredients to nullcircles, such that we can look up on the nullcircles and get the ingredients that are attacted.
        GameObject grandParentNullCircle = parentNullCircle.GetComponent<NullCircle>().Parent;
        GameObject leftChildNullCircle = parentNullCircle.GetComponent<NullCircle>().LeftChild;

        //Debug.Log("Grandparent nullcircle index: " + grandParentNullCircle.GetComponent<NullCircle>().Index);
        Debug.Log("Parent nullcircle: " + parentNullCircle.GetComponent<NullCircle>().Index);
        Debug.Log("Leftchild nullcircle: " + leftChildNullCircle.GetComponent<NullCircle>().Index);




        // Move the nodes with their subtrees.
        yield return StartCoroutine(MoveRightSubtreeAndAllDescendants(grandParentNullCircle, grandParentNewPosition, 1.0f, () => { 
            Debug.Log("MoveRightSubtreeAndAllDescendants done");
        }));
        Debug.Log("Now we move the parent and leftchild");
        yield return StartCoroutine(MoveLeftSubtreeAndAllDescendants(parentNullCircle, parentNewPosition, 1.0f, () => {}));
            
        
        Debug.Log("MoveNodeWithSubtree done and now we update active null circles");
        // Update if nullCircles should be visible or not
        _nullCircleSpawner.UpdateActiveNullCircles();
        
        

        // Update the lines
    }
    

    IEnumerator MoveRightSubtreeAndAllDescendants(GameObject startingNode, Vector3 newPosition, float duration, Action onComplete) {
        NullCircle startingNullCircle = startingNode.GetComponent<NullCircle>();

        // Move the right child and its subtree if it exists.
        if (startingNullCircle.RightChild.GetComponent<NullCircle>().Ingredient != null) {
            GameObject rightChild = startingNullCircle.RightChild;
            Vector3 rightChildNewPosition = newPosition - (startingNode.transform.position - rightChild.transform.position);
            yield return StartCoroutine(MoveNodeAndAllDescendants(rightChild, rightChildNewPosition, duration, ()=>{
                Debug.Log("Right subtree has been moved");}));
            
        }
        // After the right subtree has been moved, now move the starting node itself if it has an ingredient.
        
        Debug.Log("Now i am moving myself");
        yield return StartCoroutine(Utilities.MoveNode(startingNullCircle.Ingredient, newPosition, duration, startingNullCircle, ()=>{
            _nullCircleSpawner.DeactivateAllNullCirclesInSubtree(startingNullCircle);
            UpdateNullCircleWithIngredient(newPosition, startingNullCircle);
        }));
        
        onComplete?.Invoke();
    }


    IEnumerator MoveLeftSubtreeAndAllDescendants(GameObject startingNode, Vector3 newPosition, float duration, Action onComplete) {
        NullCircle startingNullCircle = startingNode.GetComponent<NullCircle>();

        // After the right subtree has been moved, now move the starting node itself if it has an ingredient.
        Debug.Log("Now i am moving myself");
        yield return StartCoroutine(Utilities.MoveNode(startingNullCircle.Ingredient, newPosition, duration, startingNullCircle, ()=>{
            _nullCircleSpawner.DeactivateAllNullCirclesInSubtree(startingNullCircle);
            UpdateNullCircleWithIngredient(newPosition, startingNullCircle);
        }));

        // Move the right child and its subtree if it exists.
        if (startingNullCircle.LeftChild.GetComponent<NullCircle>().Ingredient != null) {
            GameObject leftChild = startingNullCircle.LeftChild;
            Vector3 leftChildNewPosition = startingNode.transform.position;
            //newPosition - (startingNode.transform.position - leftChild.transform.position);
            yield return StartCoroutine(MoveNodeAndAllDescendants(leftChild, leftChildNewPosition, duration, ()=>{
                Debug.Log("Left subtree has been moved");}));
            
        }
        
        onComplete?.Invoke();
    }


    IEnumerator MoveNodeAndAllDescendants(GameObject nodeToMove, Vector3 newPosition, float duration, Action onComplete) {
        NullCircle nullCircle = nodeToMove.GetComponent<NullCircle>();

        // If the node has an ingredient, move it.
        if (nullCircle.Ingredient != null) {
            yield return StartCoroutine(Utilities.MoveNode(nullCircle.Ingredient, newPosition, duration, nullCircle, () => {
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

    // Find the nullCircle at the given position and update its ingredient.
    public void UpdateNullCircleWithIngredient(Vector3 newPosition, NullCircle nullCircle) {
        NullCircle foundNullCircle = _nullCircleSpawner.FindNullCircleBasedOnPosition(newPosition);
        if (foundNullCircle != null)
        {
            Debug.Log("null" + nullCircle.Index);
            Debug.Log("Found NullCircle with index: " + foundNullCircle.Index + "and updated it with ingredient" + nullCircle.Ingredient.GetComponentInChildren<TextMeshProUGUI>().text);
            foundNullCircle.Ingredient = nullCircle.Ingredient;
        }
        else
        {
            Debug.Log("NullCircle not found at position: " + newPosition);
        }
        nullCircle.Ingredient = null; // the null circle where the ingredient was earlier now has no ingredient
    }
}