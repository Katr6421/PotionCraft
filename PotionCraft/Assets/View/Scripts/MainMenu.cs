using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    //HUSK AT ÆNDRE NAVN PÅ FIL
    
    public void PlayGame()
    {
        SceneManager.LoadScene("LevelMap");
    }


    
}
