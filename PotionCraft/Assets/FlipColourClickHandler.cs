using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipColourClickHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnClickFlipColour(){
        TreeManager.instance.HandleOperationButtonClick(OperationType.FlipColors);
    }
}
