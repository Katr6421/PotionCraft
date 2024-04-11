using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButton : MonoBehaviour
{
    // On click, go back to the level map
    public void GoBack()
    {
        // Save the LevelTrackData object to the LevelTrackManager, with hasCompletedLevel set to false
        LevelTrackManager.Instance.CurrentLevelTrackData.HasCompletedLevel = false;
        LevelTrackManager.Instance.levelTrackDataDictionary[LevelManager.Instance.CurrentLevelIndex].Add(LevelTrackManager.Instance.CurrentLevelTrackData);

        // Load the level map scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("LevelMap");
    }
}
