using System;
using System.Collections;
using System.Collections.Generic;
using log4net.Core;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour, IGameManager
{
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public void Start()
    {
        //InstantiateLevels();
        SceneManager.LoadScene("StartScene");
        
    }
}
