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

        /********************************************
        Add the current levelTrackData object to the list for the selected level
        ********************************************/

        // Check if the key already exists in the dictionary
        if (!LevelTrackManager.Instance.levelTrackDataDictionary.ContainsKey(levelIndex))
        {
            // If the key does not exist, add a new key with an empty list
            LevelTrackManager.Instance.levelTrackDataDictionary[levelIndex] = new List<LevelTrackData>();
        }

        // Add the current levelTrackData to the list for this level number
        LevelTrackManager.Instance.levelTrackDataDictionary[levelIndex].Add(LevelTrackManager.Instance.CurrentLevelTrackData);

        // Reset the currentLevelTrackData
        LevelTrackManager.Instance.CurrentLevelTrackData = new LevelTrackData(levelIndex);
        // Start a timer, that we can use for TimeForCompletingLevel
        LevelTrackManager.Instance.CurrentStartTime = Time.time;


        // Now that the UI is set, you can load the new scene
        string levelName = "Level" + levelIndex;
        SceneManager.LoadScene(levelName);
    }
}
