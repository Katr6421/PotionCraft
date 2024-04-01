using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField] private TreeManager _treeManager;
    [SerializeField] private Canvas _uiCanvas; // Reference to the Canvas where the nodes will be parented
  
    [SerializeField] private NodeSpawner _nodeSpawner; // Need this to access the list of node GameObjects
    [SerializeField] private LevelUIController _levelUIController; // Need this to access the CircleMarker
    [SerializeField] private NullCircleSpawner _nullCircleSpawner; // Need this to access the list of NullCircles

    private GameObject _currentIngredient; // Reference to the current Ingredient that the user must insert in the RedBlackTree
    private GameObject _currentNullCircle; // Reference to the current NullCircle that the user has clicked on
    private int _currectIngredientIndex = 0;     // Index of the current ingredient in the list of ingredients.
    private List<GameObject> _lineRenderers = new List<GameObject>();
    private int _currentNullCircleValue; // Holds the value of the current NullCircle
    
    

    void Start()
    {        
        //When the scene starts, we instantiate the first NullCircle aka. the root of the RedBlackTree
        SpawnRoot();
        
    }

    private void SpawnRoot()
    {
        // Viser den første nullCircle
        _nullCircleSpawner.nullCircles[0].SetActive(true);
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
        _currentNullCircle = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
    
       
        /*********************************************
        PART 1
        *********************************************/

        // Set the current ingredient to the next ingredient in the list
        // Set the current nullCircles value to the current ingredient value
        setNextIngredientForPlacement();

        /*********************************************
        Set the current state of the Red-Black BST tree
        *********************************************/
        _treeManager.InsertNode(_currentNullCircleValue, _currentNullCircleValue);
        /*********************************************
        Validate if the ingredient is placed right, by checking the parent, and if it's left or right child
        *********************************************/
        _treeManager.ValidateNodePlacement(_currentNullCircle);


        // check parent
        // check if it's left or rigth child


        /*********************************************
        Validate that the tree is balanced
        *********************************************/




        
        // Check if the current ingredient is not null
        if (_currentIngredient != null)
        {
            // Move the current ingredient to this NullCircle Start position.
            // Needs to be a coroutine to be able to wait for the movement to finish before deactivating the NullCircle
            StartCoroutine(MoveAndDeactivate(_currentIngredient, _currentNullCircle.transform.position, 0.5f, ()=> {

                /*********************************************
                PART 2
                *********************************************/
                // Move the circleMarker to a new position
                _levelUIController.MoveCircleMarker(CalculatePosition(_currectIngredientIndex), 0.5f);


                /*********************************************
                PART 3
                *********************************************/
            
                // Set the current NullCircles children to active
            
                var leftChildNullCircle = _currentNullCircle.GetComponent<NullCircle>().LeftChild;
                leftChildNullCircle.SetActive(true);

                var rightChildNullCircle = _currentNullCircle.GetComponent<NullCircle>().RightChild;
                rightChildNullCircle.SetActive(true);


                /*********************************************
                PART 4
                *********************************************/

                //Draw line from parent to leftchild, and from parent to rightchild
                DrawLinesToChildren(_currentNullCircle, leftChildNullCircle, rightChildNullCircle);

            }));
        }
    }


     private void setNextIngredientForPlacement()
    {
        // If there are more ingredients to insert, get the next ingredient
        if (_currectIngredientIndex < _nodeSpawner.GetNodeObjects().Count)
        {
            _currentIngredient = _nodeSpawner.GetNodeObjects()[_currectIngredientIndex];
            // Set the value of the current NullCircle to the value of the current ingredient
            _currentNullCircle.GetComponent<NullCircle>().Value = int.Parse(_currentIngredient.GetComponentInChildren<TextMeshProUGUI>().text);
            _currentNullCircleValue = _currentNullCircle.GetComponent<NullCircle>().Value;
        }
        else{
            // Fjern alle nullCircles
            Debug.Log("Ingen flere ingredienser at indsætte");
        }
        _currectIngredientIndex++;
    }

   
    // these two method should maybe have its own class
    private void DrawLinesToChildren(GameObject parent, GameObject leftChild, GameObject rightChild)
    {
        Debug.Log("inde i DrawLinesToChildren");
        Debug.Log("parent pos " + parent.transform.position);
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
        lineRenderer.startWidth = 0.05f; // Assuming lineWidth is set elsewhere in your script
        lineRenderer.endWidth = 0.05f;
        // Set the color of the line dynamically
        lineRenderer.startColor = Color.black;
        lineRenderer.endColor = Color.black;

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

    IEnumerator MoveAndDeactivate(GameObject objectToMove, Vector3 destination, float duration, Action onComplete)
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
        _currentNullCircle.SetActive(false);
        // Invoke the callback to continue with the rest of the method's logic
        onComplete?.Invoke();
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
