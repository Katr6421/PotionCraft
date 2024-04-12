using UnityEngine;

public class Avatar : MonoBehaviour
{
    [SerializeField] private GameObject _chooseColorBubble;
    private LevelManager _levelManager;


    void Start()
    {
        _levelManager = LevelManager.Instance;
        GetComponent<SpriteRenderer>().sprite = _levelManager.CurrentAvatarSprite;
    }

    public void OnMouseDown() {
        if (_chooseColorBubble.activeSelf) {
            _chooseColorBubble.SetActive(false);
        } else {
            _chooseColorBubble.SetActive(true);
        }
    }


}
