using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UnlockClothes : MonoBehaviour
{
    [SerializeField] private Sprite[] _colors;
    [SerializeField] private GameObject _clothes;
    private Image _imageComponent;
    private float delayBeforeShowing = 2.0f; // Seconds to wait before showing the popup

    void Start()
    {
        _imageComponent = GetComponent<Image>();
        //_clothes.SetActive(false);
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
        _clothes.SetActive(true);

        // If you're using an animator, you could instead set a trigger to play the popup animation
        // Animator animator = unlockPopup.GetComponent<Animator>();
        // if (animator != null)
        // {
        //     animator.SetTrigger("ShowPopup");
        // }
    }
}