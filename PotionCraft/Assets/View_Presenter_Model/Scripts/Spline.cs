using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Splines;

public class Spline : MonoBehaviour
{
    public SplineContainer spline;
    public float duration = 5f; // Duration of the movement along the spline

    public void ChangeFirstKnotPosition(Vector3 newPosition)
    {
        if (spline != null && spline.Spline.Count > 0)
        {
            // Change the position of the first knot
            var knot0 = spline.Spline.ToArray()[0];
            knot0.Position = newPosition;
            spline.Spline.SetKnot(0, knot0);
            
        }
    }

    public void ChangeLastKnotPosition(Vector3 newPosition)
    {
        if (spline != null && spline.Spline.Count > 0)
        {
            // Change the position of the last knot
            var lastKnotIndex = spline.Spline.Count - 1;
            var lastKnot = spline.Spline.ToArray()[lastKnotIndex];
            lastKnot.Position = newPosition;
            spline.Spline.SetKnot(lastKnotIndex, lastKnot);
        }
    }


    public IEnumerator FollowSplineToJar(GameObject ingredient)
    {
        float timeElapsed = 0;

        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            float t = timeElapsed / duration;

            // Spline.EvaluatePosition() is used to get the position along the spline
            Vector3 positionOnSpline = spline.EvaluatePosition(t);

            // Move the GameObject to the position on the spline
            ingredient.transform.position = positionOnSpline;

            yield return null;
        }
        
        // Optional: Snap to the end to ensure the GameObject is precisely at the end of the spline
        ingredient.transform.position = spline.EvaluatePosition(1f);
    }
    

    public IEnumerator FollowSplineFromJar(GameObject ingredient)
    {
        float timeElapsed = 0;

        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            float t = timeElapsed / duration;

            // Spline.EvaluatePosition() is used to get the position along the spline
            Vector3 positionOnSpline = spline.Spline.EvaluatePosition(t);

            // Move the GameObject to the position on the spline
            ingredient.transform.position = positionOnSpline;

            yield return null;
        }
        
        // Optional: Snap to the end to ensure the GameObject is precisely at the end of the spline
        ingredient.transform.position = spline.Spline.EvaluatePosition(1f);
    }

    
}