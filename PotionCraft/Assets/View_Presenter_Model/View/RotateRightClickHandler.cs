using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateRightClickHandler : MonoBehaviour
{
    public void OnClickRotateRight(){
        TreeManager.instance.HandleOperationButtonClick(OperationType.RotateRight);
    }
}
