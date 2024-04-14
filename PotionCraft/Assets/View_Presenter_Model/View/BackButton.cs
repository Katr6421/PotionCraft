using System.Collections.Generic;
using UnityEngine;

public class BackButton : MonoBehaviour
{
    // On click, go back to the level map
    public void GoBack()
    {
        // Check if the key already exists in the dictionary
        if (!LevelTrackManager.Instance.levelTrackDataDictionary.ContainsKey(LevelManager.Instance.CurrentLevelIndex))
        {
            // If the key does not exist, add a new key with an empty list
            LevelTrackManager.Instance.levelTrackDataDictionary[LevelManager.Instance.CurrentLevelIndex] = new List<LevelTrackData>();
        }
        // Set the time for levelTrackData. 
        LevelTrackManager.Instance.CurrentLevelTrackData.TimeForCompletingLevel = Time.time - LevelTrackManager.Instance.CurrentStartTime;
        // Save the LevelTrackData object to the LevelTrackManager, with hasCompletedLevel set to false
        LevelTrackManager.Instance.CurrentLevelTrackData.HasCompletedLevel = false;
        LevelTrackManager.Instance.levelTrackDataDictionary[LevelManager.Instance.CurrentLevelIndex].Add(LevelTrackManager.Instance.CurrentLevelTrackData);

        // Load the level map scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("LevelMap");
    }

    public void GoBackToMainMenu() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("LevelMap");
    }
}
