using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishedLevelButton : MonoBehaviour
{
    [SerializeField] private PopUpManager _popUpManager;

    private void OnMouseDown()
    {
        HandleClick();
    }

    private void HandleClick()
    {
        _popUpManager.LoadPopUpScene();
    }
}

