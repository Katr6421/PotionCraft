using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NullCircleManager : MonoBehaviour
{
    [SerializeField] private GameObject _nullCirclePrefab;
    [SerializeField] private Canvas _uiCanvas; // Reference to the Canvas where the nullCircles will be parented
    [SerializeField] private LineManager _lineManager;
    [SerializeField] private TreeManager _treeManager;
    public Dictionary<int, GameObject> NullCircles { get; private set; } = new Dictionary<int, GameObject>();
    public GameObject Root { get; set; }


    void Start()
    {
        SpawnNullCircles();
    }

    public void SpawnNullCircles()
    {
        // Instantiate the nullCircles

        GameObject nullCircle0 = Instantiate(_nullCirclePrefab, new Vector3(440, 270, 0), Quaternion.identity);
        Root = nullCircle0;

        GameObject nullCircle1 = Instantiate(_nullCirclePrefab, new Vector3(195, 147, 0), Quaternion.identity);
        GameObject nullCircle2 = Instantiate(_nullCirclePrefab, new Vector3(685, 147, 0), Quaternion.identity);

        GameObject nullCircle3 = Instantiate(_nullCirclePrefab, new Vector3(72, -10, 0), Quaternion.identity);
        GameObject nullCircle4 = Instantiate(_nullCirclePrefab, new Vector3(317, -10, 0), Quaternion.identity);

        GameObject nullCircle5 = Instantiate(_nullCirclePrefab, new Vector3(562, -10, 0), Quaternion.identity);
        GameObject nullCircle6 = Instantiate(_nullCirclePrefab, new Vector3(807, -10, 0), Quaternion.identity);

        // Bottom row
        GameObject nullCircle7 = Instantiate(_nullCirclePrefab, new Vector3(11, -170, 0), Quaternion.identity);
        GameObject nullCircle8 = Instantiate(_nullCirclePrefab, new Vector3(133, -170, 0), Quaternion.identity);

        GameObject nullCircle9 = Instantiate(_nullCirclePrefab, new Vector3(256, -170, 0), Quaternion.identity);
        GameObject nullCircle10 = Instantiate(_nullCirclePrefab, new Vector3(378, -170, 0), Quaternion.identity);

        GameObject nullCircle11 = Instantiate(_nullCirclePrefab, new Vector3(501, -170, 0), Quaternion.identity);
        GameObject nullCircle12 = Instantiate(_nullCirclePrefab, new Vector3(623, -170, 0), Quaternion.identity);

        GameObject nullCircle13 = Instantiate(_nullCirclePrefab, new Vector3(746, -170, 0), Quaternion.identity);
        GameObject nullCircle14 = Instantiate(_nullCirclePrefab, new Vector3(868, -170, 0), Quaternion.identity);

        GameObject nullCircle15 = Instantiate(_nullCirclePrefab, new Vector3(-19, -292, 0), Quaternion.identity);
        GameObject nullCircle16 = Instantiate(_nullCirclePrefab, new Vector3(41, -292, 0), Quaternion.identity);

        GameObject nullCircle17 = Instantiate(_nullCirclePrefab, new Vector3(102, -292, 0), Quaternion.identity);
        GameObject nullCircle18 = Instantiate(_nullCirclePrefab, new Vector3(164, -292, 0), Quaternion.identity);

        GameObject nullCircle19 = Instantiate(_nullCirclePrefab, new Vector3(225, -292, 0), Quaternion.identity);
        GameObject nullCircle20 = Instantiate(_nullCirclePrefab, new Vector3(287, -292, 0), Quaternion.identity);

        GameObject nullCircle21 = Instantiate(_nullCirclePrefab, new Vector3(348, -292, 0), Quaternion.identity);
        GameObject nullCircle22 = Instantiate(_nullCirclePrefab, new Vector3(410, -292, 0), Quaternion.identity);

        GameObject nullCircle23 = Instantiate(_nullCirclePrefab, new Vector3(471, -292, 0), Quaternion.identity);
        GameObject nullCircle24 = Instantiate(_nullCirclePrefab, new Vector3(533, -292, 0), Quaternion.identity);

        GameObject nullCircle25 = Instantiate(_nullCirclePrefab, new Vector3(594, -292, 0), Quaternion.identity);
        GameObject nullCircle26 = Instantiate(_nullCirclePrefab, new Vector3(656, -292, 0), Quaternion.identity);

        GameObject nullCircle27 = Instantiate(_nullCirclePrefab, new Vector3(717, -292, 0), Quaternion.identity);
        GameObject nullCircle28 = Instantiate(_nullCirclePrefab, new Vector3(779, -292, 0), Quaternion.identity);

        GameObject nullCircle29 = Instantiate(_nullCirclePrefab, new Vector3(840, -292, 0), Quaternion.identity);
        GameObject nullCircle30 = Instantiate(_nullCirclePrefab, new Vector3(902, -292, 0), Quaternion.identity);

        // Hidden nullCircles in bottom - Should never be shown!! To avoid nullPointerExceptions
        // x = +/- 20 i forhold til parent x
        // y = -30 i forhold til parent y
        GameObject nullCircle31 = Instantiate(_nullCirclePrefab, new Vector3(1, -350, 0), Quaternion.identity);
        GameObject nullCircle32 = Instantiate(_nullCirclePrefab, new Vector3(-39, -350, 0), Quaternion.identity);
        GameObject nullCircle33 = Instantiate(_nullCirclePrefab, new Vector3(61, -350, 0), Quaternion.identity);
        GameObject nullCircle34 = Instantiate(_nullCirclePrefab, new Vector3(101, -350, 0), Quaternion.identity);
        GameObject nullCircle35 = Instantiate(_nullCirclePrefab, new Vector3(82, -350, 0), Quaternion.identity);
        GameObject nullCircle36 = Instantiate(_nullCirclePrefab, new Vector3(122, -350, 0), Quaternion.identity);
        GameObject nullCircle37 = Instantiate(_nullCirclePrefab, new Vector3(144, -350, 0), Quaternion.identity);
        GameObject nullCircle38 = Instantiate(_nullCirclePrefab, new Vector3(184, -350, 0), Quaternion.identity);
        GameObject nullCircle39 = Instantiate(_nullCirclePrefab, new Vector3(205, -350, 0), Quaternion.identity);
        GameObject nullCircle40 = Instantiate(_nullCirclePrefab, new Vector3(245, -350, 0), Quaternion.identity);
        GameObject nullCircle41 = Instantiate(_nullCirclePrefab, new Vector3(267, -350, 0), Quaternion.identity);
        GameObject nullCircle42 = Instantiate(_nullCirclePrefab, new Vector3(307, -350, 0), Quaternion.identity);
        GameObject nullCircle43 = Instantiate(_nullCirclePrefab, new Vector3(328, -350, 0), Quaternion.identity);
        GameObject nullCircle44 = Instantiate(_nullCirclePrefab, new Vector3(368, -350, 0), Quaternion.identity);
        GameObject nullCircle45 = Instantiate(_nullCirclePrefab, new Vector3(390, -350, 0), Quaternion.identity);
        GameObject nullCircle46 = Instantiate(_nullCirclePrefab, new Vector3(430, -350, 0), Quaternion.identity);
        GameObject nullCircle47 = Instantiate(_nullCirclePrefab, new Vector3(451, -350, 0), Quaternion.identity);
        GameObject nullCircle48 = Instantiate(_nullCirclePrefab, new Vector3(491, -350, 0), Quaternion.identity);
        GameObject nullCircle49 = Instantiate(_nullCirclePrefab, new Vector3(513, -350, 0), Quaternion.identity);
        GameObject nullCircle50 = Instantiate(_nullCirclePrefab, new Vector3(553, -350, 0), Quaternion.identity);
        GameObject nullCircle51 = Instantiate(_nullCirclePrefab, new Vector3(574, -350, 0), Quaternion.identity);
        GameObject nullCircle52 = Instantiate(_nullCirclePrefab, new Vector3(614, -350, 0), Quaternion.identity);
        GameObject nullCircle53 = Instantiate(_nullCirclePrefab, new Vector3(636, -350, 0), Quaternion.identity);
        GameObject nullCircle54 = Instantiate(_nullCirclePrefab, new Vector3(676, -350, 0), Quaternion.identity);
        GameObject nullCircle55 = Instantiate(_nullCirclePrefab, new Vector3(697, -350, 0), Quaternion.identity);
        GameObject nullCircle56 = Instantiate(_nullCirclePrefab, new Vector3(737, -350, 0), Quaternion.identity);
        GameObject nullCircle57 = Instantiate(_nullCirclePrefab, new Vector3(759, -350, 0), Quaternion.identity);
        GameObject nullCircle58 = Instantiate(_nullCirclePrefab, new Vector3(799, -350, 0), Quaternion.identity);
        GameObject nullCircle59 = Instantiate(_nullCirclePrefab, new Vector3(820, -350, 0), Quaternion.identity);
        GameObject nullCircle60 = Instantiate(_nullCirclePrefab, new Vector3(860, -350, 0), Quaternion.identity);
        GameObject nullCircle61 = Instantiate(_nullCirclePrefab, new Vector3(882, -350, 0), Quaternion.identity);
        GameObject nullCircle62 = Instantiate(_nullCirclePrefab, new Vector3(922, -350, 0), Quaternion.identity);


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
        // Hidden.
        nullCircle15.GetComponent<NullCircle>().LeftChild = nullCircle31;
        nullCircle15.GetComponent<NullCircle>().RightChild = nullCircle32;
        nullCircle16.GetComponent<NullCircle>().LeftChild = nullCircle33;
        nullCircle16.GetComponent<NullCircle>().RightChild = nullCircle34;
        nullCircle17.GetComponent<NullCircle>().LeftChild = nullCircle35;
        nullCircle17.GetComponent<NullCircle>().RightChild = nullCircle36;
        nullCircle18.GetComponent<NullCircle>().LeftChild = nullCircle37;
        nullCircle18.GetComponent<NullCircle>().RightChild = nullCircle38;
        nullCircle19.GetComponent<NullCircle>().LeftChild = nullCircle39;
        nullCircle19.GetComponent<NullCircle>().RightChild = nullCircle40;
        nullCircle20.GetComponent<NullCircle>().LeftChild = nullCircle41;
        nullCircle20.GetComponent<NullCircle>().RightChild = nullCircle42;
        nullCircle21.GetComponent<NullCircle>().LeftChild = nullCircle43;
        nullCircle21.GetComponent<NullCircle>().RightChild = nullCircle44;
        nullCircle22.GetComponent<NullCircle>().LeftChild = nullCircle45;
        nullCircle22.GetComponent<NullCircle>().RightChild = nullCircle46;
        nullCircle23.GetComponent<NullCircle>().LeftChild = nullCircle47;
        nullCircle23.GetComponent<NullCircle>().RightChild = nullCircle48;
        nullCircle24.GetComponent<NullCircle>().LeftChild = nullCircle49;
        nullCircle24.GetComponent<NullCircle>().RightChild = nullCircle50;
        nullCircle25.GetComponent<NullCircle>().LeftChild = nullCircle51;
        nullCircle25.GetComponent<NullCircle>().RightChild = nullCircle52;
        nullCircle26.GetComponent<NullCircle>().LeftChild = nullCircle53;
        nullCircle26.GetComponent<NullCircle>().RightChild = nullCircle54;
        nullCircle27.GetComponent<NullCircle>().LeftChild = nullCircle55;
        nullCircle27.GetComponent<NullCircle>().RightChild = nullCircle56;
        nullCircle28.GetComponent<NullCircle>().LeftChild = nullCircle57;
        nullCircle28.GetComponent<NullCircle>().RightChild = nullCircle58;
        nullCircle29.GetComponent<NullCircle>().LeftChild = nullCircle59;
        nullCircle29.GetComponent<NullCircle>().RightChild = nullCircle60;
        nullCircle30.GetComponent<NullCircle>().LeftChild = nullCircle61;
        nullCircle30.GetComponent<NullCircle>().RightChild = nullCircle62;

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
        // Hidden
        nullCircle31.GetComponent<NullCircle>().Parent = nullCircle15;
        nullCircle32.GetComponent<NullCircle>().Parent = nullCircle15;
        nullCircle33.GetComponent<NullCircle>().Parent = nullCircle16;
        nullCircle34.GetComponent<NullCircle>().Parent = nullCircle16;
        nullCircle35.GetComponent<NullCircle>().Parent = nullCircle17;
        nullCircle36.GetComponent<NullCircle>().Parent = nullCircle17;
        nullCircle37.GetComponent<NullCircle>().Parent = nullCircle18;
        nullCircle38.GetComponent<NullCircle>().Parent = nullCircle18;
        nullCircle39.GetComponent<NullCircle>().Parent = nullCircle19;
        nullCircle40.GetComponent<NullCircle>().Parent = nullCircle19;
        nullCircle41.GetComponent<NullCircle>().Parent = nullCircle20;
        nullCircle42.GetComponent<NullCircle>().Parent = nullCircle20;
        nullCircle43.GetComponent<NullCircle>().Parent = nullCircle21;
        nullCircle44.GetComponent<NullCircle>().Parent = nullCircle21;
        nullCircle45.GetComponent<NullCircle>().Parent = nullCircle22;
        nullCircle46.GetComponent<NullCircle>().Parent = nullCircle22;
        nullCircle47.GetComponent<NullCircle>().Parent = nullCircle23;
        nullCircle48.GetComponent<NullCircle>().Parent = nullCircle23;
        nullCircle49.GetComponent<NullCircle>().Parent = nullCircle24;
        nullCircle50.GetComponent<NullCircle>().Parent = nullCircle24;
        nullCircle51.GetComponent<NullCircle>().Parent = nullCircle25;
        nullCircle52.GetComponent<NullCircle>().Parent = nullCircle25;
        nullCircle53.GetComponent<NullCircle>().Parent = nullCircle26;
        nullCircle54.GetComponent<NullCircle>().Parent = nullCircle26;
        nullCircle55.GetComponent<NullCircle>().Parent = nullCircle27;
        nullCircle56.GetComponent<NullCircle>().Parent = nullCircle27;
        nullCircle57.GetComponent<NullCircle>().Parent = nullCircle28;
        nullCircle58.GetComponent<NullCircle>().Parent = nullCircle28;
        nullCircle59.GetComponent<NullCircle>().Parent = nullCircle29;
        nullCircle60.GetComponent<NullCircle>().Parent = nullCircle29;
        nullCircle61.GetComponent<NullCircle>().Parent = nullCircle30;
        nullCircle62.GetComponent<NullCircle>().Parent = nullCircle30;

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
        // Hidden
        NullCircles.Add(30, nullCircle30);
        NullCircles.Add(31, nullCircle31);
        NullCircles.Add(32, nullCircle32);
        NullCircles.Add(33, nullCircle33);
        NullCircles.Add(34, nullCircle34);
        NullCircles.Add(35, nullCircle35);
        NullCircles.Add(36, nullCircle36);
        NullCircles.Add(37, nullCircle37);
        NullCircles.Add(38, nullCircle38);
        NullCircles.Add(39, nullCircle39);
        NullCircles.Add(40, nullCircle40);
        NullCircles.Add(41, nullCircle41);
        NullCircles.Add(42, nullCircle42);
        NullCircles.Add(43, nullCircle43);
        NullCircles.Add(44, nullCircle44);
        NullCircles.Add(45, nullCircle45);
        NullCircles.Add(46, nullCircle46);
        NullCircles.Add(47, nullCircle47);
        NullCircles.Add(48, nullCircle48);
        NullCircles.Add(49, nullCircle49);
        NullCircles.Add(50, nullCircle50);
        NullCircles.Add(51, nullCircle51);
        NullCircles.Add(52, nullCircle52);
        NullCircles.Add(53, nullCircle53);
        NullCircles.Add(54, nullCircle54);
        NullCircles.Add(55, nullCircle55);
        NullCircles.Add(56, nullCircle56);
        NullCircles.Add(57, nullCircle57);
        NullCircles.Add(58, nullCircle58);
        NullCircles.Add(59, nullCircle59);
        NullCircles.Add(60, nullCircle60);
        NullCircles.Add(61, nullCircle61);
        NullCircles.Add(62, nullCircle62);

        for (int i = 0; i < NullCircles.Count; i++)
        {
            NullCircles[i].GetComponent<NullCircle>().Index = i;
        }

        /*********************************************
        Hide all nullCircles
        Set the parent of all nullCircles to the uiCanvas
        *********************************************/
        foreach (GameObject c in NullCircles.Values)
        {
            HideNullCircle(c.GetComponent<NullCircle>());
            c.transform.SetParent(_uiCanvas.transform, false);
        }

        /*********************************************
        Activate the root nullCircle
        *********************************************/
        ShowNullCircle(nullCircle0.GetComponent<NullCircle>());
    }


    public void UpdateNullCircleWithIngredient(Vector3 newPosition, NullCircle nullCircle)
    {
        NullCircle foundNullCircle = FindNullCircleBasedOnPosition(newPosition);
        if (foundNullCircle != null)
        {
            /*********************************************
            Update the found null circle with its new ingredient
            *********************************************/
            foundNullCircle.Ingredient = nullCircle.Ingredient;

            /*********************************************
            Update the value of the found null circle with the value of the ingredient. 
            *********************************************/
            foundNullCircle.Value = int.Parse(nullCircle.Ingredient.GetComponentInChildren<TextMeshProUGUI>().text);


            /*********************************************
            Update the color based on the nodes in our tree.
            We look up in our RedBlaackBST to get the node that the ingredients value corresponds to.
            Then we return the node's color
            *********************************************/
            if (foundNullCircle.Ingredient.GetComponent<Ingredient>().LineToParent != null) {
                bool isRed = _treeManager.GetColor(foundNullCircle.Value);
                foundNullCircle.Ingredient.GetComponent<Ingredient>().LineToParent.GetComponent<Line>().IsRed = isRed;
                //Update the color of the line to the parent
                if (isRed) {
                    _lineManager.UpdateLineColor(foundNullCircle, isRed);
                }
            }

            /*********************************************
            Only set null circle to null if the child is also null.
            Then we know that there will not be any more ingredients in the subtree.
            *********************************************/
            if (nullCircle.LeftChild.GetComponent<NullCircle>().Ingredient == null && nullCircle.RightChild.GetComponent<NullCircle>().Ingredient == null)
            {
                nullCircle.Ingredient = null; // the null circle where the ingredient was earlier now has no ingredient
                nullCircle.Value = 0; // the value of the null circle where the ingredient was earlier now has no value
            }
        }
        else
        {
            //Debug.Log("NullCircle not found at position: " + newPosition);
        }
    }


    /*********************************************
    Recursively checks if there is a ingredient placed onto the nullcircle, if not, the nullcircle is active.
    *********************************************/
    public void ShowAllChildrenNullCircles()
    {

        ShowAllChildrenNullCircles(Root.GetComponent<NullCircle>());
    }

    private void ShowAllChildrenNullCircles(NullCircle nullCircle)
    {
        if (nullCircle == null) return;

        /*********************************************
        If the current nullCircle has no value, it should be active,
        but we don't need to check its children because they would be beyond the current "border" of values.
        *********************************************/
        if (nullCircle.Ingredient == null)
        {
            ShowNullCircle(nullCircle);
        }
        else
        {
            HideNullCircle(nullCircle);

            /*********************************************
            If this nullCircle has a value, its children might need to be activated,
            so we recursively check them.
            *********************************************/
            NullCircle leftChild = nullCircle.LeftChild?.GetComponent<NullCircle>();
            NullCircle rightChild = nullCircle.RightChild?.GetComponent<NullCircle>();

            ShowAllChildrenNullCircles(leftChild);
            ShowAllChildrenNullCircles(rightChild);
        }
    }

    public void HideAllNullCircles()
    {
        HideAllNullCircles(Root.GetComponent<NullCircle>());
    }

    private void HideAllNullCircles(NullCircle nullCircle)
    {
        if (nullCircle == null) return;

        /*********************************************
        If the current nullCircle has no value, it should be active,
        but we don't need to check its children because they would be beyond the current "border" of values.
        *********************************************/
        if (nullCircle.Ingredient == null)
        {
            HideNullCircle(nullCircle);
        }
        else
        {

            HideNullCircle(nullCircle);

            /*********************************************
            If this nullCircle has a value, its children might need to be activated,
            so we recursively check them.
            *********************************************/
            NullCircle leftChild = nullCircle.LeftChild?.GetComponent<NullCircle>();
            NullCircle rightChild = nullCircle.RightChild?.GetComponent<NullCircle>();

            HideAllNullCircles(leftChild);
            HideAllNullCircles(rightChild);
        }
    }


    public void setNullCircleToDefault(NullCircle nullCircle)
    {
        /*********************************************
        Reset properties of the current node
        *********************************************/
        nullCircle.Value = 0;
        nullCircle.IsActive = false;
        nullCircle.Ingredient = null;


        /*********************************************
        If LeftChild exists, call ResetProperties on it
        *********************************************/
        if (nullCircle.LeftChild.GetComponent<NullCircle>().Ingredient != null)
        {
            var leftNullCircle = nullCircle.LeftChild.GetComponent<NullCircle>();
            setNullCircleToDefault(leftNullCircle);
        }

        /*********************************************
        If RightChild exists, call ResetProperties on it
        *********************************************/
        if (nullCircle.RightChild.GetComponent<NullCircle>().Ingredient != null)
        {
            var rightNullCircle = nullCircle.RightChild.GetComponent<NullCircle>();
            setNullCircleToDefault(rightNullCircle);
        }

        return;
    }

    public void DeactivateAllNullCirclesInSubtree(NullCircle nullCircle)
    {
        if (nullCircle == null) return;

        nullCircle.IsActive = false;
        HideNullCircle(nullCircle);

        /*********************************************
        If this nullCircle has a value, its children might need to be activated,
        so we recursively check them.
        *********************************************/
        NullCircle leftChild = nullCircle.LeftChild?.GetComponent<NullCircle>();
        NullCircle rightChild = nullCircle.RightChild?.GetComponent<NullCircle>();

        DeactivateAllNullCirclesInSubtree(leftChild);
        DeactivateAllNullCirclesInSubtree(rightChild);
    }

    /*********************************************
    Finds a NullCircle based on its position in the scene.
    *********************************************/
    public NullCircle FindNullCircleBasedOnPosition(Vector3 newPosition)
    {
        /*********************************************
        Assuming 'root' is accessible here and correctly references the root NullCircle object
        *********************************************/
        return FindNullCircleBasedOnPosition(Root.GetComponent<NullCircle>(), newPosition);
    }

    private NullCircle FindNullCircleBasedOnPosition(NullCircle nullCircle, Vector3 newPosition)
    {
        if (nullCircle == null) return null;

        /*********************************************
        Because we are using floating point numbers, we need to use a threshold to compare positions.
        *********************************************/
        float positionThreshold = 0.5f;
        if (Vector3.Distance(nullCircle.transform.position, newPosition) <= positionThreshold)
        {
            return nullCircle;
        }

        /*********************************************
        Recursively check the left child
        *********************************************/
        NullCircle leftChildResult = null;
        if (nullCircle.LeftChild != null)
        {
            leftChildResult = FindNullCircleBasedOnPosition(nullCircle.LeftChild.GetComponent<NullCircle>(), newPosition);
            if (leftChildResult != null) return leftChildResult; // If found in the left subtree, return it
        }

        /*********************************************
        Recursively check the right child
        *********************************************/
        NullCircle rightChildResult = null;
        if (nullCircle.RightChild != null)
        {
            rightChildResult = FindNullCircleBasedOnPosition(nullCircle.RightChild.GetComponent<NullCircle>(), newPosition);
            if (rightChildResult != null) return rightChildResult; // If found in the right subtree, return it
        }

        /*********************************************
        If not found in either subtree, return null
        *********************************************/
        return null;
    }


    public NullCircle CopyNullCircleSubtree(NullCircle root)
    {
        if (root == null) return null;

        /*********************************************
        Instantiate a new NullCircle GameObject as a copy of the root
        *********************************************/
        GameObject copyObject = Instantiate(root.gameObject);
        NullCircle copyNullCircle = copyObject.GetComponent<NullCircle>();

        /*********************************************
        Recursively copy the left and right subtrees
        *********************************************/
        copyNullCircle.LeftChild = root.LeftChild == null ? null : CopyNullCircleSubtree(root.LeftChild.GetComponent<NullCircle>()).gameObject;
        copyNullCircle.RightChild = root.RightChild == null ? null : CopyNullCircleSubtree(root.RightChild.GetComponent<NullCircle>()).gameObject;

        /*********************************************
        If this copy has children, set their parent to this new copy
        *********************************************/
        if (copyNullCircle.LeftChild != null)
        {
            copyNullCircle.LeftChild.GetComponent<NullCircle>().Parent = copyNullCircle.gameObject;
        }
        if (copyNullCircle.RightChild != null)
        {
            copyNullCircle.RightChild.GetComponent<NullCircle>().Parent = copyNullCircle.gameObject;
        }

        /*********************************************
        Copy the value type fields
        *********************************************/
        copyNullCircle.Value = root.Value;
        copyNullCircle.IsActive = root.IsActive;
        copyNullCircle.Index = root.Index;
        copyNullCircle.Ingredient = root.Ingredient;

        return copyNullCircle;
    }


    // Call this method with the root of your tree and an empty list to fill with ingredients
    public List<GameObject> CollectIngredients(NullCircle nullCircle, List<GameObject> ingredients)
    {
        if (nullCircle == null)
        {
            return ingredients; // Base case: root is null, do nothing
        }
        // Process the current node (NullCircle)
        if (nullCircle.Ingredient != null && !ingredients.Contains(nullCircle.Ingredient))
        {
            ingredients.Add(nullCircle.Ingredient);
        }

        // Convert GameObject to NullCircle for LeftChild and RightChild, if they exist, before recursive calls
        NullCircle leftChild = nullCircle.LeftChild ? nullCircle.LeftChild.GetComponent<NullCircle>() : null;
        NullCircle rightChild = nullCircle.RightChild ? nullCircle.RightChild.GetComponent<NullCircle>() : null;

        // Recursive case: Traverse the left and right children
        CollectIngredients(leftChild, ingredients);
        CollectIngredients(rightChild, ingredients);

        return ingredients;
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
        MakeNullCircleNonInteractable(nullCircle);
    }

    public void ShowNullCircle(NullCircle nullCircle)
    {
        if (nullCircle == null) return;

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

    // Used to make the null circles non-interactable but NOT hide it
    public void MakeAllNullCirclesNonInteractable()
    {
        for (int i = 0; i < NullCircles.Count; i++)
        {
            MakeNullCircleNonInteractable(NullCircles[i].GetComponent<NullCircle>());
        }
    }

    // Used to make a null circles non-interactable but NOT hide it
    public void MakeNullCircleNonInteractable(NullCircle nullCircle)
    {
        Button nullCircleButton = nullCircle.GetComponent<Button>();
        nullCircleButton.interactable = false;

        // But keep visual appearance
        var colors = nullCircleButton.colors;
        colors.disabledColor = colors.normalColor;
        nullCircleButton.colors = colors;
    }

    public void PrintNullCircles()
    {

        Debug.Log("**********Printing all NullCircles**********");
        foreach (KeyValuePair<int, GameObject> nullCirclePair in NullCircles)
        {
            NullCircle nullCircle = nullCirclePair.Value.GetComponent<NullCircle>();
            Debug.Log("NullCircleIndex " + nullCircle.Index + " | isActive " + nullCircle.IsActive + " | Value " + nullCircle.Value);
            if (nullCircle.Ingredient != null)
            {
                Debug.Log("and has a ingredient: " + nullCircle.Ingredient + " attached to it with the value" + nullCircle.Ingredient.GetComponentInChildren<TextMeshProUGUI>().text.ToString());
            }
            else
            {
                Debug.Log("and has no ingredient attached to it");
            }
        }
    }


    public void DestroyNullCircleAndAllDescendants(GameObject nullCircle)
    {
        if (nullCircle == null) return;

        NullCircle nc = nullCircle.GetComponent<NullCircle>();
        if (nc.LeftChild != null)
        {
            DestroyNullCircleAndAllDescendants(nc.LeftChild);
        }
        if (nc.RightChild != null)
        {
            DestroyNullCircleAndAllDescendants(nc.RightChild);
        }

        // Destroy the GameObject
        Destroy(nullCircle);
    }


}

/*************************************** OLD METHOD IF WE FUCK UP************************************************/
/*  /// <summary>
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
    }*/
