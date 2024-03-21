using NUnit.Framework;
using UnityEngine.TestTools;
using UnityEngine;

public class LevelManagerTests
{
    private GameObject levelManagerGameObject;
    private LevelManager levelManager;

    // Setup for each test, creates a LevelManager instance.
    [SetUp]
    public void SetUp()
    {
        levelManagerGameObject = new GameObject();
        levelManager = levelManagerGameObject.AddComponent<LevelManager>();
    }

    // Cleanup after each test, destroys the LevelManager instance.
    [TearDown]
    public void TearDown()
    {
        Object.Destroy(levelManagerGameObject);
    }

    [Test]
    public void LevelManager_SingletonInstance_IsNotNull()
    {
        // Check that the instance is not null
        Assert.IsNotNull(LevelManager.Instance);
    }

    [Test]
    public void LevelManager_InitializesLevels_Correctly()
    {
        // Call the InitializeLevels manually if it's public, otherwise you need to use reflection or make it public temporarily
        // If it's already called in Awake and it's private, you may skip calling it.
        levelManager.InitializeLevels();

        // Check that the Levels list is not empty
        Assert.IsNotEmpty(levelManager.Levels);
    }

    [Test]
    public void LevelManager_ReturnsCorrectPotionName()
    {
        string expectedPotionName = "1";
        int levelIndex = 1; // Assuming index 1 corresponds to "Potion 1"

        // You need to set up your levels here before calling the method
        // This might include adding the necessary LevelData objects to the Levels list
        levelManager.InitializeLevels();

        // Now get the potion name
        string potionName = levelManager.GetPotionName(levelIndex);

        // Check that the returned potion name is correct
        Assert.AreEqual(expectedPotionName, potionName);
    }

    // ... Additional tests for other methods like GetPotionImage, GetLevelIndex, etc.

    // Note: Testing methods that involve Unity's specific functionalities like Sprite may require
    // using [UnityTest] instead of [Test] and IEnumerator as the return type for coroutine support.
}
