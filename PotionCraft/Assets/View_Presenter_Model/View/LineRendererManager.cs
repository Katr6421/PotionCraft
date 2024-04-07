using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererManager : MonoBehaviour
{
    [SerializeField] private NullCircleManager _nullCircleManager;
    


    /*********************************************
    Create a line renderer for the connection from parent to children
    *********************************************/
    public GameObject SpawnLinesToParent(GameObject nullcircle, GameObject parent)
    {
        return CreateLineRenderer(nullcircle.transform, parent.transform);
    }


    /*********************************************
    For the bottom "invinsible" nullCircles, they should have a small line below them to indicate a null link
    *********************************************/

    /*
    public GameObject SpawnNullLinkLine(GameObject parent)
    {
        Vector3 startPosition = parent.transform.position;
        Vector3 endPosition = new Vector3(startPosition.x, startPosition.y - 1.0f, startPosition.z); // Adjust length here

        return CreateLineRenderer(parent.transform, endPositionVector: endPosition);
    }
    */


    /*********************************************
    Creates a line renderer between two points
    Takes either a Transform or a Vector3 as the end position
    *********************************************/
    private GameObject CreateLineRenderer(Transform startPosition, Transform endPositionTransform = null)
    {
        GameObject lineGameObject = new GameObject("LineRendererObject");
        LineRenderer lineRenderer = lineGameObject.AddComponent<LineRenderer>();

        lineRenderer.material = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply"));
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.startColor = Color.black;
        lineRenderer.endColor = Color.black;
        

        // Find end position based on the provided type
        Transform endPosition;
        // For nullcircle 1-30, the end position is another nullcircle and therefore type Transform
        endPosition = endPositionTransform;

        // Set up the LineController with points
        LineController lineController = lineGameObject.AddComponent<LineController>();
        List<Transform> linePoints = new List<Transform> { startPosition, endPosition };
        lineController.SetUpLine(linePoints);

        return lineGameObject;
    }


    /**************** Delete if above CreateLineRenderer method works ******************
   private GameObject CreateLineRenderer(Transform startPosition, Transform endPosition)
   {
       // Create a new GameObject with a name indicating it's a line renderer
       GameObject lineGameObject = new GameObject("LineRendererObject");

       // Add a LineRenderer component to the GameObject
       LineRenderer lineRenderer = lineGameObject.AddComponent<LineRenderer>();

       // Set up the LineRenderer
       //lineRenderer.material = lineMaterial; // Assuming lineMaterial is assigned elsewhere in your script
       lineRenderer.material = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply"));
       lineRenderer.startWidth = 0.05f; // Assuming lineWidth is set elsewhere in your script
       lineRenderer.endWidth = 0.05f;
       // Set the color of the line dynamically
       lineRenderer.startColor = Color.black;
       lineRenderer.endColor = Color.black;

       // Add your LineController script to the new GameObject
       LineController lineController = lineGameObject.AddComponent<LineController>();

       // Create a list of points for the line
       List<Transform> linePoints = new List<Transform>();
       linePoints.Add(startPosition); // Start point
       linePoints.Add(endPosition); // End point for the first line

       // Set up the line using the LineController script
       lineController.SetUpLine(linePoints);

       return lineGameObject;
   }

   **************** Delete if above CreateLineRenderer method works ******************
   private GameObject CreateLineRendererNullLinks(Transform startPosition, Vector3 endPosition)
   {
       GameObject lineGameObject = new GameObject("LineRendererObject");
       LineRenderer lineRenderer = lineGameObject.AddComponent<LineRenderer>();

       lineRenderer.material = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply"));
       lineRenderer.startWidth = 0.05f;
       lineRenderer.endWidth = 0.05f;
       lineRenderer.startColor = Color.black;
       lineRenderer.endColor = Color.black;

       lineRenderer.positionCount = 2; // Sets line point count
       lineRenderer.SetPosition(0, startPosition.position);
       lineRenderer.SetPosition(1, endPosition);

       // Create a temporary GameObject for the end position, since it needs to be type Transform
       GameObject endPositionObject = new GameObject("TemporaryEndPosition");
       endPositionObject.transform.position = endPosition;

       // Add your LineController script to the new GameObject
       LineController lineController = lineGameObject.AddComponent<LineController>();
       List<Transform> linePoints = new List<Transform>();
       linePoints.Add(startPosition); // Start point
       linePoints.Add(endPositionObject.transform); // Temporary end point

       // Set up the line using the LineController script
       lineController.SetUpLine(linePoints);

       return lineGameObject;
   }
   */


    /*
        // !!!REMEMBER THAT THE LINES GET DRAWN FROM BELOW AND UP!!!
        1. After insertion of ingredient - create a linerenderer between ingredient and nullcircles (left and right)
        2. Check if the nullCircle has a parent, if yes, update the line renderer from the parent to point to the new ingredient
        3. In update - the nullCircle is still in charge of updating the color and visibility of the line renderer
    
    */


    public void UpdateLineRenderers()
    {
        UpdateLineRenderers(_nullCircleManager.Root.GetComponent<NullCircle>());
    }

    private void UpdateLineRenderers(NullCircle nullCircle)
    {
        //Debug.Log("I am in UpdateLineRenderers");

        // In leaf
        if (nullCircle == null) return;

        // Recursively update and draw the line renderers for the children
        NullCircle leftChild = nullCircle.LeftChild?.GetComponent<NullCircle>();
        NullCircle rightChild = nullCircle.RightChild?.GetComponent<NullCircle>();
        UpdateLineRenderers(leftChild);
        UpdateLineRenderers(rightChild);

        // At root
        if (nullCircle.Parent == null) return;

        // Set color of the line
        if (nullCircle.IsRed)
        {
            //Debug.Log("I am in UpdateLineRenderers and the nullcircle is red and its index is" + nullCircle.Index);
            nullCircle.LineToParent.GetComponent<LineRenderer>().startColor = Color.red;
            nullCircle.LineToParent.GetComponent<LineRenderer>().endColor = Color.red;

        }
        if (!nullCircle.IsRed)
        {
            //Debug.Log("I am in UpdateLineRenderers and the nullcircle is black and its index is" + nullCircle.Index);
            nullCircle.LineToParent.GetComponent<LineRenderer>().startColor = Color.black;
            nullCircle.LineToParent.GetComponent<LineRenderer>().endColor = Color.black;
        }

        // If the parent has a value, draw the line renderer from nullcircle to parent
        if (nullCircle.Parent.GetComponent<NullCircle>().Ingredient != null)
        {
            ShowLineRender(nullCircle);
        }
        else
        {
            HideLineRenderer(nullCircle);
        }
    }

    public void ShowLineRender(NullCircle nullCircle)
    {
        if (nullCircle == null) return;

        // Show the line on the screen
        LineRenderer lineRenderer = nullCircle.LineToParent.GetComponent<LineRenderer>();
        if (lineRenderer != null)
        {
            lineRenderer.enabled = true;
        }
    }

    public void HideLineRenderer(NullCircle nullCircle)
    {
        if (nullCircle == null) return;

        // Hide the line on the screen
        LineRenderer lineRenderer = nullCircle.LineToParent.GetComponent<LineRenderer>();
        if (lineRenderer != null)
        {
            lineRenderer.enabled = false;
        }
    }

}