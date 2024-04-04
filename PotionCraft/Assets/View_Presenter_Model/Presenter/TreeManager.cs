using System.Collections.Generic;
using UnityEngine;


public class TreeManager : MonoBehaviour, ITreeManager
{
    // SHOULD BE INTERFACE??
    [SerializeField] private TreeVisualizationManager _treeVisualizerManager;
    [SerializeField] private NullCircleSpawner _nullCircleSpawner;
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
    {}

    // Add a node to the tree
    public void InsertNode(int key, int value)
    {
        RedBlackTree.Put(key, value);
    }


    public bool ValidateNodePlacement(GameObject clickedNullCircle)
    {

        NullCircle clickedNullCircleObject = clickedNullCircle.GetComponent<NullCircle>();
        //RunDebugPrints(clickedNullCircleObject);

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

        Debug.Log("Incorrect placement: The clicked null circle is incorrect identified as a " + (isLeftChild ? "left" : "right") + " child of its parent.");
        
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
        } else
        {
            currentSelctedNodes.Add(clickedNode);
        }

        foreach (Node node in currentSelctedNodes)
        {
            Debug.Log("Current Node value in currentSelctedNodes: " + node.Value);
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

    public void HandleOperationButtonClick(OperationType operationType)
    {
        // Get the current operation from the queue, whitout removing it
        Operation TheCurrentCorrectOperation = RedBlackTree.Operations.Peek();
        
        Debug.Log("!!!!!!!!!The current  operation is: " + TheCurrentCorrectOperation.OperationType + "!!!!!!!!");
        Debug.Log("The current operation node is: " + TheCurrentCorrectOperation.Node.Value);


        // Check if it is the correct operation/button is clicked
        if (operationType == TheCurrentCorrectOperation.OperationType)
        {
            HashSet<Node> correctNodesInTree;
            Debug.Log("Correct operationButton clicked! Wuhu you go!");

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
                    Debug.Log("!!!!!!!!!!!!!!! The correct nodes in tree when we rotate left are: !!!!!!!!!!!!!!!");
                    foreach (Node node in correctNodesInTree)
                    {
                        Debug.Log("Correct node value in tree: " + node.Value);
                    }

                    //Print current selected nodes
                    Debug.Log("!!!!!!!!!!!!!!! The current selected nodes are when we rotate left are: !!!!!!!!!!!!!!!");
                    foreach (Node node in currentSelctedNodes)
                    {
                        Debug.Log("Current selected node value: " + node.Value);
                    }
                    


                    ExecuteOperationIfCorrectNodesSelected(correctNodesInTree, OperationType.RotateLeft);
                    break;
                    
                case OperationType.RotateRight:
                // If we rotate right, we need to check if the selected node and the parent node and the grandparent node is the correct nodes
                 // We get the Node and its left child and thats left child from the node we flaged inside our red-black tree put method, that is stored in RedBlackTree.Operations
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
            // TO DO!!!!!!!! FejlHåndtering!!!!
            // Update hint
            // shake the button
            // make sure the operation is still in the queue
            // make sure that the user can not click on the ingredient. The user should only be able to click on the operation buttons
        }

    }

  

    public void ExecuteOperationIfCorrectNodesSelected(HashSet<Node> correctNodesInTree, OperationType operationType)
    {
        // If these two sets are the same, then the user has selected the correct nodes and the correct operation button
        if (correctNodesInTree.SetEquals(currentSelctedNodes))
        {
            Debug.Log("You have selected the correct ingredients! Wuhu you go!");
            // Prints the current state of our tree
            //RedBlackTree.PrintTree();

            // Perform the next operation in the queue on our RedBlackBST tree, such that the current state of our tree machted the operation we just did. RotateLeft, RotateRight or FlipColors
            RedBlackTree.ExecuteNextOperation();
            
            // Clear the selected nodes the user has selected
            currentSelctedNodes.Clear();
            correctNodesInTree.Clear();

             Debug.Log("!!!!!!!!!!*****The current state of the tree is: *****!!!!!!!");
            RedBlackTree.PrintTree();
            Debug.Log("!!!!!!!!!!!!!*****The current state of all the null circles BEFORE a visulize rotation: *****!!!!!!!!!!");
            _nullCircleSpawner.PrintNullCircles();

            // Call TreeVisualizerManager to visualize the operation. The rest of the code will wait for the visualization to finish before continuing
            StartCoroutine(_treeVisualizerManager.VisualizeRotation(operationType, CurrentSelectedIngredients, () => {
                
                // Change the image of the ingredients to the correct image
                // Change the image of the selected ingredients to default imange
                ChangeIngredientImageToDefault();
                
                // Clear the selected ingredients
                CurrentSelectedIngredients.Clear();

                // Check the current state of our RedBlackBST tree to see if we need to perfome more operations
                RedBlackTree.IsThereATreeViolation();
                    Debug.Log("!!!!!!!!!!!!!*****The current state of all the null circles AFTER a visulize rotation: *****!!!!!!!!!!");
                    _nullCircleSpawner.PrintNullCircles();
                   


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
                    //_nullCircleSpawner.UpdateActiveLineRenderersAndShow();
                    Debug.Log("*****The current state of all the null circles are: *****");
                    _nullCircleSpawner.PrintNullCircles();
                   // Debug.Log("*****The current state of the tree is: *****");
                    RedBlackTree.PrintTree();


                    // The tree is balanced!! Ready to insert the next ingredient

                    // Activate null circles again
                    //_treeVisualizerManager.ShowNullCircles();

                    // Circlemarker skal rykkes 

                    // hint skal opdateres
                }
            }));
        }     
        else
        {
            Debug.Log("Incorrect nodes selected");

            // Update hint
            // Kan klikke igen
        }
    }

    public void ChangeIngredientImageToDefault(){
        foreach (GameObject ingredient in CurrentSelectedIngredients)
            {
                var i = ingredient.GetComponent<Ingredient>();
                i.ChangePrefabImage(i.name);
            }
    }

    // Get the color of the node with the given value in the red-black tree
    public bool GetColor(int value)
    {
        //Debug.Log("GetColor in TreeManager called with value: " + value);
        //Debug.Log("Calling GetColor returns " + RedBlackTree.GetColor(value));
        return RedBlackTree.GetColor(value);
    }



    public bool ShouldDrawNullCircles()
    {
        // If the queue is empty, the tree is balanced and the null circles should be drawn
        return RedBlackTree.Operations.Count == 0;
    }

      public void HandleNextOperation()
    {
        // update hint
        // gøre så man kan klikke på alt igen


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
    /*
    
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



*/



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