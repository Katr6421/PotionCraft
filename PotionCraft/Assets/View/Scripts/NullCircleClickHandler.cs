using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NullCircleClickHandler : MonoBehaviour
{
    
    [SerializeField] private GameObject nullCirclePrefab; // Assigned the prefab in the inspector. Used to spawn new NullCircles
    NodeSpawner NodeSpawner;

    
    private GameObject currentNodeGameObject; // Reference to the current Ingredient that the user must insert in the RedBlackTree
    private int currectNodeIndex = 0;     // Index of the current node in the list of node GameObjects. 

  


    private void setCurrentIngredients()
    {
        // If there are more ingredients to insert, get the next ingredient
        if (currectNodeIndex < NodeSpawner.nodeObjects.Count){currentNodeGameObject = NodeSpawner.nodeObjects[currectNodeIndex];}
        else{Debug.Log("Ingen flere ingredienser at indsætte");}
        currectNodeIndex++;
    }


    /*********************************************
    METHOD: OnClickedNullCirkle                          
    DESCRIPTION: This method is called when the NullCircle GameObject is clicked.
    *********************************************/

     public void OnClickedNullCirkle()
    {
         Debug.Log("Jeg har klikket på en NullCircle");
        // Get the current ingredient that the user must insert in the RedBlackTree
        setCurrentIngredients();

        // Check if there are any node GameObjects in the list
        if (currentNodeGameObject != null)
        {
        // Move the current ingredient to this NullCircle's position
        //currentNodeGameObject.transform.position = transform.position;
        Vector3 NullCirclePos = transform.position;
        StartCoroutine(MoveAndDestroy(currentNodeGameObject, NullCirclePos, 0.5f));
      
        }
         else{Debug.LogError("Ingen nodes at flytte");}



        // Instantiate the two new NullCircles
        //GameObject leftChildNullCircle = Instantiate(nullCirclePrefab, CalculateLeftChildPosition(), Quaternion.identity);
        //GameObject rightChildNullCircle = Instantiate(nullCirclePrefab, CalculateRightChildPosition(), Quaternion.identity);

        // Instantiate and set up lines
        //DrawLine(nodeObject.transform.position, leftChildNullCircle.transform.position);
        //DrawLine(nodeObject.transform.position, rightChildNullCircle.transform.position);

        
    }
  
    

 

    

    Vector3 CalculateLeftChildPosition()
    {
        throw new System.NotImplementedException();
    }

    Vector3 CalculateRightChildPosition()
    {
        throw new System.NotImplementedException();
    }

    void DrawLine(Vector3 start, Vector3 end)
    {
        //GameObject line = Instantiate(linePrefab);
        //LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
        //lineRenderer.SetPosition(0, start);
        //lineRenderer.SetPosition(1, end);
    }

/*********************************************
    METHOD: MoveAndDestroy                          
    DESCRIPTION: This method moves a GameObject to a destination position over a specified duration.
    *********************************************/

      IEnumerator MoveAndDestroy(GameObject objectToMove, Vector3 destination, float duration)
    {
    float elapsedTime = 0;
    Vector3 startingPos = objectToMove.transform.position;

    while (elapsedTime < duration)
    {
        objectToMove.transform.position = Vector3.Lerp(startingPos, destination, (elapsedTime / duration));
        elapsedTime += Time.deltaTime;
        yield return null; // Wait for the next frame
    }

    objectToMove.transform.position = destination; // Ensure it reaches the destination

    // Now that the movement is complete, destroy the NullCircle GameObject
    Destroy(gameObject); // Assuming gameObject is the NullCircle you want to destroy
}
}
