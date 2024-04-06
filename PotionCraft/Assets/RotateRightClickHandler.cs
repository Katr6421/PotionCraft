using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateRightClickHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnClickRotateRight(){
        // TODO: Handle if the user clicks at a wrong time

        TreeManager.instance.HandleOperationButtonClick(OperationType.RotateRight);
    }
}
