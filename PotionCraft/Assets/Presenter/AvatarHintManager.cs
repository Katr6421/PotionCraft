using System.Collections.Generic;
using UnityEngine;

public class AvatarHintManager : MonoBehaviour, IAvatarHintManager
{
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
      HintsDict.Add(AvatarHint.SelectedRightButtonButNeedsToSelectThreeNodes, "You have selected the right placement for the ingredient, but it causes the tree to become unbalanced. You need to select three nodes");
      HintsDict.Add(AvatarHint.SelectedRightButtonButNeedsToSelectTwoNodes, "You have selected the right placement for the ingredient, but it causes the tree to become unbalanced. You need to select two nodes");
      HintsDict.Add(AvatarHint.SelectedWrongButton, "You have selected the wrong button. Please try again");
      HintsDict.Add(AvatarHint.SelectedRightButton, "Good job! You have selected the right button");
      HintsDict.Add(AvatarHint.SelectedRightButtonButNeedsToSelectThreeNodes, "You have selected the right button, but the tree is still unbalanced. You need to select three nodes");
      HintsDict.Add(AvatarHint.SelectedRightButtonButNeedsToSelectTwoNodes, "You have selected the right button, but the tree is still unbalanced. You need to select two nodes");
      HintsDict.Add(AvatarHint.NodeInTheJar, "A subtree has been put in the bag and need to be replaced in the tree");
      HintsDict.Add(AvatarHint.PotionBrewed, "The potion is finished being brewed");
  }

  public string GetHint(AvatarHint avatarEnum)
  {
      return HintsDict[avatarEnum];
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
