using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateLeftClickHandler : MonoBehaviour
{
    [SerializeField] private TreeManager _treeManager;

    // Start is called before the first frame update
    public void OnClickRotateLeft(){
        _treeManager.HandleOperationButtonClick(OperationType.RotateLeft);
    }
}
