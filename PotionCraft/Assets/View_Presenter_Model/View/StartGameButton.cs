using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameButton : MonoBehaviour
{
    [SerializeField] private GameObject _goalOverlay;

    private void OnMouseDown()
    {
        // Hide the goal overlay to begin the game
        _goalOverlay.SetActive(false);

    }
}
