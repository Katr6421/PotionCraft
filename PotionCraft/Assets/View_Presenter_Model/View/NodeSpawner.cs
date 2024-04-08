using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

//This is a gameobject on the scene. It is responsible for spawning the nodes as GameObjects and positioning them on the screen.

public class NodeSpawner : MonoBehaviour
{

    [SerializeField] private Canvas uiCanvas; // Reference to the Canvas where the nodes will be parented

    [SerializeField] private GameObject KatnissPrefab; // Assigned the prefab in the inspector
    [SerializeField] private GameObject MushroomPrefab; // Assigned the prefab in the inspector
    [SerializeField] private GameObject PinkFlowerPrefab; // Assigned the prefab in the inspector
    [SerializeField] private GameObject RadishPrefab; // Assigned the prefab in the inspector
    [SerializeField] private GameObject WaterFlowerPrefab; // Assigned the prefab in the inspector
    [SerializeField] private Transform _ingredientsPanel; // Assigned the prefab in the inspector

    // Dictionary to map node values to prefabs. This makes it easy to instantiate the correct prefab based on the node's value
    public Dictionary<int, GameObject> NodeValueToPrefabDictionary { get; private set; } = new Dictionary<int, GameObject>();

    // Holds references to the instantiated GameObjects. We need this list to move the nodes and to check thir values to see if they are inserted correctly in the RedBlackTree
    public List<GameObject> NodeObjects { get; private set; } = new List<GameObject>();


    void Awake()
    {
        /*********************************************
        Fill the dictionary. Maps the node value to the prefab
        This makes it easy to create a new ingredient based on the nodes value. 
        *********************************************/
        NodeValueToPrefabDictionary.Add(1, RadishPrefab);
        NodeValueToPrefabDictionary.Add(2, MushroomPrefab);
        NodeValueToPrefabDictionary.Add(3, PinkFlowerPrefab);
        NodeValueToPrefabDictionary.Add(4, KatnissPrefab);
        NodeValueToPrefabDictionary.Add(5, WaterFlowerPrefab);
        NodeValueToPrefabDictionary.Add(6, RadishPrefab);
        NodeValueToPrefabDictionary.Add(7, MushroomPrefab);
        NodeValueToPrefabDictionary.Add(8, PinkFlowerPrefab);
        NodeValueToPrefabDictionary.Add(9, KatnissPrefab);
        NodeValueToPrefabDictionary.Add(10, WaterFlowerPrefab);
        NodeValueToPrefabDictionary.Add(11, RadishPrefab);
        NodeValueToPrefabDictionary.Add(12, MushroomPrefab);
        NodeValueToPrefabDictionary.Add(13, PinkFlowerPrefab);
        NodeValueToPrefabDictionary.Add(14, KatnissPrefab);
        NodeValueToPrefabDictionary.Add(15, WaterFlowerPrefab);
        NodeValueToPrefabDictionary.Add(16, RadishPrefab);
        NodeValueToPrefabDictionary.Add(17, MushroomPrefab);
        NodeValueToPrefabDictionary.Add(18, PinkFlowerPrefab);
        NodeValueToPrefabDictionary.Add(19, KatnissPrefab);
        NodeValueToPrefabDictionary.Add(20, WaterFlowerPrefab);
        NodeValueToPrefabDictionary.Add(21, RadishPrefab);
        NodeValueToPrefabDictionary.Add(22, MushroomPrefab);
        NodeValueToPrefabDictionary.Add(23, PinkFlowerPrefab);
        NodeValueToPrefabDictionary.Add(24, KatnissPrefab);
        NodeValueToPrefabDictionary.Add(25, WaterFlowerPrefab);
        NodeValueToPrefabDictionary.Add(26, RadishPrefab);
    }

    public void Start()
    {
        // When the scene starts, we call the DisplayNodes method to create and position the nodes
        SpawnNodes(LevelManager.Instance.GetIngredients());
    }

    /*********************************************
    METHOD: SpawnNodes                          
    DESCRIPTION: This method handles the instantiation of the nodes as GameObjects.
    It takes a list of nodes, given for the specific level, as a parameter.
    The method loops through the list of nodes and instantiates a GameObject for each node.
    The GameObject's text value is set to the node's value.
    A reference to the GameObject is stored in a list. 
    *********************************************/
    public void SpawnNodes(List<Node> nodes)
    {
        foreach (Node node in nodes)
        {
            // Keep track of the index of the current node. Used for positioning
            int nodeIndex = nodes.IndexOf(node);
            // Calculate the position of the node based on the index
            Vector3 newNodePosition = CalculatePosition(nodeIndex);
            //Instantiate a new GameObject from the prefab
            GameObject nodeObject = Instantiate(GetPrefabForNode(node), newNodePosition, Quaternion.identity);
            // Parent the instantiated UI element to a canvas or a panel within the canvas, if needed
            nodeObject.transform.SetParent(uiCanvas.transform, false);
            // Set the text value of the GameObject to the node's value
            nodeObject.GetComponentInChildren<TextMeshProUGUI>().text = node.Value.ToString();
            // Make non-interactable but preserve the visual appearance
            MakeIngredientInteractable(false, nodeObject);
            // Store the reference to the GameObject in a list
            NodeObjects.Add(nodeObject);
        }
    }


    /*********************************************
    METHOD: GetPrefabForNode
    DESCRIPTION: This method takes a node as a parameter and returns the prefab that corresponds to the node's value.
    *********************************************/
    private GameObject GetPrefabForNode(Node node)
    {
        return NodeValueToPrefabDictionary[node.Value];
    }

    /*********************************************
    METHOD: SpawnIngredientsOnRecipe
    DESCRIPTION: This method creates a set of the unique ingredients that are needed for the recipe.
    It loops through the list of nodes and instantiates a GameObject for each unique ingredient.
    The GameObject is parented to the _ingredientsPanel with a grid layout.
    *********************************************/
    public void SpawnIngredientsOnRecipe()
    {
        HashSet<string> uniqueIngredients = new HashSet<string>();
        List<Node> nodes = LevelManager.Instance.GetIngredients();

        foreach (Node node in nodes)
        {
            GameObject prefab = GetPrefabForNode(node);

            // Create a sprite of the corresponding ingredient prefab but without the white cirle
            string newSpriteName = prefab.name + "_withoutCircle";
            // Load the new sprite from Assets folder
            Sprite newSprite = Resources.Load<Sprite>("Art/GameScreen/IngredientsWithoutCircles/" + newSpriteName);

            if (newSprite != null && uniqueIngredients.Add(newSpriteName))
            {
                // Create a new GameObject to hold the image
                GameObject instance = new GameObject(newSpriteName);
                // Set the parent to _ingredientsPanel
                instance.transform.SetParent(_ingredientsPanel, false);
                // Add an Image component to the new GameObject 
                Image instanceImage = instance.AddComponent<Image>();
                // Set the sprite to the picture of the ingredient (without the white circle)
                instanceImage.sprite = newSprite;
            }
        }
    }


    /**********************************************
    METHOD: CalculatePosition
    DESCRIPTION: This method calculates the position of the node based on the index of the node in the list of nodes.
    The method takes an integer as a parameter, which represents the index of the node in the list.
    The method calculates the x position based on the index and returns a Vector3 with the calculated position.
    **********************************************/
    public Vector3 CalculatePosition(int nodeIndex)
    {
        float leftBound = -56; // x position of the leftmost point in the red circle, HARD CODED
        float yPosition = 401; // y position where the nodes should be placed, HARD CODED
        float spaceBetweenNodes = 100 * nodeIndex; // Space between nodes, HARD CODED
                                                   // Calculate the x position for the current node
        float xPosition = leftBound + spaceBetweenNodes;
        // Return the calculated position
        return new Vector3(xPosition, yPosition, 0);
    }

    /**********************************************
    METHOD: MakeAllPlacedObjectInteractable
    DESCRIPTION: This method makes the ingredients that have been placed in the tree interactable or non-interactable.
    False makes them non-interactable, while true makes them interactable.
    **********************************************/
    public void MakeAllPlacedIngredientsInteractable(bool shouldBeInteractable, int indexOfTopIngredient)
    {
        // Only do it for the ingredients that have been placed in the tree
        for (int i = 0; i < indexOfTopIngredient; i++)
        {
            if (i < NodeObjects.Count)
            {
                MakeIngredientInteractable(shouldBeInteractable, NodeObjects[i]);
            }
        }
    }

    /**********************************************
    METHOD: MakeIngredientInteractable
    DESCRIPTION: This method makes the given ingredient interactable or non-interactable.
    False makes it non-interactable, while true makes it interactable.
    **********************************************/
    public void MakeIngredientInteractable(bool shouldBeInteractable, GameObject ingredient)
    {
        Button buttonComponent = ingredient.GetComponent<Button>();
        if (buttonComponent != null)
        {
            buttonComponent.interactable = shouldBeInteractable;
            var colors = buttonComponent.colors;
            colors.disabledColor = colors.normalColor;
            buttonComponent.colors = colors;
        }
    }

}


