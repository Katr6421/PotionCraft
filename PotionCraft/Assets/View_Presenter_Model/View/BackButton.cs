using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButton : MonoBehaviour
{
    // On click, go back to the level map
    public void GoBack()
    {
        // TODO: Save the LevelTrackData object to the LevelTrackManager, with hasCompletedLevel set to false

        // Load the level map scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("LevelMap");
    }
}
