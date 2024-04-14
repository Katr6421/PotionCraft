using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickMe : MonoBehaviour
{
    [SerializeField] private GameObject _clickMeBobble;
    public int clickCount { get; set; } = 0;
    
    
    void Update()
    {
        if (clickCount >= 1) {
            _clickMeBobble.SetActive(false);
        }
    }
}
