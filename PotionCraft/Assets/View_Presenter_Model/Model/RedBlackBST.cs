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


      if (IsRed(h.Right) && !IsRed(h.Left))
      {
         UnityEngine.Debug.Log("RotationLeft - Red violation found at node " + h.Key + " with right child " + h.Right.Key);
         Operations.Enqueue(new Operation(h, OperationType.RotateLeft)); // var h.Right
      }
      if (IsRed(h.Left) && IsRed(h.Left.Left))
      {
         UnityEngine.Debug.Log("RotationRight - Red violation found at node " + h.Key + " with left child " + h.Left.Key);
         Operations.Enqueue(new Operation(h, OperationType.RotateRight)); //h.Left.Left
      }   
      if (IsRed(h.Left) && IsRed(h.Right))
      {
         UnityEngine.Debug.Log("FlipColors - Red violation found at node " + h.Key + " with left child " + h.Left.Key + " and right child " + h.Right.Key);
         Operations.Enqueue(new Operation(h, OperationType.FlipColors));
      }

      h.N = Size(h.Left) + Size(h.Right) + 1;


      // Return the inserted node
      return h;

   }

   
   public void IsThereATreeViolation()
   {
      UnityEngine.Debug.Log("Root is: " + Root.Key + " | " + Root.Value + " | " + (Root.Color ? "RED" : "BLACK"));
      IsThereATreeViolation(Root);
   }

   private void IsThereATreeViolation(Node h) {
      PrintTree();
      UnityEngine.Debug.Log("!!!!!!!!!!INNI I IsThereATreeViolation!!!!!!!!!");
      /*
         starter fra root
         IsRed(h.Right) && !IsRed(h.Left)
         IsRed(h.Left) && IsRed(h.Left.Left)
         IsRed(h.Left) && IsRed(h.Right)

         også gå videre til children
      */
      if (h == null) return; // er på leaf node

      UnityEngine.Debug.Log("Node value: " + h.Value);

      // add operation to queue
      if (IsRed(h.Right) && !IsRed(h.Left))
      {
         UnityEngine.Debug.Log("RotationLeft - Red violation found at node " + h.Key + " with right child " + h.Right.Key);
         Operations.Enqueue(new Operation(h, OperationType.RotateLeft)); // var h.Right
      }
      if (IsRed(h.Left) && IsRed(h.Left.Left))
      {
         UnityEngine.Debug.Log("RotationRight - Red violation found at node " + h.Key + " with left child " + h.Left.Key);
         Operations.Enqueue(new Operation(h, OperationType.RotateRight)); //h.Left.Left
      }
      if (IsRed(h.Left) && IsRed(h.Right))
      {
         UnityEngine.Debug.Log("FlipColors - Red violation found at node " + h.Key + " with left child " + h.Left.Key + " and right child " + h.Right.Key);
         Operations.Enqueue(new Operation(h, OperationType.FlipColors));
      }

      // go to children
      IsThereATreeViolation(h.Left);
      IsThereATreeViolation(h.Right);
   
   }

   private bool IsRed(Node x)
   {
      if (x == null) return false;
      return x.Color == RED;
   }


   private Node RotateLeft(Node h)
   {
      Node x = h.Right;
      Node parent = h.Parent;
      h.Right = x.Left;
      x.Left = h;
      x.Color = h.Color;
      h.Color = RED;
      x.N = h.N;
      h.N = 1 + Size(h.Left) + Size(h.Right);


      // Update parent references
      x.Parent = parent; // x gets the original parent of h
      h.Parent = x; // h is now a child of x
      if (parent == null) Root = x;
      
      return x;
   }



   private Node RotateRight(Node h)
   {
     
      Node x = h.Left;
      Node parent = h.Parent;
      h.Left = x.Right;      
      x.Right = h;
      x.Color = h.Color;
      h.Color = RED;
      x.N = h.N;
      h.N = 1 + Size(h.Left) + Size(h.Right);


      // Update parent references
      x.Parent = parent; // x gets the original parent of h
      h.Parent = x; // h is now a child of x
      if (parent == null) Root = x;

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
         UnityEngine.Debug.Log("ExecuteNextOperation was called but did not have any operations to execute.");
         return;
      }

      UnityEngine.Debug.Log("Executing next operation...");
      Operation op = Operations.Dequeue();
      UnityEngine.Debug.Log("Operation type: " + op.OperationType);

      switch (op.OperationType)
      {
         case OperationType.RotateLeft:
            UnityEngine.Debug.Log("****************************I am calling RotateLeft with the node " + op.Node.Key + " as the argument.*********************************************");
            RotateLeft(op.Node);
            break;
         case OperationType.RotateRight:
            UnityEngine.Debug.Log("**********************************I am calling RotateRight with the node " + op.Node.Key + " as the argument.***************************************");
            RotateRight(op.Node);
            break;
         case OperationType.FlipColors:
            UnityEngine.Debug.Log("I am calling FlipColors with the node " + op.Node.Key + " as the argument.");
            FlipColors(op.Node); //should maybe be parent
            break;
      }

   }




   public void PrintTree()
   {
      UnityEngine.Debug.Log("Root is: " + Root.Key + " | " + Root.Value + " | " + (Root.Color ? "RED" : "BLACK"));
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


