using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class LevelSelector : MonoBehaviour
{
    public Button[] LevelButtons { get; private set; }

    // Fields to store the selected level and potion name. UniversalLevelManager will use these fields to display the selected level and potion name

    private void Start()
    {
        // Retrieve the unlocked level from the player preferences
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        // Ensure all buttons are disabled
        for (int i = 0; i < LevelButtons.Length; i++)
        {
            LevelButtons[i].interactable = false;
        }
        // Unlock all levels up to the unlocked level
        for (int i = 0; i < unlockedLevel; i++)
        {
            LevelButtons[i].interactable = true;
        }

    }

    public void OpenScene(int levelIndex)
    {
        // Current level index
        LevelManager.Instance.CurrentLevelIndex = levelIndex;

        // Now that the UI is set, you can load the new scene
        string levelName = "Level" + levelIndex;
        SceneManager.LoadScene(levelName);
    }

}