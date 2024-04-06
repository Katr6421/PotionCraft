using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeftRotationVisualization : MonoBehaviour {
    [SerializeField] private NullCircleSpawner _nullCircleSpawner;
    [SerializeField] private VisualizationHelper _visualizationHelper;


    public void Start() {
    //    _test = findObjectOfType<test>();
    }

    public IEnumerator RotateLeftAnimation(GameObject parent, GameObject rightChild, NullCircle parentNullCircle){
        /*
            Move parent to leftChild
            Move rightChild to parent
            Update lines, nullcircles and visibility
        */

        // Calculate the positions based on your tree's structure.
        // Calculate the posistion where the parent should be moved to
        //Debug.Log("????***************!!!!!!!!!!!!!!!!!!!!!!!!!!! Given Parent nullcircle index !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!***********???" + parentNullCircle.GetComponent<NullCircle>().Index);
        //Debug.Log("Rightchild nullcircle index: " + parentNullCircle.GetComponent<NullCircle>().RightChild.GetComponent<NullCircle>().Index);

        Vector3 parentNewPosition = parentNullCircle.GetComponent<NullCircle>().LeftChild.transform.position;
        Vector3 rightChildNewPosition = parentNullCircle.transform.position;

        // Find nullcircles
        GameObject rightChildNullCircle = parentNullCircle.GetComponent<NullCircle>().RightChild;

        // Delete if we find something better
        // Change color of nullcircles according to the algorithm
        //x.color = h.color;
        //h.color = RED;
        // h is parent
        // x is right child
        //rightChildNullCircle.GetComponent<NullCircle>().IsRed = parentNullCircle.GetComponent<NullCircle>().IsRed;
        //parentNullCircle.GetComponent<NullCircle>().IsRed = true;

        //Debug.Log("Parent nullcircle: " + parentNullCircle.GetComponent<NullCircle>().Index);
        //Debug.Log("Rightchild nullcircle: " + rightChildNullCircle.GetComponent<NullCircle>().Index);

        // Move the parent and the left sbtrees.
        yield return StartCoroutine(MoveLeftSubtreeAndAllDescendants(parentNullCircle, parentNewPosition, 1.0f, () => { 
            //Debug.Log("MoveLeftSubtreeAndAllDescendants done");
        }));
        // Move the right child and its subtree.
        //Debug.Log("Now we move the right subtree");
        yield return StartCoroutine(MoveRightSubtreeAndAllDescendants(rightChildNullCircle, rightChildNewPosition, 1.0f, () => {}));
        


        //Debug.Log("MoveNodeWithSubtree done and now we update active null circles");
        
        
        // Update if nullCircles should be visible or not
        //_nullCircleSpawner.UpdateActiveNullCircles();


        // Update the lines
        // Idea: Flag the nullcircle with the color and then goo theough all nullcircles and if the have an ingredient draw to lines

    }

    IEnumerator MoveLeftSubtreeAndAllDescendants(NullCircle startingNode, Vector3 newPosition, float duration, Action onComplete) {
        NullCircle startingNullCircle = startingNode.GetComponent<NullCircle>();

        // Move the left child and its subtree if it exists.
        // Check if My left child has an ingredient
        if (startingNullCircle.LeftChild.GetComponent<NullCircle>().Ingredient != null) {
            
            //Debug.Log("MoveLeftSubtreeAndAllDescendants: ");
            GameObject leftChild = startingNullCircle.LeftChild;
            //Debug.Log("Leftchild index: " + leftChild.GetComponent<NullCircle>().Index);
            Vector3 newLeftChildPosition = leftChild.GetComponent<NullCircle>().LeftChild.transform.position;
            


            // RightRotationMoveLeftSubTreeAndAllDescendants down!!!!!!!!
            yield return StartCoroutine(LeftRotationMoveLeftSubTreeAndAllDescendants(leftChild, newLeftChildPosition, duration, ()=>{
                //Debug.Log("Left subtree has been moved");
                }));
        }

        // After the right subtree has been moved, now move the starting node itself if it has an ingredient.
        //Debug.Log("Now i am moving myself");
        yield return StartCoroutine(_visualizationHelper.MoveNode(startingNullCircle.Ingredient, newPosition, duration, startingNullCircle, ()=>{
            _nullCircleSpawner.DeactivateAllNullCirclesInSubtree(startingNullCircle);
            _visualizationHelper.UpdateNullCircleWithIngredient(newPosition, startingNullCircle);
        }));
        
        
        onComplete?.Invoke();
    }


// UP
    IEnumerator MoveRightSubtreeAndAllDescendants(GameObject startingNode, Vector3 newPosition, float duration, Action onComplete) {
        NullCircle startingNullCircle = startingNode.GetComponent<NullCircle>();

        //Debug.Log("!!!!!!!!!!!!!!!!!!**********************NULLCIRCLETREE BEFORE MOVING MY SELF SUBTREE:*************************!!!!!!!!!!!!");
        //_nullCircleSpawner.PrintNullCircles();

        // After the right subtree has been moved, now move the starting node itself if it has an ingredient.
        //Debug.Log("Now i am moving myself");
        yield return StartCoroutine(_visualizationHelper.MoveNode(startingNullCircle.Ingredient, newPosition, duration, startingNullCircle, ()=>{
            _nullCircleSpawner.DeactivateAllNullCirclesInSubtree(startingNullCircle); 
             //Debug.Log("************!!!!!!!!!!!!!!NULLCIRCLETREE AFTER MOVING MY SELF SUBTREE:************!!!!!!!!");
            //_nullCircleSpawner.PrintNullCircles();
            _visualizationHelper.UpdateNullCircleWithIngredient(newPosition, startingNullCircle);
        }));
        

        // Move the right child and its subtree if it exists.
        if (startingNullCircle.RightChild.GetComponent<NullCircle>().Ingredient != null) {
            //Debug.Log("My right child has an ingredient");
            GameObject rightChild = startingNullCircle.RightChild;
            Vector3 rightChildNewPosition = startingNode.transform.position;
            yield return StartCoroutine(LeftRotationMoveRightSubTreeAndAllDescendants(rightChild, rightChildNewPosition, duration, ()=>{
                //Debug.Log("Right subtree has been moved");
                }));
            
        }
        
        onComplete?.Invoke();
    }


    /********************************************************************************************************************/

    // Down
     public IEnumerator LeftRotationMoveLeftSubTreeAndAllDescendants(GameObject nodeToMove, Vector3 newPosition, float duration, Action onComplete) {
        //Debug.Log("Calling MoveNodeAndAllDescendants");
        NullCircle nullCircle = nodeToMove.GetComponent<NullCircle>();

        // Recursively move the left subtree if it exists.
        if (nullCircle.RightChild.GetComponent<NullCircle>().Ingredient != null) {
            Debug.Log("I " + nullCircle.Index + " have a right child");
            Debug.Log("Right child index: " + nullCircle.RightChild.GetComponent<NullCircle>().Index);
            

            /*********************************************
            Make a copy of all the nullCircles and instantiate them with the ingredients' values
            *********************************************/
            NullCircle RightChild = nullCircle.RightChild.GetComponent<NullCircle>();

            NullCircle copyRootOfSubTree = _nullCircleSpawner.CopyNullCircleSubtree(RightChild);

            // left right
            NullCircle rootToPlaceSubtree = nullCircle.GetComponent<NullCircle>().LeftChild.GetComponent<NullCircle>().RightChild.GetComponent<NullCircle>();

            //Måske ikke nullcircle her
              yield return StartCoroutine( _visualizationHelper.MoveNodeAndAllDescendants(copyRootOfSubTree, rootToPlaceSubtree.transform.position, 1.0f, ()=>{}));
            //yield return StartCoroutine( _visualizationHelper.MoveNode(RightChild.Ingredient, rootToPlaceSubtree.transform.position, 1.0f, nullCircle, ()=>{}));

            /*********************************************
            Update the nullCircles to default value after we have copied them
            *********************************************/
            Debug.Log("Now resetting the nullcircles to default value at the subtree i just moved and coty");
            _nullCircleSpawner.setNullCircleToDefault(RightChild);

            /*********************************************
            Update the line renderers after we have updated the nullCircles to their current ingredients
            *********************************************/
            _nullCircleSpawner.UpdateLineRenderers();

            /*********************************************
            Destroy the copied nullCircles
            *********************************************/
            _nullCircleSpawner.destroyNullCircleAndAllDescendants(copyRootOfSubTree.gameObject);

        }

        // Recursively move the left subtree if it exists.
        if (nullCircle.LeftChild.GetComponent<NullCircle>().Ingredient != null) {
            GameObject leftChild = nullCircle.LeftChild;
            Vector3 newLeftChildPosition = leftChild.GetComponent<NullCircle>().LeftChild.transform.position;
            //Vector3 rightChildNewPosition = newPosition - (nodeToMove.transform.position - rightChild.transform.position);
            // SHOULD MAYBE BE A DIFFERENT METHOD
            yield return StartCoroutine(LeftRotationMoveLeftSubTreeAndAllDescendants(leftChild, newLeftChildPosition, duration, () => {}));
        }


        // If the node has an ingredient, move it.
        if (nullCircle.Ingredient != null) {
            yield return StartCoroutine(_visualizationHelper.MoveNode(nullCircle.Ingredient, newPosition, duration, nullCircle, () => {
                _nullCircleSpawner.DeactivateAllNullCirclesInSubtree(nullCircle);
                _visualizationHelper.UpdateNullCircleWithIngredient(newPosition, nullCircle);
            }));
           
        }

        
        onComplete?.Invoke();
    }

    // up
    public IEnumerator LeftRotationMoveRightSubTreeAndAllDescendants(GameObject nodeToMove, Vector3 newPosition, float duration, Action onComplete)
    {
        //Debug.Log("Calling MoveNodeAndAllDescendants");
        NullCircle nullCircle = nodeToMove.GetComponent<NullCircle>();

        // Recursively move the left subtree if it exists.
        if (nullCircle.LeftChild.GetComponent<NullCircle>().Ingredient != null)
        {
                
            Debug.Log("I " + nullCircle.Index + " have a right child");
            Debug.Log("Leftchild child index: " + nullCircle.LeftChild.GetComponent<NullCircle>().Index);
            

            /*********************************************
            Make a copy of all the nullCircles and instantiate them with the ingredients' values
            *********************************************/
            NullCircle LeftChild = nullCircle.LeftChild.GetComponent<NullCircle>();

            NullCircle copyRootOfSubTree = _nullCircleSpawner.CopyNullCircleSubtree(LeftChild);

            // I should go to my parents left
            NullCircle rootToPlaceSubtree = nullCircle.GetComponent<NullCircle>().Parent.GetComponent<NullCircle>().LeftChild.GetComponent<NullCircle>();

            
             yield return StartCoroutine( _visualizationHelper.MoveNodeAndAllDescendants(copyRootOfSubTree, rootToPlaceSubtree.transform.position, 1.0f, ()=>{}));

            /*********************************************
            Update the nullCircles to default value after we have copied them
            *********************************************/
            Debug.Log("Now resetting the nullcircles to default value at the subtree i just moved and coty");
            _nullCircleSpawner.setNullCircleToDefault(LeftChild);

            /*********************************************
            Update the line renderers after we have updated the nullCircles to their current ingredients
            *********************************************/
            _nullCircleSpawner.UpdateLineRenderers();

            /*********************************************
            Destroy the copied nullCircles
            *********************************************/
            _nullCircleSpawner.destroyNullCircleAndAllDescendants(copyRootOfSubTree.gameObject);
         
        }

        // Recursively move the right subtree if it exists.
        if (nullCircle.RightChild.GetComponent<NullCircle>().Ingredient != null)
        {

            GameObject RightChild = nullCircle.RightChild;
            //Debug.Log("Leftchild index: " + leftChild.GetComponent<NullCircle>().Index);
            Vector3 newRightChildPosition = RightChild.GetComponent<NullCircle>().Parent.transform.position;
           
            yield return StartCoroutine(LeftRotationMoveRightSubTreeAndAllDescendants(RightChild, newRightChildPosition, duration, () => { }));
       

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

}