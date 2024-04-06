using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererManager : MonoBehaviour {
    [SerializeField] private NullCircleSpawner _nullCircleSpawner;


    public void UpdateLineRenderers()
    {
        UpdateLineRenderers(_nullCircleSpawner.Root.GetComponent<NullCircle>());
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