using System.Collections;
using System.Collections.Generic;
using log4net.Core;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System;

public class UniversalLevelManager : MonoBehaviour
{

    [SerializeField]
    public TextMeshProUGUI SelectedLevelText;

    [SerializeField]
    public TextMeshProUGUI SelectedLevelPotionName;

    
    public void Start()
    {
        Debug.Log(LevelSelector.selectedLevel);
        Debug.Log(LevelSelector.potionName);
        SelectedLevelText.text = "Level " + LevelSelector.selectedLevel;
        SelectedLevelPotionName.text = LevelSelector.potionName;
        
    }


    
}
