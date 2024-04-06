using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeftRotationVisualization : MonoBehaviour {
    [SerializeField] private NullCircleSpawner _nullCircleSpawner;
    [SerializeField] private VisualizationHelper _visualizationHelper;


    public IEnumerator RotateLeftAnimation(GameObject parent, GameObject rightChild, NullCircle parentNullCircle){
        /*
            Move all ingredients on the left side of the tree down
            Move parent to parent.leftchild
            Move rightchild to parent
            Move all ingredients on the right side of the tree up
        */

        // Find the new positions
        Vector3 parentNewPosition = parentNullCircle.GetComponent<NullCircle>().LeftChild.transform.position;
        Vector3 rightChildNewPosition = parentNullCircle.transform.position;

        // Find nullcircles
        GameObject rightChildNullCircle = parentNullCircle.GetComponent<NullCircle>().RightChild;

        // Move 
        // Move the parent and everything on left side of tree
        yield return StartCoroutine(MoveLeftSubtreeAndAllDescendants(parentNullCircle, parentNewPosition, 1.0f, () => {}));
        // Move everything on left side of tree
        yield return StartCoroutine(MoveRightSubtreeAndAllDescendants(rightChildNullCircle, rightChildNewPosition, 1.0f, () => {}));
    }

    IEnumerator MoveLeftSubtreeAndAllDescendants(NullCircle startingNode, Vector3 newPosition, float duration, Action onComplete) {
        NullCircle startingNullCircle = startingNode.GetComponent<NullCircle>();

        // Recursively move the left child and its subtree DOWN if it exists.
        if (startingNullCircle.LeftChild.GetComponent<NullCircle>().Ingredient != null) {
            GameObject leftChild = startingNullCircle.LeftChild;
            Vector3 newLeftChildPosition = leftChild.GetComponent<NullCircle>().LeftChild.transform.position;
            
            yield return StartCoroutine(LeftRotationMoveLeftSubTreeAndAllDescendants(leftChild, newLeftChildPosition, duration, ()=>{}));
        }

        // After the left subtree has been moved, now move the parent itself
        yield return StartCoroutine(_visualizationHelper.MoveNode(startingNullCircle.Ingredient, newPosition, duration, startingNullCircle, ()=>{
            _nullCircleSpawner.DeactivateAllNullCirclesInSubtree(startingNullCircle);
            _visualizationHelper.UpdateNullCircleWithIngredient(newPosition, startingNullCircle);
        }));
        
        onComplete?.Invoke();
    }


    IEnumerator MoveRightSubtreeAndAllDescendants(GameObject startingNode, Vector3 newPosition, float duration, Action onComplete) {
        NullCircle startingNullCircle = startingNode.GetComponent<NullCircle>();
        
        // First move the right child itself 
        yield return StartCoroutine(_visualizationHelper.MoveNode(startingNullCircle.Ingredient, newPosition, duration, startingNullCircle, ()=>{
            _nullCircleSpawner.DeactivateAllNullCirclesInSubtree(startingNullCircle); 
            _visualizationHelper.UpdateNullCircleWithIngredient(newPosition, startingNullCircle);
        }));
        
        // After the right child has been moved, recursively move the right subtrees UP if they exist.
        if (startingNullCircle.RightChild.GetComponent<NullCircle>().Ingredient != null) {
            GameObject rightChild = startingNullCircle.RightChild;
            Vector3 rightChildNewPosition = startingNode.transform.position;
            yield return StartCoroutine(LeftRotationMoveRightSubTreeAndAllDescendants(rightChild, rightChildNewPosition, duration, () => { }));
        }

        onComplete?.Invoke();
    }


    // Move left part of tree DOWN
     public IEnumerator LeftRotationMoveLeftSubTreeAndAllDescendants(GameObject nodeToMove, Vector3 newPosition, float duration, Action onComplete) {
        NullCircle nullCircle = nodeToMove.GetComponent<NullCircle>();

        // Copy and move the right subtree if it exists. 
        if (nullCircle.RightChild.GetComponent<NullCircle>().Ingredient != null) {
            // Get the right child of the nodeToMove
            NullCircle RightChild = nullCircle.RightChild.GetComponent<NullCircle>();

            // Copy the subtree of the right child.
            NullCircle copyRootOfSubTree = _nullCircleSpawner.CopyNullCircleSubtree(RightChild);

            // Find the posistion of the root where we want to place the copied subtree. I should go to my parents leftchild rightchild
            NullCircle rootToPlaceSubtree = nullCircle.GetComponent<NullCircle>().LeftChild.GetComponent<NullCircle>().RightChild.GetComponent<NullCircle>();

            // Recursively move the copy subtree to rootToPlace.
            yield return StartCoroutine( _visualizationHelper.MoveNodeAndAllDescendants(copyRootOfSubTree, rootToPlaceSubtree.transform.position, 1.0f, ()=>{}));
            
            //After the move animation, we update the nullCircles to default value. This is after we have copied them
            _nullCircleSpawner.setNullCircleToDefault(RightChild);
           
            //Update the line renderers 
            _nullCircleSpawner.UpdateLineRenderers();

            //Destroy the copied nullCircles
            _nullCircleSpawner.destroyNullCircleAndAllDescendants(copyRootOfSubTree.gameObject);
        }

        // Recursively move the left subtree to its left child if it exists.
        if (nullCircle.LeftChild.GetComponent<NullCircle>().Ingredient != null) {
            GameObject leftChild = nullCircle.LeftChild;
            Vector3 newLeftChildPosition = leftChild.GetComponent<NullCircle>().LeftChild.transform.position;
            yield return StartCoroutine(LeftRotationMoveLeftSubTreeAndAllDescendants(leftChild, newLeftChildPosition, duration, () => {}));
        }

        // Move ourself
        if (nullCircle.Ingredient != null) {
            yield return StartCoroutine(_visualizationHelper.MoveNode(nullCircle.Ingredient, newPosition, duration, nullCircle, () => {
                _nullCircleSpawner.DeactivateAllNullCirclesInSubtree(nullCircle);
                _visualizationHelper.UpdateNullCircleWithIngredient(newPosition, nullCircle);
            }));
        }
        
        onComplete?.Invoke();
    }

    // Move right part of tree UP
    public IEnumerator LeftRotationMoveRightSubTreeAndAllDescendants(GameObject nodeToMove, Vector3 newPosition, float duration, Action onComplete)
    {
        NullCircle nullCircle = nodeToMove.GetComponent<NullCircle>();
        
        // Copy and move the left subtree if it exists.
        if (nullCircle.LeftChild.GetComponent<NullCircle>().Ingredient != null)
        { 
            // Get the left child of the nodeToMove
            NullCircle LeftChild = nullCircle.LeftChild.GetComponent<NullCircle>();
            
            // Copy the subtree of the left child.
            NullCircle copyRootOfSubTree = _nullCircleSpawner.CopyNullCircleSubtree(LeftChild);

            // Find the posistion of the root where we want to place the copied subtree. I should go to my parents parents leftchild
            NullCircle rootToPlaceSubtree = nullCircle.GetComponent<NullCircle>().Parent.GetComponent<NullCircle>().LeftChild.GetComponent<NullCircle>();

            // Recursively move the copy subtree to rootToPlace.
            yield return StartCoroutine( _visualizationHelper.MoveNodeAndAllDescendants(copyRootOfSubTree, rootToPlaceSubtree.transform.position, 1.0f, ()=>{}));

            // After the move animation, we update the nullCircles to default value. This is after we have copied them
            _nullCircleSpawner.setNullCircleToDefault(LeftChild);

            // Update the line renderers 
            _nullCircleSpawner.UpdateLineRenderers();

            //Destroy the copied nullCircles
            _nullCircleSpawner.destroyNullCircleAndAllDescendants(copyRootOfSubTree.gameObject);
        }

        // Recursively move the right subtree to its parent if it exists.
        if (nullCircle.RightChild.GetComponent<NullCircle>().Ingredient != null)
        {
            GameObject RightChild = nullCircle.RightChild;
            Vector3 newRightChildPosition = RightChild.GetComponent<NullCircle>().Parent.transform.position;
           
            yield return StartCoroutine(LeftRotationMoveRightSubTreeAndAllDescendants(RightChild, newRightChildPosition, duration, () => { }));
        }

        // Move ourself
        if (nullCircle.Ingredient != null)
        {
            yield return StartCoroutine(_visualizationHelper.MoveNode(nullCircle.Ingredient, newPosition, duration, nullCircle, () =>
            {
                _nullCircleSpawner.DeactivateAllNullCirclesInSubtree(nullCircle);
                _visualizationHelper.UpdateNullCircleWithIngredient(newPosition, nullCircle);
            }));
        }

        onComplete?.Invoke();
    }

}