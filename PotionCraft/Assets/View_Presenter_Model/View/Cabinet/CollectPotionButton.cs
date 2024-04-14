using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectPotionButton : MonoBehaviour
{
    [SerializeField] private PotionCabinetManager _potionCabinetManager;
    private LevelManager _levelManager;
    private PointManager _pointManager;
    private bool _hasClicked; // To avoid multiple clicks


    private void OnMouseDown()
    {
        if (!_hasClicked)
        {
            _hasClicked = true;
            _levelManager = FindObjectOfType<LevelManager>();
            _pointManager = FindObjectOfType<PointManager>();

            // Get 1 points
            _pointManager.AddPoints(1);

            // Normal level (not minigame) - Put potion in cabinet and show popup
            if (!_levelManager.Levels[_levelManager.CurrentLevelIndex].IsMiniGame){
                _potionCabinetManager.CompleteLevel(_levelManager.CurrentLevelIndex, ShowPopup);
            }
            // Mini game - Show popup
            else
            {
                ShowPopup();
            }
        }
    }

    private void ShowPopup()
    {
        _hasClicked = false;

        // if level 10 is completed, go to end scene
        if (_levelManager.CurrentLevelIndex == 10) {
            SceneManager.LoadScene("EndScene");
        } else {
            SceneManager.LoadScene("PopUpScene");
        }
        
    }
}

