using UnityEngine;

public class PopUpManager : MonoBehaviour
{
    [SerializeField] GameObject _potionImage;
    private LevelManager _levelManager;


    private void Start()
    {
        // Change sprite of the potion
        _levelManager = LevelManager.Instance;


        // Normal level (not minigame) - Show potion in popup
        if (!_levelManager.Levels[_levelManager.CurrentLevelIndex].IsMiniGame)
        {
            _potionImage.GetComponent<SpriteRenderer>().sprite = _levelManager.GetPotionSprite();
        }
        else
        {
            _potionImage.GetComponent<SpriteRenderer>().sprite = _levelManager.CurrentAvatarSprite;
        }
    }

}
