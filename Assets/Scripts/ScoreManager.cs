using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TextMeshProUGUI currentScoreText; // Reference to the TextMeshProUGUI component for displaying the current score
    public TextMeshProUGUI highestScoreText; // Reference to the TextMeshProUGUI component for displaying the highest score

    private int currentScore;
    private int highestScore;

    void Awake()
    {
        // Ensure there's only one instance of ScoreManager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        LoadScore();
        currentScore = 0; // Start with a score of 0
        UpdateScoreText();
    }

    public void AddScore(int amount)
    {
        currentScore += amount;
        UpdateScoreText();
    }

    public void SubtractScore(int amount)
    {
        currentScore -= amount;
        if (currentScore < 0) currentScore = 0; // Ensure score doesn't go below 0
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        if (currentScoreText != null)
        {
            currentScoreText.text = "Current Score: " + currentScore.ToString();
        }
        if (highestScoreText != null)
        {
            highestScoreText.text = "Highest Score: " + highestScore.ToString();
        }
    }

    public void CheckAndSaveHighestScore(bool gameOver)
    {
        if (gameOver)
        {
            if (currentScore > highestScore)
            {
                highestScore = currentScore;
                Debug.Log(highestScore.ToString());
                SaveScore();
            }
            UpdateScoreText(); // Ensure UI is updated when the game is over
        }
    }


    void SaveScore()
    {
        PlayerPrefs.SetInt("HighestScore", highestScore);
        PlayerPrefs.Save();
    }

    void LoadScore()
    {
        highestScore = PlayerPrefs.GetInt("HighestScore", 0);
        UpdateScoreText(); // Ensure UI is updated when the score is loaded
    }
}