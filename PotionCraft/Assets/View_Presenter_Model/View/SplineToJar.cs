using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Splines;

public class SplineToJar : MonoBehaviour
{
    public SplineContainer splineContainer;
    public AnimationCurve movementCurve;
    public float duration = 5f; // Duration of the movement along the spline
    

    public void ChangeFirstKnot(Vector3 newPosition){
        var firstKnot = splineContainer.Spline.ToArray()[0];
        firstKnot.Position = newPosition;
        splineContainer.Spline.SetKnot(0,firstKnot);
    }

    public IEnumerator FollowSplineToJar(GameObject ingredient)
    {
        float timeElapsed = 0;

        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            float t = timeElapsed / duration;

           // Adjust 't' based on the AnimationCurve
        float curveT = movementCurve.Evaluate(t);

        // Use the adjusted 't' (curveT) to get the position along the spline
        Vector3 positionOnSpline = splineContainer.EvaluatePosition(curveT);

            // Move the GameObject to the position on the spline
            ingredient.transform.position = positionOnSpline;

            yield return null;
        }
        
        // Optional: Snap to the end to ensure the GameObject is precisely at the end of the spline
        ingredient.transform.position = splineContainer.EvaluatePosition(1f);
    }
    
    
}