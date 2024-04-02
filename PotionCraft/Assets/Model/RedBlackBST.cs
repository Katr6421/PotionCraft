using System;
using System.Diagnostics;
using UnityEngine;

public class RedBlackBST : IRedBlackBST
{
   private const bool RED = true;
   private const bool BLACK = false;
   public Node Root { get; set; }

   public RedBlackBST()
   {
      Root = null;
   }

   public Node Get(int key)
   {  return Get(Root, key);  }

   private Node Get(Node x, int key)
   {  // Return value associated with key in the subtree rooted at x;
      // return null if key not present in subtree rooted at x.
      if (x == null) return null;
      if (key < x.Key) return Get(x.Left, key);
      else if (key > x.Key) return Get(x.Right, key);
      else return x;
   }

   public void Put(int key, int val)
   {
      Root = Put(Root, key, val, null);
      Root.Color = BLACK;
   }

   private Node Put(Node h, int key, int val, Node parent)
   {
      
      // Reached bottom of tree. Insert new node here with red link to parent.
      if (h == null) return new Node(key, val, 1, RED, parent);
     

      // Binary search tree insertion
      int cmp = key.CompareTo(h.Key);
      if (cmp < 0) h.Left = Put(h.Left, key, val, h);
      else if (cmp > 0) h.Right = Put(h.Right, key, val, h);
      else h.Value = val;

      // Verify and maintain the red-black tree properties
      // WE SHOULD WAIT TO CALL THESE METHODS UNTIL THE USER SELECTES THE NODES THAT SHOULD BE ROATED, SUCH THAT OUR RED BLACK TREE IS SHOWING THE ACUTALE STATE OF THE TREE
      if (IsRed(h.Right) && !IsRed(h.Left)) h = RotateLeft(h);
      if (IsRed(h.Left) && IsRed(h.Left.Left)) h = RotateRight(h);
      if (IsRed(h.Left) && IsRed(h.Right)) FlipColors(h);

      h.N = Size(h.Left) + Size(h.Right) + 1;

      
      // Return the inserted node
      return h;
     
   }

   private bool IsRed(Node x)
   {
      if (x == null) return false;
      return x.Color == RED;
   }

   
   private Node RotateLeft(Node h)
{
    Node x = h.Right;
    h.Right = x.Left;
    if (x.Left != null) x.Left.Parent = h; // Set the new parent reference
    x.Left = h;
    x.Parent = h.Parent; // Update parent reference
    h.Parent = x; // h is now a child of x
    // ... update colors and sizes, etc.
    return x;
}
   
/*
// Tpodo: Implement the parent reference in the RotateLeft method
   private Node RotateLeft(Node h)
   {
      Node x = h.Right;
      h.Right = x.Left;
      x.Left = h;
      x.Color = h.Color;
      h.Color = RED;
      x.N = h.N;
      h.N = 1 + Size(h.Left) + Size(h.Right);
      return x;
   }*/

   
   private Node RotateRight(Node h)
{
    Node x = h.Left;
    h.Left = x.Right;
    if (x.Right != null) x.Right.Parent = h; // Set the new parent reference
    x.Right = h;
    x.Parent = h.Parent; // Update parent reference
    h.Parent = x; // h is now a child of x
    // ... update colors and sizes, etc.
    return x;
}
   

// Tpodo: Implement the parent reference in the RotateLeft method
 /*  private Node RotateRight(Node h)
   {
      Node x = h.Left;
      h.Left = x.Right;
      x.Right = h;
      x.Color = h.Color;
      h.Color = RED;
      x.N = h.N;
      h.N = 1 + Size(h.Left) + Size(h.Right);
      return x;
   }
   */

   private void FlipColors(Node h)
   {
      h.Color = RED;
      h.Left.Color = BLACK;
      h.Right.Color = BLACK;
   }

   private int Size(Node x)
   {
      if (x == null) return 0;
      return x.N;
   }

   public int Size() => Size(Root);

   public void PrintTree()
    {
        PrintTree(Root, 0);
    }

    private void PrintTree(Node node, int depth)
    {
        if (node == null)
        {
            UnityEngine.Debug.Log(new String(' ', depth * 4) + "null");
            return;
        }
        // Retrieve the parent value, if it exists.
         string parentValueString = node.Parent != null ? node.Parent.Value.ToString() : "No parent";

        // Print the current node
        UnityEngine.Debug.Log(new String(' ', depth * 4) + "Key: " + node.Key + " | Value: " + node.Value + " | Color: " + (node.Color ? "RED" : "BLACK" ));
        if (node.Parent != null)
        {
            UnityEngine.Debug.Log("Parent: " + node.Parent.Value);
        }
        else{
            UnityEngine.Debug.Log("Parent: Null");
        
        }

        // Print the left subtree
        PrintTree(node.Left, depth + 1);

        // Print the right subtree
        PrintTree(node.Right, depth + 1);
    }
}

   
