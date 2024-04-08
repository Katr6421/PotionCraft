using System.Collections;
using UnityEngine;

public class FlipColorVisualization : MonoBehaviour
{
    [SerializeField] private LineManager _lineManager;
    public IEnumerator FlipColorAnimation(NullCircle parentNullCircle)
    {
        /*********************************************
        Find nullcircles
        *********************************************/
        NullCircle leftChildNullCircle = parentNullCircle.LeftChild.GetComponent<NullCircle>();
        NullCircle rightChildNullCircle = parentNullCircle.RightChild.GetComponent<NullCircle>();


        /*********************************************
        Change color of nullcircles according to the algorithm
            h is parentNullCircle
            h.color = RED;
            h.left.color = BLACK;
            h.right.color = BLACK;
        **********************************************/
        if (parentNullCircle.Ingredient.GetComponent<Ingredient>().LineToParent != null)
        {
            _lineManager.UpdateLineColor(parentNullCircle, true);
        }
        _lineManager.UpdateLineColor(leftChildNullCircle, false);
        _lineManager.UpdateLineColor(rightChildNullCircle, false);

        yield return null;
    }
}
