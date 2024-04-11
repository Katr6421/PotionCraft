using UnityEngine;

public class Avatar : MonoBehaviour
{
    private LevelManager _levelManager;


    void Start()
    {
        _levelManager = LevelManager.Instance;
        GetComponent<SpriteRenderer>().sprite = _levelManager.CurrentAvatarSprite;
    }


}
