
using UnityEngine;

public class PointManager : MonoBehaviour
{
    public static PointManager Instance { get; private set; }
    public int CurrentPoints { get; set; }
    
    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddPoints(int points)
    {
        CurrentPoints += points;
    }

}

