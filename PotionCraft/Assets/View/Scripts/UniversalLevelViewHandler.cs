using System.Collections;
using System.Collections.Generic;
using log4net.Core;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System;
using System.Linq;


public class UniversalLevelManager : MonoBehaviour
{

    [SerializeField] private Canvas uiCanvas;
    [SerializeField] public TextMeshProUGUI SelectedLevelText;

    [SerializeField] public TextMeshProUGUI SelectedLevelPotionName;
    [SerializeField] private GameObject NullCirclePrefab;
    private Vector3 StartNullCirclePosition = new Vector3(578, 239, 0);
    private NodeSpawner NodeSpawner;
    private List<GameObject> nodeGameObjects = new List<GameObject>();
    public GameObject CurrentIngredientMarkerPrefab;
    public GameObject circlePrefab; // Assign this in the Inspector
    


    
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

  





    }


