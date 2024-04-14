using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MoveToNextIntroScene : MonoBehaviour
{
    public void OnClickNext(Button button) {
        switch (button.name) {
            case "Next1":
                SceneManager.LoadScene("Intro2");
                break;
            case "Next2":
                SceneManager.LoadScene("Intro3");
                break;
            case "Next3":
                SceneManager.LoadScene("StartScene");
                break;
            default:
                Debug.Log("Button not found");
                break;
        }
    }
}
