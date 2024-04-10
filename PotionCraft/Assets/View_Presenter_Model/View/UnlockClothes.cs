using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UnlockClothes : MonoBehaviour
{
    [SerializeField] private Sprite[] _colors;
    [SerializeField] private GameObject _clothes;
    private float delayBeforeShowing = 1.0f; // Seconds to wait before showing the popup

    void Start()
    {
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


        
        // To change the color of the clothes
        //_clothes.GetComponent<Image>().sprite = _colors[1];        

    
    }
}