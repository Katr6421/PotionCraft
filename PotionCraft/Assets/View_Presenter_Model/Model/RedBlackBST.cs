using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class RedBlackBST : IRedBlackBST
{
   private const bool RED = true;
   private const bool BLACK = false;
   public Node Root { get; set; }
   public Queue<Operation> Operations { get; set; } = new Queue<Operation>();

   public RedBlackBST()
   {
      Root = null;
   }

   public Node Get(int key)
   {
      return Get(Root, key);
   }

   private Node Get(Node x, int key)
   {
      // Node with given key not present in tree

      if (x == null)
      {
         return null;
      }

      // Recursive search
      if (key < x.Key) return Get(x.Left, key);
      else if (key > x.Key) return Get(x.Right, key);

      // Found node with the given key - return it
      else
      {
         return x;
      }
   }

   public bool GetColor(int key)
   {

      return Get(Root, key).Color;
   }

   public void Put(int key, int val)
   {
      Root = Put(Root, key, val, null);
      Root.Color = BLACK;
   }

   private Node Put(Node h, int key, int val, Node parent)
   {

      // Reached bottom of tree. Insert new node here with red link to parent.
      if (h == null)
      {
         return new Node(key, val, 1, RED, parent);
      }

      // Binary search tree insertion
      int cmp = key.CompareTo(h.Key);
      if (cmp < 0) h.Left = Put(h.Left, key, val, h);
      else if (cmp > 0) h.Right = Put(h.Right, key, val, h);
      else h.Value = val;

      // Check red-black tree properties and add to queue if violation is found
      if (IsRed(h.Right) && !IsRed(h.Left))
      {
         Operations.Enqueue(new Operation(h, OperationType.RotateLeft));
      }
      if (IsRed(h.Left) && IsRed(h.Left.Left))
      {
         Operations.Enqueue(new Operation(h, OperationType.RotateRight));
      }
      if (IsRed(h.Left) && IsRed(h.Right))
      {
         Operations.Enqueue(new Operation(h, OperationType.FlipColors));
      }

      // Update the size of the subtree rooted at h
      h.N = Size(h.Left) + Size(h.Right) + 1;

      // Return the updated tree
      return h;

   }

   public void UpdateRootAfterRotateLeft(Node h)
   {
      Root = UpdateRootAfterRotateLeft(Root, h);
      Root.Color = BLACK;
   }

   private Node UpdateRootAfterRotateLeft(Node root, Node h)
   {
      Node oldParent = h.Parent;

      // Performing the rotation
      Node x = RotateLeft(h);

      if (h == Root) root = x;

      // Create link between the parent of h (before rotation) and the new node x (after rotation)
      x.Parent = oldParent;
      if (x.Parent != null)
      {
         if (x.Parent.Right == h)
         {
            x.Parent.Right = x;
         }
         else
         {
            x.Parent.Left = x;
         }
      }

      return root;
   }

   public void UpdateRootAfterRotateRight(Node h)
   {
      Root = UpdateRootAfterRotateRight(Root, h);
      Root.Color = BLACK;
   }

   private Node UpdateRootAfterRotateRight(Node root, Node h)
   {
      Node oldParent = h.Parent;

      // Performing the rotation
      Node x = RotateRight(h);

      if (h == Root) root = x;

      // Create link between the parent of h (before rotation) and the new node x (after rotation)
      x.Parent = oldParent;
      if (x.Parent != null)
      {
         if (x.Parent.Left == h)
         {
            x.Parent.Left = x;
         }
         else
         {
            x.Parent.Right = x;
         }
      }

      return root;
   }

   public void UpdateRootAfterFlipColors(Node h)
   {
      Root = UpdateRootAfterFlipColors(Root, h);
      Root.Color = BLACK;
   }

   private Node UpdateRootAfterFlipColors(Node root, Node h)
   {
      FlipColors(h);
      return root;
   }

   public void IsThereATreeViolation()
   {
      IsThereATreeViolation(Root);
   }

   private void IsThereATreeViolation(Node h)
   {
      if (h == null) return; // Leaf node reached

      // Check for violations and add correct operation to queue if found
      if (IsRed(h.Right) && !IsRed(h.Left))
      {
         Operations.Enqueue(new Operation(h, OperationType.RotateLeft));
      }
      if (IsRed(h.Left) && IsRed(h.Left.Left))
      {
         Operations.Enqueue(new Operation(h, OperationType.RotateRight));
      }
      if (IsRed(h.Left) && IsRed(h.Right))
      {
         Operations.Enqueue(new Operation(h, OperationType.FlipColors));
      }

      // Recursively check the left and right subtrees
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
      h.Right = x.Left; // Jar
      if (x.Left != null) x.Left.Parent = h;
      x.Left = h;
      x.Color = h.Color;
      h.Color = RED;
      x.N = h.N;
      h.N = 1 + Size(h.Left) + Size(h.Right);

      // Update parent references
      x.Parent = parent; // x gets the original parent of h
      h.Parent = x; // h is now a child of x

      return x;
   }

   private Node RotateRight(Node h)
   {
      Node x = h.Left;
      Node parent = h.Parent;
      h.Left = x.Right; // Jar
      if (x.Right != null) x.Right.Parent = h;
      x.Right = h;
      x.Color = h.Color;
      h.Color = RED;
      x.N = h.N;
      h.N = 1 + Size(h.Left) + Size(h.Right);

      // Update parent references
      x.Parent = parent; // x gets the original parent of h
      h.Parent = x; // h is now a child of x

      return x;
   }


   private void FlipColors(Node h)
   {
      h.Color = RED;
      h.Left.Color = BLACK;
      h.Right.Color = BLACK;
   }


   // When using our delete method, the node will always be at a leaf node, as the user has just placed it and not balanced the tree yet
   public void Delete(Node nodeToDelete)
   {
      // Delete the parent's link to nodeToDelete
      if (nodeToDelete.Parent.Left == nodeToDelete)
      {
         nodeToDelete.Parent.Left = null;
      }
      else
      {
         nodeToDelete.Parent.Right = null;
      }
   }

   private int Size(Node x)
   {
      if (x == null) return 0;
      return x.N;
   }

   public int Size() => Size(Root);


   public void ExecuteNextOperation()
   {
      // Queue is empty
      if (Operations.Count == 0)
      {
         return;
      }

      // Queue is not empty, execute the next operation
      Operation op = Operations.Dequeue();

      switch (op.OperationType)
      {
         case OperationType.RotateLeft:
            UpdateRootAfterRotateLeft(op.Node);
            break;
         case OperationType.RotateRight:
            UpdateRootAfterRotateRight(op.Node);
            break;
         case OperationType.FlipColors:
            UpdateRootAfterFlipColors(op.Node);
            break;
      }
   }

   public void PrintTree()
   {
      // UnityEngine.Debug.Log("Root is: " + Root.Key + " | " + Root.Value + " | " + (Root.Color ? "RED" : "BLACK"));
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
      UnityEngine.Debug.Log("Nu kigger jeg til ventre i træet");
      PrintTree(node.Left, depth + 1);


      UnityEngine.Debug.Log("Nu går jeg en op og nu kigger jeg til højre i træet");
      // Print the right subtree
      PrintTree(node.Right, depth + 1);
   }
}


