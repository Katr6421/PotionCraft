using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class NullCircleSpawnerTests
{
    //[Test]
    /*public void CopyNullCircleSubtree_CopiesEntireSubtree()
    {
        // Arrange
        var spawner = new GameObject().AddComponent<NullCircleSpawner>();
        GameObject originalRoot = Instantiate(nullCirclePrefab, new Vector3(440, 239, 0), Quaternion.identity);
       // NullCircle originalRoot = spawner.Root.GetComponent<NullCircle>(); // You may need to add this method to get the root null circle.

        // Act
        var copiedRoot = spawner.CopyNullCircleSubtree(originalRoot);

        // Assert
        AssertCopiedSubtree(originalRoot, copiedRoot);
        
        // Clean up
        Object.DestroyImmediate(spawner.gameObject);
    }

    private void AssertCopiedSubtree(NullCircle original, NullCircle copy)
    {
        Assert.IsNotNull(copy);
        Assert.AreNotSame(original, copy);
        Assert.AreEqual(original.Value, copy.Value);
        Assert.AreEqual(original.IsActive, copy.IsActive);
        Assert.AreEqual(original.Index, copy.Index);
        Assert.AreEqual(original.IsRed, copy.IsRed);
        Assert.AreEqual(original.Ingredient, copy.Ingredient);
        
        // You should not copy the Ingredient and LineToParent if they are meant to be unique per instance.
        // Assert.IsNull(copy.Ingredient);
        // Assert.IsNull(copy.LineToParent);

        // Recursively assert for children
        if (original.LeftChild != null)
        {
            AssertCopiedSubtree(original.LeftChild.GetComponent<NullCircle>(), copy.LeftChild.GetComponent<NullCircle>());
        }
        else
        {
            Assert.IsNull(copy.LeftChild);
        }

        if (original.RightChild != null)
        {
            AssertCopiedSubtree(original.RightChild.GetComponent<NullCircle>(), copy.RightChild.GetComponent<NullCircle>());
        }
        else
        {
            Assert.IsNull(copy.RightChild);
        }

        // If the original had a parent, the copy should not have one, as it's a new root.
        Assert.IsNull(copy.Parent);
    }*/
}
