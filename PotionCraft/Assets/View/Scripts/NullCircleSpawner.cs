using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NullCircleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject nullCirclePrefab;
    [SerializeField] private Canvas uiCanvas; // Reference to the Canvas where the nullCircles will be parented
    public Dictionary<int, GameObject> nullCircles { get; } = new Dictionary<int, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        SpawnNullCircles();
    }

    public void SpawnNullCircles()
    {
        // Instantiate the nullCircles
        GameObject nullCircle0 = Instantiate(nullCirclePrefab, new Vector3(440, 239, 0), Quaternion.identity);
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
        nullCircles.Add(0, nullCircle0);
        nullCircles.Add(1, nullCircle1);
        nullCircles.Add(2, nullCircle2);
        nullCircles.Add(3, nullCircle3);
        nullCircles.Add(4, nullCircle4);
        nullCircles.Add(5, nullCircle5);
        nullCircles.Add(6, nullCircle6);
        nullCircles.Add(7, nullCircle7);
        nullCircles.Add(8, nullCircle8);
        nullCircles.Add(9, nullCircle9);
        nullCircles.Add(10, nullCircle10);
        nullCircles.Add(11, nullCircle11);
        nullCircles.Add(12, nullCircle12);
        nullCircles.Add(13, nullCircle13);
        nullCircles.Add(14, nullCircle14);
        nullCircles.Add(15, nullCircle15);
        nullCircles.Add(16, nullCircle16);
        nullCircles.Add(17, nullCircle17);
        nullCircles.Add(18, nullCircle18);
        nullCircles.Add(19, nullCircle19);
        nullCircles.Add(20, nullCircle20);
        nullCircles.Add(21, nullCircle21);
        nullCircles.Add(22, nullCircle22);
        nullCircles.Add(23, nullCircle23);
        nullCircles.Add(24, nullCircle24);
        nullCircles.Add(25, nullCircle25);
        nullCircles.Add(26, nullCircle26);
        nullCircles.Add(27, nullCircle27);
        nullCircles.Add(28, nullCircle28);
        nullCircles.Add(29, nullCircle29);
        nullCircles.Add(30, nullCircle30);


        // Deactivate all nullCircles
        foreach (GameObject c in nullCircles.Values)
        {
            c.SetActive(false);
        }

        // Set the parent of all nullCircles to the uiCanvas
        foreach (GameObject c in nullCircles.Values)
        {
            c.transform.SetParent(uiCanvas.transform, false);
        }

    }
}
