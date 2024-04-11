using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MiniGame : MonoBehaviour
{
    [SerializeField] UnlockNewAvatar _unlockClothes;
    private Color _targetColor;
    private float _transitionTime = 0.2f;

    
    void Start()
    {

    }


    public void OnClicked(Button button)
    {
        if (button.name == "Correct")
        {
            // Stop time

            _targetColor = new Color(74f / 255f, 160f / 255f, 144f / 255f, 0.5f);
            StartCoroutine(OverlayWithColor(button, _targetColor, true));

            _unlockClothes.ShowUnlockPopup();
        }
        else
        {
            _targetColor = new Color(0.4745098f, 0.1372549f, 0.1215686f, 0.5f);
            StartCoroutine(OverlayWithColor(button, _targetColor, false));

        }


        // update hint?
        // if the answer is correct, show the the collect potion button
    }

    // Coroutine to flash the button with overlay color
    private IEnumerator OverlayWithColor(Button button, Color targetColor, bool isCorrect)
    {
        // Disable the button to prevent further clicks
        button.interactable = false;

        Color originalColor = button.image.color; // Original button color
        // We're going to use the original alpha, ensuring the image underneath does not become transparent
        float originalAlpha = originalColor.a;
        float elapsedTime = 0;

        // Change color to red with easing
        while (elapsedTime < _transitionTime)
        {
            Color transitionColor = Color.Lerp(originalColor, targetColor, (elapsedTime / _transitionTime));
            transitionColor.a = originalAlpha; // Apply the original alpha to the transition color
            button.image.color = transitionColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }


        // Ensure the target color is set (in case the while loop doesn't hit it exactly)
        targetColor.a = originalAlpha; // Apply the original alpha to the target color
        button.image.color = targetColor;


        if (!isCorrect)
        {
            // Hold the red color for a moment
            yield return new WaitForSeconds(0.01f);

            // Change the color back to the original color with easing
            elapsedTime = 0;
            while (elapsedTime < _transitionTime)
            {
                Color transitionColor = Color.Lerp(targetColor, originalColor, (elapsedTime / _transitionTime));
                transitionColor.a = originalAlpha; // Apply the original alpha to the transition color
                button.image.color = transitionColor;
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Ensure the original color is set
            button.image.color = originalColor;

            // Re-enable the button after the whole process
            button.interactable = true;

            // Unselect the button
            EventSystem.current.SetSelectedGameObject(null);
            yield return null;
        }
        
    }

}