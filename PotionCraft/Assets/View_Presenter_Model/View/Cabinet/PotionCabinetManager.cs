using System;
using System.Collections;
using UnityEngine;

public class PotionCabinetManager : MonoBehaviour
{
    public Potion[] potions; // Assign the array size and potions in the inspector
    public Transform[] gridPositions; // Assign the corresponding transforms in the inspector for each potion
    public GameObject sparkleEffectPrefab; // Assign this in the Unity Editor


    private void Start()
    {
        LoadPotions();
    }

    private void LoadPotions()
    {
        for (int i = 0; i < potions.Length; i++)
        {
            potions[i].isCollected = PlayerPrefs.GetInt("PotionCollected_" + i, 0) == 1;
            if (potions[i].isCollected)
            {
                GameObject potionInstance  = Instantiate(potions[i].potionPrefab, gridPositions[i].position, Quaternion.identity, gridPositions[i]);
                potionInstance.transform.localScale = new Vector3(7, 7, 1); // Set this as needed
            }
        }
    }

    public void CompleteLevel(int levelIndex, Action onComplete)
    {
        // Becuase levels are 1-indexed
        levelIndex--;

        if (levelIndex < potions.Length)
        {
            if (!potions[levelIndex].isCollected) // If the potion has not been collected yet
            {
                //Debug.Log($"Potion {levelIndex} not collected yet. Putting it in cabinet...");
                StartCoroutine(CompleteLevelSequence(levelIndex, onComplete));
            }
            else
            {
                //Debug.Log($"Potion {levelIndex} has already been collected.");
                onComplete?.Invoke();
            }
        }
    }

    private IEnumerator CompleteLevelSequence(int levelIndex, Action onComplete)
    {
        // Save the collected potion as player preference
        potions[levelIndex].isCollected = true;
        PlayerPrefs.SetInt("PotionCollected_" + levelIndex, 1);
        PlayerPrefs.Save();

        // Instantiate potion in cabinet
        GameObject potionInstance = Instantiate(potions[levelIndex].potionPrefab, gridPositions[levelIndex].position, Quaternion.identity, gridPositions[levelIndex]);
        potionInstance.transform.localScale = new Vector3(7, 7, 1); // Adjust scaling as needed
        SpriteRenderer potionRenderer = potionInstance.GetComponent<SpriteRenderer>();
        if (potionRenderer != null)
        {
            potionRenderer.sortingLayerName = "Potions";
            potionRenderer.sortingOrder = 1; // Higher number to render on top within the same layer
        }

        // Instantiate sparkle effect
        GameObject sparkle = Instantiate(sparkleEffectPrefab, gridPositions[levelIndex].position, Quaternion.identity, gridPositions[levelIndex]);
        ParticleSystemRenderer sparkleRenderer = sparkle.GetComponent<ParticleSystemRenderer>();
        if (sparkleRenderer != null)
        {
            sparkleRenderer.sortingLayerName = "Effects";
            sparkleRenderer.sortingOrder = 0; // Lower number to render behind within the same layer
        }

        //Debug.Log($"Potion {levelIndex} collected and instantiated at {gridPositions[levelIndex].position}. Sparkles initiated.");

        // Wait for the sparkle effect to finish
        yield return new WaitForSeconds(1.5f); 

        Destroy(sparkle); // Clean up the particle system after it has finished

        yield return new WaitForSeconds(0.5f); // Wait for a few seconds before loading the popup
        onComplete?.Invoke();
    }

}

