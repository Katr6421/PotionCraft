using System.Collections;
using System.Collections.Generic;
using log4net.Core;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System;
using System.Linq;

/*********************************************
CLASS: LevelUIController
DESCRIPTION: This class is responsible for controlling the UI elements in the Level scene.
             Also, it is responsible for instantiating the NullCircle aka. the root
*********************************************/

public class LevelUIController : MonoBehaviour
{

    [SerializeField] private Canvas _uiCanvas;
    [SerializeField] private TextMeshProUGUI _selectedLevelNumberText;
    [SerializeField] private TextMeshProUGUI _selectedLevelPotionName;
    [SerializeField] private TextMeshProUGUI _selectedLevelPotionDescription;
    [SerializeField] private GameObject _selectedLevelPotionSprite;
    [SerializeField] private GameObject _finishedScroll; // Scroll that appears when a level is completed
    [SerializeField] private GameObject _collectPotionButton;
    [SerializeField] private GameObject _circleMarkerPrefab;
    [SerializeField] private NodeSpawner _nodeSpawner;
    LevelManager _levelManager;
    public GameObject CircleMarker { get; set; }
    private Vector3 _circleStartPosition = new Vector3(-0.57f, 3.79f, 0);


    public void Start()
    {
        _collectPotionButton.GetComponent<BoxCollider2D>().enabled = false; // so you dont press the complete button before the potion is complete
        _levelManager = LevelManager.Instance;

        /***********************************
        Update recipe
        ***********************************/     
        UpdateRecipe();


        /***********************************
        Instantiate the CircleMarker
        ***********************************/
        SpawnCircleMarker();
    }

    public void SpawnCircleMarker()
    {
        CircleMarker = Instantiate(_circleMarkerPrefab, _circleStartPosition, Quaternion.identity);
        //CircleMarker.transform.SetParent(_uiCanvas.transform, false);
    }

    // Moves the circle marker to the new position
    public void MoveCircleMarker(Vector3 newPosition, float duration)
    {
        StartCoroutine(MoveObjectRoutine(CircleMarker, newPosition, duration));
    }

    // ANIMATION: Coroutine to move the circle marker to the new position. 
    IEnumerator MoveObjectRoutine(GameObject objectToMove, Vector3 destination, float duration)
    {
        float elapsedTime = 0;
        Vector3 startingPos = objectToMove.transform.position;

        while (elapsedTime < duration)
        {
            objectToMove.transform.position = Vector3.Lerp(startingPos, destination, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        objectToMove.transform.position = destination; // Ensure it reaches the destination
    }

    // Shows or hides circle marker based on the boolean value
    public void ShowCircleMarker(bool shouldBeShown)
    {
        CircleMarker.SetActive(shouldBeShown);
    }

    public void UpdateRecipe(){
        // Change text fields
        _selectedLevelNumberText.text = "" + _levelManager.CurrentLevelIndex;
        _selectedLevelPotionName.text = "" + _levelManager.GetPotionName();
        _selectedLevelPotionDescription.text = "" + _levelManager.GetPotionDescription();
        // Change sprite
        SpriteRenderer spriteRenderer = _selectedLevelPotionSprite.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = _levelManager.GetPotionSprite();
        // Display set of ingredients on recipe
        _nodeSpawner.SpawnIngredientsOnRecipe();
    }

    // When a level is completed, this method is called to move the scroll down to reveal a complete level button
    public void MoveScrollDown()
    {
        // Make the button visible
        _collectPotionButton.GetComponent<BoxCollider2D>().enabled = true;

        // Calculate the target position by moving down by 0.6 units
        Vector3 scrollTargetPosition = _finishedScroll.transform.position + Vector3.down * 0.8f; // Adjust how far to move down here

        // Start the coroutine to move the scroll smoothly to the target position
        StartCoroutine(MoveObjectRoutine(_finishedScroll, scrollTargetPosition, 0.5f));
    }

}

