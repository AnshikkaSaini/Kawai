using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [Header("Elements")] 
    [SerializeField] private TextMeshProUGUI gameScoreText;
    [SerializeField] private TextMeshProUGUI menuBestScoreText;

    [Header("Score")] 
    [SerializeField] 
    private float scoreMultiplier;
    
    private int currentScore;
    private int bestScore;
    
    [Header("Data")] 
    private const string bestScoreKey = "bestScoreKey";

    private void Awake()
    {
        LoadData();
        MergeManager.OnMergeProcessed += MergeProcessCallback;
        GameManager.onGameStatedChanged += GameStateChangedCallback;
    }
    
    private void OnDestroy()
    {
        MergeManager.OnMergeProcessed -= MergeProcessCallback;
        GameManager.onGameStatedChanged -= GameStateChangedCallback;
    }
    
    void Start()
    {
        UpdateScoreText();
        UpdateBestScoreText();
    }

    private void GameStateChangedCallback(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.GameOver:
                CalculateBestScore();
                break;
        }
    }
    
    private void MergeProcessCallback(FruitType fruitType, Vector2 unused)
    {
        int scoreToAdd = (int)fruitType;
        currentScore += (int)(scoreToAdd * scoreMultiplier);
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        gameScoreText.text = currentScore.ToString();
    }

    private void UpdateBestScoreText()
    {
        menuBestScoreText.text = bestScore.ToString();
    }

    public void CalculateBestScore()
    {
        if (currentScore <= bestScore)
        {
            return;
        }
        
        bestScore = currentScore;

        UpdateBestScoreText();
        SaveData();
    }

    private void LoadData()
    {
        bestScore = PlayerPrefs.GetInt(bestScoreKey);
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt(bestScoreKey, bestScore);
    }
}
