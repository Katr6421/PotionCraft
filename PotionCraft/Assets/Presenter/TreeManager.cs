public class TreeManager : ITreeManager
{
    public IRedBlackBST RedBlackTree { get; set; }

    public TreeManager(IRedBlackBST redBlackTree)
    {
        RedBlackTree = redBlackTree;
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