using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentIngredientsMover : MonoBehaviour
{
    // Start is called before the first frame update
    float xPosition = 2.12f;
    float yPosition = 3.79f;
    float spaceBetweenNodes = 100;
  



    // Call this method to move the circle to a new position
    public void MoveCircle(int index)
    {
         float newXPosition = xPosition + (index * spaceBetweenNodes);
        transform.position = new Vector3(newXPosition, yPosition, 0);
    }
}
