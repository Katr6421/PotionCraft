using UnityEngine;

public class ChooseColor : MonoBehaviour
{
    [SerializeField] private GameObject _avatar;
    [SerializeField] private GameObject _avatarSmall;
    [SerializeField] private GameObject _chooseColorBubble;
    private LevelManager _levelManager;

    void Start() {
        _levelManager = LevelManager.Instance;

    }


    public void OnMouseDown() {
        switch (gameObject.name) {
            case "Default":
                _levelManager.CurrentAvatarSprite = _levelManager.SetAvatarSprite(0);
                break;
            case "Green":
                _levelManager.CurrentAvatarSprite = _levelManager.SetAvatarSprite(1);
                break;
            case "Pink":
                _levelManager.CurrentAvatarSprite = _levelManager.SetAvatarSprite(2);
                break;
        }

        _avatar.GetComponent<SpriteRenderer>().sprite = _levelManager.CurrentAvatarSprite;
        // In minigames we don't have the hint avatar
        if (_avatarSmall != null) {
            _avatarSmall.GetComponent<SpriteRenderer>().sprite = _levelManager.CurrentAvatarSpriteSmall;
        }
        _chooseColorBubble.SetActive(false);
    }

    
}