using System.Collections;
using UnityEngine;


public class Arrow : MonoBehaviour
{
    void Start()
    {
        // if only root nullCircle is showing, then show arrow in jumping motion.
        // when root has an ingredient, destroy arrow.
        StartCoroutine(MoveArrow(0.1f));
    }

    /*********************************************
    Show arrow in a jumping motion
    *********************************************/
    private IEnumerator MoveArrow(float magnitude)
    {
        Vector3 originalPosition = transform.position;

        while (true) // Keep looping as long as keepJumping is true
        {
            // Sinusoidal shake based on Time.time for continuous oscillation
            float y = originalPosition.y + Mathf.Sin(Time.time * Mathf.PI * 2) * magnitude;
            gameObject.transform.localPosition = new Vector3(originalPosition.x, y, originalPosition.z);
            
            yield return null; // Wait for the next frame
        }
    }

}