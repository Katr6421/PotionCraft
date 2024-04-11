using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UnlockNewAvatar : MonoBehaviour
{
    [SerializeField] private GameObject _popUp;
    [SerializeField] private GameObject _newAvatar;
    private LevelManager _levelManager;
    private float delayBeforeShowing = 1.0f; // Seconds to wait before showing the popup

    void Start()
    {
        _levelManager = LevelManager.Instance;
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

        // Change the sprite of the new avatar
        if (_levelManager.CurrentLevelIndex == 4) {
            _newAvatar.GetComponent<Image>().sprite = _levelManager.GetAvatarSprite(1);
        } else {
            _newAvatar.GetComponent<Image>().sprite = _levelManager.GetAvatarSprite(2);
        }

        // Show the popup
        _popUp.SetActive(true);
    
    }
}