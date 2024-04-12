using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveToFile : MonoBehaviour
{
    // Start is called before the first frame update
    public void SaveToFileButton()
    {
        
        LevelTrackManager.Instance.SaveDataToFile();
        Quit();
        

    }

    public void Quit(){
        Application.Quit();
    }
}
