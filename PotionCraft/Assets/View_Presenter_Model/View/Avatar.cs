using UnityEngine;

public class Avatar : MonoBehaviour
{
    private LevelManager _levelManager;

    
    void Start()
    {
        _levelManager = LevelManager.Instance;

        if (_levelManager.CurrentLevelIndex > 4)
        {
            GetComponent<SpriteRenderer>().sprite = _levelManager.GetAvatarSprite(1);
        }
        if (_levelManager.CurrentLevelIndex > 7)
        {
            GetComponent<SpriteRenderer>().sprite = _levelManager.GetAvatarSprite(2);
        }
    }

    
}
