using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectPotionButton : MonoBehaviour
{
    [SerializeField] private PopUpManager _popUpManager;
    [SerializeField] private PotionCabinetManager _potionCabinetManager;
    private LevelManager _levelManager;


    private void OnMouseDown()
    {
        _levelManager = FindObjectOfType<LevelManager>();
        _potionCabinetManager.CompleteLevel(_levelManager.CurrentLevelIndex, ShowPopup);
    }

    private void ShowPopup()
    {
        SceneManager.LoadScene("PopUpScene");
    }
}

