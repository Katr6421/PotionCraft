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
    private IRedBlackBST tree;

    [SetUp]
    public void SetUp()
    {
        // Initialize RedBlackBST instance
        tree = new RedBlackBST();

        // Insert nodes into the RedBlackBST
        tree.Put(1, 1);
        tree.Put(2,2);
        tree.Put(3,3);
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
}
