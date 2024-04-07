using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateLeftClickHandler : MonoBehaviour
{
    [SerializeField] private TreeManager _treeManager;

    public void OnClickRotateLeft(){
        _treeManager.HandleOperationButtonClick(OperationType.RotateLeft);
    }
}
