using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NullCircleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject nullCirclePrefab;
    [SerializeField] private Canvas uiCanvas; // Reference to the Canvas where the nullCircles will be parented
    public Dictionary<int, GameObject> NullCircles { get; } = new Dictionary<int, GameObject>();
    private GameObject root;

    // Start is called before the first frame update
    void Start()
    {
        SpawnNullCircles();
    }

    public void SpawnNullCircles()
    {
        // Instantiate the nullCircles
        GameObject nullCircle0 = Instantiate(nullCirclePrefab, new Vector3(440, 239, 0), Quaternion.identity);
        root = nullCircle0;

        GameObject nullCircle1 = Instantiate(nullCirclePrefab, new Vector3(195, 117, 0), Quaternion.identity);
        GameObject nullCircle2 = Instantiate(nullCirclePrefab, new Vector3(685, 117, 0), Quaternion.identity);

        GameObject nullCircle3 = Instantiate(nullCirclePrefab, new Vector3(72, -40, 0), Quaternion.identity);
        GameObject nullCircle4 = Instantiate(nullCirclePrefab, new Vector3(317, -40, 0), Quaternion.identity);

        GameObject nullCircle5 = Instantiate(nullCirclePrefab, new Vector3(562, -40, 0), Quaternion.identity);
        GameObject nullCircle6 = Instantiate(nullCirclePrefab, new Vector3(807, -40, 0), Quaternion.identity);

        GameObject nullCircle7 = Instantiate(nullCirclePrefab, new Vector3(11, -200, 0), Quaternion.identity);
        GameObject nullCircle8 = Instantiate(nullCirclePrefab, new Vector3(133, -200, 0), Quaternion.identity);

        GameObject nullCircle9 = Instantiate(nullCirclePrefab, new Vector3(256, -200, 0), Quaternion.identity);
        GameObject nullCircle10 = Instantiate(nullCirclePrefab, new Vector3(378, -200, 0), Quaternion.identity);

        GameObject nullCircle11 = Instantiate(nullCirclePrefab, new Vector3(501, -200, 0), Quaternion.identity);
        GameObject nullCircle12 = Instantiate(nullCirclePrefab, new Vector3(623, -200, 0), Quaternion.identity);

        GameObject nullCircle13 = Instantiate(nullCirclePrefab, new Vector3(746, -200, 0), Quaternion.identity);
        GameObject nullCircle14 = Instantiate(nullCirclePrefab, new Vector3(868, -200, 0), Quaternion.identity);

        GameObject nullCircle15 = Instantiate(nullCirclePrefab, new Vector3(-19, -322, 0), Quaternion.identity);
        GameObject nullCircle16 = Instantiate(nullCirclePrefab, new Vector3(41, -322, 0), Quaternion.identity);

        GameObject nullCircle17 = Instantiate(nullCirclePrefab, new Vector3(102, -322, 0), Quaternion.identity);
        GameObject nullCircle18 = Instantiate(nullCirclePrefab, new Vector3(164, -322, 0), Quaternion.identity);

        GameObject nullCircle19 = Instantiate(nullCirclePrefab, new Vector3(225, -322, 0), Quaternion.identity);
        GameObject nullCircle20 = Instantiate(nullCirclePrefab, new Vector3(287, -322, 0), Quaternion.identity);

        GameObject nullCircle21 = Instantiate(nullCirclePrefab, new Vector3(348, -322, 0), Quaternion.identity);
        GameObject nullCircle22 = Instantiate(nullCirclePrefab, new Vector3(410, -322, 0), Quaternion.identity);

        GameObject nullCircle23 = Instantiate(nullCirclePrefab, new Vector3(471, -322, 0), Quaternion.identity);
        GameObject nullCircle24 = Instantiate(nullCirclePrefab, new Vector3(533, -322, 0), Quaternion.identity);

        GameObject nullCircle25 = Instantiate(nullCirclePrefab, new Vector3(594, -322, 0), Quaternion.identity);
        GameObject nullCircle26 = Instantiate(nullCirclePrefab, new Vector3(656, -322, 0), Quaternion.identity);

        GameObject nullCircle27 = Instantiate(nullCirclePrefab, new Vector3(717, -322, 0), Quaternion.identity);
        GameObject nullCircle28 = Instantiate(nullCirclePrefab, new Vector3(779, -322, 0), Quaternion.identity);

        GameObject nullCircle29 = Instantiate(nullCirclePrefab, new Vector3(840, -322, 0), Quaternion.identity);
        GameObject nullCircle30 = Instantiate(nullCirclePrefab, new Vector3(902, -322, 0), Quaternion.identity);
        

        

        //Set the children of each nullCircle
        nullCircle0.GetComponent<NullCircle>().LeftChild = nullCircle1;
        nullCircle0.GetComponent<NullCircle>().RightChild = nullCircle2;
        nullCircle1.GetComponent<NullCircle>().LeftChild = nullCircle3;
        nullCircle1.GetComponent<NullCircle>().RightChild = nullCircle4;
        nullCircle2.GetComponent<NullCircle>().LeftChild = nullCircle5;
        nullCircle2.GetComponent<NullCircle>().RightChild = nullCircle6;
        nullCircle3.GetComponent<NullCircle>().LeftChild = nullCircle7;
        nullCircle3.GetComponent<NullCircle>().RightChild = nullCircle8;
        nullCircle4.GetComponent<NullCircle>().LeftChild = nullCircle9;
        nullCircle4.GetComponent<NullCircle>().RightChild = nullCircle10;
        nullCircle5.GetComponent<NullCircle>().LeftChild = nullCircle11;
        nullCircle5.GetComponent<NullCircle>().RightChild = nullCircle12;
        nullCircle6.GetComponent<NullCircle>().LeftChild = nullCircle13;
        nullCircle6.GetComponent<NullCircle>().RightChild = nullCircle14;
        nullCircle7.GetComponent<NullCircle>().LeftChild = nullCircle15;
        nullCircle7.GetComponent<NullCircle>().RightChild = nullCircle16;
        nullCircle8.GetComponent<NullCircle>().LeftChild = nullCircle17;
        nullCircle8.GetComponent<NullCircle>().RightChild = nullCircle18;
        nullCircle9.GetComponent<NullCircle>().LeftChild = nullCircle19;
        nullCircle9.GetComponent<NullCircle>().RightChild = nullCircle20;
        nullCircle10.GetComponent<NullCircle>().LeftChild = nullCircle21;
        nullCircle10.GetComponent<NullCircle>().RightChild = nullCircle22;
        nullCircle11.GetComponent<NullCircle>().LeftChild = nullCircle23;
        nullCircle11.GetComponent<NullCircle>().RightChild = nullCircle24;
        nullCircle12.GetComponent<NullCircle>().LeftChild = nullCircle25;
        nullCircle12.GetComponent<NullCircle>().RightChild = nullCircle26;
        nullCircle13.GetComponent<NullCircle>().LeftChild = nullCircle27;
        nullCircle13.GetComponent<NullCircle>().RightChild = nullCircle28;
        nullCircle14.GetComponent<NullCircle>().LeftChild = nullCircle29;
        nullCircle14.GetComponent<NullCircle>().RightChild = nullCircle30;


        // Set the parent of each nullCircle
        nullCircle0.GetComponent<NullCircle>().Parent = null;
        nullCircle1.GetComponent<NullCircle>().Parent = nullCircle0;
        nullCircle2.GetComponent<NullCircle>().Parent = nullCircle0;
        nullCircle3.GetComponent<NullCircle>().Parent = nullCircle1;
        nullCircle4.GetComponent<NullCircle>().Parent = nullCircle1;
        nullCircle5.GetComponent<NullCircle>().Parent = nullCircle2;
        nullCircle6.GetComponent<NullCircle>().Parent = nullCircle2;
        nullCircle7.GetComponent<NullCircle>().Parent = nullCircle3;
        nullCircle8.GetComponent<NullCircle>().Parent = nullCircle3;
        nullCircle9.GetComponent<NullCircle>().Parent = nullCircle4;
        nullCircle10.GetComponent<NullCircle>().Parent = nullCircle4;
        nullCircle11.GetComponent<NullCircle>().Parent = nullCircle5;
        nullCircle12.GetComponent<NullCircle>().Parent = nullCircle5;
        nullCircle13.GetComponent<NullCircle>().Parent = nullCircle6;
        nullCircle14.GetComponent<NullCircle>().Parent = nullCircle6;
        nullCircle15.GetComponent<NullCircle>().Parent = nullCircle7;
        nullCircle16.GetComponent<NullCircle>().Parent = nullCircle7;
        nullCircle17.GetComponent<NullCircle>().Parent = nullCircle8;
        nullCircle18.GetComponent<NullCircle>().Parent = nullCircle8;
        nullCircle19.GetComponent<NullCircle>().Parent = nullCircle9;
        nullCircle20.GetComponent<NullCircle>().Parent = nullCircle9;
        nullCircle21.GetComponent<NullCircle>().Parent = nullCircle10;
        nullCircle22.GetComponent<NullCircle>().Parent = nullCircle10;
        nullCircle23.GetComponent<NullCircle>().Parent = nullCircle11;
        nullCircle24.GetComponent<NullCircle>().Parent = nullCircle11;
        nullCircle25.GetComponent<NullCircle>().Parent = nullCircle12;
        nullCircle26.GetComponent<NullCircle>().Parent = nullCircle12;
        nullCircle27.GetComponent<NullCircle>().Parent = nullCircle13;
        nullCircle28.GetComponent<NullCircle>().Parent = nullCircle13;
        nullCircle29.GetComponent<NullCircle>().Parent = nullCircle14;
        nullCircle30.GetComponent<NullCircle>().Parent = nullCircle14;


        // Add the nullCircles to the dictionary
        NullCircles.Add(0, nullCircle0);
        NullCircles.Add(1, nullCircle1);
        NullCircles.Add(2, nullCircle2);
        NullCircles.Add(3, nullCircle3);
        NullCircles.Add(4, nullCircle4);
        NullCircles.Add(5, nullCircle5);
        NullCircles.Add(6, nullCircle6);
        NullCircles.Add(7, nullCircle7);
        NullCircles.Add(8, nullCircle8);
        NullCircles.Add(9, nullCircle9);
        NullCircles.Add(10, nullCircle10);
        NullCircles.Add(11, nullCircle11);
        NullCircles.Add(12, nullCircle12);
        NullCircles.Add(13, nullCircle13);
        NullCircles.Add(14, nullCircle14);
        NullCircles.Add(15, nullCircle15);
        NullCircles.Add(16, nullCircle16);
        NullCircles.Add(17, nullCircle17);
        NullCircles.Add(18, nullCircle18);
        NullCircles.Add(19, nullCircle19);
        NullCircles.Add(20, nullCircle20);
        NullCircles.Add(21, nullCircle21);
        NullCircles.Add(22, nullCircle22);
        NullCircles.Add(23, nullCircle23);
        NullCircles.Add(24, nullCircle24);
        NullCircles.Add(25, nullCircle25);
        NullCircles.Add(26, nullCircle26);
        NullCircles.Add(27, nullCircle27);
        NullCircles.Add(28, nullCircle28);
        NullCircles.Add(29, nullCircle29);
        NullCircles.Add(30, nullCircle30);

        // Spawns the lines and assigns them to the nullCircles
        nullCircle1.GetComponent<NullCircle>().LineToParent = SpawnLinesToParent(nullCircle1, nullCircle0);
        nullCircle2.GetComponent<NullCircle>().LineToParent = SpawnLinesToParent(nullCircle2, nullCircle0);
        nullCircle3.GetComponent<NullCircle>().LineToParent = SpawnLinesToParent(nullCircle3, nullCircle1);
        nullCircle4.GetComponent<NullCircle>().LineToParent = SpawnLinesToParent(nullCircle4, nullCircle1);
        nullCircle5.GetComponent<NullCircle>().LineToParent = SpawnLinesToParent(nullCircle5, nullCircle2);
        nullCircle6.GetComponent<NullCircle>().LineToParent = SpawnLinesToParent(nullCircle6, nullCircle2);
        nullCircle7.GetComponent<NullCircle>().LineToParent = SpawnLinesToParent(nullCircle7, nullCircle3);
        nullCircle8.GetComponent<NullCircle>().LineToParent = SpawnLinesToParent(nullCircle8, nullCircle3);
        nullCircle9.GetComponent<NullCircle>().LineToParent = SpawnLinesToParent(nullCircle9, nullCircle4);
        nullCircle10.GetComponent<NullCircle>().LineToParent = SpawnLinesToParent(nullCircle10, nullCircle4);
        nullCircle11.GetComponent<NullCircle>().LineToParent = SpawnLinesToParent(nullCircle11, nullCircle5);
        nullCircle12.GetComponent<NullCircle>().LineToParent = SpawnLinesToParent(nullCircle12, nullCircle5);
        nullCircle13.GetComponent<NullCircle>().LineToParent = SpawnLinesToParent(nullCircle13, nullCircle6);
        nullCircle14.GetComponent<NullCircle>().LineToParent = SpawnLinesToParent(nullCircle14, nullCircle6);
        nullCircle15.GetComponent<NullCircle>().LineToParent = SpawnLinesToParent(nullCircle15, nullCircle7);
        nullCircle16.GetComponent<NullCircle>().LineToParent = SpawnLinesToParent(nullCircle16, nullCircle7);
        nullCircle17.GetComponent<NullCircle>().LineToParent = SpawnLinesToParent(nullCircle17, nullCircle8);
        nullCircle18.GetComponent<NullCircle>().LineToParent = SpawnLinesToParent(nullCircle18, nullCircle8);
        nullCircle19.GetComponent<NullCircle>().LineToParent = SpawnLinesToParent(nullCircle19, nullCircle9);
        nullCircle20.GetComponent<NullCircle>().LineToParent = SpawnLinesToParent(nullCircle20, nullCircle9);
        nullCircle21.GetComponent<NullCircle>().LineToParent = SpawnLinesToParent(nullCircle21, nullCircle10);
        nullCircle22.GetComponent<NullCircle>().LineToParent = SpawnLinesToParent(nullCircle22, nullCircle10);
        nullCircle23.GetComponent<NullCircle>().LineToParent = SpawnLinesToParent(nullCircle23, nullCircle11);
        nullCircle24.GetComponent<NullCircle>().LineToParent = SpawnLinesToParent(nullCircle24, nullCircle11);
        nullCircle25.GetComponent<NullCircle>().LineToParent = SpawnLinesToParent(nullCircle25, nullCircle12);
        nullCircle26.GetComponent<NullCircle>().LineToParent = SpawnLinesToParent(nullCircle26, nullCircle12);
        nullCircle27.GetComponent<NullCircle>().LineToParent = SpawnLinesToParent(nullCircle27, nullCircle13);
        nullCircle28.GetComponent<NullCircle>().LineToParent = SpawnLinesToParent(nullCircle28, nullCircle13);
        nullCircle29.GetComponent<NullCircle>().LineToParent = SpawnLinesToParent(nullCircle29, nullCircle14);
        nullCircle30.GetComponent<NullCircle>().LineToParent = SpawnLinesToParent(nullCircle30, nullCircle14);

        // Hide all linderenders to begin with
        UpdateLineRenderers();


        for (int i = 0; i < NullCircles.Count; i++)
        {
            NullCircles[i].GetComponent<NullCircle>().Index = i;
        }

        
        // Deactivate all nullCircles
        // Set the parent of all nullCircles to the uiCanvas
        foreach (GameObject c in NullCircles.Values)
        {
            HideNullCircle(c.GetComponent<NullCircle>());
            c.transform.SetParent(uiCanvas.transform, false);
        }

        // Activate the root nullCircle
        ShowNullCircle(nullCircle0.GetComponent<NullCircle>());
    }

    public GameObject Get(int index)
    { return Get(root, index); }

    private GameObject Get(GameObject x, int key)
    {
        NullCircle nc = x.GetComponent<NullCircle>();
        // Return Node associated with key in the subtree rooted at x;
        // return null if key not present in subtree rooted at x.
        if (nc == null) return null;
        if (key < nc.Index) return Get(nc.LeftChild, key);
        else if (key > nc.Index) return Get(nc.RightChild, key);
        else return x;
    }




    /// <summary>
    /// Recursively updates the IsActive property of NullCircles.
    /// Recursively checks if there is a ingredient places onto the nullcircle, if not, the nullcircle is active.
    /// </summary>
    public void UpdateActiveNullCirclesAndShow()
    {
        
        UpdateActiveNullCirclesAndShow(root.GetComponent<NullCircle>());
    }
    
    private void UpdateActiveNullCirclesAndShow(NullCircle nullCircle)
    {
        if (nullCircle == null) return;

        // If the current nullCircle has no value, it should be active,
        // but we don't need to check its children because they would be beyond the current "border" of values.
        if (nullCircle.Ingredient == null)
        {
            nullCircle.IsActive = true;
            ShowNullCircle(nullCircle);
        }
        else
        {
            nullCircle.IsActive = false;
            HideNullCircle(nullCircle);
            // If this nullCircle has a value, its children might need to be activated,
            // so we recursively check them.
            NullCircle leftChild = nullCircle.LeftChild?.GetComponent<NullCircle>();
            NullCircle rightChild = nullCircle.RightChild?.GetComponent<NullCircle>();

            UpdateActiveNullCirclesAndShow(leftChild);
            UpdateActiveNullCirclesAndShow(rightChild);
        }
    }

    public void DeactivateAllNullCirclesInSubtree(NullCircle nullCircle)
    {
        if (nullCircle == null) return;

        nullCircle.IsActive = false;
        HideNullCircle(nullCircle);
        // If this nullCircle has a value, its children might need to be activated,
        // so we recursively check them.
        NullCircle leftChild = nullCircle.LeftChild?.GetComponent<NullCircle>();
        NullCircle rightChild = nullCircle.RightChild?.GetComponent<NullCircle>();

        DeactivateAllNullCirclesInSubtree(leftChild);
        DeactivateAllNullCirclesInSubtree(rightChild);
    }

    public void UpdateLineRenderers()
    {
        UpdateLineRenderers(root.GetComponent<NullCircle>());
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
        if(nullCircle.IsRed ){
            //Debug.Log("I am in UpdateLineRenderers and the nullcircle is red and its index is" + nullCircle.Index);
            nullCircle.LineToParent.GetComponent<LineRenderer>().startColor = Color.red;
            nullCircle.LineToParent.GetComponent<LineRenderer>().endColor = Color.red;

        }
        if(!nullCircle.IsRed){
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

    /// <summary>
    /// Finds a NullCircle based on its position in the scene.
    /// </summary>

    public NullCircle FindNullCircleBasedOnPosition(Vector3 newPosition)
    {
        // Assuming 'root' is accessible here and correctly references the root NullCircle object
        return FindNullCircleBasedOnPosition(root.GetComponent<NullCircle>(), newPosition);
    }

    private NullCircle FindNullCircleBasedOnPosition(NullCircle nullCircle, Vector3 newPosition)
    {
        //Debug.Log("!!!!!!!!I am looking for a nullcircle at this posistion" + newPosition );
        //Debug.Log("!!!!!!!! and the current nullcircle that i am looking at has this posistion" + nullCircle.transform.position );
        if (nullCircle == null) return null;

        
        // Consider using a small threshold for comparison to handle floating-point imprecision
        float positionThreshold = 0.5f; // This threshold can be adjusted based on your needs
        if (Vector3.Distance(nullCircle.transform.position, newPosition) <= positionThreshold)
        {
            
            //Debug.Log("I have found the nullcircle that has this position" + nullCircle.transform.position);
            return nullCircle; // Found the matching NullCircle
            
        }

        // Recursively check the left child
        NullCircle leftChildResult = null;
        if (nullCircle.LeftChild != null)
        {
            leftChildResult = FindNullCircleBasedOnPosition(nullCircle.LeftChild.GetComponent<NullCircle>(), newPosition);
            if (leftChildResult != null) return leftChildResult; // If found in the left subtree, return it
        }

        // Recursively check the right child
        NullCircle rightChildResult = null;
        if (nullCircle.RightChild != null)
        {
            rightChildResult = FindNullCircleBasedOnPosition(nullCircle.RightChild.GetComponent<NullCircle>(), newPosition);
            if (rightChildResult != null) return rightChildResult; // If found in the right subtree, return it
        }

        // If not found in either subtree, return null
        return null;
    }

    /// <summary>
    /// Iterates throught a dictionary of NullCircles and shows the visual representation of the NullCircles that are active, by calling ShowNullCircle.
    /// </summary>

    public void ShowNullCircles()
    {
        foreach (KeyValuePair<int, GameObject> nullCirclePair in NullCircles)
        {
            NullCircle nullCircle = nullCirclePair.Value.GetComponent<NullCircle>();
            
            if (nullCircle != null && nullCircle.IsActive) // Checking if component is not null and IsActive is true
            {
                //nullCirclePair.Value.SetActive(true); // Activate the GameObject
                ShowNullCircle(nullCircle);
            }

        }

    }
    
    /// <summary>
    /// Iterates throught a dictionary of NullCircles and hides the visual representation of the NullCircles that are not active, by calling HideNullCircle.
    /// </summary>

    public void HideNullCircles()
    {
        foreach (KeyValuePair<int, GameObject> nullCirclePair in NullCircles)
        {
            NullCircle nullCircle = nullCirclePair.Value.GetComponent<NullCircle>();

            // If the NullCircle component is found and IsActive is false, deactivate the GameObject
            if (nullCircle != null && nullCircle.IsActive) // Checking if component is not null and IsActive is false
            {
                //nullCirclePair.Value.SetActive(false); // Deactivate the GameObject
                HideNullCircle(nullCircle);
            }
        }
    }

    /// <summary>
    /// Hides the visual representation of one NullCircle and prevents interaction with it.
    /// </summary>
    /// <param name="nullCircle"></param>
    public void HideNullCircle(NullCircle nullCircle)
    {
        if (nullCircle == null) return;

        // Disable the Image component to hide the visual representation
        Image image = nullCircle.GetComponent<Image>();
        if (image != null)
        {
            image.enabled = false;
        }

        // Disable the Button component to prevent interaction
        Button button = nullCircle.GetComponent<Button>();
        if (button != null)
        {
            button.interactable = false;
        }
    }

       
    public void ShowNullCircle(NullCircle nullCircle)
    {
        if(nullCircle == null) return;

        // Re-enable the Image component
        Image image = nullCircle.GetComponent<Image>();
        if (image != null)
        {
            image.enabled = true;
        }

        // Re-enable the Button component
        Button button = nullCircle.GetComponent<Button>();
        if (button != null)
        {
            button.interactable = true;
        }
    }

    /// <summary>
    /// Spawn lindeRenderer between one NullCircle and its parent
    /// </summary>
    public GameObject SpawnLinesToParent(GameObject nullcircle, GameObject parent)
    {
        //Debug.Log("inde i DrawLinesToChildren");
        //Debug.Log("parent pos " + parent.transform.position);
        // Create a line renderer for the connection from parent to left child
        return CreateLineRenderer(nullcircle.transform, parent.transform);
        
    }

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

    public void PrintNullCircles()
    {
        foreach (KeyValuePair<int, GameObject> nullCirclePair in NullCircles)
        {
            NullCircle nullCircle = nullCirclePair.Value.GetComponent<NullCircle>();
            Debug.Log("NullCircleIndex " + nullCircle.Index + " | isActive " + nullCircle.IsActive + " | Value " + nullCircle.Value + " Color " + nullCircle.IsRed);
            if(nullCircle.Ingredient != null)
            {
                Debug.Log("and has a ingredient: " + nullCircle.Ingredient + " attached to it with the value" + nullCircle.Ingredient.GetComponentInChildren<TextMeshProUGUI>().text.ToString());
            }
            else
            {
                Debug.Log("and has no ingredient attached to it");
            }
        }
    }
    



}
