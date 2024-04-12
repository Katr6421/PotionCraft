using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameButton : MonoBehaviour
{
    [SerializeField] private GameObject _goalOverlay;
    [SerializeField] private GameObject _startRulesOverlay;


    private void OnMouseDown()
    {
        // Hide the goal overlay
        _goalOverlay.SetActive(false);

        // Show how to play 
        _startRulesOverlay.SetActive(true);
        // Disable all particle systems
        DisableAllInteractions();
    }


     public void DisableAllInteractions()
    {
        // Disable all particle systems
        ParticleSystem[] particleSystems = FindObjectsOfType<ParticleSystem>();
        foreach (var ps in particleSystems)
        {
            ps.Stop();
            ps.Clear();
        }

        // Make all buttons non-interactable
       /*Button[] buttons = FindObjectsOfType<Button>();
        foreach (var button in buttons)
        {
            button.interactable = false;
        }*/
    }
}
