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
        // Check if the clickedNullCircles parent is the correct parent
        // Check if the clickedNullCircles parent is the correct parent in the red black tree
        //int parentNullCircleValue = clickedNullCircle.GetComponent<NullCircle>().Parent.Value;
        //int parentNodeValue = RedBlackTree.Get(clickedNullCircle.GetComponent<NullCircle>().Parent.Value).Value;
        //if (parentNullCircleValue == parentNodeValue){
        //    return true;
        //}
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