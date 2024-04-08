using UnityEngine;

public class RotateRightClickHandler : MonoBehaviour
{
    [SerializeField] private TreeManager _treeManager;

    public void OnClickRotateRight()
    {
        _treeManager.HandleOperationButtonClick(OperationType.RotateRight);
    }
}
