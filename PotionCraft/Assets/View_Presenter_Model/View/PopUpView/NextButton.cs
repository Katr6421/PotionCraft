using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextButton : MonoBehaviour
{
    public void UnLockNextLevel()
    {
        int levelIndex = LevelManager.Instance.CurrentLevelIndex;

        if (LevelManager.Instance.CurrentLevelIndex >= PlayerPrefs.GetInt("ReachedIndex"))
        {
            // Save the reached level index and unlock next level
            PlayerPrefs.SetInt("ReachedIndex", LevelManager.Instance.CurrentLevelIndex + 1);
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
            PlayerPrefs.Save();

        }

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

        SceneManager.LoadScene("LevelMap");
    }
}