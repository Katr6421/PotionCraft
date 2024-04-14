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
        string path = Path.Combine(desktopPath, "Red_Black_BST.txt");
        
        // Create a StringBuilder to hold the formatted text
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append($"PotionCraft");
        sb.AppendLine();
        sb.AppendLine();

        foreach (var entry in levelTrackDataDictionary)
        {
            if(entry.Key == 4 || entry.Key == 7) sb.AppendLine($"MiniGame!");
            sb.AppendLine($"Level Number: {entry.Key}"); // Assuming you want to label the levels
            
            foreach (var data in entry.Value)
            {
                // Each property on a new line
                sb.AppendLine($"  Level Number: {data.LevelNumber}");
                sb.AppendLine($"  Wrong Clicks: {data.WrongClickCounter}");
                sb.AppendLine($"  Time: {data.TimeForCompletingLevel} seconds");
                sb.AppendLine($"  Clicks on Rules: {data.ClickOnRulesButton}");
                sb.AppendLine($"  Completed: {data.HasCompletedLevel}");
                sb.AppendLine(); // Add an extra blank line for separation between entries
            }
        }

        // Write the string to a text file
        File.WriteAllText(path, sb.ToString());
        Debug.Log("Data saved to " + path);

    }


}
