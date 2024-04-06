using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeftRotationVisualization : MonoBehaviour
{
    [SerializeField] private NullCircleManager _nullCircleManager;
    [SerializeField] private VisualizationHelper _visualizationHelper;
    [SerializeField] private LineRendererManager _lineRendererManager;


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


        yield return StartCoroutine(_visualizationHelper.RotateTree(true, true, parentNullCircle.gameObject, parentNewPosition, 1.0f, () => { }));
        yield return StartCoroutine(_visualizationHelper.RotateTree(false, false, rightChildNullCircle, rightChildNewPosition, 1.0f, () => { }));
    }

}