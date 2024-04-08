using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectPotionButton : MonoBehaviour
{
    [SerializeField] private PopUpManager _popUpManager;
    [SerializeField] private PotionCabinetManager _potionCabinetManager;
    private LevelManager _levelManager;

    private void OnMouseDown()
    {
        StartCoroutine(CollectPotionAndShowPopup());
    }

    private IEnumerator CollectPotionAndShowPopup()
    {
        // Get the level manager
        _levelManager = FindObjectOfType<LevelManager>();

        // Call the method to place the potion in the cabinet
        _potionCabinetManager.CompleteLevel(_levelManager.CurrentLevelIndex);

        // Wait for a few seconds
        yield return new WaitForSeconds(3);

        // Then load the popup
        _popUpManager.LoadPopUpScene();
    }
}

