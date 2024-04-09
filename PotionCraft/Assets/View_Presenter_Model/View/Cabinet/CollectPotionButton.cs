using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectPotionButton : MonoBehaviour
{
    [SerializeField] private PopUpManager _popUpManager;
    [SerializeField] private PotionCabinetManager _potionCabinetManager;
    private LevelManager _levelManager;
    private PointManager _pointManager;


    private void OnMouseDown()
    {
        _levelManager = FindObjectOfType<LevelManager>();
        _pointManager = FindObjectOfType<PointManager>();

        // Get 3 points
        _pointManager.AddPoints(1);

        // Put the potion in the cabinet or show a popup if the potion has already been collected
        _potionCabinetManager.CompleteLevel(_levelManager.CurrentLevelIndex, ShowPopup);
    }

    private void ShowPopup()
    {
        SceneManager.LoadScene("PopUpScene");
    }
}

