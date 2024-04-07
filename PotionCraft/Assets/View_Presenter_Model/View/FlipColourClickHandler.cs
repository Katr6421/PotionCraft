using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipColourClickHandler : MonoBehaviour
{
    [SerializeField] private TreeManager _treeManager;
    public void OnClickFlipColour()
    {
        _treeManager.HandleOperationButtonClick(OperationType.FlipColors);
    }
}
