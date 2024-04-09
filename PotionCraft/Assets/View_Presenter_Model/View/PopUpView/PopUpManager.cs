using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PopUpManager : MonoBehaviour
{
    [SerializeField] GameObject _potionImage;
    private LevelManager _levelManager;

    private void Start()
    {
        // Change sprite of the potion
        _levelManager = LevelManager.Instance;
        _potionImage.GetComponent<SpriteRenderer>().sprite = _levelManager.GetPotionSprite();
    }

}
