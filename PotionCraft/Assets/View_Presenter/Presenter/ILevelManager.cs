using System.Collections.Generic;
using UnityEngine;

public interface ILevelManager
{
    public LevelData LoadLevel(int levelIndex);
    public void CompleteLevel(int levelIndex);
    public void LoadProgress();
    public void SaveProgress();
    public string GetPotionName(int levelIndex);

    public int GetLevelIndex(int levelIndex);
    public List<Node> GetIngredients(int levelIndex);

}