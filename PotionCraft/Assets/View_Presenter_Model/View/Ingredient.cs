using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Ingredient : MonoBehaviour
{
    [SerializeField] private Sprite[] _radishSprite;
    [SerializeField] private Sprite[] _mushroomSprite;
    [SerializeField] private Sprite[] _pinkFlowerSprite;
    [SerializeField] private Sprite[] _waterFlowerSprite;
    [SerializeField] private Sprite[] _katnissSprite;
    public GameObject LineToParent { get; set; }
    public GameObject LineToLeft { get; set; }
    public GameObject LineToRight { get; set; }
    private Image imageComponent;
    private TreeManager _treeManager;

    void Awake()
    {
        /*********************************************
        Get the Image component from the GameObject
        *********************************************/
        imageComponent = GetComponent<Image>();
        _treeManager = FindObjectOfType<TreeManager>();
    }

    public void OnClick()
    {
        ChangePrefabImage(gameObject.name);
        var setOfSelectedIngredients = _treeManager.CurrentSelectedIngredients;

        /*********************************************
        Add node from the real tree to the selected ingredients/nodes
        *********************************************/
        _treeManager.HandleIngredientClick(int.Parse(GetComponentInChildren<TextMeshProUGUI>().text));

        /*********************************************
        Add ingredient to the set of selected ingredients
        *********************************************/
        if (setOfSelectedIngredients.Contains(gameObject))
        {
            setOfSelectedIngredients.Remove(gameObject);
        }
        else
        {
            setOfSelectedIngredients.Add(gameObject);
        }
    }

    public void ChangePrefabImage(string prefabName)
    {
        /*********************************************
        Image: default = 0, glow = 1
        *********************************************/
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
