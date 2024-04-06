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


  public Dictionary<AvatarHint, string> HintsDict { get; set; } = new Dictionary<AvatarHint, string>();
  public static AvatarHintManager instance { get; private set; }

    private void Awake()
    {
        // Singleton pattern
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }
  
  public AvatarHintManager()
  {
      HintsDict.Add(AvatarHint.SelectedWrongPlacementForIngredient, "Hmm, you have selected the wrong placement for that ingredient");
      HintsDict.Add(AvatarHint.SelectedRightPlacementAndInBalance, "Perfect placement! You have selected the right placement for the ingredient and the tree is balanced");
      HintsDict.Add(AvatarHint.SelectedRightPlacementButNeedsToSelectThreeNodes, "You have selected the right placement for the ingredient, but it causes the tree to become unbalanced. You need to select three nodes");
      HintsDict.Add(AvatarHint.SelectedRightPlacementButNeedsToSelectTwoNodes, "You have selected the right placement for the ingredient, but it causes the tree to become unbalanced. You need to select two nodes");
      HintsDict.Add(AvatarHint.SelectedWrongButton, "You have selected the wrong button. Please try again");
      HintsDict.Add(AvatarHint.SelectedRightButton, "Good job! You have selected the right button");
      HintsDict.Add(AvatarHint.SelectedRightButtonButNeedsToSelectThreeNodes, "You have selected the right button, but the tree is still unbalanced. You need to select three nodes");
      HintsDict.Add(AvatarHint.SelectedRightButtonButNeedsToSelectTwoNodes, "You have selected the right button, but the tree is still unbalanced. You need to select two nodes");
      HintsDict.Add(AvatarHint.NodeInTheJar, "A subtree has been put in the bag and need to be replaced in the tree");
      HintsDict.Add(AvatarHint.PotionBrewed, "The potion is finished being brewed");
      HintsDict.Add(AvatarHint.SelectedRightPlacementButNeedToFlipColor, "You have selected the right placement for the ingredient, but it causes the tree to become unbalanced. You need to flip the color");
  }

// Call this method with "correct" or "hint" and the corresponding enum
  public void UpdateHint(String spriteName, AvatarHint avatarEnum)
  {
      ChangeAvatarHintBoxSprinte(spriteName);
      SetHint(avatarEnum);
      if(spriteName.Equals("correct")){
        StartCoroutine(ShowSpriteTemporarily(_avatarHintBoks, 10f)); // Call the coroutine with 10 seconds
      }
      else{
        ShowAvatarHintBoks();
        StartCoroutine(Shake(1.0f, 1.0f));
        
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

  public void TriggerShake(){
    //_avatarHintBoks.TriggerShake();
  }


  public void HideAvatarHintBoks()
  {
      SpriteRenderer spriteRenderer = _avatarHintBoks.GetComponent<SpriteRenderer>();
      if (spriteRenderer != null)
      {
          spriteRenderer.enabled = false; // This hides the sprite
      }
  }

public void ShowAvatarHintBoks()
  {
      SpriteRenderer spriteRenderer = _avatarHintBoks.GetComponent<SpriteRenderer>();
      if (spriteRenderer != null)
      {
          spriteRenderer.enabled = true; // This shows the sprite
      }
  }

  public IEnumerator ShowSpriteTemporarily(GameObject spriteObject, float duration)
{
    SpriteRenderer spriteRenderer = spriteObject.GetComponent<SpriteRenderer>();
    if (spriteRenderer != null)
    {
        spriteRenderer.enabled = true; // Show the sprite
        yield return new WaitForSeconds(duration); // Wait for the duration
        spriteRenderer.enabled = false; // Hide the sprite after the duration
    }
}



  public void ChangeAvatarHintBoxSprinte(String spriteName){
    SpriteRenderer spriteRenderer = _avatarHintBoks.GetComponent<SpriteRenderer>();
    if (spriteRenderer != null)
    {
        if (spriteName == "hint")
        {
            spriteRenderer.sprite = _avatarHintSprites[0];
        }
        else if (spriteName == "correct")
        {
            spriteRenderer.sprite = _avatarHintSprites[1];
        }
    }
  
  }

  private IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPosition = _avatarHintBoks.transform.localPosition;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = originalPosition.x + UnityEngine.Random.Range(-1f, 1f) * magnitude;
            float y = originalPosition.y + UnityEngine.Random.Range(-1f, 1f) * magnitude;

            _avatarHintBoks.transform.localPosition = new Vector3(x, y, originalPosition.z);
            elapsed += Time.deltaTime;

            yield return null;
        }

        _avatarHintBoks.transform.localPosition = originalPosition;
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
