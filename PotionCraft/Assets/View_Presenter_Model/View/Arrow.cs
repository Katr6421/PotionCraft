using System.Collections;
using UnityEngine;


public class Arrow : MonoBehaviour
{
    public void Start()
    {
        // if only root nullCircle is showing, then show arrow in jumping motion.
        // when root has an ingredient, hide arrow.

        
    }

    /*********************************************
    Show arrow in a jumping motion
    *********************************************/
    private IEnumerator ShowArrow(float duration, float magnitude)
    {
        Vector3 originalPosition = gameObject.transform.localPosition;
        float elapsed = 0.0f;

        // Determine the number of shakes based on duration and a desired shake speed
        float shakePeriod = duration / 2; // This will give you approximately 5 back and forth shakes during the entire duration

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float percentComplete = elapsed / duration;

            // Sinusoidal shake based on elapsed time
            float sinWave = Mathf.Sin((elapsed / shakePeriod) * Mathf.PI * 2); // Sin wave for smooth oscillation
            float x = originalPosition.x + sinWave * magnitude;

            gameObject.transform.localPosition = new Vector3(x, originalPosition.y, originalPosition.z);
            yield return null;
        }

        gameObject.transform.localPosition = originalPosition;
    }


    /*********************************************
    Hide arrow
    *********************************************/
    private void HideArrow()
    {
        // hide arrow
    }

}