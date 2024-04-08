using UnityEngine;
using UnityEngine.SceneManagement;

public class StartPlayButton : MonoBehaviour
{
    //HUSK AT ÆNDRE NAVN PÅ FIL

    public void PlayGame()
    {
        SceneManager.LoadScene("LevelMap");
    }

}
