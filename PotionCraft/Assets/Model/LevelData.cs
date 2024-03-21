using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class LevelData : MonoBehaviour
{
    public string PotionName { get; set; }
    public Sprite PotionImage { get; set; }
    public int LevelIndex { get; set; }
    public List<Node> Ingredients { get; set; }

    public LevelData(string potionName, Sprite potionImage, int levelIndex, List<Node> ingredients) // Potion potion, Recipe recipe, List<Ingredient> ingredients
    {
        PotionName = potionName;
        PotionImage = potionImage;
        LevelIndex = levelIndex;
        Ingredients = ingredients;
    }

}