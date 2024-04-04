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
   { 
      //UnityEngine.Debug.Log("FIRST GET --- Get thinks root is " + Root.Key + " | " + Root.Value + " | " + (Root.Color ? "RED" : "BLACK"));
      return Get(Root, key); 
   }

   private Node Get(Node x, int key){
      // Node with given key not present in tree
      if (x == null) 
      {
         UnityEngine.Debug.Log("GET ---- Couldn't find node with key " + key + " in the tree");
         return null;
      }

      // Recursive search
      if (key < x.Key) return Get(x.Left, key);
      else if (key > x.Key) return Get(x.Right, key);

      // Found node with the given key - return it
      else 
      {
         UnityEngine.Debug.Log("GET ---- Found node with key " + key + " and value " + x.Value);
         return x;
      }
   }

   public bool GetColor(int key)
   {
      UnityEngine.Debug.Log("GETCOLOR --- Found color of node with key " + key + " to be " + Get(Root, key).Color);
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
         UnityEngine.Debug.Log("-------- Inserting new node with key " + key + " and value " + val + " --------");
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

      // Update the size of the subtree rooted at h
      h.N = Size(h.Left) + Size(h.Right) + 1;

      // Return the updated tree
      return h;

   }

   public void UpdateRootAfterRotateLeft(Node h){
      Root = UpdateRootAfterRotateLeft(Root, h);
      Root.Color = BLACK;
      UnityEngine.Debug.Log("Root is: " + Root.Key + " | " + Root.Value);
   }

   private Node UpdateRootAfterRotateLeft(Node root, Node h){
      // Update the link between the parent of h (before rotation) and the new node x (after rotation)
      //Node rotatedSubTree = RotateLeft(h);

      UnityEngine.Debug.Log("!!!!!!!********!!!!!!!!!!!!!!UpdateRootAfterRotateLeft!!!!!!!!!!!!!!!!!!");
      UnityEngine.Debug.Log("Root is: " + root.Key + " | " + root.Value);

      Node oldParent = h.Parent;
      if (h.Parent == null)
      {
         UnityEngine.Debug.Log("h.Parent and oldParent is null");
      }
      else
      {
         UnityEngine.Debug.Log("Node h.parent is: " + h.Parent.Key + " | " + h.Parent.Value);
         UnityEngine.Debug.Log("Node oldparent is: " + oldParent.Key + " | " + oldParent.Value);
      }

      UnityEngine.Debug.Log("Calling rotateLeft with node " + h.Key + " as the argument");
      Node x = RotateLeft(h);

      UnityEngine.Debug.Log("Node h is: " + h.Key + " | " + h.Value);
      UnityEngine.Debug.Log("Node root is: " + Root.Key + " | " + Root.Value);
      if (h == Root)
      {
         UnityEngine.Debug.Log("Root is h. Update root to x");
         root = x;
      }

      UnityEngine.Debug.Log("Node x is: " + x.Key + " | " + x.Value);
      x.Parent = oldParent;
      if (x.Parent == null)
      {
         UnityEngine.Debug.Log("x.Parent is null");
      }
      else
      {
         UnityEngine.Debug.Log("Node x.parent is: " + x.Parent.Key + " | " + x.Parent.Value);
      }

      if (x.Parent != null)
      {
         x.Parent.Left = x;
         UnityEngine.Debug.Log("Node x.parent.left is: " + x.Parent.Left.Key + " | " + x.Parent.Left.Value);
      }

      UnityEngine.Debug.Log("Root is: " + root.Key + " | " + root.Value);
      return root;


   }

   public void UpdateRootAfterRotateRight(Node h){
      Root = UpdateRootAfterRotateRight(Root, h);
      Root.Color = BLACK;
      UnityEngine.Debug.Log("Root is: " + Root.Key + " | " + Root.Value);


   }

   private Node UpdateRootAfterRotateRight(Node root, Node h){
      // Update the link between the parent of h (before rotation) and the new node x (after rotation)
      // Node rotatedSubTree = RotateLeft(h);

      
      UnityEngine.Debug.Log("!!!!!!!********!!!!!!!!!!!!!!UpdateRootAfterRotateRight!!!!!!!!!!!!!!!!!!");
      UnityEngine.Debug.Log("Root starts beeing: " + root.Key + " | " + root.Value);

      Node oldParent = h.Parent;
      if (h.Parent == null)
      {
         UnityEngine.Debug.Log("h.Parent and oldParent is null");
      }
      else
      {
         UnityEngine.Debug.Log("Node h.parent is: " + h.Parent.Key + " | " + h.Parent.Value);
         UnityEngine.Debug.Log("Node oldparent is: " + oldParent.Key + " | " + oldParent.Value);
      }

      UnityEngine.Debug.Log("RotateRight is called with node " + h.Key + " as the argument.");
      Node x = RotateRight(h);
      
      UnityEngine.Debug.Log("det vi leder efter nu!!!!");
      UnityEngine.Debug.Log("Node h is: " + h.Key + " | " + h.Value);
       UnityEngine.Debug.Log("Node root is: " + Root.Key + " | " + Root.Value);

      if (h == Root)
      {
         UnityEngine.Debug.Log("Root is h. Update root to x");
         root = x;
      }


      UnityEngine.Debug.Log("Node x is: " + x.Key + " | " + x.Value);
      x.Parent = oldParent;
      if (x.Parent == null)
      {
         UnityEngine.Debug.Log("x.Parent is null");
      }
      else
      {
         UnityEngine.Debug.Log("Node x.parent is: " + x.Parent.Key + " | " + x.Parent.Value);
      }

      if (x.Parent != null)
      {
         x.Parent.Right = x;
      }


      if (x.Parent == null)
      {
         UnityEngine.Debug.Log("x.Parent is null");
      }
      else
      {
         UnityEngine.Debug.Log("Node x.parent.right is: " + x.Parent.Right.Key + " | " + x.Parent.Right.Value);
      }

      

      UnityEngine.Debug.Log("Root is: " + root.Key + " | " + root.Value);
      return root;
   }

   public void UpdateRootAfterFlipColors(Node h){
      UnityEngine.Debug.Log("BEFORE FLIPPING COLORS, Root is: " + Root.Key);
      Root = UpdateRootAfterFlipColors(Root, h);
      Root.Color = BLACK;

   }

   private Node UpdateRootAfterFlipColors(Node root, Node h){
      FlipColors(h);
      return root;

   }



   
   public void IsThereATreeViolation()
   {
      UnityEngine.Debug.Log("Root is: " + Root.Key + " | " + Root.Value + " | " + (Root.Color ? "RED" : "BLACK"));
      IsThereATreeViolation(Root);
   }

   private void IsThereATreeViolation(Node h) {
      //PrintTree();
      //UnityEngine.Debug.Log("!!!!!!!!!!INSIDE IsThereATreeViolation!!!!!!!!!");
      /*
         starter fra root
         IsRed(h.Right) && !IsRed(h.Left)
         IsRed(h.Left) && IsRed(h.Left.Left)
         IsRed(h.Left) && IsRed(h.Right)

         også gå videre til children
      */
      if (h == null) return; // er på leaf node

      UnityEngine.Debug.Log("Node value inside IsThereATreeViolation: " + h.Value);

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

      // Maybe wrong when we have a lot of nodes
      //if (parent == null) Root = x;
      
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

      // Maybe wrong when we have a lot of nodes
      UnityEngine.Debug.Log("Inside RotateRight: Root is: " + Root.Key + " | " + Root.Value);
      //if (parent == null) Root = x;

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
            UnityEngine.Debug.Log("I am calling RotateLeft with the node " + op.Node.Key + " as the argument.");
            //RotateLeft(op.Node);
            UpdateRootAfterRotateLeft(op.Node);
            break;
         case OperationType.RotateRight:
            UnityEngine.Debug.Log("I am calling RotateRight with the node " + op.Node.Key + " as the argument.");
            UpdateRootAfterRotateRight(op.Node);
            break;
         case OperationType.FlipColors:
            UnityEngine.Debug.Log("*******************I am calling FlipColors with the node " + op.Node.Key + " as the argument.*******************");
            UpdateRootAfterFlipColors(op.Node); 
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


