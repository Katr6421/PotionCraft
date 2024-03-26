using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class NodeSpawner : MonoBehaviour
{
    
    [SerializeField] private Canvas uiCanvas; // Reference to the Canvas where the nodes will be parented
  
    [SerializeField] private GameObject KatnissPrefab; // Assigned the prefab in the inspector
    [SerializeField] private GameObject NullCirclePrefab; // Assigned the prefab in the inspector
    [SerializeField] private GameObject MushroomPrefab; // Assigned the prefab in the inspector
    [SerializeField] private GameObject PinkFlowerPrefab; // Assigned the prefab in the inspector
    [SerializeField] private GameObject RadishPrefab; // Assigned the prefab in the inspector
    [SerializeField] private GameObject WaterFlowerPrefab; // Assigned the prefab in the inspector

    // Dictionary to map node values to prefabs. This makes it easy to instantiate the correct prefab based on the node's value
    private Dictionary<int, GameObject> nodeValueToPrefabDictionary = new Dictionary<int, GameObject>();
    // Holds references to the instantiated GameObjects. We need this list to move the nodes and to check thir values to see if they are inserted correctly in the RedBlackTree
    public static List<GameObject> nodeObjects = new List<GameObject>();
    

    void Awake() {
        // Fill the dictionary. Maps the node value to the prefab
        nodeValueToPrefabDictionary.Add(1, RadishPrefab);
        nodeValueToPrefabDictionary.Add(2, MushroomPrefab);
        nodeValueToPrefabDictionary.Add(3, PinkFlowerPrefab);
        nodeValueToPrefabDictionary.Add(4, KatnissPrefab);
        nodeValueToPrefabDictionary.Add(5, WaterFlowerPrefab);
        nodeValueToPrefabDictionary.Add(6, RadishPrefab);
        nodeValueToPrefabDictionary.Add(7, MushroomPrefab);
        nodeValueToPrefabDictionary.Add(8, PinkFlowerPrefab);
        nodeValueToPrefabDictionary.Add(9, KatnissPrefab);
        nodeValueToPrefabDictionary.Add(10, WaterFlowerPrefab);
        nodeValueToPrefabDictionary.Add(11, RadishPrefab);
        nodeValueToPrefabDictionary.Add(12, MushroomPrefab);
        nodeValueToPrefabDictionary.Add(13, PinkFlowerPrefab);
        nodeValueToPrefabDictionary.Add(14, KatnissPrefab);
        nodeValueToPrefabDictionary.Add(15, WaterFlowerPrefab);
        nodeValueToPrefabDictionary.Add(16, RadishPrefab);
        nodeValueToPrefabDictionary.Add(17, MushroomPrefab);
        nodeValueToPrefabDictionary.Add(18, PinkFlowerPrefab);
        nodeValueToPrefabDictionary.Add(19, KatnissPrefab);
        nodeValueToPrefabDictionary.Add(20, WaterFlowerPrefab);
        nodeValueToPrefabDictionary.Add(21, RadishPrefab);
        nodeValueToPrefabDictionary.Add(22, MushroomPrefab);
        nodeValueToPrefabDictionary.Add(23, PinkFlowerPrefab);
        nodeValueToPrefabDictionary.Add(24, KatnissPrefab);
        nodeValueToPrefabDictionary.Add(25, WaterFlowerPrefab);
        nodeValueToPrefabDictionary.Add(26, RadishPrefab);
    }

    public void Start(){
       // When the scene starts, we call the DisplayNodes method to create and position the nodes
        SpawnNodes(LevelManager.Instance.GetIngredients(LevelSelector.selectedLevel));

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
            // Store the reference to the GameObject in a list
            nodeObjects.Add(nodeObject);
        }
        
    }


    /*********************************************
    METHOD: GetPrefabForNode
    DESCRIPTION: This method takes a node as a parameter and returns the prefab that corresponds to the node's value.
    *********************************************/
    private GameObject GetPrefabForNode(Node node)
    {
        return nodeValueToPrefabDictionary[node.Value];
    }

   /**********************************************
    METHOD: CalculatePosition
    DESCRIPTION: This method calculates the position of the node based on the index of the node in the list of nodes.
    The method takes an integer as a parameter, which represents the index of the node in the list.
    The method calculates the x position based on the index and returns a Vector3 with the calculated position.
    **********************************************/
   public Vector3 CalculatePosition(int nodeIndex)
{
        float leftBound = 230; // x position of the leftmost point in the red circle, HARD CODED
        float yPosition = 401; // y position where the nodes should be placed, HARD CODED
        float spaceBetweenNodes = 100 * nodeIndex; // Space between nodes, HARD CODED
        // Calculate the x position for the current node
        float xPosition = leftBound + spaceBetweenNodes;
        // Return the calculated position
        return new Vector3(xPosition, yPosition, 0);
    
}

}


