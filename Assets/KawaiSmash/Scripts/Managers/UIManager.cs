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
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject shopPanel;

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
        settingsPanel.SetActive(false);
        Debug.LogError("Menu State");
    }
    private void SetGame()
    {
        menuPanel.SetActive(false);
        gamePanel.SetActive(true);
        gameOverPanel.SetActive(false);
        Debug.LogError("Game State");
        
    }
    private void SetGameOver()
    {
        menuPanel.SetActive(false);
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(true);
        Debug.LogError("Game Over State");
    }

    public void SettingsButtonCallback()
    {
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
     settingsPanel.SetActive(false);   
    }

    public void GetShopButtonCallback() => shopPanel.SetActive(true);
    public void GetCloseShopPanel() => shopPanel.SetActive(false);
    public void GoBacktoMenu() =>SetMenu();
}

