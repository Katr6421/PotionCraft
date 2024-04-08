using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeftRotationVisualization : MonoBehaviour
{
    [SerializeField] private NullCircleManager _nullCircleManager;
    [SerializeField] private VisualizationHelper _visualizationHelper;
    [SerializeField] private LineManager _lineManager;


    public IEnumerator RotateLeftAnimation(GameObject parent, GameObject rightChild, NullCircle parentNullCircle)
    {
        /*
            Move all ingredients on the left side of the tree down
            Move parent to parent.leftchild
            Move rightchild to parent
            Move all ingredients on the right side of the tree up
        */

        /*********************************************
        Find the new positions
        *********************************************/
        GameObject parentNewPosition = parentNullCircle.GetComponent<NullCircle>().LeftChild;
        GameObject rightChildNewPosition = parentNullCircle.gameObject;

        /*********************************************
        Find nullcircles
        *********************************************/
        GameObject rightChildNullCircle = parentNullCircle.GetComponent<NullCircle>().RightChild;

        UpdateLines(parent, rightChild, parentNullCircle);

        yield return StartCoroutine(_visualizationHelper.RotateTree(true, true, true, parentNullCircle.gameObject, parentNewPosition, 1.0f, () => { }));
        yield return StartCoroutine(_visualizationHelper.RotateTree(true, false, false, rightChildNullCircle, rightChildNewPosition, 1.0f, () => { }));
    }

    public void UpdateLines(GameObject parent, GameObject rightChild, NullCircle parentNullCircle) {
        /*
            grandparent -> får parent's line
                        -> if parent has bag, grandparent's leftChild = parent's rightChild
                        -> if parent has no bag, grandparent's leftChild = null
            parent      -> får grandparent's old line
                        -> rightChild = null
            leftChild   -> uændret
        */

        GameObject parentOldParent = parent.GetComponent<Ingredient>().LineToParent;
        if (parentOldParent != null) {
            Debug.Log("ParentOldParent is not null: " + parentNullCircle.Parent.GetComponent<NullCircle>().Value);
            if (parentNullCircle.transform.position.x < parentNullCircle.Parent.transform.position.x) {
                parentNullCircle.Parent.GetComponent<NullCircle>().Ingredient.GetComponent<Ingredient>().LineToLeft.GetComponent<Line>().ChangeEndPoint(rightChild.transform);
            } else {
                parentNullCircle.Parent.GetComponent<NullCircle>().Ingredient.GetComponent<Ingredient>().LineToRight.GetComponent<Line>().ChangeEndPoint(rightChild.transform);
            }
        }

        rightChild.GetComponent<Ingredient>().LineToLeft = _lineManager.CreateLine(rightChild, parent);
        rightChild.GetComponent<Ingredient>().LineToParent = parentOldParent;
        parent.GetComponent<Ingredient>().LineToParent = rightChild.GetComponent<Ingredient>().LineToLeft;

    }

}