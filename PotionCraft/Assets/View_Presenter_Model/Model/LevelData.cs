using UnityEngine;
using System.Collections.Generic;
using TMPro;
public class LevelData : MonoBehaviour
{


    public string PotionName { get; set; }
    public PotionDescription PotionDescription { get; set; }
    public int LevelIndex { get; set; }
    public List<Node> Ingredients { get; set; }
    public bool IsMiniGame { get; set; }


    public LevelData(string potionName, int levelIndex, List<Node> ingredients, PotionDescription potionDescription, bool isMiniGame = false)
    {
        PotionName = potionName;
        LevelIndex = levelIndex;
        Ingredients = ingredients;
        PotionDescription = potionDescription;
        IsMiniGame = isMiniGame;
    }

}

public enum PotionDescription
{
    Dummy,
    InsertionInfusion,
    RotationTonic,
    ColorSwapSerum,
    BinaryBlend,
    LeafLixir,
    BlackBalanceDraught,
    RootRevitalizer,
    KeyKombucha,
    BadBlood,
    MasterRedBlackPotion
}