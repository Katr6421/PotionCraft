using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGame : MonoBehaviour
{
    [SerializeField] UnlockClothes _unlockClothes;
    private float _transitionTime = 0.2f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClicked(Button button)
    {


        // if the answer is correct, make the button sparkle
        if (button.name == "Correct")
        {
            // sparkle 
            Debug.Log("Correct answer");
            StartCoroutine(ShowRightAnswer(button));
            _unlockClothes.ShowUnlockPopup();
        }
        // if the answer is wrong, make a little red glow around the button
        else
        {
            // glow red
            Debug.Log("Wrong answer");
            StartCoroutine(ShowWrongAnswer(button));

        }


        // update hint?
        // if the answer is correct, show the the collect potion button
    }

    // Coroutine to flash the button red
    private IEnumerator ShowWrongAnswer(Button button)
    {
        //Color targetColor = new Color(1, 0, 0, 0.8f); // Transparent red
        Color targetColor = new Color(0.4745098f, 0.1372549f, 0.1215686f, 0.5f); // Corresponds to #79231F
        Color originalColor = button.image.color; // Original button color
        float elapsedTime = 0;

        // Change color to red with easing
        while (elapsedTime < _transitionTime)
        {
            button.image.color = Color.Lerp(originalColor, targetColor, (elapsedTime / _transitionTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the target color is set (in case the while loop doesn't hit it exactly)
        button.image.color = targetColor;

        // Hold the red color for a moment
        yield return new WaitForSeconds(0.01f);

        // Change the color back to the original color with easing
        elapsedTime = 0;
        while (elapsedTime < _transitionTime)
        {
            button.image.color = Color.Lerp(targetColor, originalColor, (elapsedTime / _transitionTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the original color is set
        button.image.color = originalColor;
    }

    private IEnumerator ShowRightAnswer(Button button)
    {
        //Color targetColor = new Color(1, 0, 0, 0.8f); // Transparent red
        Color targetColor = new Color(74f / 255f, 160f / 255f, 144f / 255f, 0.5f); // Corresponds to #4AA090
        Color originalColor = button.image.color; // Original button color
        float elapsedTime = 0;

        // Change color to red with easing
        while (elapsedTime < _transitionTime)
        {
            button.image.color = Color.Lerp(originalColor, targetColor, (elapsedTime / _transitionTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the target color is set (in case the while loop doesn't hit it exactly)
        button.image.color = targetColor;
    }

    public void CompleteLevel()
    {
        Debug.Log("Level complete");
    }

}