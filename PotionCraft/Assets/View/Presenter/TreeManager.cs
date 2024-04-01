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

    public bool ValidateNodePlacement(GameObject clickedNullCircle){
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
        Node parentNode = (RedBlackTree.Get(clickedNullCircleObject.Value)).Parent;
        Debug.Log("NullCircle rightchild " + (parentNullCircle.RightChild).GetComponent<NullCircle>().Value);
        Debug.Log("NullCircle leftchild " + (parentNullCircle.LeftChild).GetComponent<NullCircle>().Value);
        Debug.Log("Node right " + (RedBlackTree.Get(clickedNullCircleObject.Value)).Parent.Right.Value);
       
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