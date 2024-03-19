using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AvatarHint
{
    //Selecting a wrong placement for the ingredient:
    SelectedWrongPlacementForIngredient,
    //Selecting the right placement for the ingredient (and tree is balanced):
    SelectedRightPlacementAndInBalance,
    //Selecting the right placement for the ingredient - but it causes the tree to become unbalanced(rotate left and flipColour)
    SelectedRightPlacementButNeedsToSelectThreeNodes,
    //Selecting the right placement for the ingredient - but it causes the tree to become unbalanced(rotate right)
    SelectedRightPlacementButNeedsToSelectTwoNodes,
    //#Selecting a wrong button (Rotate left, Flip, Rotate right)
    SelectedWrongButton,
    //#Selecting the right button (Rotate left, Flip, Rotate right)
    SelectedRightButton,
    //#Selecting the right button (Rotate left, Flip, Rotate right) but the tree is still unbalanced
    SelectedRightButtonButNeedsToSelectThreeNodes,
    //#Selecting the right button (Rotate left, Flip, Rotate right) but the tree is still unbalanced
    SelectedRightButtonButNeedsToSelectTwoNodes,
    //#When a subtree has been put in the bag and need to be replaced in the tree:
    NodeInTheJar,
    //#When the potion is finished being brewed
    PotionBrewed
}
public class AvatarHintModel
{
    
}
