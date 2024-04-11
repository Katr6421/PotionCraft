using UnityEngine;

public class PointManager : MonoBehaviour
{
    public static PointManager Instance { get; private set; }
    private int currentPoints;
    public int CurrentPoints
    {
        get => currentPoints;
        private set
        {
            currentPoints = value;
            SavePoints();
        }
    }

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadPoints();
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

    private void SavePoints()
    {
        PlayerPrefs.SetInt("CurrentPoints", CurrentPoints);
        PlayerPrefs.Save();
    }

    private void LoadPoints()
    {
        CurrentPoints = PlayerPrefs.GetInt("CurrentPoints", 0);
    }

    public void ResetPoints()
    {
        CurrentPoints = 0;
        PlayerPrefs.DeleteKey("CurrentPoints");
    }
}


