using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RulesButton : MonoBehaviour
{
    public GameObject rulesScriptObject; // Assign this in the inspector
    public float moveSpeed = 5f; // Speed at which the box will move
    private Vector3 originalPosition; // The original starting position of the rulesScriptObject

    public Vector3 targetPosition; // The target position at the bottom of the screen

    private bool isMoving = false; // Flag to check if the object is currently moving

    private void Start()
    {
        // Store the original position of the rulesScriptObject
        originalPosition = rulesScriptObject.transform.position;

        // Calculate the target position such that the bottom of the rulesScriptObject reaches the bottom of the screen
        float objectHeight = rulesScriptObject.GetComponent<SpriteRenderer>().bounds.size.y;
        targetPosition = new Vector3(rulesScriptObject.transform.position.x, -Camera.main.orthographicSize + objectHeight / 2, rulesScriptObject.transform.position.z);
    }

    public void ShowRulesBox()
    {
        LevelTrackManager.Instance.CurrentLevelTrackData.ClickOnRulesButton++;
        if (!isMoving)
        {
            DisableAllInteractions(); // Disable all particle systems and make all buttons non-interactable
            StartCoroutine(MoveRulesBox(targetPosition, false));
        }
    }

    public void HideRulesBox()
    {
        if (!isMoving)
        {
            StartCoroutine(MoveRulesBox(originalPosition, true));
            EnableAllInteractions(); // Enable all particle systems and make all buttons interactable
        }
    }

    private IEnumerator MoveRulesBox(Vector3 targetPos, bool activatingParticles)
    {
        isMoving = true;

        while (Vector3.Distance(rulesScriptObject.transform.position, targetPos) > 0.01f)
        {
            rulesScriptObject.transform.position = Vector3.Lerp(rulesScriptObject.transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }

        rulesScriptObject.transform.position = targetPos;

        isMoving = false;
    }

    // Call this method to disable all particle systems and make all buttons non-interactable
    public void DisableAllInteractions()
    {
        // Disable all particle systems
        ParticleSystem[] particleSystems = FindObjectsOfType<ParticleSystem>();
        foreach (var ps in particleSystems)
        {
            ps.Stop();
            ps.Clear();
        }

        // Make all buttons non-interactable
        Button[] buttons = FindObjectsOfType<Button>();
        foreach (var button in buttons)
        {
            button.interactable = false;
        }
    }

    // Call this method to enable all particle systems and make all buttons interactable again
    public void EnableAllInteractions()
    {
        // Enable all particle systems
        ParticleSystem[] particleSystems = FindObjectsOfType<ParticleSystem>();
        foreach (var ps in particleSystems)
        {
            ps.Play();
        }

        // Make all buttons interactable
        Button[] buttons = FindObjectsOfType<Button>();
        foreach (var button in buttons)
        {
            button.interactable = true;
        }
    }
}
