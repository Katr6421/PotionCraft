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
    private LineController LineController; // Need this to draw lines between nodes

    private GameObject currentNodeGameObject; // Reference to the current Ingredient that the user must insert in the RedBlackTree
    private int currectNodeIndex = 0;     // Index of the current node in the list of node GameObjects. 
    
    private GameObject CircleMarker; // Reference to the instantiated circle
    private Vector3 circleStartPosition = new Vector3(2.12f, 3.79f, 0); // Position of the circle

    void Start()
    {
        // Instantiate the circle from start
        CircleMarker = Instantiate(circleMarkerPrefab, circleStartPosition, Quaternion.identity);
        LineController = FindObjectOfType<LineController>();
        uiCanvas = FindObjectOfType<Canvas>();
       

    }


    private void setCurrentIngredientsGameObject()
    {
        // If there are more ingredients to insert, get the next ingredient
        if (currectNodeIndex < NodeSpawner.nodeObjects.Count){currentNodeGameObject = NodeSpawner.nodeObjects[currectNodeIndex];}
        else{Debug.Log("Ingen flere ingredienser at indsætte");}
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
        // Move the existing circle to a new position
        StartCoroutine(MoveCircle(CircleMarker, CalculatePosition(currectNodeIndex), 0.5f));

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
        DrawLinesToChildren(currentNodeGameObject, leftChildNullCircle, rightChildNullCircle);


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
    private void DrawLinesToChildren(GameObject parent, GameObject leftChild, GameObject rightChild)
    {
    List<Transform> linePoints = new List<Transform>();

    // Add the parent (current node), left child, and right child to the list
    linePoints.Add(parent.transform); // Start point
    linePoints.Add(leftChild.transform); // End point for the first line

    // Since LineController.DrawLine expects a continuous list of points for drawing,
    // you need to add the parent node again to draw from it to the right child
    linePoints.Add(parent.transform); // Start point for the second line
    linePoints.Add(rightChild.transform); // End point for the second line

    // Update the LineController with the new points
    LineController.SetUpLine(linePoints);
    // Since LineController already updates line positions in its Update method,
    // the lines should now properly connect the parent with its children.
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

IEnumerator MoveCircle(GameObject objectToMove, Vector3 destination, float duration)
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
