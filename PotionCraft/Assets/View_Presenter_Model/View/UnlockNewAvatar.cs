using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UnlockNewAvatar : MonoBehaviour
{
    [SerializeField] private Sprite[] _colors;
    [SerializeField] private GameObject _newAvatar;
    private float delayBeforeShowing = 1.0f; // Seconds to wait before showing the popup

    void Start()
    {
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
        _newAvatar.SetActive(true);
    
    }
}