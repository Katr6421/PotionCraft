using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private List<GameObject> _currentInstantiatedNullCircles = new List<GameObject>();




    void Start()
    {
        //When the scene starts, we instantiate the first NullCircle aka. the root of the RedBlackTree
        SpawnRoot();

    }

    private void SpawnRoot()
    {
        // Viser den første nullCircle
        _nullCircleSpawner.NullCircles[0].SetActive(true);
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
        bool isRightPlacement = _treeManager.ValidateNodePlacement(_currentNullCircle);

        // Check if the current ingredient is not null
        if (_currentIngredient != null)
        {
            // Move the current ingredient to this NullCircle Start position.
            // Needs to be a coroutine to be able to wait for the movement to finish before deactivating the NullCircle
            StartCoroutine(MoveAndDeactivate(_currentIngredient, _currentNullCircle.transform.position, 0.5f, () =>
            {
                Debug.Log("Current ingredient has the value : " + int.Parse(_currentIngredient.GetComponentInChildren<TextMeshProUGUI>().text) + " and is placed in the nullCircle with the index " + _currentNullCircle.GetComponent<NullCircle>().Index);
                _currentIngredient.GetComponent<Ingredient>().NullCircleIndex = _currentNullCircle.GetComponent<NullCircle>().Index;
                /*********************************************
                PART 2
                *********************************************/
                // Move the circleMarker to a new position
                _levelUIController.MoveCircleMarker(CalculatePosition(_currectIngredientIndex), 0.5f);

                /*********************************************
                PART 2.1. Should we draw nullCircles? Or should we deactive them all?
                *********************************************/
                bool shouldDrawNullCircles = _treeManager.ShouldDrawNullCircles();

               
                var leftChildNullCircle = _currentNullCircle.GetComponent<NullCircle>().LeftChild;
                var rightChildNullCircle = _currentNullCircle.GetComponent<NullCircle>().RightChild;
                //Draw line from parent to leftchild, and from parent to rightchild
                DrawLinesToChildren(_currentNullCircle, leftChildNullCircle, rightChildNullCircle);
                leftChildNullCircle.GetComponent<NullCircle>().IsActive = true;
                rightChildNullCircle.GetComponent<NullCircle>().IsActive = true;


                //Maybe slet this
                if (shouldDrawNullCircles){
                    
                    ShowNullCircles();
    
                }
                else {
                    // Deactivate all nullCircles
                    HideNullCircles();
                }
                

                /*********************************************
                PART 3
                *********************************************/

                // Set the current NullCircles children to active

                

                /*********************************************
                PART 5: Validate that all red black tree rules are followed
                *********************************************/
                
                

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
        else
        {
            // Fjern alle nullCircles
            Debug.Log("Ingen flere ingredienser at indsætte");
        }
        _currectIngredientIndex++;
    }


    // these two method should maybe have its own class
    private void DrawLinesToChildren(GameObject parent, GameObject leftChild, GameObject rightChild)
    {
        //Debug.Log("inde i DrawLinesToChildren");
        //Debug.Log("parent pos " + parent.transform.position);
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

    public void VisualizeRotation(OperationType operationType, List<GameObject> ingredientsToRotate){
        // If we need to rotate Left
        GameObject grandparent;
        GameObject parent;
        GameObject rightChild;
        GameObject leftChild;
        WhoIsWho(operationType, ingredientsToRotate, out grandparent, out parent, out rightChild, out leftChild);
        
        switch (operationType)
        {
            case OperationType.RotateLeft:
                //int(index) to gameObject
                // først slå vores nullcircle op fra vores ingredient
                
                GameObject rightChildNullCircle = _nullCircleSpawner.GetComponent<NullCircleSpawner>().NullCircles[rightChild.GetComponent<Ingredient>().NullCircleIndex];
                // Så se vores nullcircles rigt child
                GameObject grandleftChildNullCircle  =  rightChildNullCircle.GetComponent<NullCircle>().LeftChild;
                
                //Check if the rightChild is  null
                if (grandleftChildNullCircle.GetComponent<NullCircle>().Value != 0){
                    Debug.Log("Jeg har et leftChild, og derfor skal mit subtree op i bagen");
                    //Bag animation
                    //rotate animation

                    // move parent to leftChild
                    // move rightChild to parent

                }
                else {

                    StartCoroutine(RotateLeftAnimation(parent, rightChild, rightChildNullCircle));
                    //kun rotate animation
                }

            
                break;
            case OperationType.RotateRight:
                
                break;
            case OperationType.FlipColors:
                throw new NotImplementedException();
            default:
                Debug.LogError("Invalid operation type. Eller vi glemte at give den en operationtype med");
                throw new ArgumentOutOfRangeException();
        }
    }



    /*********************************************
    METHOD: MoveAndDestroy                          
    DESCRIPTION: This method moves a GameObject to a destination position over a specified duration.
    *********************************************/
    IEnumerator RotateLeftAnimation(GameObject parent, GameObject rightChild, GameObject rightChildNullCircle) {
        //Move parent to leftChild
        // first remove the line from rightchild to its leftchld
        // move parent down to rightChildNullCircle parent leftchild nullcircle posistion
        // move rightchild to parent null circle posistion
        // update the nullcircle index for the parent and rightchild
        // update nullcircle value 
        // rightchild nullcircles børn skal deaktiveres

        // Assume positions are Vector3. If they are not, you will need to adjust.
        Vector3 parentOriginalPosition = parent.transform.position;
        Vector3 rightChildOriginalPosition = rightChild.transform.position;
        
        // Temporarily just picking a position for the left child based on the parent.
        // You should calculate this based on your tree's actual structure.
        Vector3 leftChildPosition = rightChildNullCircle.GetComponent<NullCircle>().Parent.GetComponent<NullCircle>().LeftChild.transform.position;

        float duration = 1.0f; // Duration of the animation in seconds
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            // Calculate the current frame's progress
            float t = elapsedTime / duration;
            // Smoothly interpolate the position of each node
            parent.transform.position = Vector3.Lerp(parentOriginalPosition, leftChildPosition, t);
            rightChild.transform.position = Vector3.Lerp(rightChildOriginalPosition, parentOriginalPosition, t);

            // Increment the elapsed time and wait for the next frame
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure both nodes are exactly in their final positions
        parent.transform.position = leftChildPosition;
        rightChild.transform.position = parentOriginalPosition;

        // Update visual connections if necessary
        // For example, you might want to adjust the lines connecting these nodes to their parent/children
        UpdateLinesAfterRotation(parent, rightChild);

        // Update the logical structure of your tree if not already done
        // This might involve updating parent/child references, colors, etc.
        // Tree.UpdateStructureAfterLeftRotation(parent, rightChild); // Example method call
        
        // Update which null circles are active
        // Dectivate rightChildNullCircle's parent's leftChild
        rightChildNullCircle.GetComponent<NullCircle>().Parent.GetComponent<NullCircle>().LeftChild.GetComponent<NullCircle>().IsActive = false;
        rightChildNullCircle.GetComponent<NullCircle>().Parent.GetComponent<NullCircle>().LeftChild.SetActive(false);
        // Deactive rightChildNullCircle's children
        rightChildNullCircle.GetComponent<NullCircle>().LeftChild.GetComponent<NullCircle>().IsActive = false;
        rightChildNullCircle.GetComponent<NullCircle>().RightChild.GetComponent<NullCircle>().IsActive = false;
        rightChildNullCircle.GetComponent<NullCircle>().LeftChild.SetActive(false);
        rightChildNullCircle.GetComponent<NullCircle>().RightChild.SetActive(false);
        
        // Activate parent's leftChild's children
        rightChildNullCircle.GetComponent<NullCircle>().Parent.GetComponent<NullCircle>().LeftChild.GetComponent<NullCircle>().LeftChild.GetComponent<NullCircle>().IsActive = true;
        rightChildNullCircle.GetComponent<NullCircle>().Parent.GetComponent<NullCircle>().LeftChild.GetComponent<NullCircle>().RightChild.GetComponent<NullCircle>().IsActive = true;
        rightChildNullCircle.GetComponent<NullCircle>().Parent.GetComponent<NullCircle>().LeftChild.GetComponent<NullCircle>().LeftChild.SetActive(true);
        rightChildNullCircle.GetComponent<NullCircle>().Parent.GetComponent<NullCircle>().LeftChild.GetComponent<NullCircle>().RightChild.SetActive(true);

        // Activate rightChildNullCircle
        rightChildNullCircle.GetComponent<NullCircle>().IsActive = true;
        rightChildNullCircle.SetActive(true);
    }
    
    void UpdateLinesAfterRotation(GameObject parent, GameObject rightChild)
    {
        // Implement logic here to update or redraw lines between nodes to reflect the new tree structure.
        // This might involve finding the line renderer components and updating their start and end points.
    }

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
        _currentNullCircle.GetComponent<NullCircle>().IsActive = false;
        // Invoke the callback to continue with the rest of the method's logic
        onComplete?.Invoke();
    }

    public void WhoIsWho(OperationType operationType, List<GameObject> ingredientsToRotate, out GameObject grandparent, out GameObject parent, out GameObject rightChild, out GameObject leftChild){
        switch (operationType)
        {
            case OperationType.RotateLeft:
                // Angive hvem der er rightChild, hvem der er parent - grandparent og leftChild må sættes til null pga 'out'
                grandparent = null;
                leftChild = null;
                if (ingredientsToRotate[0].transform.position.x > ingredientsToRotate[1].transform.position.x){
                    rightChild = ingredientsToRotate[0];
                    parent = ingredientsToRotate[1];
                } else {
                    rightChild = ingredientsToRotate[1];
                    parent = ingredientsToRotate[0];
                }
                break;
            case OperationType.RotateRight:
                // Angive hvem der er leftChild, hvem der er parent, og hvem der er grandparent - rightChild må sættes til null pga 'out'
                // Use LINQ to find the GameObject with the maximum y value
                ingredientsToRotate = ingredientsToRotate.OrderByDescending(obj => obj.transform.position.y).ToList();
                grandparent = ingredientsToRotate[0];    
                parent = ingredientsToRotate[1];
                leftChild = ingredientsToRotate[2];
                rightChild = null;
                break;
            case OperationType.FlipColors:
                throw new NotImplementedException();
            default:
                Debug.LogError("Invalid operation type. Eller vi glemte at give den en operationtype med");
                throw new ArgumentOutOfRangeException();
        }
    }


    // Used to calculate the posistion of the current ingredient marker circle
    private Vector3 CalculatePosition(int nodeIndex)
    {
        float leftBound = -0.57f; // x position of the leftmost point in the red circle, HARD CODED
        float yPosition = 3.79f; // y position where the nodes should be placed, HARD CODED
        float spaceBetweenNodes = 0.95f * nodeIndex; // Space between nodes, HARD CODED
        // Calculate the x position for the current node
        float xPosition = leftBound + spaceBetweenNodes;
        // Return the calculated position
        return new Vector3(xPosition, yPosition, 0);

    }
    public void ShowNullCircles()
    {
        foreach (KeyValuePair<int, GameObject> nullCirclePair in _nullCircleSpawner.NullCircles)
        {
            NullCircle nullCircle = nullCirclePair.Value.GetComponent<NullCircle>();
            
            if (nullCircle != null && nullCircle.IsActive) // Checking if component is not null and IsActive is true
            {
                nullCirclePair.Value.SetActive(true); // Activate the GameObject
            }

        }

    }

    public void HideNullCircles()
    {
        foreach (KeyValuePair<int, GameObject> nullCirclePair in _nullCircleSpawner.NullCircles)
        {
            NullCircle nullCircle = nullCirclePair.Value.GetComponent<NullCircle>();

            // If the NullCircle component is found and IsActive is false, deactivate the GameObject
            if (nullCircle != null && nullCircle.IsActive) // Checking if component is not null and IsActive is false
            {
                nullCirclePair.Value.SetActive(false); // Deactivate the GameObject
            }
        }

    }

   
}
