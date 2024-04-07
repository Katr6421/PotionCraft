using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using log4net.Core;
using PlasticPipe.PlasticProtocol.Messages;

public class LevelManager : MonoBehaviour, ILevelManager
{
    [SerializeField] private Sprite[] _potionSprites;
    public List<LevelData> Levels { get; set; } = new List<LevelData>();
    public static LevelManager Instance { get; private set; }
    public Dictionary<PotionDescription, string> DescriptionsDict { get; set; } = new Dictionary<PotionDescription, string>();
    public int CurrentLevelIndex { get; set; }

    private void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        MakeDescriptionDictionary();
        InitializeLevels();
    }

    public void InitializeLevels()
    {
        // Make nodes for each level
        Node node1 = new Node(1, 1, 1, false, null);
        Node node2 = new Node(2, 2, 1, false, null);
        Node node3 = new Node(3, 3, 1, false, null);
        Node node4 = new Node(4, 4, 1, false, null);
        Node node5 = new Node(5, 5, 1, false, null);
        Node node6 = new Node(6, 6, 1, false, null);
        Node node7 = new Node(7, 7, 1, false, null);
        Node node8 = new Node(8, 8, 1, false, null);
        Node node9 = new Node(9, 9, 1, false, null);
        Node node10 = new Node(10, 10, 1, false, null);
        Node node11 = new Node(11, 11, 1, false, null);
        Node node12 = new Node(12, 12, 1, false, null);
        Node node13 = new Node(13, 13, 1, false, null);
        Node node14 = new Node(14, 14, 1, false, null);
        Node node15 = new Node(15, 15, 1, false, null);
        Node node16 = new Node(16, 16, 1, false, null);
        Node node17 = new Node(17, 17, 1, false, null);
        Node node18 = new Node(18, 18, 1, false, null);
        Node node19 = new Node(19, 19, 1, false, null);
        Node node20 = new Node(20, 20, 1, false, null);
        Node node21 = new Node(21, 21, 1, false, null);
        Node node22 = new Node(22, 22, 1, false, null);
        Node node23 = new Node(23, 23, 1, false, null);
        Node node24 = new Node(24, 24, 1, false, null);
        Node node25 = new Node(25, 25, 1, false, null);
        Node node26 = new Node(26, 26, 1, false, null);

        // Make a list of nodes for each level
        List<Node> nodesLevel1 = new List<Node>
        {
            node19,
            node5,
            node1,
            node18,
            node3,
            node8,
            node24,
            node13,
            node16,
            node12
        };

        List<Node> nodesLevel2 = new List<Node>
        {
            node4,
            node5,
            node6
        };

        List<Node> testLeftRotation = new List<Node>
        {
            node1,
            node2
        };

        List<Node> testLeftRotationShort = new List<Node>
        {
            node1,
            node5,
            node8,
            node3
        };

        // Add levels to the list of levels. Hardcoded for each level
        Levels.Add(new LevelData("Dummy", 1, nodesLevel1, PotionDescription.Dummy));
        Levels.Add(new LevelData("Insertion Infusion", 1, nodesLevel1, PotionDescription.InsertionInfusion));
        Levels.Add(new LevelData("Rotation Tonic", 2, nodesLevel2, PotionDescription.RotationTonic)); 
        Levels.Add(new LevelData("Color Swap Serum", 3, new List<Node>(), PotionDescription.ColorSwapSerum));
        Levels.Add(new LevelData("Binary Blend", 4, new List<Node>(), PotionDescription.BinaryBlend));
        Levels.Add(new LevelData("Leaf Lixir", 5, new List<Node>(), PotionDescription.LeafLixir));
        Levels.Add(new LevelData("Black Balance Draught", 6, new List<Node>(), PotionDescription.BlackBalanceDraught));
        Levels.Add(new LevelData("Root Revitalizer", 7, new List<Node>(), PotionDescription.RootRevitalizer));
        Levels.Add(new LevelData("Key Kombucha", 8, new List<Node>(), PotionDescription.KeyKombucha));
        Levels.Add(new LevelData("Bad Blood", 9, new List<Node>(), PotionDescription.BadBlood));
        Levels.Add(new LevelData("Master Red-Black Potion", 10, new List<Node>(), PotionDescription.MasterRedBlackPotion));
    }

    public void MakeDescriptionDictionary()
    {
        DescriptionsDict.Add(PotionDescription.Dummy, "Dummy");
        DescriptionsDict.Add(PotionDescription.InsertionInfusion, "Start your quest to defeat darkness with this foundational brew");
        DescriptionsDict.Add(PotionDescription.RotationTonic, "Swirl your way to perfection");
        DescriptionsDict.Add(PotionDescription.ColorSwapSerum, "Flip your fate with a splash of color");
        DescriptionsDict.Add(PotionDescription.BinaryBlend, "Double the power, double the fun");
        DescriptionsDict.Add(PotionDescription.LeafLixir, "Leaf your worries behind");
        DescriptionsDict.Add(PotionDescription.BlackBalanceDraught, "Dark, mysterious, perfectly harmonious");
        DescriptionsDict.Add(PotionDescription.RootRevitalizer, "Get back to your roots to give your potion a solid foundation!");
        DescriptionsDict.Add(PotionDescription.KeyKombucha, "Unlock your potion's potential with this essential essence");
        DescriptionsDict.Add(PotionDescription.BadBlood, "Clear away the curses with one purifying potion");
        DescriptionsDict.Add(PotionDescription.MasterRedBlackPotion, "The ultimate brew to balance power and end tyranny");
    }

public string GetPotionName()
    {
        return Levels[CurrentLevelIndex].PotionName;
    }

    public Sprite GetPotionSprite()
    {
        return _potionSprites[CurrentLevelIndex];
    }

    public string GetPotionDescription()
    {
        return DescriptionsDict[Levels[CurrentLevelIndex].PotionDescription];
    }

    public int GetLevelIndex()
    {
        return Levels[CurrentLevelIndex].LevelIndex;
    }

    public List<Node> GetIngredients()
    {
        return Levels[CurrentLevelIndex].Ingredients;
    }
}