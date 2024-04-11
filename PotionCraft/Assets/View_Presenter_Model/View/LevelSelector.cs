using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LevelSelector : MonoBehaviour
{
    [SerializeField]
    public Button[] LevelButtons;


    /*********************************************
    Fields to store the selected level and potion name.
    UniversalLevelManager will use these fields to display the selected level and potion name
    *********************************************/

    private void Start()
    {
        /*********************************************
        Retrieve the unlocked level from the player preferences
        *********************************************/
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        //Debug.Log("LevelSelector Start, unlockedLevel: " + unlockedLevel);


        /*********************************************
        Ensure all buttons are disabled
        *********************************************/
        for (int i = 0; i < LevelButtons.Length; i++)
        {
            LevelButtons[i].interactable = false;
        }

        /*********************************************
        Unlock all levels up to the unlocked level
        *********************************************/
        for (int i = 0; i < unlockedLevel; i++)
        {
            LevelButtons[i].interactable = true;
            //Debug.Log("Make interactable, LevelButtons[" + i + "] interactable: " + LevelButtons[i].interactable);
        }

    }

    public void OpenScene(int levelIndex)
    {
        /*********************************************
        Current level index
        *********************************************/
        LevelManager.Instance.CurrentLevelIndex = levelIndex;

        // Instantiate a new LevelTrackData object. Save the reference into LevelTrackManager that keeps track of the current LevelTrackData
        LevelTrackManager.Instance.CurrentLevelTrackData = new LevelTrackData(LevelManager.Instance.CurrentLevelIndex);
        // Start a timer, that we can use for TimeForCompletingLevel
        LevelTrackManager.Instance.CurrentStartTime = Time.time;


        /*********************************************
        Now that the UI is set, you can load the new scene
        *********************************************/
        string levelName = "Level" + levelIndex;
        SceneManager.LoadScene(levelName);
    }

}