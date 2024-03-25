using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NullCircle : MonoBehaviour
{

    [SerializeField] private GameObject nullCirclePrefab;
    

    
    
    // Start is called before the first frame update
    void Start(){}
    

     private void OnClickedNullCirkle()
    {
        Debug.Log("Jeg har klikket på en NullCircle");
    
        // Destroy the clicked NullCircle

        //get the Nodedata from the component
         NodeData nodeDataToMove = GetComponent<NodeData>();
        //get the Node from the NodeData
        Debug.Log("Nu flytter jeg den første node");
        nodeDataToMove.MoveTo(transform.position);
        Debug.Log("Nu sletter jeg NullCircle");
        Destroy(gameObject);



        




        // Instantiate the ingredient/node at this NullCircle's position
        
        // Optionally, set up the node with specific data
        //nodeObject.GetComponent<NodeController>().SetupNode(CurrentNodeData, CurrentNodeData.Value);

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
}
