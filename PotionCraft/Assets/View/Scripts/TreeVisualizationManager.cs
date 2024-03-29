using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

/*********************************************
CLASS: TreeVisualizationManager
DESCRIPTION: This class is responsible for managing the visualization of the RedBlackTree, when clicking on a nullCircle.
It is responsible for moving the current ingredient to the NullCircle's position, deleting the clicked NullCircle, moving the CircleMarker to a new position, and instantiating two new NullCircles.
Summary: Everyting that happens when a NullCircle is clicked.
// TODO: Should be refactored so that it does not have so many responsibilities.
*********************************************/


public class TreeVisualizationManager : MonoBehaviour
{
    
    [SerializeField] private GameObject _nullCirclePrefab;
    private Canvas _uiCanvas; // Reference to the Canvas where the nodes will be parented
  
    private NodeSpawner _nodeSpawner; // Need this to access the list of node GameObjects

    private GameObject _currentNodeGameObject; // Reference to the current Ingredient that the user must insert in the RedBlackTree
    private static int _currectNodeIndex = 0;     // Index of the current node in the list of node GameObjects.
    
    private LevelUIController _levelUIController; // Need this to access the CircleMarker
    private List<GameObject> _lineRenderers = new List<GameObject>();
    private FrameController _frameController;

    void Start()
    {
        _levelUIController = FindObjectOfType<LevelUIController>();
        _nodeSpawner = FindObjectOfType<NodeSpawner>();
        _uiCanvas = FindObjectOfType<Canvas>();
        _frameController = FindObjectOfType<FrameController>();
        
        //When the scene starts, we instantiate the first NullCircle aka. the root of the RedBlackTree
        SpawnRoot();
        
    }

    private void SpawnRoot(){
        Vector3 vector = _frameController.PlaceRoot(_uiCanvas);
        GameObject nullCircle = Instantiate(_nullCirclePrefab, vector, Quaternion.identity);
        nullCircle.transform.SetParent(_uiCanvas.transform, false);
    }

    /*********************************************
    METHOD: OnClickedNullCirkle                          
    DESCRIPTION: This method is called when the NullCircle GameObject is clicked.
    Part 1: Move the current ingredient to the NullCircle's position
    Part 2: Moves the CircleMarker to a new position. The CircleMarker is used to indicate the current ingredient that the user must insert in the RedBlackTree.
    Part 3: Instantiate two new NullCircles
    Part 4: Draw lines from the parent to the left and right child
    Part 5: Should only move the nodes so there are room for the new nodes. Not implemented yet. Should NOT do the rotation. 
    *********************************************/

     public void OnClickedNullCircle()
    {
        Debug.Log("inde i OnClickedNullCircle");
        /*********************************************
        PART 1
        *********************************************/

        // Get the current ingredient that the user must insert in the RedBlackTree
        setCurrentIngredientsGameObject();

        // Needs to store this position, to be able to move the current ingredient to this position and to calculate the position of the new NullCircles
        Vector3 CurrentNullCircleStartPosistion = transform.position;
        // Check if the current ingredient is not null
        if (_currentNodeGameObject != null)
        {
            // Move the current ingredient to this NullCircle Start position.
            // Needs to be a coroutine to be able to wait for the movement to finish before destroying the NullCircle
            StartCoroutine(MoveAndDestroy(_currentNodeGameObject, CurrentNullCircleStartPosistion, 0.5f));

        }

        /*********************************************
        PART 2
        *********************************************/
        // Move the circlemarker to a new position
        _levelUIController.MoveCircleMarker(CalculatePosition(_currectNodeIndex), 0.5f);


        /*********************************************
        PART 3
        *********************************************/
    
        // Instantiate the two new NullCircles. When instantiating the new NullCircles, we need to calculate the position based on canvas. We need to translate the world position to a canvas position.
        //GameObject leftChildNullCircle = Instantiate(nullCirclePrefab, CalculateLeftChildPosition(Utilities.WorldToCanvasPosition(uiCanvas,CurrentNullCircleStartPosistion)), Quaternion.identity);
        GameObject leftChildNullCircle = Instantiate(_nullCirclePrefab, CalcLeftChildPos(_currentNodeGameObject), Quaternion.identity);
        GameObject rightChildNullCircle = Instantiate(_nullCirclePrefab, CalculateRightChildPosition(Utilities.WorldToCanvasPosition(_uiCanvas,CurrentNullCircleStartPosistion)), Quaternion.identity);
        // Just needs this, not sure why
        leftChildNullCircle.transform.SetParent(_uiCanvas.transform, false);
        rightChildNullCircle.transform.SetParent(_uiCanvas.transform, false);

        /*********************************************
        PART 4
        *********************************************/

        //Draw line from parent to leftchild, and from parent to rightchild
        DrawLinesToChildren(_currentNodeGameObject, leftChildNullCircle, rightChildNullCircle);
        //DrawLinesToChildren(NullCirclePos, leftChildNullCircle, rightChildNullCircle);

        /*********************************************
        PART 5 (TODO) 
        *********************************************/

        //Move nodes to new positions, based on deepth of the tree. Makes room for the nodes

        
    }

     private void setCurrentIngredientsGameObject()
    {
        // If there are more ingredients to insert, get the next ingredient

        if (_currectNodeIndex < _nodeSpawner.GetNodeObjects().Count)
        {
            _currentNodeGameObject = _nodeSpawner.GetNodeObjects()[_currectNodeIndex];
        }
        else{
            Debug.Log("Ingen flere ingredienser at indsætte");
        }
        _currectNodeIndex++;
    }
  

    Vector3 CalculateLeftChildPosition(Vector3 ParentNodePosition)
    {
        float xPosition = ParentNodePosition.x - 85;
        float yPosition = ParentNodePosition.y - 122;
        return new Vector3(xPosition, yPosition, 0);
    }

    private Vector3 CalcLeftChildPos(GameObject ParentNodePos)
    {
        return _frameController.FindMiddleX(_uiCanvas, ParentNodePos, true);
    }

    Vector3 CalculateRightChildPosition(Vector3 ParentNodePosition)
    {
        float xPosition = ParentNodePosition.x + 85;
        float yPosition = ParentNodePosition.y - 122;
        return new Vector3(xPosition, yPosition, 0);
    }

   
    // these two method should maybe have its own class
    private void DrawLinesToChildren(GameObject parent, GameObject leftChild, GameObject rightChild)
    {
    // Create a line renderer for the connection from parent to left child
    GameObject lineRendererLeft = CreateLineRenderer(parent.transform, leftChild.transform);
    // Store the line renderer GameObject in the list
    _lineRenderers.Add(lineRendererLeft);

    // Create a line renderer for the connection from parent to right child
    GameObject lineRendererRight = CreateLineRenderer(parent.transform, rightChild.transform);
    // Store the line renderer GameObject in the list
    
    _lineRenderers.Add(lineRendererRight);
    }

    // these two method should maybe have its own class
    private GameObject CreateLineRenderer(Transform startPosition, Transform endPosition)
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

    // Add your LineController script to the new GameObject
    LineController lineController = lineGameObject.AddComponent<LineController>();

    // Create a list of points for the line
    List<Transform> linePoints = new List<Transform>();
    linePoints.Add(startPosition); // Start point
    linePoints.Add(endPosition); // End point for the first line

    // Set up the line using the LineController script
    lineController.SetUpLine(linePoints);

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


// Used to calculate the posistion of the current ingredient marker circle
 private Vector3 CalculatePosition(int nodeIndex)
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
