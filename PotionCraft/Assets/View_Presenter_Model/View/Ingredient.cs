using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class Ingredient : MonoBehaviour
{
    public int NullCircleIndex {get; set;}
    [SerializeField] private Sprite[] _radishSprite;
    [SerializeField] private Sprite[] _mushroomSprite;
    [SerializeField] private Sprite[] _pinkFlowerSprite;
    [SerializeField] private Sprite[] _waterFlowerSprite;
    [SerializeField] private Sprite[] _katnissSprite;
    private Image imageComponent;
    public GameObject LineToParent { get; set; } // Saves a referens to the line that goes to the parent of this ingredient
    private TreeManager _treeManager;

    void Awake()
    {
        // Get the Image component from the GameObject
        imageComponent = GetComponent<Image>();
        _treeManager = FindObjectOfType<TreeManager>();
    }


    // Start is called before the first frame update
    public void OnClick(){
        //Debug.Log("Clicked on ingredient, with the value:  " + GetComponentInChildren<TextMeshProUGUI>().text);
        
        ChangePrefabImage(gameObject.name);
        var setOfSelectedIngredients = _treeManager.CurrentSelectedIngredients;

        // Add node from the real tree to the selected ingredients/nodes
        _treeManager.HandleIngredientClick(int.Parse(GetComponentInChildren<TextMeshProUGUI>().text));

        // Add ingredient to the set of selected ingredients
        if (setOfSelectedIngredients.Contains(gameObject))
        {
            setOfSelectedIngredients.Remove(gameObject);
        }
        else
        {
            setOfSelectedIngredients.Add(gameObject);
        }

        // print  setOfSelectedIngredients
        /*foreach (var ingredient in setOfSelectedIngredients)
        {
            Debug.Log("Current selected ingredients: " + ingredient.name);
        }*/
    }

    public void ChangePrefabImage(string prefabName)
    {
        //(default = 0, glow = 1)
        if (prefabName.StartsWith("Radish"))
        {
            if (imageComponent.sprite == _radishSprite[0])
                imageComponent.sprite = _radishSprite[1];
            else
                imageComponent.sprite = _radishSprite[0];
        }
        if (prefabName.StartsWith("Mushroom"))
        {
            if (imageComponent.sprite == _mushroomSprite[0])
                imageComponent.sprite = _mushroomSprite[1];
            else
                imageComponent.sprite = _mushroomSprite[0];
        }
        if (prefabName.StartsWith("PinkFlower"))
        {
            if (imageComponent.sprite == _pinkFlowerSprite[0])
                imageComponent.sprite = _pinkFlowerSprite[1];
            else
                imageComponent.sprite = _pinkFlowerSprite[0];
        }
        if (prefabName.StartsWith("WaterFlower"))
        {
            if (imageComponent.sprite == _waterFlowerSprite[0])
                imageComponent.sprite = _waterFlowerSprite[1];
            else
                imageComponent.sprite = _waterFlowerSprite[0];
        }
        if (prefabName.StartsWith("Katniss"))
        {
            if (imageComponent.sprite == _katnissSprite[0])
                imageComponent.sprite = _katnissSprite[1];
            else
                imageComponent.sprite = _katnissSprite[0];
        }
    }

}
