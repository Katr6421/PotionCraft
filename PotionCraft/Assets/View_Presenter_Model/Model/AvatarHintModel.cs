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

    // selecting the right placement for the ingredient - but it cause the tree to become unbalanced(flipcolor)
    SelectedRightPlacementButNeedToFlipColor,

    //#Selecting a wrong button (Rotate left, Flip, Rotate right)
    SelectedRightIngredientsButWrongButton,

    //#Selecting the right button (Rotate left, Flip, Rotate right) and ingredients
    SelectedRightIngredientsAndButton,

    //#Selecting the right button (Rotate left, Flip, Rotate right) but the tree is still unbalanced
    SelectedRightButtonButNeedsToSelectThreeNodes,

    //#Selecting the right button (Rotate left, Flip, Rotate right) but the tree is still unbalanced
    SelectedRightButtonButNeedsToSelectTwoNodes,

    //# Selected right button but wrong ingredients
    SelectedRightButtonButWrongIngredients,

    //# Selected both wrong ingredients and button
    SelectedWrongIngredientsAndButton,

    //# Selected everything right, the ingredient is placede and operation animation has run. But the tree is still in unbalance (right rotation)
    NeedsToSelectThreeNodes,

     //# Selected everything right, the ingredient is placede and operation animation has run. But the tree is still in unbalance (Left rotation)
    NeedsToSelectTwoNodes,

     //# Selected everything right, the ingredient is placede and operation animation has run. But the tree is still in unbalance (flip color)
    NeedsToSelectTwoNodesToFlipColor,

    // You have balanced the tree and now you can insert a new one!
    InBalance,


    //#When a subtree has been put in the bag and need to be replaced in the tree:
    NodeInTheJar,

    //#When the potion is finished being brewed
    PotionBrewed
}
public class AvatarHintModel
{
    
}
