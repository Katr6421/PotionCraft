using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class RedBlackBST : IRedBlackBST
{

   private TreeManager _treeManager;
   private const bool RED = true;
   private const bool BLACK = false;
   public Node Root { get; set; }
   public Queue<Operation> Operations { get; set; } = new Queue<Operation>();

   public RedBlackBST()
   {
      Root = null;
      _treeManager = TreeManager.instance;
   }

   public Node Get(int key)
   { return Get(Root, key); }

   private Node Get(Node x, int key)
   {  // Return Node associated with key in the subtree rooted at x;
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
      // Enqueue operations instead of executing them
      if (IsRed(h.Right) && !IsRed(h.Left))
         Operations.Enqueue(new Operation(h.Right, OperationType.RotateLeft));
      if (IsRed(h.Left) && IsRed(h.Left.Left))
         Operations.Enqueue(new Operation(h.Left, OperationType.RotateRight));
      if (IsRed(h.Left) && IsRed(h.Right)) //Should not be h here, later problem
         Operations.Enqueue(new Operation(h, OperationType.FlipColors));

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


   public void ExecuteNextOperation()
   {
      if (Operations.Count == 0)
      {
         UnityEngine.Debug.Log("No more operations to perform.");
         return;
      }

      UnityEngine.Debug.Log("Executing next operation...");
      Operation op = Operations.Dequeue();
      UnityEngine.Debug.Log("Operation type: " + op.OperationType);

      switch (op.OperationType)
      {
         case OperationType.RotateLeft:
            UnityEngine.Debug.Log("I am calling RotateLeft with the node " + op.Node.Key + " as the argument.");
            RotateLeft(op.Node.Parent);
            break;
         case OperationType.RotateRight:
            UnityEngine.Debug.Log("I am calling RotateRight with the node " + op.Node.Key + " as the argument.");
            RotateRight(op.Node.Parent);
            break;
         case OperationType.FlipColors:
            UnityEngine.Debug.Log("I am calling FlipColors with the node " + op.Node.Key + " as the argument.");
            FlipColors(op.Node); //should maybe be parent
            break;
      }

      // Update the tree visualization here
   }




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
      UnityEngine.Debug.Log(new String(' ', depth * 4) + "Key: " + node.Key + " | Value: " + node.Value + " | Color: " + (node.Color ? "RED" : "BLACK"));
      if (node.Parent != null)
      {
         UnityEngine.Debug.Log("Parent: " + node.Parent.Value);
      }
      else
      {
         UnityEngine.Debug.Log("Parent: Null");

      }

      // Print the left subtree
      PrintTree(node.Left, depth + 1);

      // Print the right subtree
      PrintTree(node.Right, depth + 1);
   }
}


