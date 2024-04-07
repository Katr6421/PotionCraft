using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayAgainButton : MonoBehaviour
{
    public void LoadLevelAgain(){

        Debug.Log("Loading level again");
        Debug.Log("Selected level: " + LevelSelector.selectedLevel);
        int levelIndex = LevelSelector.selectedLevel;
        // Now that the UI is set, you can load the new scene
        string levelName = "Level" + levelIndex;
        SceneManager.LoadScene(levelName);
    
    }
}
