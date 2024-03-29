using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NullCircle : MonoBehaviour
{
    [SerializeField] private Button _prefab; // Assign this in the inspector or find it at runtime
    private TreeVisualizationManager _treeVisualizationManager;
    
    public GameObject LeftChild { get; set; }
    public GameObject RightChild { get; set; }
    public bool IsActive { get; set; } = false;
    
    public Vector3 Position
    {
        get => transform.position;
        set => transform.position = value;
    }
    
    void Start()
    {
        _treeVisualizationManager = FindObjectOfType<TreeVisualizationManager>();
        
        if (_prefab != null && _treeVisualizationManager != null)
        {
            _prefab.onClick.AddListener(_treeVisualizationManager.OnClickedNullCircle);
        }
    }
    
    
}

