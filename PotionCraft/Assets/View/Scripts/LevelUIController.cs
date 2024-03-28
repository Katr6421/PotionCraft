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
    [SerializeField] private GameObject NullCirclePrefab;
    private Vector3 StartNullCirclePosition = new Vector3(578, 239, 0);
 
    
    public void Start()
    {
        //When the scene starts, we update the recipe text.
        SelectedLevelText.text = "Level " + LevelSelector.selectedLevel;
        SelectedLevelPotionName.text = LevelSelector.potionName;
        //When the scene starts, we instantiate the first NullCircle aka. the root of the RedBlackTree
        SpawnRoot();

    }
    public void SpawnRoot(){
        GameObject nullCircle = Instantiate(NullCirclePrefab, StartNullCirclePosition, Quaternion.identity);
        nullCircle.transform.SetParent(uiCanvas.transform, false);
    }

    public Canvas getUiCanvas(){
        return uiCanvas;

    }
}

