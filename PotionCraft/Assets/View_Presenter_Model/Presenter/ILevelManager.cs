using System.Collections.Generic;
using UnityEngine;

public interface ILevelManager
{
    public string GetPotionName();
    public List<Node> GetIngredients();

}