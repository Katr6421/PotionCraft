using UnityEngine;
using UnityEngine.UI;

public class NullCircle : MonoBehaviour
{
    [SerializeField] private Button _prefab; // Assign this in the inspector or find it at runtime
    private TreeVisualizationManager _treeVisualizationManager;
    public int Value { get; set; }
    public GameObject LeftChild { get; set; }
    public GameObject RightChild { get; set; }
    public GameObject Parent { get; set; }
    public bool IsActive { get; set; } = false;
    public int Index { get; set; }
    public GameObject Ingredient { get; set; }


    void Start()
    {
        _treeVisualizationManager = FindObjectOfType<TreeVisualizationManager>();

        if (_prefab != null && _treeVisualizationManager != null)
        {
            _prefab.onClick.AddListener(_treeVisualizationManager.OnClickedNullCircle);
        }
    }


}

