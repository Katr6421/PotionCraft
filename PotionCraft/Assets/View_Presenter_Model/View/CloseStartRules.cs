using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseStartRules : MonoBehaviour

{
    [SerializeField] private GameObject _startGameRulesOverlay;
    // Start is called before the first frame update
    private void OnMouseDown()
    {
        // Call HideRulesBox() when the sprite is clicked
        _startGameRulesOverlay.SetActive(false);
        EnableAllInteractions();
    }

    public void EnableAllInteractions()
    {
        // Enable all particle systems
        ParticleSystem[] particleSystems = FindObjectsOfType<ParticleSystem>();
        foreach (var ps in particleSystems)
        {
            ps.Play();
        }

        // Make all buttons interactable
       /* Button[] buttons = FindObjectsOfType<Button>();
        foreach (var button in buttons)
        {
            button.interactable = true;
        }*/
    }
}
