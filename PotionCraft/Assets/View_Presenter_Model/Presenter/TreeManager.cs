using System.Collections.Generic;
using UnityEngine;


public class TreeManager : MonoBehaviour, ITreeManager
{
    [SerializeField] private TreeVisualizationManager _treeVisualizerManager;
    [SerializeField] private NullCircleManager _nullCircleManager;
    [SerializeField] private AvatarHintManager _avatarHintManager;
    [SerializeField] private NodeSpawner _nodeSpawner;
    [SerializeField] private PopUpManager _popUpManager;
    public RedBlackBST RedBlackTree { get; set; } = new RedBlackBST();
    public static TreeManager instance { get; private set; }
    private HashSet<Node> currentSelctedNodes = new HashSet<Node>();
    public List<GameObject> CurrentSelectedIngredients { get; set; } = new List<GameObject>();

    /*
   private void Awake()
    {
        // Singleton pattern
        if (instance != null && instance != this)
        {
            Debug.Log("Destroying the tree manager");
           // Destroy(gameObject);
        }
        else
        {
            Debug.Log("Setting the instance of the tree manager");
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
    }
    */

    void Start()
    { }

    // Add a node to the tree
    public void InsertNode(int key, int value)
    {
        RedBlackTree.Put(key, value);
    }

    // Checks if the clicked null circles parent is the same as the nodes parent in the tree
    public bool ValidateNodePlacement(GameObject clickedNullCircle)
    {

        NullCircle clickedNullCircleObject = clickedNullCircle.GetComponent<NullCircle>();
        //RunDebugPrints(clickedNullCircleObject);

        // Base case: If the clickedNullCircle is the root
        if ((clickedNullCircleObject.Parent) == null && (RedBlackTree.Get((clickedNullCircleObject.Value)).Parent) == null)
        {
            //Debug.Log("We have inserted the root node correctly.");
            //Debug.Log("I return true.");
            return true;
        }

        // Assuming clickedNullCircleObject.Parent is a reference to the GameObject that visually represents the parent of the null circle
        // and not directly the parent Node object in the red-black tree.
        // We need to extract the value from this parent GameObject to find the corresponding Node in the tree.
        int clickedParentValue = clickedNullCircleObject.Parent.GetComponent<NullCircle>().Value;
        Node parentInTree = RedBlackTree.Get(clickedParentValue);

        if (parentInTree == null)
        {
            //Debug.LogError("Parent node not found in the tree. This should not happen if the tree and visual representation are synchronized.");
            //Debug.Log("I return false.");
            return false;
        }

        // Check if the clicked null circle is supposed to be a left or right child based on its position
        bool isLeftChild = clickedNullCircleObject.transform.position.x < clickedNullCircleObject.Parent.transform.position.x;

        if (isLeftChild)
        {
            // If it's supposed to be a left child, but the parent's left in the tree is not null or doesn't match expected value
            if (parentInTree.Left != null && parentInTree.Left.Value == clickedNullCircleObject.Value)
            {
                //Debug.Log("Correct placement of the left child");
                //Debug.Log("I return true.");
                clickedNullCircle.GetComponent<NullCircle>().IsRed = true;
                return true;
            }
        }
        else
        {
            // If it's supposed to be a right child, but the parent's right in the tree is not null or doesn't match expected value
            if (parentInTree.Right != null && parentInTree.Right.Value == clickedNullCircleObject.Value)
            {
                //Debug.Log("Correct placement of the right child");
                //Debug.Log("I return true.");
                clickedNullCircle.GetComponent<NullCircle>().IsRed = true;
                return true;
            }
        }

        //Debug.Log("Incorrect placement: The clicked null circle is incorrect identified as a " + (isLeftChild ? "left" : "right") + " child of its parent.");
        return false;
    }

    public void HandleIngredientClick(int NodeValue)
    {
        Node clickedNode = RedBlackTree.Get(NodeValue);
        //Debug.Log("I have translatede the clicked ingredient with the value: " + NodeValue + " to the corresponding node in the tree: " + clickedNode + " and its value is: " + clickedNode.Value);

        // Togle between selected and not selected nodes
        if (currentSelctedNodes.Contains(clickedNode))
        {
            currentSelctedNodes.Remove(clickedNode);
        }
        else
        {
            currentSelctedNodes.Add(clickedNode);
        }

        foreach (Node node in currentSelctedNodes)
        {
            //Debug.Log("Current Node value in currentSelctedNodes: " + node.Value);
        }

        // Vi sætter ikke en grænse for hvor mange ingredients de må vælge - så afslører det ikke noget om hvilken operation der er næste
        // De kan deselecte en ingredient

        //but if we want to, this is how we could do it:
        // Check what the next operation is in the queue
        // If the next operation is:
        //// RotateLeft: can only select 2 nodes - så længe størrelsen på set'et er mindre end 2
        //// RotateRight: can only select 3 nodes - så længe størrelsen på set'et er mindre end 3
        //// FlipColors: can only select 3 nodes - så længe størrelsen på set'et er mindre end 3
    }

    public void HandleOperationButtonClick(OperationType selectedOperationType)
    {
        // If there is no operations in the queue to perform, do nothing. The user is supposed to insert a new ingredient instead
        if (IsTreeInBalanced())
        {
            Debug.Log("There are no more operations in the queue. The tree is in balance. Insert next ingredient.");
            return;
        }

        // Get the current operation from the queue, whitout removing it
        Operation nextCorrectOperation = RedBlackTree.Operations.Peek();

        // Check if the user has selected the correct nodes and right operation
        bool userSelectedCorrectOperation = selectedOperationType == nextCorrectOperation.OperationType;
        bool userSelectedCorrectIngredients = SelectedCorrectIngredients(nextCorrectOperation, out HashSet<Node> correctNodesInTree);

        if (userSelectedCorrectIngredients)
        {
            // Correct ingredients and correct operation
            if (userSelectedCorrectOperation)
            {
                // Updates hint to Green Sprite, Shows for some secunds. The ingredients and operation were both correct, execute the operation
                // Idea: Could be nice to convert the correctOperation to string and add it to the hint
                _avatarHintManager.UpdateHint("correct", AvatarHint.SelectedRightIngredientsAndButton);
                ExecuteNextOperation(correctNodesInTree, nextCorrectOperation.OperationType);
                
            }
            // Correct ingredients but wrong operation
            else
            {
                // The ingredients were correct but not the operation.
                // Updates hint to Red Sprite, and keep showing
                _avatarHintManager.UpdateHint("wrong", AvatarHint.SelectedRightIngredientsButWrongButton);
            }

        }
        else
        {
            // Wrong ingredients but correct operation
            if (userSelectedCorrectOperation)
            {
                //  Update hint - The operation was correct but not the ingredients. Red Sprite is shown, until you select something right
                _avatarHintManager.UpdateHint("wrong", AvatarHint.SelectedRightButtonButWrongIngredients);
            }
            // Wrong ingredients and wrong operation
            else
            {
                // Update hint - The ingredients and operation were both wrong. Red Sprite is shown, until you select something right
                _avatarHintManager.UpdateHint("wrong", AvatarHint.SelectedWrongIngredientsAndButton);
            }
        }
    }

    public bool SelectedCorrectIngredients(Operation nextCorrectOperation, out HashSet<Node> correctNodesInTree)
    {
        switch (nextCorrectOperation.OperationType)
        {
            case OperationType.RotateLeft:
                correctNodesInTree = new HashSet<Node>
                {
                    nextCorrectOperation.Node,
                    nextCorrectOperation.Node.Right
                };
                break;

            case OperationType.RotateRight:
                correctNodesInTree = new HashSet<Node>
                {
                    nextCorrectOperation.Node,
                    nextCorrectOperation.Node.Left,
                    nextCorrectOperation.Node.Left.Left
                };
                break;

            case OperationType.FlipColors:
                correctNodesInTree = new HashSet<Node>
                {
                    nextCorrectOperation.Node,
                    nextCorrectOperation.Node.Left,
                    nextCorrectOperation.Node.Right
                };
                break;
            default:
                // Will not happen
                correctNodesInTree = new HashSet<Node>();
                return false;
        }

        // Print sets for debugging
        /*
        Debug.Log("!!!!!!!!!!!!!!! The CORRECT NODES for operation " + nextCorrectOperation.OperationType + " are: !!!!!!!!!!!!!!!");
        foreach (Node node in correctNodesInTree)
        {
            Debug.Log("Correct node value in tree: " + node.Value);
        }
        */

        // Print current selected nodes
        /*
        Debug.Log("!!!!!!!!!!!!!!! The current SELECTED nodes are: !!!!!!!!!!!!!!!");
        foreach (Node node in currentSelctedNodes)
        {
            Debug.Log("Current selected node value: " + node.Value);
        }
        */

        return correctNodesInTree.SetEquals(currentSelctedNodes);
    }

    public void ExecuteNextOperation(HashSet<Node> correctNodesInTree, OperationType operationType)
    {
        // Prints the current state of our tree
        // Debug.Log("!!!!!!!!!!!!!!! Current state of the tree BEFORE ROTATION !!!!!!" + + operationType + "**********************");
        // RedBlackTree.PrintTree();

        // Perform the next operation in the queue on our RedBlackBST tree, such that the current state of our tree machted the operation we just did. RotateLeft, RotateRight or FlipColors
        RedBlackTree.ExecuteNextOperation();

        //Debug.Log("!!!!!!!!!!!!!!! Current state of the tree AFTER rotation!!!!!!" + operationType + "**********************");
        //RedBlackTree.PrintTree();

        // Clear the sets
        currentSelctedNodes.Clear();
        correctNodesInTree.Clear();

        //Debug.Log("*************** Print all nullcircles BEFORE VISUALIZE ROTATION in ExecuteOperation **********");
        //_nullCircleSpawner.PrintNullCircles();

        // Make it impossible to select the ingredients while the rotation is visualized
        _nodeSpawner.MakeAllPlacedIngredientsInteractable(false, _treeVisualizerManager.CurrectIngredientIndex);

        // Call TreeVisualizerManager to visualize the operation. The rest of the code will wait for the visualization to finish before continuing
        StartCoroutine(_treeVisualizerManager.VisualizeRotation(operationType, CurrentSelectedIngredients, () =>
        {
            //Debug.Log("*************** Print all nullcircles AFTER VISUALIZE ROTATION in ExecuteOperation ***********");
            //_nullCircleSpawner.PrintNullCircles();

            // Change the image of the selected ingredients to the correct image
            ChangeIngredientImageToDefault();

            // Clear the selected ingredients
            CurrentSelectedIngredients.Clear();

            // Check the current state of our RedBlackBST tree to see if we need to perform more operations
            RedBlackTree.IsThereATreeViolation();

            // There are still more operations to perform
            if (!IsTreeInBalanced())
            {
                // Make it possible to select the ingredients again
                _nodeSpawner.MakeAllPlacedIngredientsInteractable(true, _treeVisualizerManager.CurrectIngredientIndex);
                // Update hint
                HandleNextOperation();
            }
            // There are no more operations to perform - The tree is in balance
            else
            {
                // Update hint - Wuhuu the tree is in balance. Insert next ingredient. Green hint
                _avatarHintManager.UpdateHint("correct", AvatarHint.InBalance);
                 
                // check om vi er færdige med levellet
                CheckIfCompletedLevel();

                // Show nullcircles to allow for new ingredients to be inserted
                _nullCircleManager.ShowAllChildrenNullCircles();
            }
        }));

    }

    public void ChangeIngredientImageToDefault()
    {
        foreach (GameObject ingredient in CurrentSelectedIngredients)
        {
            var i = ingredient.GetComponent<Ingredient>();
            i.ChangePrefabImage(i.name);
        }
    }

    // Get the color of the node with the given value in the red-black tree
    public bool GetColor(int value)
    {
        return RedBlackTree.GetColor(value);
    }

    public bool IsTreeInBalanced()
    {
        // If the queue is empty, the tree is balanced
        return RedBlackTree.Operations.Count == 0;
    }

    public void HandleNextOperation()
    {
        
        OperationType nextCorrectOperation = GetOperationType();
        Debug.Log(nextCorrectOperation);
        switch (nextCorrectOperation)
        {
            case OperationType.RotateLeft:
                // Update hint - Need to rotate left
                _avatarHintManager.UpdateHint("hint", AvatarHint.NeedsToSelectTwoNodes);
                break;
            case OperationType.RotateRight:
                // Update hint - Need to rotate right
                _avatarHintManager.UpdateHint("hint", AvatarHint.NeedsToSelectThreeNodes);
                break;
            case OperationType.FlipColors:
                // Update hint - Need to flip colors
                _avatarHintManager.UpdateHint("hint", AvatarHint.NeedsToSelectTwoNodesToFlipColor);
                break;
            default:
                break;
        }
    }

    public void CheckIfCompletedLevel(){
        // If the current ingredient index is equal to the number of ingredients (we are on the last ingredient to insert) and there are no more operations in the queue, then the user has completed the level
        if(_treeVisualizerManager.CurrectIngredientIndex == _nodeSpawner.GetNodeObjects().Count && RedBlackTree.Operations.Count == 0)
        {
            Debug.Log("There are no more ingredients to insert. There are no more operation, meaning The tree is in balance. You have completed the level.");
            // If the user has completed the level, load the pop-up scene
            _popUpManager.LoadPopUpScene(); 
        }
    }

    // Only use on newly inserted nodes (before any rotations or color flips)
    public void DeleteNodeAndClearOperations(int key)
    {
        // Find node with given value in the tree
        Node nodeToDelete = RedBlackTree.Get(key);

        // Delete nodeToDelete
        RedBlackTree.Delete(nodeToDelete);

        // Clear the operations queue
        RedBlackTree.Operations.Clear();

        //Debug.Log("!!! TREE AFTER DELETION !!!");
        //RedBlackTree.PrintTree();
    }

    // See what the next operation is in the queue
    public OperationType GetOperationType()
    {
        return RedBlackTree.Operations.Peek().OperationType;
    }

    public void RunDebugPrints(NullCircle clickedNullCircleObject)
    {

        Debug.Log("!!!!!RUNS ALL DEBUG PRINTS FOR ValidateNodePlacement!!!!!!!!!!!!");
        // Check and log the value of the clicked null circle itself
        Debug.Log("Clicked null circle value: " + clickedNullCircleObject.Value);

        // Check and log the parent's value of the clicked null circle, if it exists
        var parentNullCircleComponent = clickedNullCircleObject.Parent?.GetComponent<NullCircle>();
        string parentNullCircleValue = parentNullCircleComponent != null ? parentNullCircleComponent.Value.ToString() : "null";
        Debug.Log("Clicked null circle parent value: " + parentNullCircleValue);

        // Check and log the right child's value of the clicked null circle's parent, if it exists
        var parentRightNullCircleComponent = parentNullCircleComponent?.RightChild?.GetComponent<NullCircle>();
        string parentRightChildValue = parentRightNullCircleComponent != null ? parentRightNullCircleComponent.Value.ToString() : "null";
        Debug.Log("Clicked null circle parent right child value: " + parentRightChildValue);

        // Check and log the left child's value of the clicked null circle's parent, if it exists
        var parentLeftNullCircleComponent = parentNullCircleComponent?.LeftChild?.GetComponent<NullCircle>();
        string parentLeftChildValue = parentLeftNullCircleComponent != null ? parentLeftNullCircleComponent.Value.ToString() : "null";
        Debug.Log("Clicked null circle parent left child value: " + parentLeftChildValue);

        // Check and log the parent node's value in the red-black tree, if it exists
        var parentNodeInTree = RedBlackTree.Get(clickedNullCircleObject.Value)?.Parent;
        string parentNodeValue = parentNodeInTree != null ? parentNodeInTree.Value.ToString() : "null";
        Debug.Log("Parent node in tree value: " + parentNodeValue);

        // Check and log the right child's value of the parent node in the tree, if it exists
        string parentNodeRightValue = parentNodeInTree?.Right != null ? parentNodeInTree.Right.Value.ToString() : "null";
        Debug.Log("Parent node in tree right value: " + parentNodeRightValue);

        // Check and log the left child's value of the parent node in the tree, if it exists
        string parentNodeLeftValue = parentNodeInTree?.Left != null ? parentNodeInTree.Left.Value.ToString() : "null";
        Debug.Log("Parent node in tree left value: " + parentNodeLeftValue);


    }

}



/******************* SKRALDESPAND MEN BEHOLD LIGE INDTIL VI VED DET ANDET VIRKER *******************/

/*
public void HandleOperationButtonClick(OperationType operationType)
{
    // Get the current operation from the queue, whitout removing it
    Operation TheCurrentCorrectOperation = RedBlackTree.Operations.Peek();

    //Debug.Log("!!!!!!!!!The current  operation is: " + TheCurrentCorrectOperation.OperationType + "!!!!!!!!");
    //Debug.Log("The current operation node is: " + TheCurrentCorrectOperation.Node.Value);


    // Lav om så: 
    /// Den først tjekker om det er de riggtige nodes
    /// Så tjekker om det er den rigtige operation
    /// Hvis begge er rigtige, så udfør operationen
    /// 


    // The user selected the correct operation button
    if (operationType == TheCurrentCorrectOperation.OperationType)
    {
        HashSet<Node> correctNodesInTree;
        //Debug.Log("Correct operationButton clicked! Wuhu you go!");

        switch (operationType)
        {
            case OperationType.RotateLeft:

                // If we rotate left, we need to check if the selected node and the parent node is the correct nodes
                // We get the Node and its right child from the node we flaged inside our red-black tree put method, that is stored in RedBlackTree.Operations
                correctNodesInTree = new HashSet<Node>
                    {
                        TheCurrentCorrectOperation.Node,
                        TheCurrentCorrectOperation.Node.Right
                    };


                //print correctnodes
                //Debug.Log("!!!!!!!!!!!!!!! The correct nodes in tree when we rotate left are: !!!!!!!!!!!!!!!");
                foreach (Node node in correctNodesInTree)
                {
                    //Debug.Log("Correct node value in tree: " + node.Value);
                }

                //Print current selected nodes
                //Debug.Log("!!!!!!!!!!!!!!! The current selected nodes are when we rotate left are: !!!!!!!!!!!!!!!");
                foreach (Node node in currentSelctedNodes)
                {
                    //Debug.Log("Current selected node value: " + node.Value);
                }



                ExecuteOperationIfCorrectNodesSelected(correctNodesInTree, OperationType.RotateLeft);
                break;

            case OperationType.RotateRight:
                // If we rotate right, we need to check if the selected node and the parent node and the grandparent node is the correct nodes
                // We get the Node and its left child and thats left child from the node we flaged inside our red-black tree put method, that is stored in RedBlackTree.Operations

                correctNodesInTree = new HashSet<Node>
                    {
                        TheCurrentCorrectOperation.Node,
                        TheCurrentCorrectOperation.Node.Left,
                        TheCurrentCorrectOperation.Node.Left.Left
                    };

                ExecuteOperationIfCorrectNodesSelected(correctNodesInTree, OperationType.RotateRight);


                break;

            case OperationType.FlipColors:
                // If we flip colors, we need to check if the selected nodes are the correct nodes    
                correctNodesInTree = new HashSet<Node>
                    {
                        TheCurrentCorrectOperation.Node,
                        TheCurrentCorrectOperation.Node.Left,
                        TheCurrentCorrectOperation.Node.Right
                    };
                ExecuteOperationIfCorrectNodesSelected(correctNodesInTree, OperationType.FlipColors);
                break;
        }


    }
    // The user has selected the wrong operation button
    // We do not know if the user has selected the correct nodes yet
    else
    {
        Debug.Log("Incorrect operationButton clicked");

        // If correct nodes are selected, but wrong operation button
        //if (correctNodesInTree.SetEquals(currentSelctedNodes)){
        // TODO: Update hint
        //}

        // If neither the correct nodes or the correct operation button is selected
        // TODO: Update hint




        // Make sure the operation is still in the queue. 
        // It is, because we only peek
    }
}



public void ExecuteOperationIfCorrectNodesSelected(HashSet<Node> correctNodesInTree, OperationType operationType)
{
    // If these two sets are the same, then the user has selected the correct nodes and the correct operation button
    if (correctNodesInTree.SetEquals(currentSelctedNodes))
    {
        //Debug.Log("You have selected the correct ingredients! Wuhu you go!");
        // Prints the current state of our tree
        //RedBlackTree.PrintTree();

        // Perform the next operation in the queue on our RedBlackBST tree, such that the current state of our tree machted the operation we just did. RotateLeft, RotateRight or FlipColors
        RedBlackTree.ExecuteNextOperation();

        //Debug.Log("!!!!!!!!!!!!!!!Current state of the tree after rotation!!!!!! **********************" + operationType);
        //RedBlackTree.PrintTree();
        Debug.Log("***************Færdig med at printe træet i ExecuteOperationIfCorrectNodesSelected***********");

        // Clear the selected nodes the user has selected
        currentSelctedNodes.Clear();
        correctNodesInTree.Clear();

        //Debug.Log("!!!!!!!!!!*****The current state of the tree is: *****!!!!!!!");
        //RedBlackTree.PrintTree();
        //Debug.Log("!!!!!!!!!!!!!*****The current state of all the null circles BEFORE a visulize rotation: *****!!!!!!!!!!");
        //_nullCircleSpawner.PrintNullCircles();

        Debug.Log("***************PRINTER ALL NULLCIRCLES BEFORE VISUALIZE ROTATION IN ExecuteOperationIfCorrectNodesSelected***********");
        _nullCircleSpawner.PrintNullCircles();

        // Call TreeVisualizerManager to visualize the operation. The rest of the code will wait for the visualization to finish before continuing
        StartCoroutine(_treeVisualizerManager.VisualizeRotation(operationType, CurrentSelectedIngredients, () =>
        {
            Debug.Log("***************VisualizeRotation DONE ***********");
            Debug.Log("***************PRINTER ALL NULLCIRCLES BEFORE VISUALIZE ROTATION IN ExecuteOperationIfCorrectNodesSelected***********");
            _nullCircleSpawner.PrintNullCircles();
            // Change the image of the ingredients to the correct image
            // Change the image of the selected ingredients to default imange
            ChangeIngredientImageToDefault();

            // Clear the selected ingredients
            CurrentSelectedIngredients.Clear();

            // Check the current state of our RedBlackBST tree to see if we need to perfome more operations
            RedBlackTree.IsThereATreeViolation();
            // Debug.Log("!!!!!!!!!!!!!*****The current state of all the null circles AFTER a visulize rotation: *****!!!!!!!!!!");
            //_nullCircleSpawner.PrintNullCircles();

            // Kalde en metode der håndtere næste operation i køen
            if (RedBlackTree.Operations.Count > 0)
            {
                HandleNextOperation();
            }
            else
            {
                Debug.Log("*****No more operations in the queue! The tree is in balance! Insert next ingredient! *****");

                // Update whitch null circles are visible, based on if they now have a ingredient or not
                _nullCircleSpawner.UpdateActiveNullCirclesAndShow();

                // TODO: Update hint
            }
        }));
    }
    // The user has selected the wrong nodes     
    else
    {
        Debug.Log("Incorrect nodes selected");
        // TODO: Update hint
    }
}
*/

