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

    [SerializeField] public Canvas uiCanvas;
    [SerializeField] public TextMeshProUGUI SelectedLevelText;

    [SerializeField] public TextMeshProUGUI SelectedLevelPotionName;
    [SerializeField] private GameObject CircleMarkerPrefab;
    [SerializeField] private GameObject Frame;
    private GameObject CircleMarker;
    private Vector3 circleStartPosition = new Vector3(2.12f, 3.79f, 0);
    private FrameController frameController;
 
    
    public void Start()
    {
        //When the scene starts, we update the recipe text.
        SelectedLevelText.text = "Level " + LevelSelector.selectedLevel;
        SelectedLevelPotionName.text = LevelSelector.potionName;
        frameController = FindObjectOfType<FrameController>();
    
        // When the scene starts, we instantiate the CircleMarker
        SpawnCircleMarker();

    }
   

    public void SpawnCircleMarker(){
        CircleMarker = Instantiate(CircleMarkerPrefab, circleStartPosition, Quaternion.identity);
        //CircleMarker.transform.SetParent(uiCanvas.transform, false);
    }

    public Canvas getUiCanvas(){
        return uiCanvas;

    }

    // Moves the circle marker to the new position

    public void MoveCircleMarker(Vector3 newPosition, float duration)
    {
        StartCoroutine(MoveCircleRoutine(CircleMarker, newPosition, duration));
    }

    // ANIMATION: Coroutine to move the circle marker to the new position. 
    IEnumerator MoveCircleRoutine(GameObject objectToMove, Vector3 destination, float duration)
    {
    float elapsedTime = 0;
    Vector3 startingPos = objectToMove.transform.position;

    while (elapsedTime < duration)
    {
        objectToMove.transform.position = Vector3.Lerp(startingPos, destination, (elapsedTime / duration));
        elapsedTime += Time.deltaTime;
        yield return null; // Wait for the next frame
    }

    objectToMove.transform.position = destination; // Ensure it reaches the destination
    }

    
}

