using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextButton : MonoBehaviour
{
    public void UnLockNextLevel()
    {
        if (LevelManager.Instance.CurrentLevelIndex >= PlayerPrefs.GetInt("ReachedIndex"))
        {
            // Save the reached level index and unlock next level
            PlayerPrefs.SetInt("ReachedIndex", LevelManager.Instance.CurrentLevelIndex + 1);
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
            PlayerPrefs.Save();

        }
        SceneManager.LoadScene("LevelMap");
    }
}