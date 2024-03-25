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

    [SerializeField] public TextMeshProUGUI SelectedLevelText;

    [SerializeField] public TextMeshProUGUI SelectedLevelPotionName;
    [SerializeField] private GameObject NullCirclePrefab;
    private Vector3 StartNullCirclePosition = new Vector3(578, 239, 0);

    
    public void Start()
    {
        Debug.Log(LevelSelector.selectedLevel);
        Debug.Log(LevelSelector.potionName);
        SelectedLevelText.text = "Level " + LevelSelector.selectedLevel;
        SelectedLevelPotionName.text = LevelSelector.potionName;
        Debug.Log("Besked fra UniversalLevelManager: Nu kalder jeg InstantiateNullCircle");
        InstantiateNullCircle(LevelManager.Instance.GetIngredients(LevelSelector.selectedLevel));
    }

    public void InstantiateNullCircle(List<Node> nodes)
    {
            Debug.Log("Besked fra UniversalLevelManager: Nu laver jeg en NullCircle");
             // Example of instantiating a null circle with NodeData
            GameObject nullCircleObject = Instantiate(NullCirclePrefab, StartNullCirclePosition, Quaternion.identity);
            NodeData nodeDataComponent = nullCircleObject.AddComponent<NodeData>();
            nodeDataComponent.Node = nodes.First();

            
        


    }

 


    
}
