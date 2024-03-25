using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private Canvas uiCanvas;
    // Assign the prefab in the inspector
    [SerializeField] private GameObject KatnissPrefab; 
    [SerializeField] private GameObject MushroomPrefab;
    [SerializeField] private GameObject PinkFlowerPrefab;
    [SerializeField] private GameObject RadishPrefab;
    [SerializeField] private GameObject WaterFlowerPrefab;

    private Dictionary<int, GameObject> nodeValueToPrefabDictionary = new Dictionary<int, GameObject>();

    void Awake() {
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
        Debug.Log("Besked fra NodeView: Nu kalder jeg DisplayNodes");
        DisplayNodes(LevelManager.Instance.GetIngredients(LevelSelector.selectedLevel));
  
    }
  
    // Call this method to create and position sprites based on your Node data
    public void DisplayNodes(List<Node> nodes)
    {
        foreach (Node node in nodes)
        {
            Debug.Log("Besked fra NodeView: Node.Value er " + node.Value);
            
            // Keep track of the index of the current node. Used for positioning
            int nodeIndex = nodes.IndexOf(node);
            // Calculate the position of the node based on the index
            Vector3 newNodePosition = CalculatePosition(nodeIndex);
            //Instantiate a new GameObject from the prefab
            GameObject nodeObject = Instantiate(GetPrefabForNode(node), newNodePosition, Quaternion.identity);
            // Parent the instantiated UI element to a canvas or a panel within the canvas, if needed
            nodeObject.transform.SetParent(uiCanvas.transform, false);

            nodeObject.GetComponentInChildren<TextMeshProUGUI>().text = node.Value.ToString();

            // ikke sikker på at vi skal bruge det, men det lyder smart
            // Set the Node data to the GameObject for later reference
            nodeObject.AddComponent<NodeData>().Data = node;

        }
    }


    private GameObject GetPrefabForNode(Node node)
    {
        // TODO: ved ikke om det er rigtigt at bruge Value her. Om det overholder vores arkitektur
        return nodeValueToPrefabDictionary[node.Value];
    }

    /*private Sprite GetSpriteForNode(Node node)
    {
        // Logic to select the correct sprite based on the Node's data
        // You can have a dictionary mapping node types to sprites, for example
    }*/

   public Vector3 CalculatePosition(int nodeIndex)
{
        float leftBound = 230; // x position of the leftmost point in the red circle
        float yPosition = 401; // y position where the nodes should be placed
        float spaceBetweenNodes = 100 * nodeIndex;
        // Calculate the x position for the current node
        float xPosition = leftBound + spaceBetweenNodes;
        // Return the calculated position
        return new Vector3(xPosition, yPosition, 0);
    
}

}

// This component will store the reference to the Node data
public class NodeData : MonoBehaviour
{
    public Node Data { get; set; }

}
