using UnityEngine;

public class SpriteClickDetector : MonoBehaviour
{
    public RulesButton rulesButton; // Assign this in the inspector

    private void OnMouseDown()
    {
        // Call HideRulesBox() when the sprite is clicked
        rulesButton.HideRulesBox();
    }
}

