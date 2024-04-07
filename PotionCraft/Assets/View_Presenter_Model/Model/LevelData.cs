using UnityEngine;
using System.Collections.Generic;
public class LevelData : MonoBehaviour
{
    public string PotionName { get; set; }

    public int LevelIndex { get; set; }
    public List<Node> Ingredients { get; set; }

    public LevelData(string potionName, int levelIndex, List<Node> ingredients) // Potion potion, Recipe recipe, List<Ingredient> ingredients
    {
        PotionName = potionName;
        LevelIndex = levelIndex;
        Ingredients = ingredients;
    }

}