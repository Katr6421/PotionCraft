using UnityEngine;

public class Avatar : MonoBehaviour
{
    [SerializeField] private GameObject _chooseColorBubble;
    private LevelManager _levelManager;
    private ClickMe _clickMeBobble;


    void Start()
    {
        _levelManager = LevelManager.Instance;
        _clickMeBobble = FindObjectOfType<ClickMe>();
        GetComponent<SpriteRenderer>().sprite = _levelManager.CurrentAvatarSprite;
    }

    public void OnMouseDown() {
        if (_levelManager.CurrentLevelIndex == 5) {
            _clickMeBobble.clickCount++;
        }

        if (_chooseColorBubble.activeSelf) {
            _chooseColorBubble.SetActive(false);
        } else {
            _chooseColorBubble.SetActive(true);
        }
    }


}
