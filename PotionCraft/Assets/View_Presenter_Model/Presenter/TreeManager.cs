using System.Collections.Generic;
using UnityEngine;


public class TreeManager : MonoBehaviour, ITreeManager
{
    // SHOULD BE INTERFACE??
    [SerializeField] private TreeVisualizationManager _treeVisualizerManager;
    public RedBlackBST RedBlackTree { get; set; } = new RedBlackBST();
    public static TreeManager instance { get; private set; }
    private HashSet<Node> currentSelctedNodes = new HashSet<Node>();
    public List<GameObject> CurrentSelectedIngredients { get; set; } = new List<GameObject>();

    private void Awake()
    {
        // Singleton pattern
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    void Start()
    {
        //RedBlackTree = new RedBlackBST();

    }

    // Add a node to the tree
    public void InsertNode(int key, int value)
    {
        RedBlackTree.Put(key, value);
        //return parent value and what child it is
    }


    public bool ValidateNodePlacement(GameObject clickedNullCircle)
    {

        NullCircle clickedNullCircleObject = clickedNullCircle.GetComponent<NullCircle>();
        RunDebugPrints(clickedNullCircleObject);

        // Base case: If the clickedNullCircle is the root
        if ((clickedNullCircleObject.Parent) == null && (RedBlackTree.Get((clickedNullCircleObject.Value)).Parent) == null)
        {
            Debug.Log("We have inserted the root node correctly.");
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
            Debug.LogError("Parent node not found in the tree. This should not happen if the tree and visual representation are synchronized.");
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
                return true;
            }
        }

        Debug.Log("Incorrect placement: The clicked null circle is incorrect identified as a " + (isLeftChild ? "left" : "right") + " child of its parent.");
        //Debug.Log("I return false.");
        return false;
    }

    public void HandleIngredientClick(int NodeValue)
    {
        Node clickedNode = RedBlackTree.Get(NodeValue);
        Debug.Log("I have translatede the clicked ingredient with the value: " + NodeValue + " to the corresponding node in the tree: " + clickedNode + " and its value is: " + clickedNode.Value);
        currentSelctedNodes.Add(clickedNode);

        /*
        //print the list
        foreach (Node node in currentSelctedNodes)
        {
            Debug.Log("Node value: " + node.Value);
        }
        */


        // Remember: Make sure it is only possible to select 2-3 nodes

    }

    public void HandleOperationButtonClick(OperationType operationType)
    {
        // Get the current operation from the queue, whitout removing it
        Operation TheCurrentCorrectOperation = RedBlackTree.Operations.Peek();
        
        Debug.Log("!!!!!!!!!The current  operation is: " + TheCurrentCorrectOperation.OperationType + "!!!!!!!!");
        Debug.Log("!!!!!!!!And the rest of the operations in the queue are: !!!!!!!!!!");
        Debug.Log("The current operation node is: " + TheCurrentCorrectOperation.Node.Value);


        // Check if it is the correct operation/button is clicked
        if (operationType == TheCurrentCorrectOperation.OperationType)
        {
            HashSet<Node> correctNodesInTree;
            Debug.Log("Correct operationButton clicked");

            switch (operationType)
            {
                case OperationType.RotateLeft:
                // If we rotate left, we need to check if the selected node and the parent node is the correct nodes
                    correctNodesInTree = new HashSet<Node>
                    {
                    TheCurrentCorrectOperation.Node,
                    TheCurrentCorrectOperation.Node.Right
                    };

                    /*
                    //print correctnodes
                    foreach (Node node in correctNodesInTree)
                    {
                        Debug.Log("Correct node value: " + node.Value);
                    }
                    */


                    ExecuteOperationIfCorrectNodesSelected(correctNodesInTree, OperationType.RotateLeft);
                    break;
                    
                case OperationType.RotateRight:
                // If we rotate right, we need to check if the selected node and the parent node and the grandparent node is the correct nodes
                    {
                    correctNodesInTree = new HashSet<Node>
                    {
                        TheCurrentCorrectOperation.Node,
                        TheCurrentCorrectOperation.Node.Left,
                        TheCurrentCorrectOperation.Node.Left.Left
                    };
                    
                    ExecuteOperationIfCorrectNodesSelected(correctNodesInTree, OperationType.RotateRight);

                    }
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
        else
        {
            Debug.Log("Incorrect operationButton clicked");
            // Update hint
            // shake the button
            // make sure the operation is still in the queue
            // make sure that the user can not click on the ingredient. The user should only be able to click on the operation buttons
        }


    }

    public void HandleNextOperation()
    {
        // update hint
        // gøre så man kan klikke på alt igen


    }

    public void ExecuteOperationIfCorrectNodesSelected(HashSet<Node> correctNodesInTree, OperationType operationType)
    {
        // If these two sets are the same, then the user has selected the correct nodes and the correct operation button
        if (correctNodesInTree.SetEquals(currentSelctedNodes))
        {
            Debug.Log("You have selected the correct ingredients");
            // Prints the current state of our tree
            RedBlackTree.PrintTree();

            // Perform the next operation in the queue. RotateLeft, RotateRight or FlipColors
            RedBlackTree.ExecuteNextOperation();
            
            // Clear the selected nodes the user has selected
            //currentSelctedNodes.Clear();
            correctNodesInTree.Clear();

            // kalde treeVisualizer til at opdatere visningen
            _treeVisualizerManager.VisualizeRotation(operationType, CurrentSelectedIngredients);
            CurrentSelectedIngredients.Clear();

            // Kalde isThereARedViolation
            Debug.Log("Checking for a tree violation");
            RedBlackTree.IsThereATreeViolation();
            Debug.Log("The operation there is left is: " );
            foreach (Operation operation in RedBlackTree.Operations)
            {
                Debug.Log(operation.OperationType);
            }

            // Kalde en metode der håndtere næste operation i køen
            if (RedBlackTree.Operations.Count > 0)
            {
                HandleNextOperation();
            }
            else
            {
                Debug.Log("No more operations in the queue");
                // The tree is balanced!! Ready to insert the next ingredient

                // Activate null circles again
                //_treeVisualizerManager.ShowNullCircles();

                // Circlemarker skal rykkes 

                // hint skal opdateres
            }
        }
        else
        {
            Debug.Log("Incorrect nodes selected");
            // Update hint
            // Kan klikke igen
        }
    }

    public bool ShouldDrawNullCircles()
    {
        // If the queue is empty, the tree is balanced and the null circles should be drawn
        return RedBlackTree.Operations.Count == 0;
    }

    public void WaitForRotateRight(Node currentNode)
    {
        // Update hint

        // Wait to check which button is pressed
        // Update hint according to the button pressed

        // If correct button is pressed, continue algorithm
    }

    public void WaitForRotateLeft(Node currentNode)
    {
        // Update hint

        // Wait to check which button is pressed
        // Update hint according to the button pressed

        // If correct button is pressed, continue algorithm
    }

    public void WaitForFliColour()
    {
        // Update hint

        // Wait to check which button is pressed
        // Update hint according to the button pressed

        // If correct button is pressed, continue algorithm
    }

    public void SetCurrentHint()
    {
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

    //////////////// SKRALDESPANDEN /////////////////
    /* public bool ValidateNodePlacement(GameObject clickedNullCircle){
        Debug.Log("Nu printer jeg træet");
        RedBlackTree.PrintTree();

        // Check if the clickedNullCircles parent is the correct parent
        // Check if the clickedNullCircles parent is the correct parent in the red black tree
        NullCircle clickedNullCircleObject = clickedNullCircle.GetComponent<NullCircle>();

        // Base case: If the clickedNullCircle is the root
        if ((clickedNullCircleObject.Parent) == null && (RedBlackTree.Get((clickedNullCircleObject.Value)).Parent) == null ) {
            // is root
            Debug.Log("Root");
            return true;
        }

        // Check if one of the parent is null. Not sure if we need, or will ever come to this case
        if ((clickedNullCircleObject.Parent) == null || (RedBlackTree.Get((clickedNullCircleObject.Value)).Parent) == null ) {
            return false;
        }


        int parentNullCircleValue = (clickedNullCircleObject.Parent).GetComponent<NullCircle>().Value;
        Debug.Log("Parent Null Circle Value: " + parentNullCircleValue);
        int parentNodeValue = RedBlackTree.Get((clickedNullCircleObject.Value)).Parent.Value;
        Debug.Log("Parent Node Value: " + parentNodeValue);


        NullCircle parentNullCircle = clickedNullCircleObject.Parent.GetComponent<NullCircle>();
        //Node parentNode = (RedBlackTree.Get(clickedNullCircleObject.Value)).Parent;
        Debug.Log("NullCircle rightchild " + (parentNullCircle.RightChild).GetComponent<NullCircle>().Value);
        Debug.Log("NullCircle leftchild " + (parentNullCircle.LeftChild).GetComponent<NullCircle>().Value);
        Debug.Log("clickedNullCircleObject.Value " + clickedNullCircleObject.Value);
        Debug.Log("node parent " + (RedBlackTree.Get(clickedNullCircleObject.Value)).Parent);
        Debug.Log("node parent right " + (RedBlackTree.Get(clickedNullCircleObject.Value)).Parent.Right);

        Node parentNode = RedBlackTree.Get(clickedNullCircleObject.Value).Parent;
        if (parentNode.Right != null)
        {
            int rightValue = parentNode.Right.Value; // Assuming Value is a non-nullable int, otherwise check for null
            UnityEngine.Debug.Log("Node parent right value: " + rightValue);
        }
        else
        {
            UnityEngine.Debug.Log("Node parent right is null.");
        }
         if (parentNode.Left != null)
        {
            int leftValue = parentNode.Left.Value; // Assuming Value is a non-nullable int, otherwise check for null
            UnityEngine.Debug.Log("Node parent left value: " + leftValue);
        }
        else
        {
            UnityEngine.Debug.Log("Node parent left is null.");
        }

        //Debug.Log("Node right" + parentNode.Right.Value);
        //Debug.Log("Node left" + parentNode.Left.Value);

        if ((parentNullCircle.RightChild).GetComponent<NullCircle>().Value == parentNode.Right.Value) {
            Debug.Log("Right Child");
        }
        else if ((parentNullCircle.LeftChild).GetComponent<NullCircle>().Value == parentNode.Left.Value) {
            Debug.Log("Left Child");
        }
        else {
            Debug.Log("Not a child");
        }




        if (parentNullCircleValue == parentNodeValue) {
            return true;
        }
        return false;



    }*/
}