using UnityEngine;

public class ChooseColor : MonoBehaviour
{
    [SerializeField] private GameObject _avatar;
    [SerializeField] private GameObject _avatarSmall;
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
        _avatarSmall.GetComponent<SpriteRenderer>().sprite = _levelManager.CurrentAvatarSpriteSmall;
    }
}