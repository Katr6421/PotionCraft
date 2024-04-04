using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipColorVisualization : MonoBehaviour
{
    [SerializeField] private NullCircleSpawner _nullCircleSpawner;
    public IEnumerator FlipColorAnimation(NullCircle parentNullCircle)
    {
        // Find nullcircles
        NullCircle leftChildNullCircle = parentNullCircle.LeftChild.GetComponent<NullCircle>();
        NullCircle rightChildNullCircle = parentNullCircle.RightChild.GetComponent<NullCircle>();

        // Change color of nullcircles according to the algorithm
        // h is parentNullCircle
        // h.color = RED;
        // h.left.color = BLACK;
        // h.right.color = BLACK;
        parentNullCircle.IsRed = true;
        leftChildNullCircle.IsRed = false;
        rightChildNullCircle.IsRed = false;
        _nullCircleSpawner.UpdateLineRenderers();

        yield return null; 
    }
}
