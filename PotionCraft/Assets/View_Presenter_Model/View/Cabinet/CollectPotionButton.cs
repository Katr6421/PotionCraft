using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.Mathematics;
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

            // Put the potion in the cabinet or show a popup if the potion has already been collected
            _potionCabinetManager.CompleteLevel(_levelManager.CurrentLevelIndex, ShowPopup);
        }
    }

    private void ShowPopup()
    {
        _hasClicked = false;
        SceneManager.LoadScene("PopUpScene");
    }
}

