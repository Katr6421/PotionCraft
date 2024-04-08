using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PopUpManager : MonoBehaviour
{
    ///  DOESNT WORK :((())) I want to change the image of the potion but the OnSceneLoaded method never runs

    [SerializeField] GameObject _potionImage;
    private LevelManager _levelManager;

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // Register the method to be called when the scene is loaded
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Unregister the method to avoid memory leaks
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _levelManager = FindObjectOfType<LevelManager>(); // Find the LevelManager when the scene is loaded
        UpdatePotionImage(); // Update the potion image after the scene is loaded
    }

    private void UpdatePotionImage()
    {
        if (_levelManager != null)
        {
            // Get the potion image from the level manager
            Sprite potionSprite = _levelManager.GetPotionSprite();
            if (_potionImage != null)
            {
                Debug.Log("Potion image found.");
                SpriteRenderer spriteRenderer = _potionImage.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    spriteRenderer.sprite = potionSprite;
                    _potionImage.transform.localScale = new Vector3(7, 7, 7); // Adjust the scale as needed

                }
                else
                {
                    Debug.LogError("SpriteRenderer component not found on potion image object.");
                }
            }
            else
            {
                Debug.LogError("Potion image object not found in the scene.");
            }
        }
        else
        {
            Debug.LogError("LevelManager not found.");
        }
    }
}
