using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Runtime.InteropServices;

public class LevelTrackManager : MonoBehaviour
{
    public static LevelTrackManager Instance { get; private set; }
    public LevelTrackData CurrentLevelTrackData;
    public float CurrentStartTime { get; set; }

    private void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }


    }



    //private int counter = 100; //test value
    public Dictionary<int, List<LevelTrackData>> levelTrackDataDictionary = new Dictionary<int, List<LevelTrackData>>();


    public void SaveDataToFile()
    {




         // Use Environment to get the desktop path
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string path = Path.Combine(desktopPath, "LevelTrackData.txt");
        
        // Create a StringBuilder to hold the formatted text
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        foreach (var entry in levelTrackDataDictionary)
        {
            foreach (var data in entry.Value)
            {
                // Each LevelTrackData on a new line
                sb.AppendLine($"Level Number: {data.LevelNumber}, Wrong Clicks: {data.WrongClickCounter}, " +
                              $"Time: {data.TimeForCompletingLevel} seconds, Clicks on Rules: {data.ClickOnRulesButton}, " +
                              $"Completed: {data.HasCompletedLevel}");
            }
        }

        // Write the string to a text file
        File.WriteAllText(path, sb.ToString());
        Debug.Log("Data saved to " + path);
    

        // Use Environment to get the desktop path
        /*string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string path = Path.Combine(desktopPath, "counter.txt");
        //File.WriteAllText(path, counter.ToString());
        Debug.Log("Counter saved to " + path);*/

    }


}
