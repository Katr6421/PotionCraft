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
            Move grandparent to grandparent.rightChild
            Move parent to grandparent
            Move leftChild to parent
            Update lines, nullcircles' ingredient, and visibility
        */


        // Calculate the positions based on your tree's structure.
        Vector3 grandParentNewPosition = parentNullCircle.GetComponent<NullCircle>().Parent.GetComponent<NullCircle>().RightChild.transform.position;
        Vector3 parentNewPosition = grandparent.transform.position;
        Vector3 leftChildNewPosition = parent.transform.position;

        // Find nullcircles
        GameObject grandParentNullCircle = parentNullCircle.GetComponent<NullCircle>().Parent; 
        GameObject leftChildNullCircle = parentNullCircle.GetComponent<NullCircle>().LeftChild;

        // Delete if we find something better
        // Change color of nullcircles according to the algorithm
        // Bytter om på x og h
        // h is parent
        // x is grandparent
        /*x.color = h.color;
        h.color = RED;*/
        //grandParentNullCircle.GetComponent<NullCircle>().IsRed = parentNullCircle.GetComponent<NullCircle>().IsRed;
        //parentNullCircle.GetComponent<NullCircle>().IsRed = true;




        //Debug.Log("Grandparent nullcircle index: " + grandParentNullCircle.GetComponent<NullCircle>().Index);
        // Debug.Log("Parent nullcircle: " + parentNullCircle.GetComponent<NullCircle>().Index);
        // Debug.Log("Leftchild nullcircle: " + leftChildNullCircle.GetComponent<NullCircle>().Index);




        // Move the nodes with their subtrees.
        yield return StartCoroutine(MoveRightSubtreeAndAllDescendants(grandParentNullCircle, grandParentNewPosition, 1.0f, () => { 
            //Debug.Log("MoveRightSubtreeAndAllDescendants done");
        }));
        //Debug.Log("Now we move the parent and leftchild");
        yield return StartCoroutine(MoveLeftSubtreeAndAllDescendants(parentNullCircle, parentNewPosition, 1.0f, () => {}));
            
        
        // Debug.Log("MoveNodeWithSubtree done and now we update active null circles");
        
        // Update if nullCircles should be visible or not
        //_nullCircleSpawner.UpdateActiveNullCircles();
        
        

        // Update the lines
    }
    

    IEnumerator MoveRightSubtreeAndAllDescendants(GameObject startingNode, Vector3 newPosition, float duration, Action onComplete) {
        NullCircle startingNullCircle = startingNode.GetComponent<NullCircle>();

        // Move the right child and its subtree if it exists.
        if (startingNullCircle.RightChild.GetComponent<NullCircle>().Ingredient != null) {
            GameObject rightChild = startingNullCircle.RightChild;
            Vector3 rightChildNewPosition = newPosition - (startingNode.transform.position - rightChild.transform.position);
            yield return StartCoroutine(_visualizationHelper.MoveNodeAndAllDescendants(rightChild, rightChildNewPosition, duration, ()=>{
                //Debug.Log("Right subtree has been moved");
                }));
            
        }
        // After the right subtree has been moved, now move the starting node itself if it has an ingredient.
        
        //Debug.Log("Now i am moving myself");
        yield return StartCoroutine(_visualizationHelper.MoveNode(startingNullCircle.Ingredient, newPosition, duration, startingNullCircle, ()=>{
            _nullCircleSpawner.DeactivateAllNullCirclesInSubtree(startingNullCircle);
            //_visualizationHelper.UpdateNullCircleWithIngredient(newPosition, startingNullCircle);
        }));

        yield return StartCoroutine(_visualizationHelper.UpdateNullCircleWithIngredient(newPosition, startingNullCircle, ()=>{}));
        
        onComplete?.Invoke();
    }


    IEnumerator MoveLeftSubtreeAndAllDescendants(NullCircle startingNode, Vector3 newPosition, float duration, Action onComplete) {
        NullCircle startingNullCircle = startingNode.GetComponent<NullCircle>();

        // After the right subtree has been moved, now move the starting node itself if it has an ingredient.
        //Debug.Log("Now i am moving myself");
        yield return StartCoroutine(_visualizationHelper.MoveNode(startingNullCircle.Ingredient, newPosition, duration, startingNullCircle, ()=>{
            _nullCircleSpawner.DeactivateAllNullCirclesInSubtree(startingNullCircle);
            //_visualizationHelper.UpdateNullCircleWithIngredient(newPosition, startingNullCircle);
        }));

        yield return StartCoroutine(_visualizationHelper.UpdateNullCircleWithIngredient(newPosition, startingNullCircle, ()=>{}));

        // Move the left child and its subtree if it exists.
        if (startingNullCircle.LeftChild.GetComponent<NullCircle>().Ingredient != null) {
            GameObject leftChild = startingNullCircle.LeftChild;
            Vector3 leftChildNewPosition = startingNode.transform.position;
            //newPosition - (startingNode.transform.position - leftChild.transform.position);
            yield return StartCoroutine(_visualizationHelper.MoveNodeAndAllDescendants(leftChild, leftChildNewPosition, duration, ()=>{
                //Debug.Log("Left subtree has been moved");
                }));
            
        }
        
        onComplete?.Invoke();
    }

}