using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RightRotationVisualization : MonoBehaviour
{
    [SerializeField] private NullCircleManager _nullCircleManager;
    [SerializeField] private VisualizationHelper _visualizationHelper;
    [SerializeField] private LineRendererManager _lineRendererManager;


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


        yield return StartCoroutine(_visualizationHelper.RotateTree(false, true, grandParentNullCircle, grandParentNewPosition, 1.0f, () => { }));
        yield return StartCoroutine(_visualizationHelper.RotateTree(true, false, parentNullCircle.gameObject, parentNewPosition, 1.0f, () => { }));

    }

}