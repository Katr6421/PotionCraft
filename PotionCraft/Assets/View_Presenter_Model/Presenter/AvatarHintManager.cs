using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AvatarHintManager : MonoBehaviour, IAvatarHintManager
{
    [SerializeField] TextMeshProUGUI _avatarHintText;  // This is the text that will be displayed in the hint box
    [SerializeField] private GameObject _avatarHintBoks; // The Script that is attached to the hint box
    [SerializeField] private Sprite[] _avatarHintSprites; // The sprites that will be displayed in the hint box
    //[SerializeField] private Sprite[] _avatarSprites;
    //[SerializeField] private Sprite[] _HintBoxSprites;
    [SerializeField] private AnimationCurve shakeCurve;
    public Dictionary<AvatarHint, string> HintsDict { get; set; } = new Dictionary<AvatarHint, string>();
    private string _currentAvatarState;


    private void Awake()
    {
        HideAvatarHintBoks();
    }

    public AvatarHintManager()
    {
        HintsDict.Add(AvatarHint.InsertFirstIngredient, "Welcome to the potion crafting chamber! Let's start by placing the first ingredient in the potion tree");
        HintsDict.Add(AvatarHint.SelectedWrongPlacementForIngredient, "Hmm, placing that ingredient there won’t help us craft the perfect potion. Let’s find its proper place");
        HintsDict.Add(AvatarHint.SelectedRightPlacementAndInBalance, "Perfect placement! The potion's mystical balance remains undisturbed. Onward to our next ingredient");
        HintsDict.Add(AvatarHint.SelectedRightPlacementButNeedsToSelectTwoNodes, "You've found the right spot! However, the potion is unbalanced. Select two ingredients and find the operation that will restore harmony");
        HintsDict.Add(AvatarHint.SelectedRightPlacementButNeedsToSelectThreeNodes, "You've found the right spot! However, the potion is unbalanced. Select three ingredients and find the operation that will restore harmony");
        HintsDict.Add(AvatarHint.SelectedRightPlacementButNeedToFlipColor, "You've found the right spot! However, the potion is unbalanced. Select three ingredients and find the operation that will restore harmony");
        HintsDict.Add(AvatarHint.SelectedRightIngredientsButWrongButton, "You have chosen the ingredients wisely but not found the correct operation. Please try again");
        HintsDict.Add(AvatarHint.SelectedRightIngredientsAndButton, "Splendid! Your chosen magical twist will bring our potion back in balance");
        HintsDict.Add(AvatarHint.SelectedRightButtonButNeedsToSelectTwoNodes, "The right operation, but not quite there yet. We need to adjust the selected ingredients to keep our potion stable");
        HintsDict.Add(AvatarHint.SelectedRightButtonButWrongIngredients, "The right operation, but not quite there yet. We need to adjust the selected ingredients to keep our potion stable");
        HintsDict.Add(AvatarHint.SelectedWrongIngredientsAndButton, "Oh no, both the ingredients and the operation were incorrect. We must reconsider our choices");
        HintsDict.Add(AvatarHint.NeedsToSelectThreeNodes, "Your magical manipulation was successful, but the potion is still off. Select three ingredients and find the operation that will restore harmony");
        HintsDict.Add(AvatarHint.NeedsToSelectTwoNodes, "Your magical manipulation was successful, but the potion is still off. Select two ingredients and find the operation that will restore harmony");
        HintsDict.Add(AvatarHint.NeedsToSelectTwoNodesToFlipColor, "Your magical manipulation was successful, but the potion is still off. Select three ingredients and find the operation that will restore harmony");
        HintsDict.Add(AvatarHint.InBalance, "Exquisite! The potion is balanced, and a new ingredient can now be introduced");
        HintsDict.Add(AvatarHint.NodeInTheJar, "During the rotation, a sub-potion was displaced and awaits a new place in the potion. It has been placed in the jar for now.");
        HintsDict.Add(AvatarHint.PotionBrewed, "The potion is brewed to perfection! The magic is complete, and your reward is ready to be collected");

    }

    // Call this method with "correct" or "hint" and the corresponding enum
    public void UpdateHint(String spriteName, AvatarHint avatarEnum)
    {
        _currentAvatarState = spriteName;

        ChangeAvatarHintBoxSprinte(spriteName);
        SetHint(avatarEnum);

        // If the user inserts a ingredient correct and the tree is in balance
        if (spriteName.Equals("correct"))
        {
            // Has a bug. If i call it, and then call it again, the first one will end before the secound one, resulting in hiding it to early
            // StartCoroutine(ShowSpriteTemporarily(_avatarHintBoks, 30f)); // Call the coroutine with 10 seconds
            ShowAvatarHintBoks();
        }

        //if the user selects something total wrong
        else if (spriteName.Equals("wrong"))
        {

            ShowAvatarHintBoks();
            StartCoroutine(TriggerShakeText(1f, 10.1f));
            StartCoroutine(TriggerShakeBox(1f, 0.1f));

        }
        // If subtree in jar or 
        // If the user select something correct but that lead to the tree is unblancede (hint)
        else
        {
            ShowAvatarHintBoks();
        }
    }



    public string GetHint(AvatarHint avatarEnum)
    {
        return HintsDict[avatarEnum];
    }

    public void SetHint(AvatarHint avatarEnum)
    {
        _avatarHintText.text = HintsDict[avatarEnum];
    }


    public void HideAvatarHintBoks()
    {
        SpriteRenderer spriteRenderer = _avatarHintBoks.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = false; // This hides the sprite
        }

        HideText();
    }

    public void ShowAvatarHintBoks()
    {
        SpriteRenderer spriteRenderer = _avatarHintBoks.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = true; // This shows the sprite
        }

        ShowText();
    }


    public void ChangeAvatarHintBoxSprinte(String spriteName)
    {
        SpriteRenderer spriteRenderer = _avatarHintBoks.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            if (spriteName.Equals("hint"))
            {
                spriteRenderer.sprite = _avatarHintSprites[0];
                //spriteRenderer.sprite = _HintBoxSprites[0];
            }
            else if (spriteName.Equals("correct"))
            {
                spriteRenderer.sprite = _avatarHintSprites[1];
                //spriteRenderer.sprite = _HintBoxSprites[1];
            }
            else if (spriteName.Equals("wrong"))
            {
                spriteRenderer.sprite = _avatarHintSprites[2];
                //spriteRenderer.sprite = _HintBoxSprites[2];
            }
            else if (spriteName.Equals("explain"))
            {
                spriteRenderer.sprite = _avatarHintSprites[3];
                //spriteRenderer.sprite = _HintBoxSprites[3];
            }
        }

    }

    private IEnumerator TriggerShakeBox(float duration, float magnitude)
    {
        Vector3 originalPosition = _avatarHintBoks.transform.localPosition;
        float elapsed = 0.0f;

        // Determine the number of shakes based on duration and a desired shake speed
        float shakePeriod = duration / 2; // This will give you approximately 5 back and forth shakes during the entire duration

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float percentComplete = elapsed / duration;

            // Sinusoidal shake based on elapsed time
            float sinWave = Mathf.Sin((elapsed / shakePeriod) * Mathf.PI * 2); // Sin wave for smooth oscillation
            float x = originalPosition.x + sinWave * magnitude;

            _avatarHintBoks.transform.localPosition = new Vector3(x, originalPosition.y, originalPosition.z);
            yield return null;
        }

        _avatarHintBoks.transform.localPosition = originalPosition;
    }

    private IEnumerator TriggerShakeText(float duration, float magnitude)
    {
        Vector3 originalPosition = _avatarHintText.rectTransform.anchoredPosition;
        float elapsed = 0.0f;

        // Determine the number of shakes based on duration and a desired shake speed
        float shakePeriod = duration / 2; // This will give you approximately 5 back and forth shakes during the entire duration

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float percentComplete = elapsed / duration;

            // Sinusoidal shake based on elapsed time
            float sinWave = Mathf.Sin((elapsed / shakePeriod) * Mathf.PI * 2); // Sin wave for smooth oscillation
            float x = originalPosition.x + sinWave * magnitude;

            _avatarHintText.rectTransform.anchoredPosition = new Vector3(x, originalPosition.y, originalPosition.z);
            yield return null;
        }

        _avatarHintText.rectTransform.anchoredPosition = originalPosition;
    }

    public void HideText()
    {

        if (_avatarHintText != null)
        {
            _avatarHintText.enabled = false; // This hides the TextMesh
        }
    }

    public void ShowText()
    {
        _avatarHintText.enabled = true; // This shows the TextMesh
    }

    // DOES NOT WORK PERFECTLY
    public IEnumerator ShowSpriteTemporarily(GameObject spriteObject, float duration)
    {
        SpriteRenderer spriteRenderer = spriteObject.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            ShowText();
            spriteRenderer.enabled = true; // Show the sprite
            yield return new WaitForSeconds(duration); // Wait for the duration
            if (_currentAvatarState.Equals("correct"))
            {
                spriteRenderer.enabled = false; // Hide the sprite after the duration
                HideText();
            }

        }

    }


}


/*
 *///Selecting a wrong placement for the ingredient:
   //SelectWrongPlacementForIngredient,
   //Selecting the right placement for the ingredient (and tree is balanced):
   //SelectRightPlacementAndInBalanced,
   //Selecting the right placement for the ingredient - but it causes the tree to become unbalanced(rotate left and flipColour)
   //RightPlacementButNeedsToSelectThreeNodes,
   //Selecting the right placement for the ingredient - but it causes the tree to become unbalanced(rotate right)
   //RightPlacementButNeedsToSelectTwoNodes,
   //#Selecting a wrong button (Rotate left, Flip, Rotate right)
   //SelectWrongButton,
   //#Selecting the right button (Rotate left, Flip, Rotate right)
   //SelectRightButton
   //#Selecting the right button (Rotate left, Flip, Rotate right) but the tree is still unbalanced
   //RightRotationtButNeedsToSelectThreeNodes,
   //#Selecting the right button (Rotate left, Flip, Rotate right) but the tree is still unbalanced
   //RightRotationtButNeedsToSelectTwoNodes,
   //#When a subtree has been put in the bag and need to be replaced in the tree:
   //NodeInTheJar,
   //#When the potion is finished being brewed
   //PotionBrewed
