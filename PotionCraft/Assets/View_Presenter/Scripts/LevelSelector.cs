using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class LevelSelector : MonoBehaviour
{
    
    public Button[] levelButtons;

    // Fields to store the selected level and potion name. UniversalLevelManager will use these fields to display the selected level and potion name
    public static int selectedLevel;
    public static string potionName;


    

    private void Start(){
        // Retrieve the unlocked level from the player preferences
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

    public void OpenScene(int levelIndex)
{
    //Store the selected level index in a static variable
    selectedLevel = levelIndex;
    // Retrieve the potion name and level index using the singleton instance, which is safer
    potionName = LevelManager.Instance.GetPotionName(levelIndex);

    // Now that the UI is set, you can load the new scene
    string levelName = "Level" + levelIndex;
    SceneManager.LoadScene(levelName);
}

}