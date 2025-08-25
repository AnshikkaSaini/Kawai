using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

public class ScoreManager : MonoBehaviour
{
    [Header("Elements")] 
    [SerializeField] private TextMeshProUGUI gameScoreText;
    [SerializeField] private TextMeshProUGUI menuBestScoreText;

    [Header("Score")] 
    [SerializeField] private float scoreMultiplier;
    private int _score;
    private int _bestScore;
    [Header("Data")] private const string bestScoreKey = "bestScoreKey";

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
        UpdateBestScoretext();
      

    }

    // Update is called once per frame
    private void GameStateChangedCallback(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.GameOver:
            case GameState.Menu:
                CalculateBestScore();
                break;
        }
    }

    
    private void MergeProcessCallback(FruitType fruitType, Vector2 unused)
    {
        int scoreToAdd = (int)fruitType;
        _score += (int)(scoreToAdd * scoreMultiplier);
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        gameScoreText.text = _score.ToString();
    }

    private void UpdateBestScoretext()
    {
        menuBestScoreText.text = _bestScore.ToString();
    }
    

    private void CalculateBestScore()
    {
        if (_score > _bestScore)
            _bestScore = _score;
        UpdateBestScoretext();
        SaveData();
    }

    private void LoadData()
    {
        _bestScore = PlayerPrefs.GetInt(bestScoreKey);
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt(bestScoreKey, _bestScore);
    }

}
