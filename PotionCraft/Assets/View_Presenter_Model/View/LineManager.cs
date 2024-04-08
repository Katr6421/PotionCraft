using System.Collections.Generic;
using UnityEngine;

public class LineManager : MonoBehaviour
{
    [SerializeField] private GameObject _line;


    /*********************************************
    1. After insertion of ingredient - create a line between ingredient and nullcircles (left and right)
    2. Check if the nullCircle has a parent, if yes, update the line from the parent to point to the new ingredient
    3. The line gets updated through the nullCircle
    *********************************************/

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

}