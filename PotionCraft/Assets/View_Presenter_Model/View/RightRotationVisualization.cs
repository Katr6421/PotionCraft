using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RightRotationVisualization : MonoBehaviour
{
    [SerializeField] private NullCircleManager _nullCircleManager;
    [SerializeField] private VisualizationHelper _visualizationHelper;
    [SerializeField] private LineManager _lineManager;


    public IEnumerator RotateRightAnimation(GameObject leftChild, GameObject parent, GameObject grandparent, NullCircle parentNullCircle)
    {
        /* 
            Move all ingredients on right side of tree down
            Move grandparent to grandparent.rightChild
            Move parent to grandparent
            Move all ingredients on left side of tree up
        */


        /*********************************************
        Find nullcircles
        *********************************************/
        GameObject grandParentNullCircle = parentNullCircle.GetComponent<NullCircle>().Parent;

        /*********************************************
        Find the new positions 
        *********************************************/
        GameObject grandParentNewPosition = parentNullCircle.GetComponent<NullCircle>().Parent.GetComponent<NullCircle>().RightChild;
        GameObject parentNewPosition = grandParentNullCircle;

        UpdateLines(leftChild, parent, grandparent, parentNullCircle.Parent.GetComponent<NullCircle>());

        yield return StartCoroutine(_visualizationHelper.RotateTree(false, false, true, grandParentNullCircle, grandParentNewPosition, 1.0f, () => { }));
        yield return StartCoroutine(_visualizationHelper.RotateTree(false, true, false, parentNullCircle.gameObject, parentNewPosition, 1.0f, () => { }));

    }

    public void UpdateLines(GameObject leftChild, GameObject parent, GameObject grandparent, NullCircle parentNullCircle) {
        /*
            grandparent -> får parent's line
                        -> if parent has bag, grandparent's leftChild = parent's rightChild
                        -> if parent has no bag, grandparent's leftChild = null
            parent      -> får grandparent's old line
                        -> rightChild = null
            leftChild   -> uændret
        */

        GameObject grandparentOldParent = grandparent.GetComponent<Ingredient>().LineToParent;
        if (grandparentOldParent != null) {
            if (parentNullCircle.transform.position.x < parentNullCircle.Parent.transform.position.x) {
                parentNullCircle.Parent.GetComponent<NullCircle>().Ingredient.GetComponent<Ingredient>().LineToLeft.GetComponent<Line>().ChangeEndPoint(parent.transform);
            } else {
                parentNullCircle.Parent.GetComponent<NullCircle>().Ingredient.GetComponent<Ingredient>().LineToRight.GetComponent<Line>().ChangeEndPoint(parent.transform);
            }
        }

        parent.GetComponent<Ingredient>().LineToRight = _lineManager.CreateLine(parent, grandparent);
        parent.GetComponent<Ingredient>().LineToParent = grandparentOldParent;
        grandparent.GetComponent<Ingredient>().LineToParent = parent.GetComponent<Ingredient>().LineToRight;

    }

}