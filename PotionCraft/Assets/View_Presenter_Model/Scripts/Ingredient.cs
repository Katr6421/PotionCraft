using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public int NullCircleIndex {get; set;}


    // Start is called before the first frame update
    public void OnClick(){
        Debug.Log("Clicked on ingredient, with the value:  " + GetComponentInChildren<TextMeshProUGUI>().text);
        TreeManager.instance.HandleIngredientClick(int.Parse(GetComponentInChildren<TextMeshProUGUI>().text));
        TreeManager.instance.CurrentSelectedIngredients.Add(gameObject);
    }
}
