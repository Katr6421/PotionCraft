using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayAgainButton : MonoBehaviour
{
    LevelManager _levelManager;

    void Start()
    {
        _levelManager = LevelManager.Instance;
    }

    public void LoadLevelAgain()
    {
        Debug.Log("Selected level: " + _levelManager.CurrentLevelIndex);
        int levelIndex = _levelManager.CurrentLevelIndex;
        // Now that the UI is set, you can load the new scene
        string levelName = "Level" + levelIndex;
        SceneManager.LoadScene(levelName);
    }
}
