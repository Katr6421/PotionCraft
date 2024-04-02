using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateRightClickHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnClickRotateRight(){
        TreeManager.instance.HandleOperationButtonClick(OperationType.RotateRight);
    }
}
