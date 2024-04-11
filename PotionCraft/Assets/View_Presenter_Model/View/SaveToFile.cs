using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Runtime.InteropServices;

public class SaveToFile : MonoBehaviour
{

    private int counter = 100; //test value


    public void SaveDataToFile(){

        // Use Environment to get the desktop path
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string path = Path.Combine(desktopPath, "counter.txt");
        File.WriteAllText(path, counter.ToString());
        Debug.Log("Counter saved to " + path);

       

        /*
        
         string path = Path.Combine(Application.persistentDataPath, "PotionCraftData.txt");
        File.WriteAllText(path, counter.ToString());
        Debug.Log("Counter saved to " + path);
        
        
        */


    }

    /*public void SaveCounterToFile(int counter)
    {
        // Open file dialog
        var extensions = new[] {
            new ExtensionFilter("Text Files", "txt"),
            new ExtensionFilter("All Files", "*" ),
        };
        string path = StandaloneFileBrowser.SaveFilePanel("Save Counter", "", "counter", extensions);

        if (!string.IsNullOrEmpty(path))
        {
            File.WriteAllText(path, counter.ToString());
            Debug.Log("Counter saved to " + path);
        }
    }*/
}
