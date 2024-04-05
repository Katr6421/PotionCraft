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
            Vector3 newRightChildPosition = rightChild.GetComponent<NullCircle>().RightChild.transform.position;
            
            //Vector3 rightChildNewPosition = newPosition - (startingNode.transform.position - rightChild.transform.position);
            yield return StartCoroutine(RightRotationMoveRightSubTreeAndAllDescendants(rightChild, newRightChildPosition, duration, ()=>{
                //Debug.Log("Right subtree has been moved");
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


    IEnumerator MoveLeftSubtreeAndAllDescendants(NullCircle startingNode, Vector3 newPosition, float duration, Action onComplete) {
        NullCircle startingNullCircle = startingNode.GetComponent<NullCircle>();

        // After the right subtree has been moved, now move the starting node itself if it has an ingredient.
        //Debug.Log("Now i am moving myself");
        yield return StartCoroutine(_visualizationHelper.MoveNode(startingNullCircle.Ingredient, newPosition, duration, startingNullCircle, ()=>{
            _nullCircleSpawner.DeactivateAllNullCirclesInSubtree(startingNullCircle);
            _visualizationHelper.UpdateNullCircleWithIngredient(newPosition, startingNullCircle);
        }));


        // Move the left child and its subtree if it exists.
        if (startingNullCircle.LeftChild.GetComponent<NullCircle>().Ingredient != null) {
            GameObject leftChild = startingNullCircle.LeftChild;
            Vector3 leftChildNewPosition = startingNode.transform.position;
            //newPosition - (startingNode.transform.position - leftChild.transform.position);
            yield return StartCoroutine(RightRotationMoveLeftSubTreeAndAllDescendants(leftChild, leftChildNewPosition, duration, ()=>{
                //Debug.Log("Left subtree has been moved");
                }));
            
        }
        
        onComplete?.Invoke();
    }

    // Down
    public IEnumerator RightRotationMoveRightSubTreeAndAllDescendants(GameObject nodeToMove, Vector3 newPosition, float duration, Action onComplete) {
        //Debug.Log("Calling MoveNodeAndAllDescendants");
        NullCircle nullCircle = nodeToMove.GetComponent<NullCircle>();

        // Recursively move the left subtree if it exists.
        if (nullCircle.LeftChild.GetComponent<NullCircle>().Ingredient != null) {
        Debug.Log("I " + nullCircle.Index + " have a left child");
        Debug.Log("Left child index: " + nullCircle.LeftChild.GetComponent<NullCircle>().Index);
        

         /*********************************************
        Make a copy of all the nullCircles and instantiate them with the ingredients' values
        *********************************************/
        NullCircle LeftChild = nullCircle.LeftChild.GetComponent<NullCircle>();


        NullCircle copyRootOfSubTree = _nullCircleSpawner.CopyNullCircleSubtree(LeftChild);

                    

        /*********************************************
        Update the nullCircles to default value after we have copied them
        *********************************************/

        //Måske heller ikke
        //_nullCircleSpawner.setNullCircleToDefault(LeftChild);

                  


        // right left
        NullCircle rootToPlaceSubtree = nullCircle.GetComponent<NullCircle>().RightChild.GetComponent<NullCircle>().LeftChild.GetComponent<NullCircle>();

        //Måske ikke nullcircle her
        yield return StartCoroutine( _visualizationHelper.MoveNode(LeftChild.Ingredient, rootToPlaceSubtree.transform.position, 1.0f, nullCircle, ()=>{}));

        /*********************************************
        Update the line renderers after we have updated the nullCircles to their current ingredients
        *********************************************/
        _nullCircleSpawner.UpdateLineRenderers();

        /*********************************************
        Destroy the copied nullCircles
        *********************************************/
        _nullCircleSpawner.destroyNullCircleAndAllDescendants(copyRootOfSubTree.gameObject);







        /* //Debug.Log("Jeg har et venstre barn");
            GameObject leftChild = nullCircle.LeftChild;
            //Debug.Log("Leftchild index: " + leftChild.GetComponent<NullCircle>().Index);
            Vector3 newLeftChildPosition = leftChild.GetComponent<NullCircle>().LeftChild.transform.position;
            //Debug.Log("newleftchildPosistion index:" + leftChild.GetComponent<NullCircle>().LeftChild.GetComponent<NullCircle>().Index);

            //Vector3 leftChildNewPosition = newPosition - (nodeToMove.transform.position - leftChild.transform.position);
            //leftChild.transform.position
            yield return StartCoroutine(RightRotationMoveNodeAndAllDescendants(leftChild, newLeftChildPosition, duration, () => {}));
        */
        }

        // Recursively move the right subtree if it exists.
        if (nullCircle.RightChild.GetComponent<NullCircle>().Ingredient != null) {
            GameObject rightChild = nullCircle.RightChild;
            Vector3 newRightChildPosition = rightChild.GetComponent<NullCircle>().RightChild.transform.position;
            //Vector3 rightChildNewPosition = newPosition - (nodeToMove.transform.position - rightChild.transform.position);
            // SHOULD MAYBE BE A DIFFERENT METHOD
            yield return StartCoroutine(RightRotationMoveRightSubTreeAndAllDescendants(rightChild, newRightChildPosition, duration, () => {}));
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

    public IEnumerator LeftRotationMoveRightSubTreeAndAllDescendants(GameObject nodeToMove, Vector3 newPosition, float duration, Action onComplete)
    {
        //Debug.Log("Calling MoveNodeAndAllDescendants");
        NullCircle nullCircle = nodeToMove.GetComponent<NullCircle>();

        // Recursively move the right subtree if it exists.
        if (nullCircle.RightChild.GetComponent<NullCircle>().Ingredient != null)
        {

            GameObject RightChild = nullCircle.RightChild;
            //Debug.Log("Leftchild index: " + leftChild.GetComponent<NullCircle>().Index);
            Vector3 newRightChildPosition = RightChild.GetComponent<NullCircle>().Parent.transform.position;

            yield return StartCoroutine(LeftRotationMoveRightSubTreeAndAllDescendants(RightChild, newRightChildPosition, duration, () => { }));
        }

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

            
            yield return StartCoroutine( _visualizationHelper.MoveNode(LeftChild.Ingredient, rootToPlaceSubtree.transform.position, 1.0f, nullCircle, ()=>{}));

            /*********************************************
            Update the line renderers after we have updated the nullCircles to their current ingredients
            *********************************************/
            _nullCircleSpawner.UpdateLineRenderers();

            /*********************************************
            Destroy the copied nullCircles
            *********************************************/
            _nullCircleSpawner.destroyNullCircleAndAllDescendants(copyRootOfSubTree.gameObject);
         
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

    // up
    public IEnumerator RightRotationMoveLeftSubTreeAndAllDescendants(GameObject nodeToMove, Vector3 newPosition, float duration, Action onComplete)
    {
        //Debug.Log("Calling MoveNodeAndAllDescendants");
        NullCircle nullCircle = nodeToMove.GetComponent<NullCircle>();

        // If the node has an ingredient, move it.
        if (nullCircle.Ingredient != null)
        {
            yield return StartCoroutine(_visualizationHelper.MoveNode(nullCircle.Ingredient, newPosition, duration, nullCircle, () =>
            {
                _nullCircleSpawner.DeactivateAllNullCirclesInSubtree(nullCircle);
                _visualizationHelper.UpdateNullCircleWithIngredient(newPosition, nullCircle);
            }));
        }

        //|| nullCircle.RightChild == null
        if (nullCircle.LeftChild == null ) 
        {
            Debug.Log("!!!!!!!!!!!!!!!!!NodeToMove index: " + nullCircle.Index);
            Debug.Log("!!!!!!!!!!!!!!!! Leftchild is null !!!!!!!!!!!");
            yield return null;
        }
        
        // Recursively move the left subtree if it exists.

        if(nullCircle.RightChild.GetComponent<NullCircle>().Ingredient != null){
            
            Debug.Log("I " + nullCircle.Index + " have a right child");
            Debug.Log("rightchild child index: " + nullCircle.RightChild.GetComponent<NullCircle>().Index);
            

            /*********************************************
            Make a copy of all the nullCircles and instantiate them with the ingredients' values
            *********************************************/
            NullCircle RighChild = nullCircle.RightChild.GetComponent<NullCircle>();

            NullCircle copyRootOfSubTree = _nullCircleSpawner.CopyNullCircleSubtree(RighChild);

            // I should go to my parents rught
            NullCircle rootToPlaceSubtree = nullCircle.GetComponent<NullCircle>().Parent.GetComponent<NullCircle>().RightChild.GetComponent<NullCircle>();

            
            yield return StartCoroutine( _visualizationHelper.MoveNode(RighChild.Ingredient, rootToPlaceSubtree.transform.position, 1.0f, nullCircle, ()=>{}));

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
            if (nullCircle.LeftChild.GetComponent<NullCircle>().Ingredient != null)
            {

                GameObject LeftChild = nullCircle.LeftChild;
                //Debug.Log("Leftchild index: " + leftChild.GetComponent<NullCircle>().Index);

                // Move leftchild to its parent
                //Vector3 newLeftChildPosition = LeftChild.GetComponent<NullCircle>().Parent.transform.position;
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