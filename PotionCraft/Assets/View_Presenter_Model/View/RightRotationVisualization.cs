using System.Collections;
using UnityEngine;

public class RightRotationVisualization : MonoBehaviour
{
    [SerializeField] private VisualizationHelper _visualizationHelper;
    [SerializeField] private LineManager _lineManager;


    public IEnumerator RotateRightAnimation(GameObject leftChild, GameObject parent, GameObject grandparent, NullCircle parentNullCircle)
    {
        /*********************************************
            Move all ingredients on right side of tree down
            Move grandparent to grandparent.rightChild
            Move parent to grandparent
            Move all ingredients on left side of tree up
        *********************************************/


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

    public void UpdateLines(GameObject leftChild, GameObject parent, GameObject grandparent, NullCircle parentNullCircle)
    {
        /*********************************************
            grandparent.lineToParent    -> Er den samme linje som parent's lineToRight - den der må instantieres

            parent.lineToParent         -> Får grandparent's gamle lineToParent
            parent.lineToRight          -> Må instantiate en ny line for reference årsager (DrawLineToNullCircle sletter altid lineToLeft og lineToRight)
                        -> rightChild = null
            leftChild   -> uændret
        *********************************************/


        /*********************************************
        Hvis parent ikke var root, må vi opdatere lineToParent's endepunkt, så den peger på rightChild
        *********************************************/
        GameObject grandparentOldParent = grandparent.GetComponent<Ingredient>().LineToParent;
        if (grandparentOldParent != null)
        {
            if (parentNullCircle.transform.position.x < parentNullCircle.Parent.transform.position.x)
            {
                parentNullCircle.Parent.GetComponent<NullCircle>().Ingredient.GetComponent<Ingredient>().LineToLeft.GetComponent<Line>().ChangeEndPoint(parent.transform);
            }
            else
            {
                parentNullCircle.Parent.GetComponent<NullCircle>().Ingredient.GetComponent<Ingredient>().LineToRight.GetComponent<Line>().ChangeEndPoint(parent.transform);
            }
        }

        parent.GetComponent<Ingredient>().LineToRight = _lineManager.CreateLine(parent, grandparent);
        parent.GetComponent<Ingredient>().LineToParent = grandparentOldParent;
        grandparent.GetComponent<Ingredient>().LineToParent = parent.GetComponent<Ingredient>().LineToRight;

    }

}