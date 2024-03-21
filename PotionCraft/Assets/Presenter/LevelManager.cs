using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour, ILevelManager
{
    public List<LevelData> Levels { get; set; } = new List<LevelData>();
    public static LevelManager Instance { get; private set; }

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

        InitializeLevels();
    }

    public void InitializeLevels()
    {

        //Make nodes for each level
        // Kan være at det skal være true
        Node node1 = new Node(1, 1, 1, false);
        Node node2 = new Node(2, 2, 1, false);
        Node node3 = new Node(3, 3, 1, false);
        Node node4 = new Node(4, 4, 1, false);
        Node node5 = new Node(5, 5, 1, false);
        Node node6 = new Node(6, 6, 1, false);
        Node node7 = new Node(7, 7, 1, false);
        Node node8 = new Node(8, 8, 1, false);
        Node node9 = new Node(9, 9, 1, false);
        Node node10 = new Node(10, 10, 1, false);
        Node node11 = new Node(11, 11, 1, false);
        Node node12 = new Node(12, 12, 1, false);
        Node node13 = new Node(13, 13, 1, false);
        Node node14 = new Node(14, 14, 1, false);
        Node node15 = new Node(15, 15, 1, false);
        Node node16 = new Node(16, 16, 1, false);
        Node node17 = new Node(17, 17, 1, false);
        Node node18 = new Node(18, 18, 1, false);
        Node node19 = new Node(19, 19, 1, false);
        Node node20 = new Node(20, 20, 1, false);
        Node node21 = new Node(21, 21, 1, false);
        Node node22 = new Node(22, 22, 1, false);
        Node node23 = new Node(23, 23, 1, false);
        Node node24 = new Node(24, 24, 1, false);
        Node node25 = new Node(25, 25, 1, false);
        Node node26 = new Node(26, 26, 1, false);

        // Make a list of nodes for each level
        List<Node> nodesLevel1 = new List<Node>();
        nodesLevel1.Add(node1);
        nodesLevel1.Add(node2);
        nodesLevel1.Add(node3);

        // Add levels to the list of levels. Hardcoded for each level
        Levels.Add(new LevelData("Dummy", null, 1, nodesLevel1));
        Levels.Add(new LevelData("1", null, 1, nodesLevel1));
        Levels.Add(new LevelData("Potion 2", null, 2, new List<Node>()));
        Levels.Add(new LevelData("Potion 3", null, 3, new List<Node>()));
        Levels.Add(new LevelData("Potion 4", null, 4, new List<Node>()));
        Levels.Add(new LevelData("Potion 5", null, 5, new List<Node>()));
        Levels.Add(new LevelData("Potion 6", null, 6, new List<Node>()));
        Levels.Add(new LevelData("Potion 7", null, 7, new List<Node>()));
        Levels.Add(new LevelData("Potion 8", null, 8, new List<Node>()));
        Levels.Add(new LevelData("Potion 9", null, 9, new List<Node>()));
        Levels.Add(new LevelData("Potion 10", null, 10, new List<Node>()));
    }

    public string GetPotionName(int levelIndex)
    {
        Debug.Log(Levels[levelIndex].PotionName);
        return Levels[levelIndex].PotionName;
    }

    public Sprite GetPotionImage(int levelIndex)
    {
        return Levels[levelIndex].PotionImage;
    }

    public int GetLevelIndex(int levelIndex)
    {
        return Levels[levelIndex].LevelIndex;
    }

    public List<Node> GetIngredients(int levelIndex)
    {
        return Levels[levelIndex].Ingredients;
    }



    public LevelData LoadLevel(int levelIndex)
    {
        return Levels[levelIndex];

    }

    public void CompleteLevel(int levelIndex)
    {
    }

    public void LoadProgress()
    {
        throw new NotImplementedException();
    }

    public void SaveProgress()
    {
        throw new NotImplementedException();
    }
}