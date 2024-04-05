using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;



/*
    LeftRotation:
        1. left subtree move down
            1.1 if has right subtree, move right subtree down (parent.leftchild.rightchild)
        2. parent move down
        3. rightChild move up
        4. right subtree move up
            4.1 if has left subtree, move left subtree up (parent.parent.leftchild)

            Først: MoveSubTree(left, down)
            Anden: MoveSubTree(right, up)

    RightRotation:
        1. right subtree move down
            1.1 if has left subtree, move left subtree down (parent.rightchild.leftchild)
        2. grandparent move down
        3. parent move up
        4. left subtree move up
            4.1 if has right subtree, move right subtree up (parent.parent.rightchild)

            Først: MoveSubTree(right, down)
            Anden: MoveSubTree(left, up)


    Fælles træk:
        - left subtree move down + right subtree move down
        - if has right subtree, move right subtree down (parent.leftchild.rightchild) + if has left subtree, move left subtree down (parent.rightchild.leftchild)
        - parent move down + grandparent move down
        - right subtree move up + left subtree move up
        - if has left subtree, move left subtree up (parent.parent.leftchild) + if has right subtree, move right subtree up (parent.parent.rightchild)
*/



public class test : MonoBehaviour {
    [SerializeField] private NullCircleSpawner _nullCircleSpawner;
    [SerializeField] private VisualizationHelper _visualizationHelper;


    public IEnumerator MoveSubTree(bool isLeft, bool isDown, GameObject startingNullCircle, GameObject EndingNullCircle, float duration, Action onComplete) {
        
        NullCircle startNullCircle = startingNullCircle.GetComponent<NullCircle>();
        var leftright = isLeft ? "LeftChild" : "RightChild";
      

        if (isDown) {
            /*********************************************
            Move by the defined (left-right) subtree down
            *********************************************/
            if (startNullCircle.leftright.GetComponent<NullCircle>().Ingredient != null) {
                GameObject leftrightChild = startNullCircle.leftright;
                GameObject newLeftRightChildPosition = leftright.GetComponent<NullCircle>().leftright;


                // lav en til MoveNodeAndAllDescendants
                yield return StartCoroutine(_visualizationHelper.MoveNodeAndAllDescendants(isLeft, leftrightChild, newLeftRightChildPosition, duration, ()=>{}));
            

                /*********************************************
                Move the starting node itself down
                *********************************************/
                yield return StartCoroutine(_visualizationHelper.MoveNode(startNullCircle.Ingredient, EndingNullCircle.transform.position, duration, startNullCircle, ()=>{
                    _nullCircleSpawner.DeactivateAllNullCirclesInSubtree(startNullCircle);
                    _visualizationHelper.UpdateNullCircleWithIngredient(EndingNullCircle.transform.position, startNullCircle);
                    _nullCircleSpawner.UpdateLineRenderers();
                }));
            }
        } else {

            /*********************************************
            Move the starting node itself up
            *********************************************/
            yield return StartCoroutine(_visualizationHelper.MoveNode(startNullCircle.Ingredient, EndingNullCircle.transform.position, duration, startNullCircle, ()=>{
                _nullCircleSpawner.DeactivateAllNullCirclesInSubtree(startNullCircle);             
                _visualizationHelper.UpdateNullCircleWithIngredient(EndingNullCircle.transform.position, startNullCircle);
                _nullCircleSpawner.UpdateLineRenderers();
            }));
            

            /*********************************************
            Move by the defined (left-right) subtree up
            *********************************************/
            if (startNullCircle.leftright.GetComponent<NullCircle>().Ingredient != null) {
                GameObject leftrightChild = startNullCircle.leftright;
                GameObject newLeftRightChildPosition = startingNode.transform.position;

                // lav en til MoveNodeAndAllDescendants
                yield return StartCoroutine(_visualizationHelper.MoveNodeAndAllDescendants(isLeft, leftrightChild, newLeftRightChildPosition, duration, ()=>{}));   
            }
        }

        onComplete?.Invoke();
    }




    private IEnumerator MoveNodeAndAllDescendants(bool isLeft, bool isDown, GameObject nodeToMove, GameObject EndingNullCircle, float duration, Action onComplete) {
        NullCircle nullCircle = nodeToMove.GetComponent<NullCircle>();
        var leftright = isLeft ? "LeftChild" : "RightChild";

        
        /*********************************************
        Go recursively through the left/right subtree
        *********************************************/
        if (nullCircle.leftright.GetComponent<NullCircle>().Ingredient != null) {
            GameObject leftrightChild = nullCircle.leftright;
            GameObject newLeftRightChildPosition = leftrightChild.GetComponent<NullCircle>().leftright;

            yield return StartCoroutine(MoveNodeAndAllDescendants(isLeft, leftrightChild, newLeftRightChildPosition, duration, () => {
                
            }));
        }


        /*********************************************
        Check if the node has a left/right child and move it in one move
        *********************************************/
        if (isLeft) {
            if (nullCircle.RightChild.GetComponent<NullCircle>().Ingredient != null) {
                GameObject rootOfSubtree = nullCircle.RightChild;
                GameObject rootToPlaceSubtree;
                if (isDown) {
                    rootToPlaceSubtree = nullCircle.LeftChild.GetComponent<NullCircle>().RightChild;
                } else {
                    rootToPlaceSubtree = nullCircle.Parent.GetComponent<NullCircle>().RightChild;
                }
                MoveSubtreeToNewPosition(rootOfSubtree, rootToPlaceSubtree, duration, () => {});
            }
        } else {
            if (nullCircle.LeftChild.GetComponent<NullCircle>().Ingredient != null) {
                GameObject rootOfSubtree = nullCircle.LeftChild;
                GameObject rootToPlaceSubtree;
                if (isDown) {
                    rootToPlaceSubtree = nullCircle.RightChild.GetComponent<NullCircle>().LeftChild;
                } else {
                    rootToPlaceSubtree = nullCircle.Parent.GetComponent<NullCircle>().LeftChild;
                }
                MoveSubtreeToNewPosition(rootOfSubtree, rootToPlaceSubtree, duration, () => {});
            }
        }
        

        /*********************************************
        Move the node up/down
        *********************************************/
        if (nullCircle.Ingredient != null) {
            yield return StartCoroutine(MoveNode(nullCircle.Ingredient, EndingNullCircle.transform.position, duration, nullCircle, () => {
                _nullCircleSpawner.DeactivateAllNullCirclesInSubtree(nullCircle);
                _visualizationHelper.UpdateNullCircleWithIngredient(EndingNullCircle, nullCircle);
                _nullCircleSpawner.UpdateLineRenderers();
            }));
        }
        
        onComplete?.Invoke();
    }




    private IEnumerator MoveSubtreeToNewPosition(NullCircle rootOfSubtree, GameObject rootToPlaceSubtree, float duration, Action onComplete) {
        /*********************************************
        Recursively move the left subtree if it exists.
        *********************************************/
        if (rootOfSubtree.LeftChild.GetComponent<NullCircle>().Ingredient != null) {
            NullCircle leftChild = rootOfSubtree.LeftChild.GetComponent<NullCircle>();
            NullCircle newLeftChildPosition = rootToPlaceSubtree.LeftChild.GetComponent<NullCircle>();
    
            yield return StartCoroutine(MoveSubtreeToNewPosition(leftChild, newLeftChildPosition, duration, () => {}));
        }

        /*********************************************
        Recursively move the right subtree if it exists.
        *********************************************/
        if (rootOfSubtree.RightChild.GetComponent<NullCircle>().Ingredient != null) {
            NullCircle rightChild = rootOfSubtree.RightChild.GetComponent<NullCircle>();
            NullCircle newRightChildPosition = rootToPlaceSubtree.RightChild.GetComponent<NullCircle>();
            yield return StartCoroutine(MoveSubtreeToNewPosition(rightChild, newRightChildPosition, duration, () => {}));
        }


        /*********************************************
        If the node has an ingredient, move it.
        *********************************************/
        if (rootOfSubtree.Ingredient != null) {
            yield return StartCoroutine(MoveNode(nullCircle.Ingredient, EndingNullCircle.transform.position, duration, nullCircle, () => {
                _nullCircleSpawner.DeactivateAllNullCirclesInSubtree(nullCircle);
                _visualizationHelper.UpdateNullCircleWithIngredient(rootToPlaceSubtree.transform.position, rootOfSubtree); 
                _nullCircleSpawner.UpdateLineRenderers();
            }));
        }
        

        onComplete?.Invoke();
    }


}












/*********************************************************************************************/
public class LeftRotationVisualization2 : MonoBehaviour {
    [SerializeField] private NullCircleSpawner _nullCircleSpawner;
    [SerializeField] private VisualizationHelper _visualizationHelper;

    public IEnumerator RotateLeftAnimation2(GameObject parent, GameObject rightChild, NullCircle parentNullCircle){
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

    





    

    // Down
     public IEnumerator RightRotationMoveRightSubTreeAndAllDescendants2235(GameObject nodeToMove, Vector3 newPosition, float duration, Action onComplete) {
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

                  


        /* right left*/
        NullCircle rootToPlaceSubtree = nullCircle.GetComponent<NullCircle>().RightChild.GetComponent<NullCircle>().LeftChild.GetComponent<NullCircle>();

        /*Måske ikke nullcircle her*/
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

        // Recursively move the right subtree if it exists.
        if (nullCircle.RightChild.GetComponent<NullCircle>().Ingredient != null) {
            GameObject rightChild = nullCircle.RightChild;
            Vector3 newRightChildPosition = rightChild.GetComponent<NullCircle>().RightChild.transform.position;
            /* SHOULD MAYBE BE A DIFFERENT METHOD*/
            yield return StartCoroutine(RightRotationMoveRightSubTreeAndAllDescendants(rightChild, newRightChildPosition, duration, () => {}));
        }


        /* If the node has an ingredient, move it.*/
        if (nullCircle.Ingredient != null) {
            yield return StartCoroutine(_visualizationHelper.MoveNode(nullCircle.Ingredient, newPosition, duration, nullCircle, () => {
                _nullCircleSpawner.DeactivateAllNullCirclesInSubtree(nullCircle);
                _visualizationHelper.UpdateNullCircleWithIngredient(newPosition, nullCircle);
            }));
           
        }

        
        onComplete?.Invoke();
    }

    public IEnumerator RightRotationMoveLeftSubTreeAndAllDescendants245(GameObject nodeToMove, Vector3 newPosition, float duration, Action onComplete)
    {
        /*Debug.Log("Calling MoveNodeAndAllDescendants"); */
        NullCircle nullCircle = nodeToMove.GetComponent<NullCircle>();

        /* Recursively move the left subtree if it exists.*/
        if (nullCircle.LeftChild.GetComponent<NullCircle>().Ingredient != null)
        {
            GameObject leftChild = nullCircle.LeftChild;
            Vector3 newLeftChildPosition = leftChild.GetComponent<NullCircle>().LeftChild.transform.position;
           
            yield return StartCoroutine(RightRotationMoveLeftSubTreeAndAllDescendants(leftChild, newLeftChildPosition, duration, () => { }));
        }

        // Recursively move the right subtree if it exists.
        if (nullCircle.RightChild.GetComponent<NullCircle>().Ingredient != null)
        {
        Debug.Log("I " + nullCircle.Index + " have a right child");
        Debug.Log("Right child index: " + nullCircle.RightChild.GetComponent<NullCircle>().Index);
        

         /*********************************************
        Make a copy of all the nullCircles and instantiate them with the ingredients' values
        *********************************************/
        NullCircle RightChild = nullCircle.RightChild.GetComponent<NullCircle>();

        NullCircle copyRootOfSubTree = _nullCircleSpawner.CopyNullCircleSubtree(RightChild);

        // Our new posistion is our parent parent right left
        NullCircle rootToPlaceSubtree = nullCircle.GetComponent<NullCircle>().Parent.GetComponent<NullCircle>().RightChild.GetComponent<NullCircle>();

        
        yield return StartCoroutine( _visualizationHelper.MoveNode(RightChild.Ingredient, rootToPlaceSubtree.transform.position, 1.0f, nullCircle, ()=>{}));

        /*********************************************
        Update the line renderers after we have updated the nullCircles to their current ingredients
        *********************************************/
        _nullCircleSpawner.UpdateLineRenderers();

        /*********************************************
        Destroy the copied nullCircles
        *********************************************/
        _nullCircleSpawner.destroyNullCircleAndAllDescendants(copyRootOfSubTree.gameObject);


        }


        /* If the node has an ingredient, move it.*/
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





/*********************************************************************************************/


public class RightRotationVisualization2 : MonoBehaviour
{
    [SerializeField] private NullCircleSpawner _nullCircleSpawner; 
    [SerializeField] private VisualizationHelper _visualizationHelper;


    public IEnumerator RotateRightAnimation2(GameObject leftChild, GameObject parent, GameObject grandparent, NullCircle parentNullCircle) {
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
    

   




    public IEnumerator RightRotationMoveRightSubTreeAndAllDescendants67892(GameObject nodeToMove, Vector3 newPosition, float duration, Action onComplete) {
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

    public IEnumerator RightRotationMoveLeftSubTreeAndAllDescendants2(GameObject nodeToMove, Vector3 newPosition, float duration, Action onComplete)
    {
        //Debug.Log("Calling MoveNodeAndAllDescendants");
        NullCircle nullCircle = nodeToMove.GetComponent<NullCircle>();

        // Recursively move the left subtree if it exists.
        if (nullCircle.LeftChild.GetComponent<NullCircle>().Ingredient != null)
        {
            //Debug.Log("Jeg har et venstre barn");
            GameObject leftChild = nullCircle.LeftChild;
            //Debug.Log("Leftchild index: " + leftChild.GetComponent<NullCircle>().Index);
            Vector3 newLeftChildPosition = leftChild.GetComponent<NullCircle>().LeftChild.transform.position;
           
            yield return StartCoroutine(RightRotationMoveLeftSubTreeAndAllDescendants(leftChild, newLeftChildPosition, duration, () => { }));
        }

        // Recursively move the right subtree if it exists.
        if (nullCircle.RightChild.GetComponent<NullCircle>().Ingredient != null)
        {
        Debug.Log("I " + nullCircle.Index + " have a right child");
        Debug.Log("Right child index: " + nullCircle.RightChild.GetComponent<NullCircle>().Index);
        

         /*********************************************
        Make a copy of all the nullCircles and instantiate them with the ingredients' values
        *********************************************/
        NullCircle RightChild = nullCircle.RightChild.GetComponent<NullCircle>();

        NullCircle copyRootOfSubTree = _nullCircleSpawner.CopyNullCircleSubtree(RightChild);

        // Our new posistion is our parent parent right left
        NullCircle rootToPlaceSubtree = nullCircle.GetComponent<NullCircle>().Parent.GetComponent<NullCircle>().RightChild.GetComponent<NullCircle>();

        
        yield return StartCoroutine( _visualizationHelper.MoveNode(RightChild.Ingredient, rootToPlaceSubtree.transform.position, 1.0f, nullCircle, ()=>{}));

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



}