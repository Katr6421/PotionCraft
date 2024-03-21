using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class LevelMenu : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI potionNameText;
    [SerializeField]
    private Sprite potionImage;
    [SerializeField]
    private TextMeshProUGUI levelNumber;

    private ILevelManager levelManager;
    
    public Button[] levelButtons;

    private void Start(){
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        
        // Ensure all buttons are disabled
        for (int i = 0; i < levelButtons.Length; i++){
            levelButtons[i].interactable = false;
        }
        // Unlock all levels up to the unlocked level
        for (int i = 0; i < unlockedLevel; i++){
            levelButtons[i].interactable = true;
        }
    }

    public void OpenLevel(int levelIndex)
    {
        // Make the level ready to play
        potionNameText.text = levelManager.GetPotionName(levelIndex);
    





        // Show the level when ready to play
        string levelName = "Level" + levelIndex;
        SceneManager.LoadScene(levelName);

    }
}