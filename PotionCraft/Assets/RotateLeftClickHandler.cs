using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateLeftClickHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnClickRotateLeft(){
        // TODO: Handle if the user clicks at a wrong time

        TreeManager.instance.HandleOperationButtonClick(OperationType.RotateLeft);
    }
}
