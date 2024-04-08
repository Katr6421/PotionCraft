using System.Collections.Generic;
using UnityEngine;

public class LineManager : MonoBehaviour
{
    [SerializeField] private NullCircleManager _nullCircleManager;
    [SerializeField] private GameObject _line;



    public GameObject CreateLine(GameObject startPosition, GameObject endPosition)
    {
        GameObject lineGameObject = Instantiate(_line);

        List<Transform> linePoints = new List<Transform> { startPosition.transform, endPosition.transform };
        lineGameObject.GetComponent<Line>().SetUpLine(linePoints);

        return lineGameObject;
    }

    public void UpdateLineColor(NullCircle nullCircle, bool isRed)
    {
        if (isRed)
        {
            nullCircle.Ingredient.GetComponent<Ingredient>().LineToParent.GetComponent<Line>().SetColor(Color.red);
        }
        else
        {
            nullCircle.Ingredient.GetComponent<Ingredient>().LineToParent.GetComponent<Line>().SetColor(Color.black);
        }
    }

    public void DrawLineToNullCircle(NullCircle endNullCircle)
    {

        if (endNullCircle.LeftChild.GetComponent<NullCircle>().Ingredient == null)
        {
            Destroy(endNullCircle.Ingredient.GetComponent<Ingredient>().LineToLeft);
            endNullCircle.Ingredient.GetComponent<Ingredient>().LineToLeft = CreateLine(endNullCircle.Ingredient, endNullCircle.LeftChild);
        }

        if (endNullCircle.RightChild.GetComponent<NullCircle>().Ingredient == null)
        {
            Destroy(endNullCircle.Ingredient.GetComponent<Ingredient>().LineToRight);
            endNullCircle.Ingredient.GetComponent<Ingredient>().LineToRight = CreateLine(endNullCircle.Ingredient, endNullCircle.RightChild);
        }


    }




























/*
    *********************************************
    Create a line renderer for the connection from parent to children
    *********************************************
    public GameObject SpawnLinesToParent(GameObject nullcircle, GameObject parent)
    {
        return CreateLineRenderer(nullcircle.transform, parent.transform);
    }

    *********************************************
    Creates a line renderer between two points
    Takes either a Transform or a Vector3 as the end position
    *********************************************
    private GameObject CreateLineRenderer(Transform startPosition, Transform endPosition)
    {
        GameObject lineGameObject = new GameObject("LineRendererObject");
        LineRenderer lineRenderer = lineGameObject.AddComponent<LineRenderer>();

        lineRenderer.material = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply"));
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.startColor = Color.black;
        lineRenderer.endColor = Color.black;

        // Set up the LineController with points
        LineController lineController = lineGameObject.AddComponent<LineController>();
        List<Transform> linePoints = new List<Transform> { startPosition, endPosition };
        lineController.SetUpLine(linePoints);

        return lineGameObject;
    }

    
         !!!REMEMBER THAT THE LINES GET DRAWN FROM BELOW AND UP!!!
        1. After insertion of ingredient - create a linerenderer between ingredient and nullcircles (left and right)
        2. Check if the nullCircle has a parent, if yes, update the line renderer from the parent to point to the new ingredient
        3. In update - the nullCircle is still in charge of updating the color and visibility of the line renderer
    
    



    public void UpdateLineRenderers()
    {
        UpdateLineRenderers(_nullCircleManager.Root.GetComponent<NullCircle>());
    }

    private void UpdateLineRenderers(NullCircle nullCircle)
    {
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
*/
}