using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject gameOverPanel;

    private void Awake()
    {
        GameManager.onGameStatedChanged += GameStateChangedCallback;
    }

    private void OnDestroy()
    {
        GameManager.onGameStatedChanged -= GameStateChangedCallback;
    }

    private void GameStateChangedCallback(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.Menu: 
                SetMenu();
                break;
            case GameState.Game:
                SetGame();
                break;
            case GameState.GameOver: 
                SetGameOver();
                break;
        }
    }

    private void SetMenu()
    {
        menuPanel.SetActive(true);
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }
    private void SetGame()
    {
        menuPanel.SetActive(false);
        gamePanel.SetActive(true);
        gameOverPanel.SetActive(false);
        
    }
    private void SetGameOver()
    {
        menuPanel.SetActive(false);
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(true);
    }

    
}

