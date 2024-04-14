using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void ExitGame()
    {
        // Debug.Log is used to show the message in Unity Editor's console
        Debug.Log("Exit Game called");

        // Application.Quit() does not work in the editor, so it's commented out here
        // Uncomment this line when building the game
        // Application.Quit();
        
        #if UNITY_EDITOR
        // This line is for exiting the game in the Unity Editor.
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
