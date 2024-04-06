using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RightRotationVisualization : MonoBehaviour
{
    [SerializeField] private NullCircleSpawner _nullCircleSpawner; 
    [SerializeField] private VisualizationHelper _visualizationHelper;


    public IEnumerator RotateRightAnimation(GameObject leftChild, GameObject parent, GameObject grandparent, NullCircle parentNullCircle) {
        /* 
            Move all ingredients on right side of tree down
            Move grandparent to grandparent.rightChild
            Move parent to grandparent
            Move all ingredients on left side of tree up
        */

        // Find the new positions
        Vector3 grandParentNewPosition = parentNullCircle.GetComponent<NullCircle>().Parent.GetComponent<NullCircle>().RightChild.transform.position;
        Vector3 parentNewPosition = grandparent.transform.position;

        // Find nullcircles
        GameObject grandParentNullCircle = parentNullCircle.GetComponent<NullCircle>().Parent; 

        // Move
        // Move the grandparent and everything on right side of tree
        yield return StartCoroutine(MoveRightSubtreeAndAllDescendants(grandParentNullCircle, grandParentNewPosition, 1.0f, () => {}));
        // Move everything on left side of tree
        yield return StartCoroutine(MoveLeftSubtreeAndAllDescendants(parentNullCircle, parentNewPosition, 1.0f, () => {}));
    }
    

    IEnumerator MoveRightSubtreeAndAllDescendants(GameObject startingNode, Vector3 newPosition, float duration, Action onComplete) {
        NullCircle startingNullCircle = startingNode.GetComponent<NullCircle>();

        // Recursively move the right child and its subtree DOWN if it exists.
        if (startingNullCircle.RightChild.GetComponent<NullCircle>().Ingredient != null) {
            GameObject rightChild = startingNullCircle.RightChild;
            Vector3 newRightChildPosition = rightChild.GetComponent<NullCircle>().RightChild.transform.position;
            
            yield return StartCoroutine(RightRotationMoveRightSubTreeAndAllDescendants(rightChild, newRightChildPosition, duration, ()=>{}));
        }
        
        // After the right subtree has been moved, now move the grandparent itself 
        yield return StartCoroutine(_visualizationHelper.MoveNode(startingNullCircle.Ingredient, newPosition, duration, startingNullCircle, ()=>{
            _nullCircleSpawner.DeactivateAllNullCirclesInSubtree(startingNullCircle);
            _visualizationHelper.UpdateNullCircleWithIngredient(newPosition, startingNullCircle);
        }));
        
        onComplete?.Invoke();
    }

    IEnumerator MoveLeftSubtreeAndAllDescendants(NullCircle startingNode, Vector3 newPosition, float duration, Action onComplete) {
        NullCircle startingNullCircle = startingNode.GetComponent<NullCircle>();

        // First move the parent itself
        yield return StartCoroutine(_visualizationHelper.MoveNode(startingNullCircle.Ingredient, newPosition, duration, startingNullCircle, ()=>{
            _nullCircleSpawner.DeactivateAllNullCirclesInSubtree(startingNullCircle);
            _visualizationHelper.UpdateNullCircleWithIngredient(newPosition, startingNullCircle);
        }));


        // After the parent has been moved, recursively move the left subtree UP if it exists.
        if (startingNullCircle.LeftChild.GetComponent<NullCircle>().Ingredient != null) {
            GameObject leftChild = startingNullCircle.LeftChild;
            Vector3 leftChildNewPosition = startingNode.transform.position;

            yield return StartCoroutine(RightRotationMoveLeftSubTreeAndAllDescendants(leftChild, leftChildNewPosition, duration, ()=>{}));
        }
        
        onComplete?.Invoke();
    }

    // Move right part of tree DOWN
     public IEnumerator RightRotationMoveRightSubTreeAndAllDescendants(GameObject nodeToMove, Vector3 newPosition, float duration, Action onComplete) {
        NullCircle nullCircle = nodeToMove.GetComponent<NullCircle>();

        // Recursively move the right subtree to its right child if it exists.
        if (nullCircle.RightChild.GetComponent<NullCircle>().Ingredient != null)
        {
            GameObject rightChild = nullCircle.RightChild;
            Vector3 newRightChildPosition = rightChild.GetComponent<NullCircle>().RightChild.transform.position;

            yield return StartCoroutine(RightRotationMoveRightSubTreeAndAllDescendants(rightChild, newRightChildPosition, duration, () => { }));
        }

        // Copy and move the left subtree if it exists. 
        if (nullCircle.LeftChild.GetComponent<NullCircle>().Ingredient != null) {
            // Get the left child of the nodeToMove
            NullCircle LeftChild = nullCircle.LeftChild.GetComponent<NullCircle>();

            // Copy the subtree of the left child.
            NullCircle copyRootOfSubTree = _nullCircleSpawner.CopyNullCircleSubtree(LeftChild);

            // Find the posistion of the root where we want to place the copied subtree. I should go to my parents rightchild leftchild
            NullCircle rootToPlaceSubtree = nullCircle.GetComponent<NullCircle>().RightChild.GetComponent<NullCircle>().LeftChild.GetComponent<NullCircle>();

            // Recursively move the copy subtree to rootToPlace.
            yield return StartCoroutine( _visualizationHelper.MoveNodeAndAllDescendants(copyRootOfSubTree, rootToPlaceSubtree.transform.position, 1.0f, ()=>{}));

            // After the move animation, we update the nullCircles to default value. This is after we have copied them
            _nullCircleSpawner.setNullCircleToDefault(LeftChild);

            // Update the line renderers 
            _nullCircleSpawner.UpdateLineRenderers();

            // Destroy the copied nullCircles
            _nullCircleSpawner.destroyNullCircleAndAllDescendants(copyRootOfSubTree.gameObject);
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

    // Move left part of tree UP
    public IEnumerator RightRotationMoveLeftSubTreeAndAllDescendants(GameObject nodeToMove, Vector3 newPosition, float duration, Action onComplete)
    {
        NullCircle nullCircle = nodeToMove.GetComponent<NullCircle>();

        // Move ourself
        if (nullCircle.Ingredient != null)
        {
            yield return StartCoroutine(_visualizationHelper.MoveNode(nullCircle.Ingredient, newPosition, duration, nullCircle, () =>
            {
                _nullCircleSpawner.DeactivateAllNullCirclesInSubtree(nullCircle);
                _visualizationHelper.UpdateNullCircleWithIngredient(newPosition, nullCircle);
            }));
        }
        
        // Copy and move the right subtree if it exists.
        if(nullCircle.RightChild.GetComponent<NullCircle>().Ingredient != null)
        {
            // Get the right child of the nodeToMove
            NullCircle RightChild = nullCircle.RightChild.GetComponent<NullCircle>();

            // Copy the subtree of the right child.
            NullCircle copyRootOfSubTree = _nullCircleSpawner.CopyNullCircleSubtree(RightChild);

            // Find the posistion of the root where we want to place the copied subtree. I should go to my parents parents rightchild
            NullCircle rootToPlaceSubtree = nullCircle.GetComponent<NullCircle>().Parent.GetComponent<NullCircle>().RightChild.GetComponent<NullCircle>();

            // Recursively move the copy subtree to rootToPlace.
            yield return StartCoroutine( _visualizationHelper.MoveNodeAndAllDescendants(copyRootOfSubTree, rootToPlaceSubtree.transform.position, 1.0f, ()=>{}));

            // After the move animation, we update the nullCircles to default value. This is after we have copied them
            _nullCircleSpawner.setNullCircleToDefault(RightChild);

            // Update the line renderers 
            _nullCircleSpawner.UpdateLineRenderers();

            //Destroy the copied nullCircles
            _nullCircleSpawner.destroyNullCircleAndAllDescendants(copyRootOfSubTree.gameObject);
        }
        
        // Recursively move the left subtree to its parent if it exists.
        if (nullCircle.LeftChild.GetComponent<NullCircle>().Ingredient != null)
        {
            GameObject LeftChild = nullCircle.LeftChild;
            Vector3 newLeftChildPosition = nullCircle.transform.position;

            yield return StartCoroutine(RightRotationMoveLeftSubTreeAndAllDescendants(LeftChild, newLeftChildPosition, duration, () => { }));
        }
        
        onComplete?.Invoke();
    }


    

    

    /*******************************************************SKRALDE SPAND************************************************************* */

    // Up --- DOESNT WORK
/**
    public IEnumerator RightRotationMoveLeftSubTreeAndAllDescendants(GameObject nodeToMove, Vector3 newPosition, float duration, Action onComplete)
    {


       
        //_nullCircleSpawner.PrintNullCircles();
        //Debug.Log("Calling MoveNodeAndAllDescendants");
        NullCircle nullCircle = nodeToMove.GetComponent<NullCircle>();

        if (nullCircle.LeftChild == null) {
            Debug.Log("!!!!!!!!!!!!!!!! Leftchild is null !!!!!!!!!!!");
        }

        // Recursively move the left subtree if it exists.
        if (nullCircle.LeftChild.GetComponent<NullCircle>().Ingredient != null)
        {
            Debug.Log("--------- INDE I IF --------");
            Debug.Log("nodeToMode/nullCircle index: " + nullCircle.Index);
            Debug.Log("nullcircle has ingredient:" + nullCircle.Ingredient);
            //Debug.Log("Jeg har et venstre barn");
            Debug.Log("nullcircle.Leftchild index: " + nullCircle.LeftChild.GetComponent<NullCircle>().Index);

            GameObject leftChild = nullCircle.LeftChild;
            //Debug.Log("Leftchild index: " + leftChild.GetComponent<NullCircle>().Index);
            Vector3 newLeftChildPosition = leftChild.GetComponent<NullCircle>().Parent.transform.position;
           
            yield return StartCoroutine(RightRotationMoveLeftSubTreeAndAllDescendants(leftChild, newLeftChildPosition, duration, () => { }));

            
            /*
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
            
            
            
            
            */



/*
        }

        // Recursively move the right subtree if it exists.
        if (nullCircle.RightChild.GetComponent<NullCircle>().Ingredient != null)
        {
            Debug.Log("I " + nullCircle.Index + " have a right child");
            Debug.Log("Right child index: " + nullCircle.RightChild.GetComponent<NullCircle>().Index);
            

            /*********************************************
            Make a copy of all the nullCircles and instantiate them with the ingredients' values
            *********************************************/
            /*
            NullCircle RightChild = nullCircle.RightChild.GetComponent<NullCircle>();

            NullCircle copyRootOfSubTree = _nullCircleSpawner.CopyNullCircleSubtree(RightChild);

            // Our new posistion is our parent parent right left
            NullCircle rootToPlaceSubtree = nullCircle.GetComponent<NullCircle>().Parent.GetComponent<NullCircle>().RightChild.GetComponent<NullCircle>();

            
            yield return StartCoroutine( _visualizationHelper.MoveNode(RightChild.Ingredient, rootToPlaceSubtree.transform.position, 1.0f, nullCircle, ()=>{}));

            /*********************************************
            Update the line renderers after we have updated the nullCircles to their current ingredients
            *********************************************/
            /*
            _nullCircleSpawner.UpdateLineRenderers();

            /*********************************************
            Destroy the copied nullCircles
            *********************************************/
            /*
            _nullCircleSpawner.destroyNullCircleAndAllDescendants(copyRootOfSubTree.gameObject);
*/

/*
        }


        // If the node has an ingredient, move it.
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
*/



}