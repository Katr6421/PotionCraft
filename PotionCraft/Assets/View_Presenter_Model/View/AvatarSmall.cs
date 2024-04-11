using UnityEngine;

public class AvatarSmall : MonoBehaviour
{
    private LevelManager _levelManager;


    void Start()
    {
        _levelManager = LevelManager.Instance;
        GetComponent<SpriteRenderer>().sprite = _levelManager.CurrentAvatarSpriteSmall;
    }

}
