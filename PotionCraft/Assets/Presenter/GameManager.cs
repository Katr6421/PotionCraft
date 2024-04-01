using System;
using System.Collections;
using System.Collections.Generic;
using log4net.Core;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour, IGameManager
{
    public static GameManager instance { get; private set; }

    private void Awake()
    {
        // Singleton pattern
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public void Start()
    {
        //InstantiateLevels();
        SceneManager.LoadScene("StartScene");
        
    }

    public Node GetCurrentIngredient()
    {
        throw new NotImplementedException();
    }

    public void PlaceIngredient()
    {
    }

    public void PlacePlacementCircles()
    {
    }
}
