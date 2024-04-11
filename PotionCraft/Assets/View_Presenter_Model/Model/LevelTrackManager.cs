using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Runtime.InteropServices;

public class LevelTrackManager : MonoBehaviour
{
    public static LevelTrackManager Instance { get; private set; }
    public LevelTrackData CurrectLevelTrackData;
    public float CurrentStartTime {get; set;}

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


    public void SaveDataToFile(){

        // Use Environment to get the desktop path
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string path = Path.Combine(desktopPath, "counter.txt");
        //File.WriteAllText(path, counter.ToString());
        Debug.Log("Counter saved to " + path);

    }

    
}
