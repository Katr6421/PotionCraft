using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/*********************************************
CLASS: TreeVisualizationManager
DESCRIPTION: This class is responsible for managing the visualization of the RedBlackTree, when clicking on a nullCircle.
It is responsible for moving the current ingredient to the NullCircle's position, deleting the clicked NullCircle, moving the CircleMarker to a new position, and instantiating two new NullCircles.
Summary: Everyting that happens when a NullCircle is clicked.
*********************************************/


public class TreeVisualizationManager : MonoBehaviour
{
    
    [SerializeField] private GameObject nullCirclePrefab;
    [SerializeField] private GameObject circleMarkerPrefab;
    
    private Canvas uiCanvas; // Reference to the Canvas where the nodes will be parented
  
    private NodeSpawner NodeSpawner; // Need this to access the list of node GameObjects

    private GameObject currentNodeGameObject; // Reference to the current Ingredient that the user must insert in the RedBlackTree
    private static int currectNodeIndex = 0;     // Index of the current node in the list of node GameObjects.
    private LineController LineController; // Need this to draw lines between nodes 
    
    
    private LevelUIController LevelUIController; // Need this to access the CircleMarker
   
    public Material lineMaterial;
    public float lineWidth = 0.1f;

    private List<GameObject> lineRenderers = new List<GameObject>();

    void Start()
    {
        // Instantiate the circle from start
       // CircleMarker = Instantiate(circleMarkerPrefab, circleStartPosition, Quaternion.identity);
        LineController = FindObjectOfType<LineController>();
        LevelUIController = FindObjectOfType<LevelUIController>();
        NodeSpawner = FindObjectOfType<NodeSpawner>();
        uiCanvas = FindObjectOfType<Canvas>();
       

    }


    private void setCurrentIngredientsGameObject()
    {
        // If there are more ingredients to insert, get the next ingredient
        Debug.Log("currectNodeIndex" + currectNodeIndex);
        Debug.Log("Hvor mange ingredienser der er i listen" + NodeSpawner.GetNodeObjects().Count);
        //(currectNodeIndex < NodeSpawner.nodeObjects.Count)

        if (currectNodeIndex < NodeSpawner.GetNodeObjects().Count)
        {
            currentNodeGameObject = NodeSpawner.GetNodeObjects()[currectNodeIndex];
        }
        else{
            Debug.Log("Ingen flere ingredienser at indsætte");
        }
        currectNodeIndex++;
    }


    /*********************************************
    METHOD: OnClickedNullCirkle                          
    DESCRIPTION: This method is called when the NullCircle GameObject is clicked.
    Part 1: Move the current ingredient to the NullCircle's position
    Part 2: Moves the CircleMarker to a new position. The CircleMarker is used to indicate the current ingredient that the user must insert in the RedBlackTree.
    Part 3: Instantiate two new NullCircles
    *********************************************/

     public void OnClickedNullCirkle()
    {
        /*********************************************
        PART 1
        *********************************************/

        // Get the current ingredient that the user must insert in the RedBlackTree
        setCurrentIngredientsGameObject();
        Debug.Log("Clokation for nullcircle først" + transform.position);
        Vector3 NullCirclePos = transform.position;
        Transform NullCircleTransform = transform;
        // Check if there are any node GameObjects in the list
        if (currentNodeGameObject != null)
        {
            // Move the current ingredient to this NullCircle's position
            
            StartCoroutine(MoveAndDestroy(currentNodeGameObject, NullCirclePos, 0.5f));

        }
        else
        {
            Debug.Log("Ingen flere ingredienser at indsætte");
        }

        /*********************************************
        PART 2
        *********************************************/
        // Move the circlemarker to a new position
        LevelUIController.MoveCircleMarker(CalculatePosition(currectNodeIndex), 0.5f);



        /*********************************************
        PART 3
        *********************************************/
    
        Debug.Log("Clokation for nullcircle først" + transform.position);
        // Instantiate the two new NullCircles
        GameObject leftChildNullCircle = Instantiate(nullCirclePrefab, CalculateLeftChildPosition(WorldToCanvasPosition(uiCanvas,NullCirclePos)), Quaternion.identity);
        GameObject rightChildNullCircle = Instantiate(nullCirclePrefab, CalculateRightChildPosition(WorldToCanvasPosition(uiCanvas,NullCirclePos)), Quaternion.identity);
        leftChildNullCircle.transform.SetParent(uiCanvas.transform, false);
        rightChildNullCircle.transform.SetParent(uiCanvas.transform, false);

        //Draw line from parent to leftchild, and from parent to rightchild
        DrawLinesToChildren(NullCircleTransform, leftChildNullCircle, rightChildNullCircle);
        //DrawLinesToChildren(NullCirclePos, leftChildNullCircle, rightChildNullCircle);


        //Move nodes to new positions, based on deepth of the tree. Makes room for the nodes

        
    }
  
    

 

    

    Vector3 CalculateLeftChildPosition(Vector3 ParentNodePosition)
    {
        float xPosition = ParentNodePosition.x - 85;
        float yPosition = ParentNodePosition.y - 122;
        return new Vector3(xPosition, yPosition, 0);
    }

    Vector3 CalculateRightChildPosition(Vector3 ParentNodePosition)
    {
        float xPosition = ParentNodePosition.x + 85;
        float yPosition = ParentNodePosition.y - 122;
        return new Vector3(xPosition, yPosition, 0);
    }

    public Vector2 WorldToCanvasPosition(Canvas canvas, Vector3 worldPosition)
{
    // Calculate the position of the world position on the canvas
    Vector2 viewportPosition = Camera.main.WorldToViewportPoint(worldPosition);
    Vector2 canvasSize = canvas.GetComponent<RectTransform>().sizeDelta;

    // Convert the viewport position to be relative to the canvas
    return new Vector2((viewportPosition.x * canvasSize.x) - (canvasSize.x * 0.5f),
                       (viewportPosition.y * canvasSize.y) - (canvasSize.y * 0.5f));
}

   

    // Assuming this method is inside TreeVisualizationManager class
    private void DrawLinesToChildren(Transform parent, GameObject leftChild, GameObject rightChild)
{
    // Create a line renderer for the connection from parent to left child
    GameObject lineRendererLeft = CreateLineRenderer(parent.transform.position, leftChild.transform.position);
    // Store the line renderer GameObject in the list
    lineRenderers.Add(lineRendererLeft);

    // Create a line renderer for the connection from parent to right child
    GameObject lineRendererRight = CreateLineRenderer(parent.transform.position, rightChild.transform.position);
    // Store the line renderer GameObject in the list
    lineRenderers.Add(lineRendererRight);
}

private GameObject CreateLineRenderer(Vector3 startPosition, Vector3 endPosition)
{
    // Create a new GameObject with a name indicating it's a line renderer
    GameObject lineGameObject = new GameObject("LineRendererObject");

    // Add a LineRenderer component to the GameObject
    LineRenderer lineRenderer = lineGameObject.AddComponent<LineRenderer>();

    // Set up the LineRenderer
    //lineRenderer.material = lineMaterial; // Assuming lineMaterial is assigned elsewhere in your script
    lineRenderer.material = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply"));
    lineRenderer.startWidth = 0.1f; // Assuming lineWidth is set elsewhere in your script
    lineRenderer.endWidth = 0.1f;

    // Set the positions
    lineRenderer.positionCount = 2;
    lineRenderer.SetPositions(new Vector3[] { startPosition, endPosition });

    return lineGameObject;
}



/*********************************************
    METHOD: MoveAndDestroy                          
    DESCRIPTION: This method moves a GameObject to a destination position over a specified duration.
    *********************************************/

      IEnumerator MoveAndDestroy(GameObject objectToMove, Vector3 destination, float duration)
    {
    float elapsedTime = 0;
    Vector3 startingPos = objectToMove.transform.position;

    while (elapsedTime < duration)
    {
        objectToMove.transform.position = Vector3.Lerp(startingPos, destination, (elapsedTime / duration));
        elapsedTime += Time.deltaTime;
        yield return null; // Wait for the next frame
    }

    objectToMove.transform.position = destination; // Ensure it reaches the destination

    // Now that the movement is complete, destroy the NullCircle GameObject
    Destroy(gameObject); // Assuming gameObject is the NullCircle you want to destroy
    
}



 public Vector3 CalculatePosition(int nodeIndex)
{
        float leftBound = 2.12f; // x position of the leftmost point in the red circle, HARD CODED
        float yPosition = 3.79f; // y position where the nodes should be placed, HARD CODED
        float spaceBetweenNodes = 0.95f * nodeIndex; // Space between nodes, HARD CODED
        // Calculate the x position for the current node
        float xPosition = leftBound + spaceBetweenNodes;
        // Return the calculated position
        return new Vector3(xPosition, yPosition, 0);
    
}
}
