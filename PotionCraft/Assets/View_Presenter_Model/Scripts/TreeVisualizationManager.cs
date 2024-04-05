using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;


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
    [SerializeField] private RightRotationVisualization _rightRotationVisualization; // Need this to make right rotation animations
    [SerializeField] private LeftRotationVisualization _leftRotationVisualization; // Need this to make left rotation animations
    [SerializeField] private FlipColorVisualization _flipColorVisualization; // Need this to make left rotation animations
    [SerializeField] private JarVisualization _jarVisualization; // Need this to make left rotation animations
    [SerializeField] private Spline _spline;

    private GameObject _currentIngredient; // Reference to the current Ingredient that the user must insert in the RedBlackTree
    private GameObject _currentNullCircle; // Reference to the latest NullCircle that the user has clicked on
    private int _currectIngredientIndex = 0;     // Index of the current ingredient in the list of ingredients.
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

    // Passer ikke helt mere
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
        // The nullcircle that was clicked - Do we need to update this after rotations?
        _currentNullCircle = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        //Debug.Log("!!!!! _currentNullCircle !!!!! - I just clicked on the nullCircle with the index: " + _currentNullCircle.GetComponent<NullCircle>().Index);

        /*********************************************
        Set _currentIngredient and _currentNullCircle.Value
        *********************************************/
        setNextIngredientForPlacement();

        /*********************************************
        Set the current state of the Red-Black BST tree
        *********************************************/
        var currentIngredientValue = int.Parse(_currentIngredient.GetComponentInChildren<TextMeshProUGUI>().text);
        //Debug.Log("I am inserting the node with the value: " + currentIngredientValue + " into the tree.");
        _treeManager.InsertNode(currentIngredientValue, currentIngredientValue);

        /*********************************************
        Validate if the ingredient is placed right, by checking the parent, and if it's left or right child
        *********************************************/
        bool isRightPlacement = _treeManager.ValidateNodePlacement(_currentNullCircle);

        // If the ingredient is placed right
        if (_currentIngredient != null && isRightPlacement)
        {
             // Prepare the next ingredient for placement. Update the index in the list of ingredients, that we need to place. 
            _currectIngredientIndex++;
            
            // Move the current ingredient to _currentNullCircle position.
            // Needs to be a coroutine to be able to wait for the movement to finish before deactivating the NullCircle
            StartCoroutine(MoveAndHide(_currentIngredient, _currentNullCircle.transform.position, 0.5f, () =>
            {
                // After move and hide is done: 

                /*********************************************
                Update _currentNullCircle with the placed ingredient
                *********************************************/
                _currentNullCircle.GetComponent<NullCircle>().Ingredient = _currentIngredient;
                _currentNullCircle.GetComponent<NullCircle>().Value = currentIngredientValue;
                
                /*********************************************
                Move the CircleMarker to the new position
                *********************************************/
                _levelUIController.MoveCircleMarker(CalculatePosition(_currectIngredientIndex), 0.5f);


                /*********************************************
                Draw lines between nullCircles
                *********************************************/
                _nullCircleSpawner.UpdateLineRenderers();


                /*********************************************
                Update and show/hide all relevant nullCircles
                *********************************************/
                // Make the left and right child nullCircles active
                var leftChildNullCircle = _currentNullCircle.GetComponent<NullCircle>().LeftChild;
                var rightChildNullCircle = _currentNullCircle.GetComponent<NullCircle>().RightChild;
                leftChildNullCircle.GetComponent<NullCircle>().IsActive = true;
                rightChildNullCircle.GetComponent<NullCircle>().IsActive = true;

                // If the tree is unbalanced (something is in the queue), hide all the nullCircles 
                bool shouldDrawNullCircles = _treeManager.ShouldDrawNullCircles();
                if (shouldDrawNullCircles)
                {
                    _nullCircleSpawner.ShowNullCircles();
                }
                else 
                {
                    _nullCircleSpawner.HideNullCircles();
                }
            }));
        }
        // If the ingredient is placed wrong
        else {
            Debug.Log("Wrong null circle. You placede the ingredient wrong, try again");
            // update hint 
        }
    }


    private void setNextIngredientForPlacement()
    {
        // If there are more ingredients to insert, get the next ingredient
        if (_currectIngredientIndex < _nodeSpawner.GetNodeObjects().Count)
        {
            _currentIngredient = _nodeSpawner.GetNodeObjects()[_currectIngredientIndex];
            // Update the value of the current NullCircle to the value of the current ingredient
            _currentNullCircle.GetComponent<NullCircle>().Value = int.Parse(_currentIngredient.GetComponentInChildren<TextMeshProUGUI>().text);            
        }
        else
        {
            Debug.Log("No more ingredients to place");
        }
    }
    

    public IEnumerator VisualizeRotation(OperationType operationType, List<GameObject> ingredientsToRotate, Action onComplete){
        // All ingredients we might need to rotate
        GameObject grandparent;
        GameObject parent;
        GameObject rightChild;
        GameObject leftChild;
        WhoIsWho(operationType, ingredientsToRotate, out grandparent, out parent, out rightChild, out leftChild);

        Debug.Log("");

        // Find parentNullcircle, so our left and right rotation always takes the right nullcircle into account
        Vector3 parentPosition = parent.transform.position;
        NullCircle parentNullCircle = _nullCircleSpawner.FindNullCircleBasedOnPosition(parentPosition);
        //GameObject parentNullCircle = _currentNullCircle.GetComponent<NullCircle>().Parent;


        //Debug.Log("I am in VisualizeRotation. _currentNullCircle is index " + _currentNullCircle.GetComponent<NullCircle>().Index);
        //Debug.Log("I am in VisualizeRotation. My parentNullCircle is index " + parentNullCircle.GetComponent<NullCircle>().Index);

        switch (operationType)
        {
            case OperationType.RotateLeft:
                // h node = parent
                // Så se vores nullcircles rigt child
                //GameObject leftChildNullCircle  =  _currentNullCircle.GetComponent<NullCircle>().LeftChild;
                NullCircle rightChildLeftChildNullCircle = parentNullCircle.GetComponent<NullCircle>().RightChild.GetComponent<NullCircle>().LeftChild.GetComponent<NullCircle>();
                
                //Check if the rightChild is  null
                if (rightChildLeftChildNullCircle.Ingredient != null){
                    //Debug.Log("**********Jeg har et leftChild, og derfor skal mit subtree op i bagen**********");

                    // Bag animationl
                    NullCircle CopyRoot = _nullCircleSpawner.CopyNullCircleSubtree(rightChildLeftChildNullCircle);
                    List<GameObject> ingredientsToJar = _nullCircleSpawner.CollectIngredients(rightChildLeftChildNullCircle, new List<GameObject>());
                    //Update nullcircle
                    _nullCircleSpawner.setNullCircleToDefault(rightChildLeftChildNullCircle);
                    _nullCircleSpawner.UpdateLineRenderers();
                    


                    //yield return StartCoroutine(_jarVisualization.ShrinkMultiple(ingredientsToJar, () => {}));
                    // Move subtree to jar
                    //Debug.Log("Jeg starter followSpline animation wuuuuuu!!!!");
            
                    foreach (GameObject ingredient in ingredientsToJar)
                    {
                        _spline.ChangeFirstKnotPosition(ingredient.transform.position);
                        yield return StartCoroutine(_spline.FollowSpline(ingredient));
                        
                    }
                    
                    yield return StartCoroutine(_leftRotationVisualization.RotateLeftAnimation(parent, rightChild, parentNullCircle));
                    // yield return (_jarAnimation.GrowAndMove(yourGameObject, pathPoints, finalPosition, totalAnimationDuration));
                    Debug.Log("Jeg har kopieret nullcircle og nu er jeg blevet nulstillet og mine lijer er blevet opdateret");
                    _nullCircleSpawner.PrintNullCircles();
                    
                    
                    
                    

                

                }
                else {
                    // Debug.Log("Jeg har ikke et leftChild, og derfor skal jeg bare rotere");
                    Debug.Log("Jeg starter RotateLeftAnimation");
                    yield return StartCoroutine(_leftRotationVisualization.RotateLeftAnimation(parent, rightChild, parentNullCircle));
                }
                break;


            case OperationType.RotateRight:
                //h node = grandparent
                GameObject rightChildNullCircle  =  parentNullCircle.GetComponent<NullCircle>().RightChild;
                
                 //check if parent right child er null
                if (rightChildNullCircle.GetComponent<NullCircle>().Ingredient != null){
                    // Debug.Log("Jeg har et rightChild, og derfor skal mit subtree op i bagen");
                    // Bag animation
                    // rotate animation
                    // move parent to leftChild
                    // move rightChild to parent
                }
                else {
                    // Debug.Log("Jeg har ikke et rightChild, og derfor skal jeg bare rotere");
                    Debug.Log("Jeg starter RotateRightAnimation");
                    yield return StartCoroutine(_rightRotationVisualization.RotateRightAnimation(leftChild, parent, grandparent, parentNullCircle));
                }                
                break;
            case OperationType.FlipColors:
                // h node = parent
                Debug.Log("Jeg starter FlipColorAnimation");
                yield return StartCoroutine(_flipColorVisualization.FlipColorAnimation(parentNullCircle));
                break;
            default:
                Debug.LogError("Invalid operation type. Eller vi glemte at give den en operationtype med");
                throw new ArgumentOutOfRangeException();
        }
        onComplete?.Invoke();
    }


    IEnumerator MoveAndHide(GameObject objectToMove, Vector3 destination, float duration, Action onComplete)
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
        //_currentNullCircle.SetActive(false);
        _nullCircleSpawner.HideNullCircle(_currentNullCircle.GetComponent<NullCircle>());
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
                grandparent = null;

                // The parent is the node with the highest y value.
                parent = ingredientsToRotate.OrderByDescending(obj => obj.transform.position.y).FirstOrDefault();

                // The left child is the node with the lowest x value, excluding the parent.
                leftChild = ingredientsToRotate
                            .OrderBy(obj => obj.transform.position.x)
                            .FirstOrDefault();

                // The right child is the node with the highest x value, excluding the parent.
                rightChild = ingredientsToRotate
                             .OrderByDescending(obj => obj.transform.position.x)
                             .FirstOrDefault();

                // Additional checks to ensure we have found all nodes.
                if (parent == null || leftChild == null || rightChild == null)
                {
                    Debug.LogError("Failed to identify nodes correctly for FlipColors operation.");
                }
                break;
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
    

      /***************************** GAMMELT  UDKOMMENTERET *************************************/

    /*IEnumerator MoveNodeWithSubtree(GameObject nullCircleToMove, Vector3 newPosition, float duration, Action onComplete) {
        Debug.Log(" !!!! IN MOVENODEWITHSUBTREE !!!!");
        NullCircle nullCircle = nullCircleToMove.GetComponent<NullCircle>();


        // Check grandparent's rightChild + subtree
        // Recursively move children subtrees
        // Move parent to grandparent's original position
        // Move leftChild to parent's original position
        // Check if leftChild has subtree (left + right)
        // Recursively move children subtrees

        // set grandparent's leftChild.Ingredient to null - as test
        // if grandparent has rightChild


        // Recursively move children subtrees.
        if (nullCircle.RightChild.GetComponent<NullCircle>().Ingredient != null) {
            yield return StartCoroutine(MoveNodeWithSubtree(nullCircle.RightChild, newPosition - (nullCircleToMove.transform.position - nullCircle.RightChild.transform.position), duration,  () => {}));
        }
        if (nullCircle.LeftChild.GetComponent<NullCircle>().Ingredient != null) {
            yield return StartCoroutine(MoveNodeWithSubtree(nullCircle.LeftChild, newPosition - (nullCircleToMove.transform.position - nullCircle.LeftChild.transform.position), duration, () => {}));
        }
        

        if (nullCircle.Ingredient != null) {
            yield return StartCoroutine(MoveNode(nullCircle.Ingredient, newPosition, duration, nullCircle, () => {}));
        }

        yield return null;
        onComplete?.Invoke();
    }*/ 
    
    /*IEnumerator RotateRightAnimation(GameObject leftChild, GameObject parent, GameObject grandparent, GameObject parentNullCircle) {
        // Move grandparent to grandparent.rightChild
        // Move parent to grandparent
        // Move leftChild to parent
        
       
        
        // update lines
        // update the nullcircle index for the parent and rightchild
        // update nullcircle value
        // update hvilke nullCircles der skal vises


        // Assume positions are Vector3. If they are not, you will need to adjust.
        Vector3 grandParentOriginalPosition = grandparent.transform.position;
        Vector3 parentOriginalPosition = parent.transform.position;
        Vector3 leftChildOriginalPosition = leftChild.transform.position;
        
        // This is the posistion we dont have yet, but we need to move the grandparent to its rightChild
        Vector3 grandParentRightChildPosition = parentNullCircle.GetComponent<NullCircle>().Parent.GetComponent<NullCircle>().RightChild.transform.position;

        float duration = 1.0f; // Duration of the animation in seconds
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            // Calculate the current frame's progress
            float t = elapsedTime / duration;
            // Smoothly interpolate the position of each node
            grandparent.transform.position = Vector3.Lerp(grandParentOriginalPosition, grandParentRightChildPosition, t);
            parent.transform.position = Vector3.Lerp(parentOriginalPosition, grandParentOriginalPosition, t);
            leftChild.transform.position = Vector3.Lerp(leftChildOriginalPosition, parentOriginalPosition, t);

            // Increment the elapsed time and wait for the next frame
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure both nodes are exactly in their final positions
        grandparent.transform.position = grandParentRightChildPosition;
        parent.transform.position = grandParentOriginalPosition;
        leftChild.transform.position = parentOriginalPosition;

        //All nullCircles values need to be updated
        UpdateNullCirclesValuesAfterRightRotation(leftChild, parent, grandparent, parentNullCircle);

        // Update visual connections if necessary
        // For example, you might want to adjust the lines connecting these nodes to their parent/children
        UpdateLinesAfterRotation(leftChild, parent, grandparent);
        _nullCircleSpawner.UpdateActiveNullCircles();
        ShowNullCircles();
    }*/

   
    

    /*public void UpdateNullCirclesValuesAfterRightRotation(GameObject leftChild, GameObject parent, GameObject grandparent, GameObject parentNullCircle)
    {
        // Update the values of the null circles after a right rotation
        // The grandparent's value should be moved to the parent

        // Update the null cirle grandparents right child to ingredient grandparent's value (GRANDPARENT orignal)
        parentNullCircle.GetComponent<NullCircle>().Parent.GetComponent<NullCircle>().RightChild.GetComponent<NullCircle>().Value = int.Parse(grandparent.GetComponentInChildren<TextMeshProUGUI>().text);

        //Update null circle grandparent value to ingredient parent's value (PARENT orignal)
        parentNullCircle.GetComponent<NullCircle>().Parent.GetComponent<NullCircle>().Value = int.Parse(parent.GetComponentInChildren<TextMeshProUGUI>().text);
        
        // Update the null cirle parent to ingredient leftChild's value (LEFTCHILD orignal)
        parentNullCircle.GetComponent<NullCircle>().Value = int.Parse(leftChild.GetComponentInChildren<TextMeshProUGUI>().text);
        
        // Child left null circles should be deactivated if they do not have a value
    }*/

   
}
