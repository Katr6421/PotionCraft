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
*********************************************/


public class TreeVisualizationManager : MonoBehaviour
{

    [SerializeField] private GameObject _nullCirclePrefab;
    [SerializeField] private TreeManager _treeManager;
    [SerializeField] private Canvas _uiCanvas; // Reference to the Canvas where the nodes will be parented
    [SerializeField] private NodeSpawner _nodeSpawner; // Need this to access the list of node GameObjects
    [SerializeField] private LevelUIController _levelUIController; // Need this to access the CircleMarker
    [SerializeField] private NullCircleManager _nullCircleManager; // Need this to access the list of NullCircles
    [SerializeField] private RightRotationVisualization _rightRotationVisualization; // Need this to make right rotation animations
    [SerializeField] private LeftRotationVisualization _leftRotationVisualization; // Need this to make left rotation animations
    [SerializeField] private FlipColorVisualization _flipColorVisualization; // Need this to make left rotation animations
    [SerializeField] private JarVisualization _jarVisualization; // Need this to make left rotation animations
    [SerializeField] private VisualizationHelper _visualizationHelper;
    [SerializeField] private SplineToJar _splineToJar;
    [SerializeField] private AvatarHintManager _avatarHintManager;
    [SerializeField] private LineRendererManager _lineRendererManager;


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
        _nullCircleManager.NullCircles[0].SetActive(true);
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


        /*********************************************
        Set _currentIngredient and _currentNullCircle.Value
        *********************************************/
        setNextIngredientForPlacement();

        /*********************************************
        Insert the value of the current ingredient the user needs to insert as a node in the Red-Black BST tree
        *********************************************/
        var currentIngredientValue = int.Parse(_currentIngredient.GetComponentInChildren<TextMeshProUGUI>().text);
        _treeManager.InsertNode(currentIngredientValue, currentIngredientValue);

        /*********************************************
        Validate if the clicked nullCircle is the right one by looking up in the tree
        *********************************************/
        bool isValidPlacement = _treeManager.ValidateNodePlacement(_currentNullCircle);

        /*********************************************
        If the ingredient is placed right, we can continue with the visualization
        *********************************************/
        if (_currentIngredient != null && isValidPlacement)
        {

            // Prepare the next ingredient for placement. Update the index in the list of ingredients, that we need to place. 
            _currectIngredientIndex++;


            // Move the current ingredient to _currentNullCircle position.
            // Needs to be a coroutine to be able to wait for the movement to finish before deactivating the NullCircle
            StartCoroutine(MoveAndHide(_currentIngredient, _currentNullCircle.transform.position, 0.5f, () =>
            {


                /*********************************************
                Update _currentNullCircle with the placed ingredient
                *********************************************/
                _currentNullCircle.GetComponent<NullCircle>().Ingredient = _currentIngredient;
                _currentNullCircle.GetComponent<NullCircle>().Value = currentIngredientValue;

                /*********************************************
                Move the CircleMarker to the new position
                // TODO: - This needs to be done somewhere else. Maybe treemanager.
                *********************************************/
                _levelUIController.MoveCircleMarker(CalculatePosition(_currectIngredientIndex), 0.5f);

                /*********************************************
                Draw lines between nullCircles
                *********************************************/
                _lineRendererManager.UpdateLineRenderers();
                //_lineRendererManager.SpawnLinesToParent(_currentNullCircle.GetComponent<NullCircle>().LeftChild, _currentIngredient);
                //_lineRendererManager.SpawnLinesToParent(_currentNullCircle.GetComponent<NullCircle>().RightChild, _currentIngredient);
                //_lineRendererManager.ShowLineRender(_currentNullCircle.GetComponent<NullCircle>().LeftChild);
                //_lineRendererManager.ShowLineRender(_currentNullCircle.GetComponent<NullCircle>().RightChild);


                /*********************************************
                Check if the tree is unbalanced (something is in the operation queue) after we have inserted the ingredient
                *********************************************/

                bool isTreeInBalanced = _treeManager.IsTreeInBalanced();
                if (isTreeInBalanced)
                {
                    _nullCircleManager.ShowAllChildrenNullCircles();
                    // The user has placed the ingredient in the right place and the tree is in balance
                    _avatarHintManager.UpdateHint("correct", AvatarHint.SelectedRightPlacementAndInBalance);

                }
                else
                {
                    _nullCircleManager.HideAllNullCircles();

                    /********************************************
                    The user has placed the ingredient in the right place, but the tree is unbalanced
                    Update the hint based on the current operation type, the user needs to perform next
                    *********************************************/
                    OperationType currentOperationType = _treeManager.GetOperationType();
                    switch (currentOperationType)
                    {
                        case OperationType.RotateLeft:
                            _avatarHintManager.UpdateHint("hint", AvatarHint.SelectedRightPlacementButNeedsToSelectTwoNodes);
                            break;
                        case OperationType.RotateRight:
                            _avatarHintManager.UpdateHint("hint", AvatarHint.SelectedRightPlacementButNeedsToSelectThreeNodes);
                            break;
                        case OperationType.FlipColors:
                            _avatarHintManager.UpdateHint("hint", AvatarHint.SelectedRightPlacementButNeedToFlipColor);
                            break;
                        default:
                            Debug.LogError("Invalid operation type. Eller vi glemte at give den en operationtype med");
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }));
        }
        /*********************************************
        If the ingredient is placed wrong
        *********************************************/
        else
        {
            Debug.Log("Wrong null circle. You placed the ingredient wrong, try again");
            // Delete the node from the Red-Black BST tree and clear the operation queue
            _treeManager.DeleteNodeAndClearOperations(currentIngredientValue);

            // TODO: Update hint 

            // Allow user to click on the nullCircles
            _nullCircleManager.ShowAllChildrenNullCircles();
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


    public IEnumerator VisualizeRotation(OperationType operationType, List<GameObject> ingredientsToRotate, Action onComplete)
    {
        // All ingredients we might need to rotate
        GameObject grandparent;
        GameObject parent;
        GameObject rightChild;
        GameObject leftChild;
        WhoIsWho(operationType, ingredientsToRotate, out grandparent, out parent, out rightChild, out leftChild);

        //Debug.Log("");

        // Find parentNullcircle, so our left and right rotation always takes the right nullcircle into account
        Vector3 parentPosition = parent.transform.position;
        NullCircle parentNullCircle = _nullCircleManager.FindNullCircleBasedOnPosition(parentPosition);
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

                /*********************************************
                Check if the rightChild's leftChild is null -> Bag animation
                *********************************************/
                if (rightChildLeftChildNullCircle.Ingredient != null)
                {
                    //Debug.Log("**********Jeg har et leftChild, og derfor skal mit subtree op i bagen**********");

                    /*********************************************
                    Make a copy of all the nullCircles and instantiate them with the ingredients' values
                    *********************************************/
                    NullCircle copyRootOfSubTree = _nullCircleManager.CopyNullCircleSubtree(rightChildLeftChildNullCircle);


                    /*********************************************
                    Store all the ingredients in the subtree in a list
                    *********************************************/
                    List<GameObject> ingredientsToJar = _nullCircleManager.CollectIngredients(rightChildLeftChildNullCircle, new List<GameObject>());


                    /*********************************************
                    Update the nullCircles to default value after we have copied them
                    *********************************************/
                    _nullCircleManager.setNullCircleToDefault(rightChildLeftChildNullCircle);


                    /*********************************************
                    Update the line renderers after we have sat the nullCircles to default
                    *********************************************/
                    _lineRendererManager.UpdateLineRenderers();



                    // MÅSKE UDNØDVENDIGT MED AT SHRINK
                    //yield return StartCoroutine(_jarVisualization.ShrinkMultiple(ingredientsToJar, () => {}));


                    /*********************************************
                    Move the ingredients to the jar (visually)
                    *********************************************/
                    foreach (GameObject ingredient in ingredientsToJar)
                    {
                        //_spline.ChangeFirstVector3Position(ingredient.transform.position);
                        //yield return StartCoroutine(_spline.FollowPointsToJar(ingredient));
                        _splineToJar.ChangeFirstKnot(ingredient.transform.position);
                        yield return StartCoroutine(_splineToJar.FollowSplineToJar(ingredient));

                    }

                    /*********************************************
                    Start the left rotation animation
                    *********************************************/
                    yield return StartCoroutine(_leftRotationVisualization.RotateLeftAnimation(parent, rightChild, parentNullCircle));


                    // MÅSKE UDNØDVENDIGT MED AT SHRINK
                    // yield return (_jarAnimation.GrowAndMove(yourGameObject, pathPoints, finalPosition, totalAnimationDuration));
                    //Debug.Log("Jeg har kopieret nullcircle og nu er jeg blevet nulstillet og mine lijer er blevet opdateret");
                    //_nullCircleSpawner.PrintNullCircles();



                    /*********************************************
                    Move the ingredients back to the tree (both visually and on the nullCircles)
                    *********************************************/
                    NullCircle rootToPlaceSubtree = parentNullCircle.GetComponent<NullCircle>().LeftChild.GetComponent<NullCircle>().RightChild.GetComponent<NullCircle>();
                    yield return StartCoroutine(_jarVisualization.MoveNodeAndAllDescendantsJar(copyRootOfSubTree, rootToPlaceSubtree, 1.0f, () => { }));


                    /*********************************************
                    Update the line renderers after we have updated the nullCircles to their current ingredients
                    *********************************************/
                    _lineRendererManager.UpdateLineRenderers();



                    //Debug.Log("PRINTER ALL NULLCIRCLES!!!!!!");
                    //_nullCircleSpawner.PrintNullCircles();


                    /*********************************************
                    Destroy the copied nullCircles
                    *********************************************/
                    _nullCircleManager.destroyNullCircleAndAllDescendants(copyRootOfSubTree.gameObject);
                }
                else
                {
                    // Debug.Log("Jeg har ikke et leftChild, og derfor skal jeg bare rotere");
                    //Debug.Log("Jeg starter RotateLeftAnimation");

                    /*********************************************
                    Start the left rotation animation
                    *********************************************/
                    yield return StartCoroutine(_leftRotationVisualization.RotateLeftAnimation(parent, rightChild, parentNullCircle));


                }
                break;


            case OperationType.RotateRight:
                //h node = grandparent
                NullCircle rightChildNullCircle = parentNullCircle.GetComponent<NullCircle>().RightChild.GetComponent<NullCircle>();

                /*********************************************
                Check if the rightChild is null -> Bag animation
                *********************************************/
                if (rightChildNullCircle.Ingredient != null)
                {
                    // Debug.Log("Jeg har et rightChild, og derfor skal mit subtree op i bagen");
                    // Bag animation
                    // rotate animation
                    // move parent to leftChild
                    // move rightChild to parent


                    /*********************************************
                    Make a copy of all the nullCircles and instantiate them with the ingredients' values
                    *********************************************/
                    NullCircle copyRootOfSubTree = _nullCircleManager.CopyNullCircleSubtree(rightChildNullCircle);

                    /*********************************************
                    Store all the ingredients in the subtree in a list
                    *********************************************/
                    List<GameObject> ingredientsToJar = _nullCircleManager.CollectIngredients(rightChildNullCircle, new List<GameObject>());

                    /*********************************************
                    Update the nullCircles to default value after we have copied them
                    *********************************************/
                    _nullCircleManager.setNullCircleToDefault(rightChildNullCircle);

                    /*********************************************
                    Update the line renderers after we have sat the nullCircles to default
                    *********************************************/
                    _lineRendererManager.UpdateLineRenderers();

                    // MÅSKE UDNØDVENDIGT MED AT SHRINK
                    //yield return StartCoroutine(_jarVisualization.ShrinkMultiple(ingredientsToJar, () => {}));


                    /*********************************************
                    Move the ingredients to the jar (visually)
                    *********************************************/
                    foreach (GameObject ingredient in ingredientsToJar)
                    {
                        //_spline.ChangeFirstVector3Position(ingredient.transform.position);
                        //yield return StartCoroutine(_spline.FollowPointsToJar(ingredient));
                        _splineToJar.ChangeFirstKnot(ingredient.transform.position);
                        yield return StartCoroutine(_splineToJar.FollowSplineToJar(ingredient));
                    }


                    /*********************************************
                    Start the right rotation animation
                    *********************************************/
                    yield return StartCoroutine(_rightRotationVisualization.RotateRightAnimation(leftChild, parent, grandparent, parentNullCircle));

                    // MÅSKE UDNØDVENDIGT MED AT SHRINK
                    // yield return (_jarAnimation.GrowAndMove(yourGameObject, pathPoints, finalPosition, totalAnimationDuration));


                    /*********************************************
                    Move the ingredients back to the tree (both visually and on the nullCircles)
                    *********************************************/

                    // Parent Parent Rightchild
                    NullCircle rootToPlaceSubtree = parentNullCircle.GetComponent<NullCircle>().Parent.GetComponent<NullCircle>().RightChild.GetComponent<NullCircle>().LeftChild.GetComponent<NullCircle>();
                    yield return StartCoroutine(_jarVisualization.MoveNodeAndAllDescendantsJar(copyRootOfSubTree, rootToPlaceSubtree, 1.0f, () => { }));

                    /*********************************************
                    Update the line renderers after we have updated the nullCircles to their current ingredients
                    *********************************************/
                    _lineRendererManager.UpdateLineRenderers();

                    /*********************************************
                    Destroy the copied nullCircles
                    *********************************************/
                    _nullCircleManager.destroyNullCircleAndAllDescendants(copyRootOfSubTree.gameObject);
                }
                else
                {
                    // Debug.Log("Jeg har ikke et rightChild, og derfor skal jeg bare rotere");
                    //Debug.Log("Jeg starter RotateRightAnimation");

                    /*********************************************
                    Start the right rotation animation
                    *********************************************/
                    yield return StartCoroutine(_rightRotationVisualization.RotateRightAnimation(leftChild, parent, grandparent, parentNullCircle));
                }
                break;





            case OperationType.FlipColors:
                // h node = parent
                //Debug.Log("Jeg starter FlipColorAnimation");

                /*********************************************
                Start the flip colors rotation animation
                *********************************************/
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


        // Now that the movement is complete, hide the NullCircle GameObject
        _nullCircleManager.HideNullCircle(_currentNullCircle.GetComponent<NullCircle>());
        _currentNullCircle.GetComponent<NullCircle>().IsActive = false;
        
        onComplete?.Invoke();
    }

    public void WhoIsWho(OperationType operationType, List<GameObject> ingredientsToRotate, out GameObject grandparent, out GameObject parent, out GameObject rightChild, out GameObject leftChild)
    {
        switch (operationType)
        {
            case OperationType.RotateLeft:
                // Angive hvem der er rightChild, hvem der er parent - grandparent og leftChild må sættes til null pga 'out'
                grandparent = null;
                leftChild = null;
                if (ingredientsToRotate[0].transform.position.x > ingredientsToRotate[1].transform.position.x)
                {
                    rightChild = ingredientsToRotate[0];
                    parent = ingredientsToRotate[1];
                }
                else
                {
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
