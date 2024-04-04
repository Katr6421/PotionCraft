using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks.Sources;
using UnityEngine;

public class JarVisualization : MonoBehaviour
{
    private Vector3 jarPosition = new Vector3(0, 0, 0);
    private float duration = 2f;






    public IEnumerator ShrinkMultiple(List<GameObject> objectsToShrink, Action onComplete)
    {
        // Start the shrink and move coroutine for each GameObject.
        foreach (var obj in objectsToShrink) {
            StartCoroutine(Shrink(obj, () => {}));
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
        while (elapsedTime < shrinkDuration) {
            float t = elapsedTime / shrinkDuration;
            objectsToShrink.transform.localScale = Vector3.Lerp(originalScale, targetScale, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        objectsToShrink.transform.localScale = targetScale;

        onComplete?.Invoke();
    }
    

    
}
