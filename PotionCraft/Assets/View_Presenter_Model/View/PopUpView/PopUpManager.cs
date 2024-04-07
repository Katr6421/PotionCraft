using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PopUpManager : MonoBehaviour
{
    public void LoadPopUpScene()
    {
        SceneManager.LoadScene("PopUpScene");
    }
}
