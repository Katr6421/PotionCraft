using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

/*public class NewTestScript
{
    // A Test behaves as an ordinary method
    [Test]
    public void NewTestScriptSimplePasses()
    {
        // Use the Assert class to test conditions
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator NewTestScriptWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}*/
public class RedBlackBSTTest
{
    private RedBlackBST tree;

    [SetUp]
    public void SetUp()
    {
        // Initialize RedBlackBST instance
        tree = new RedBlackBST();

        // Insert nodes into the RedBlackBST
        tree.Put(2,2);
        tree.Put(1,1);
        
    }


    // Example from chat
    [Test]
    public void Insert_IncreasesSize()
    {
        // Arrange
        var initialSize = tree.Size();

        // Act
        tree.Put(4, 4);

        // Assert
        Assert.AreEqual(initialSize + 1, tree.Size());
    }

    // Test if parent is assigned correctly
    [Test]
    public void IsCorrectParent()
    {
        // Arrange
        var node = tree.Get(1);

        // Act
        var parent = node.Parent;

        // Assert
        Assert.AreEqual(2, parent.Key);
    }

    [Test]
    public void isCorrectParentLeftValue()
    {
        // Arrange
        var node = tree.Get(1);

        // Act
        var parent = node.Parent;

        // Assert
        Assert.AreEqual(1, parent.Left.Value);
    }
}
