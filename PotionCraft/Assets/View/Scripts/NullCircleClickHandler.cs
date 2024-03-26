using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NullCircleClickHandler : MonoBehaviour
{
    
    [SerializeField] private GameObject nullCirclePrefab; // Assigned the prefab in the inspector. Used to spawn new NullCircles
  
    private NodeSpawner NodeSpawner;

    private GameObject currentNodeGameObject; // Reference to the current Ingredient that the user must insert in the RedBlackTree
    private int currectNodeIndex = 0;     // Index of the current node in the list of node GameObjects. 

    [SerializeField] private GameObject circleMarkerPrefab;
    private GameObject CircleMarker; // Reference to the instantiated circle
    private Vector3 circleStartPosition = new Vector3(2.12f, 3.79f, 0); // Position of the circle

    void Start()
    {
        // Instantiate the circle from start
        CircleMarker = Instantiate(circleMarkerPrefab, circleStartPosition, Quaternion.identity);
    }

  
  

    private void setCurrentIngredientsGameObject()
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
        setCurrentIngredientsGameObject();

        // Check if there are any node GameObjects in the list
        if (currentNodeGameObject != null)
        {
            // Move the current ingredient to this NullCircle's position
            //currentNodeGameObject.transform.position = transform.position;
            Vector3 NullCirclePos = transform.position;
            StartCoroutine(MoveAndDestroy(currentNodeGameObject, NullCirclePos, 0.5f));

        }
        else
        {
            Debug.LogError("Ingen nodes at flytte");
        }

        
        if (CircleMarker == null)
    {
        // Instantiate the circle here if it only should appear upon clicking
        CircleMarker = Instantiate(circleMarkerPrefab, circleStartPosition, Quaternion.identity);
    }
    else
    {
        // Move the existing circle to a new position
        StartCoroutine(MoveCircle(CircleMarker, CalculatePosition(currectNodeIndex), 0.5f));
    }
  


        

       





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

IEnumerator MoveCircle(GameObject objectToMove, Vector3 destination, float duration)
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
}

 public Vector3 CalculatePosition(int nodeIndex)
{
        float leftBound = 2.12f; // x position of the leftmost point in the red circle, HARD CODED
        float yPosition = 3.79f; // y position where the nodes should be placed, HARD CODED
        float spaceBetweenNodes = 0.95f * nodeIndex; // Space between nodes, HARD CODED
        // Calculate the x position for the current node
        float xPosition = leftBound + spaceBetweenNodes;
        // Return the calculated position
        return new Vector3(xPosition, yPosition, 0);
    
}
}
