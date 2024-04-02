using UnityEngine;


public class TreeManager : MonoBehaviour, ITreeManager
{
    // SHOULD BE INTERFACE??
    public RedBlackBST RedBlackTree { get; set; }
    public static TreeManager instance { get; private set; }

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
        RedBlackTree = new RedBlackBST();
    }

    // Add a node to the tree
    public void InsertNode(int key, int value)
    {
        RedBlackTree.Put(key, value);
        //return parent value and what child it is
    }

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
    public bool ValidateNodePlacement(GameObject clickedNullCircle)
{
    
    NullCircle clickedNullCircleObject = clickedNullCircle.GetComponent<NullCircle>();
    RunDebugPrints(clickedNullCircleObject);

    // Base case: If the clickedNullCircle is the root
    if ((clickedNullCircleObject.Parent) == null && (RedBlackTree.Get((clickedNullCircleObject.Value)).Parent) == null ) {
            Debug.Log("We have inserted the root node correctly.");
            Debug.Log("I return true.");
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
        Debug.Log("I return false.");
        return false;
    }

    // Check if the clicked null circle is supposed to be a left or right child based on its position
    bool isLeftChild = clickedNullCircleObject.transform.position.x < clickedNullCircleObject.Parent.transform.position.x;
    
    if (isLeftChild)
    {
        // If it's supposed to be a left child, but the parent's left in the tree is not null or doesn't match expected value
        if (parentInTree.Left != null && parentInTree.Left.Value == clickedNullCircleObject.Value)
        {
            Debug.Log("Correct placement of the left child");
            Debug.Log("I return true.");
            return true;
        }
    }
    else
    {
        // If it's supposed to be a right child, but the parent's right in the tree is not null or doesn't match expected value
        if (parentInTree.Right != null && parentInTree.Right.Value == clickedNullCircleObject.Value)
        {
            Debug.Log("Correct placement of the right child");
            Debug.Log("I return true.");
            return true;
        }
    }

    Debug.Log("Incorrect placement: The clicked null circle is incorrect identified as a " + (isLeftChild ? "left" : "right") + " child of its parent.");
    Debug.Log("I return false.");
    return false;
}

public void RunDebugPrints(NullCircle clickedNullCircleObject){

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




























    public void WaitForRotateRight()
    {
        // Update hint

        // Wait to check which button is pressed
        // Update hint according to the button pressed

        // If correct button is pressed, continue algorithm
    }

    public void WaitForRotateLeft()
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
}