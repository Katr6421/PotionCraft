using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeftRotationVisualization : MonoBehaviour {
    [SerializeField] private NullCircleSpawner _nullCircleSpawner;
    [SerializeField] private VisualizationHelper _visualizationHelper;

    public IEnumerator RotateLeftAnimation(GameObject parent, GameObject rightChild, GameObject parentNullCircle){
        /*
            Move parent to leftChild
            Move rightChild to parent
            Update lines, nullcircles and visibility
        */

        // Calculate the positions based on your tree's structure.
        // Calculate the posistion where the parent should be moved to
        Vector3 parentNewPosition = parentNullCircle.GetComponent<NullCircle>().LeftChild.transform.position;
        Vector3 rightChildNewPosition = parentNullCircle.transform.position;


        //Translate our ingredients to nullcircles, such that we can look up on the nullcircles and get the ingredients that are attacted.
        GameObject rightChildNullCircle = parentNullCircle.GetComponent<NullCircle>().RightChild;

        Debug.Log("Parent nullcircle: " + parentNullCircle.GetComponent<NullCircle>().Index);
        Debug.Log("Rightchild nullcircle: " + rightChildNullCircle.GetComponent<NullCircle>().Index);

        // Move the parent and the ueft sbtrees.
        yield return StartCoroutine(MoveLeftSubtreeAndAllDescendants(parentNullCircle, parentNewPosition, 1.0f, () => { 
            Debug.Log("MoveLeftSubtreeAndAllDescendants done");
        }));
        // Move the right child and its subtree.
        Debug.Log("Now we move the right subtree");
        yield return StartCoroutine(MoveRightSubtreeAndAllDescendants(rightChildNullCircle, rightChildNewPosition, 1.0f, () => {}));
        

        Debug.Log("MoveNodeWithSubtree done and now we update active null circles");
        // Update if nullCircles should be visible or not
        _nullCircleSpawner.UpdateActiveNullCircles();


        // Update the lines
        // Idea: Flag the nullcircle with the color and then goo theough all nullcircles and if the have an ingredient draw to lines

    }

    IEnumerator MoveLeftSubtreeAndAllDescendants(GameObject startingNode, Vector3 newPosition, float duration, Action onComplete) {
        NullCircle startingNullCircle = startingNode.GetComponent<NullCircle>();

        // Move the left child and its subtree if it exists.
        if (startingNullCircle.LeftChild.GetComponent<NullCircle>().Ingredient != null) {
            GameObject leftChild = startingNullCircle.LeftChild;
            Vector3 leftChildNewPosition = newPosition - (startingNode.transform.position - leftChild.transform.position);
            yield return StartCoroutine(_visualizationHelper.MoveNodeAndAllDescendants(leftChild, leftChildNewPosition, duration, ()=>{
                Debug.Log("Left subtree has been moved");}));
            
        }

        // After the right subtree has been moved, now move the starting node itself if it has an ingredient.
        Debug.Log("Now i am moving myself");
        yield return StartCoroutine(_visualizationHelper.MoveNode(startingNullCircle.Ingredient, newPosition, duration, startingNullCircle, ()=>{
            _nullCircleSpawner.DeactivateAllNullCirclesInSubtree(startingNullCircle);
            _visualizationHelper.UpdateNullCircleWithIngredient(newPosition, startingNullCircle);
        }));
        
        onComplete?.Invoke();
    }


    IEnumerator MoveRightSubtreeAndAllDescendants(GameObject startingNode, Vector3 newPosition, float duration, Action onComplete) {
        NullCircle startingNullCircle = startingNode.GetComponent<NullCircle>();

        // After the right subtree has been moved, now move the starting node itself if it has an ingredient.
        Debug.Log("Now i am moving myself");
        yield return StartCoroutine(_visualizationHelper.MoveNode(startingNullCircle.Ingredient, newPosition, duration, startingNullCircle, ()=>{
            _nullCircleSpawner.DeactivateAllNullCirclesInSubtree(startingNullCircle);
            _visualizationHelper.UpdateNullCircleWithIngredient(newPosition, startingNullCircle);
        }));

        // Move the right child and its subtree if it exists.
        if (startingNullCircle.RightChild.GetComponent<NullCircle>().Ingredient != null) {
            GameObject rightChild = startingNullCircle.RightChild;
            Vector3 rightChildNewPosition = startingNode.transform.position;
            //newPosition - (startingNode.transform.position - leftChild.transform.position);
            yield return StartCoroutine(_visualizationHelper.MoveNodeAndAllDescendants(rightChild, rightChildNewPosition, duration, ()=>{
                Debug.Log("Right subtree has been moved");}));
            
        }
        
        onComplete?.Invoke();
    }

}