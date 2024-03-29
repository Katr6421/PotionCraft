using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private Button _prefab; // Assign this in the inspector or find it at runtime
    private TreeVisualizationManager _treeVisualizationManager;
    

    void Start()
    {
        _treeVisualizationManager = FindObjectOfType<TreeVisualizationManager>();
        
        if (_prefab != null && _treeVisualizationManager != null)
        {
            _prefab.onClick.AddListener(_treeVisualizationManager.OnClickedNullCircle);
        }
    }
}

