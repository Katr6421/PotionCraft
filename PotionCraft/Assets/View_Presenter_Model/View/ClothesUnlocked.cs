using UnityEngine;
using System.Collections;

public class ClothesUnlocked : MonoBehaviour
{
    [SerializeField] private GameObject _unicorn; // Assign the popup GameObject in the inspector
    private float delayBeforeShowing = 2.0f; // Seconds to wait before showing the popup

    void Start()
    {
        _unicorn.SetActive(true);
    }

    // Call this method when the correct answer is chosen
    public void ShowUnlockPopup()
    {
        StartCoroutine(ShowPopupAfterDelay());
    }

    private IEnumerator ShowPopupAfterDelay()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delayBeforeShowing);

        // Show the popup
        _unicorn.SetActive(true);

        // If you're using an animator, you could instead set a trigger to play the popup animation
        // Animator animator = unlockPopup.GetComponent<Animator>();
        // if (animator != null)
        // {
        //     animator.SetTrigger("ShowPopup");
        // }
    }
}