using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks.Sources;
using UnityEngine;

public class JarVisualization : MonoBehaviour
{
    [SerializeField] private VisualizationHelper _visualizationHelper;
    [SerializeField] private SplineFromJar _splineFromJar;
    [SerializeField] private NullCircleManager _nullCircleManager;
    private Vector3 jarPosition = new Vector3(0, 0, 0);
    private float duration = 2f;






    public IEnumerator ShrinkMultiple(List<GameObject> objectsToShrink, Action onComplete)
    {
        // Start the shrink and move coroutine for each GameObject.
        foreach (var obj in objectsToShrink)
        {
            StartCoroutine(Shrink(obj, () => { }));
        }

        yield return null;
        onComplete?.Invoke();

        // Optionally, wait for all to complete if needed elsewhere.
        // This would only be necessary if you need to trigger something after all objects have finished animating.
        //float waitTime = duration + 0.1f; // Slightly longer to ensure all animations have completed.
        //yield return new WaitForSeconds(waitTime);

        // Do something after all objects have finished animating.

    }

    public IEnumerator Shrink(GameObject objectsToShrink, Action onComplete)
    {
        // Initial scale
        Vector3 originalScale = objectsToShrink.transform.localScale;
        Vector3 targetScale = Vector3.zero; // Assuming you want to shrink it to nothing

        // Split duration in two phases
        float shrinkDuration = duration * 0.5f; // First half of the duration for shrinking
                                                //float moveDuration = duration * 0.5f; // Second half for moving

        // Shrink phase
        float elapsedTime = 0;
        while (elapsedTime < shrinkDuration)
        {
            float t = elapsedTime / shrinkDuration;
            objectsToShrink.transform.localScale = Vector3.Lerp(originalScale, targetScale, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        objectsToShrink.transform.localScale = targetScale;

        onComplete?.Invoke();
    }


    public IEnumerator MoveNodeAndAllDescendantsJar(NullCircle nodeToMove, NullCircle newPosition, float duration, Action onComplete)
    {
        // Debug.Log("Calling MoveNodeAndAllDescendantsJar");

        // Recursively move the left subtree if it exists.
        if (nodeToMove.LeftChild.GetComponent<NullCircle>().Ingredient != null)
        {
            NullCircle leftChild = nodeToMove.LeftChild.GetComponent<NullCircle>();
            NullCircle newLeftChildPosition = newPosition.LeftChild.GetComponent<NullCircle>();
            // Debug.Log("newleftchildPosistion index:" + leftChild.GetComponent<NullCircle>().LeftChild.GetComponent<NullCircle>().Index);

            //Vector3 leftChildNewPosition = newPosition - (nodeToMove.transform.position - leftChild.transform.position);
            //leftChild.transform.position
            yield return StartCoroutine(MoveNodeAndAllDescendantsJar(leftChild, newLeftChildPosition, duration, () => { }));
        }

        // Recursively move the right subtree if it exists.
        if (nodeToMove.RightChild.GetComponent<NullCircle>().Ingredient != null)
        {
            NullCircle rightChild = nodeToMove.RightChild.GetComponent<NullCircle>();
            NullCircle newRightChildPosition = newPosition.RightChild.GetComponent<NullCircle>();
            yield return StartCoroutine(MoveNodeAndAllDescendantsJar(rightChild, newRightChildPosition, duration, () => { }));
        }


        // If the node has an ingredient, move it.
        if (nodeToMove.Ingredient != null)
        {
            _splineFromJar.ChangeLastKnot(newPosition.transform.position);
            yield return StartCoroutine(_splineFromJar.FollowSplineFromJar(nodeToMove.Ingredient));
            //_spline.ChangeLastVector3Position(newPosition.transform.position);
            //yield return StartCoroutine(_spline.FollowPointsFromJar(nodeToMove.Ingredient));
            _nullCircleManager.UpdateNullCircleWithIngredient(newPosition.transform.position, nodeToMove);
        }
        onComplete?.Invoke();
    }
}
