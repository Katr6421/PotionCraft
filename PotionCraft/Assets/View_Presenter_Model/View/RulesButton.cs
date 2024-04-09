using UnityEngine;
using System.Collections;

public class RulesButton : MonoBehaviour
{
    public GameObject rulesScriptObject; // Assign this in the inspector
    public ParticleSystem rulesParticleSystem; // Assign this in the inspector
    public float moveSpeed = 5f; // Speed at which the box will move
    public Vector3 targetPosition; // The target position at the bottom of the screen

    private bool isMoving = false; // Flag to check if the object is currently moving

    public void ShowRulesBox()
    {
        if (!isMoving) // Only start the coroutine if the object is not already moving
        {
            StartCoroutine(MoveRulesBox());
            // Optionally disable the particle system here if you want it to hide immediately
            rulesParticleSystem.gameObject.SetActive(false);
        }
    }

    private IEnumerator MoveRulesBox()
    {
        isMoving = true;

        // Calculate the target position such that the bottom of the rulesScriptObject reaches the bottom of the screen
        float objectHeight = rulesScriptObject.GetComponent<SpriteRenderer>().bounds.size.y;
        targetPosition = new Vector3(rulesScriptObject.transform.position.x, -Camera.main.orthographicSize + objectHeight / 2, rulesScriptObject.transform.position.z);

        while (Vector3.Distance(rulesScriptObject.transform.position, targetPosition) > 0.01f)
        {
            // Move the object towards the target position
            rulesScriptObject.transform.position = Vector3.Lerp(rulesScriptObject.transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null; // Wait until next frame
        }

        // Ensure the position is set to the target position when done
        rulesScriptObject.transform.position = targetPosition;

        // Re-enable the particle system here if you want it to appear once the box stops moving
        rulesParticleSystem.gameObject.SetActive(true);

        isMoving = false;
    }
}
