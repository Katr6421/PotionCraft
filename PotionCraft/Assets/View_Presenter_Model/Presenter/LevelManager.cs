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
            node5, 
            /*
            node6,
            node7,*/
        };

        List<Node> nodesLevel2 = new List<Node>
        {
            node6,/*
            node5,
            node4,
            node3,
            node2,
            node1*/
        };

        List<Node> nodesLevel3 = new List<Node>
        {
            node1,
            node2,/*
            node3,
            node4,
            node5,
            node6,*/
        };

        // MINI GAME - TOM LISTE
        List<Node> nodesLevel4 = new List<Node>
        {
            node1
        };

        List<Node> nodesLevel5 = new List<Node>
        {
            node5,
            node1, /*
            node19, 
            node25, 
            node17, 
            node21, 
            node20, 
            node9,
            node15, 
            node14*/
        };

        List<Node> nodesLevel6 = new List<Node>
        {
            node25, 
            node12, /*
            node16, 
            node13, 
            node24, 
            node8, 
            node3, 
            node18, 
            node1,
            node19*/

        };

        // MINI GAME
        List<Node> nodesLevel7 = new List<Node>
        {
            node1
        };

        List<Node> nodesLevel8 = new List<Node>
        {
            node2
        };

        List<Node> nodesLevel9 = new List<Node>
        {
            node3
        };

        List<Node> nodesLevel10 = new List<Node>
        {
            node4
        };

        // Add levels to the list of levels. Hardcoded for each level
        Levels.Add(new LevelData("Dummy", 1, nodesLevel1, PotionDescription.Dummy));
        Levels.Add(new LevelData("Insertion Infusion", 1, nodesLevel1, PotionDescription.InsertionInfusion));
        Levels.Add(new LevelData("Rotation Tonic", 2, nodesLevel2, PotionDescription.RotationTonic)); 
        Levels.Add(new LevelData("Color Swap Serum", 3, nodesLevel3, PotionDescription.ColorSwapSerum));
        Levels.Add(new LevelData(" ", 4, nodesLevel4, PotionDescription.Dummy)); // MINI GAME
        Levels.Add(new LevelData("Binary Blend", 5, nodesLevel5, PotionDescription.BinaryBlend)); 
        Levels.Add(new LevelData("Leaf Lixir", 6, nodesLevel6, PotionDescription.LeafLixir));
        Levels.Add(new LevelData("", 7, nodesLevel7, PotionDescription.Dummy, true)); // MINI GAME 
        Levels.Add(new LevelData("Key Kombucha", 8, nodesLevel8, PotionDescription.KeyKombucha));
        Levels.Add(new LevelData("Bad Blood", 9, nodesLevel9, PotionDescription.BadBlood));
        Levels.Add(new LevelData("Master Red-Black Potion", 10, nodesLevel10, PotionDescription.MasterRedBlackPotion));
    }

    public void MakeDescriptionDictionary()
    {
        DescriptionsDict.Add(PotionDescription.Dummy, "Dummy");
        DescriptionsDict.Add(PotionDescription.InsertionInfusion, "Start your quest to defeat darkness with this foundational brew");
        DescriptionsDict.Add(PotionDescription.RotationTonic, "Stir your way to mastery with this swirling tonic that aligns energies and balances forces");
        DescriptionsDict.Add(PotionDescription.ColorSwapSerum, "Flip your fate with this vibrant serum that switches hues and fortunes alike");
        DescriptionsDict.Add(PotionDescription.BinaryBlend, "Double the power, double the fun");
        DescriptionsDict.Add(PotionDescription.LeafLixir, "'Leaf' your worries behind with this herbal elixir that soothes the soul and calms the mind");
        DescriptionsDict.Add(PotionDescription.KeyKombucha, "Unlock your potential with this critical, fermenting concoction");
        DescriptionsDict.Add(PotionDescription.BadBlood, "Clear away the curses with one purifying potion");
        DescriptionsDict.Add(PotionDescription.MasterRedBlackPotion, "The ultimate brew to restore balance and overthrow tyranny");
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

    public bool IsMiniGame()
    {
        return Levels[CurrentLevelIndex].IsMiniGame;
    }
}