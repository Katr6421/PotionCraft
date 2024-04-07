using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestoryOnLoad : MonoBehaviour
{
     void Awake() {
        DontDestroyOnLoad(gameObject);  // This will prevent the GameObject this script is attached to from being destroyed when loading a new scene.
    }
}
