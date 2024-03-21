using System;



public class Node
{
    private const bool RED = true; // Assuming you intended this to be a constant
    private const bool BLACK = false;
  
    public int Key { get; set; }

    public int Value { get; set; } // When inserting numbers, letters etc. both key and value have the same value (the number)

    // Children 
    public Node Left { get; set; }
    public Node Right { get; set; }

    // Number of nodes in the subtree
    public int N { get; set; }

    // Color of the link from the parent to this node
    public bool Color { get; set; }

    public Node(int key, int val, int n, bool color)
    {
        Key = key;
        Value = val;
        N = 1;
        Color = color;
    }
}
