using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager GameManagerInstance;
    [Header("Settings")] 
    private GameState gameState;

    [Header("Actions")] public static Action<GameState> onGameStatedChanged;

    private void Awake()
    {
        if (GameManagerInstance == null)
        {
            GameManagerInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        SetMenu();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetMenu()
    {
    
        SetGameState(GameState.Menu);
    }
    private void SetGame()
    {
        
        SetGameState(GameState.Game);
    }
    private void SetGameOver()
    {
        
        SetGameState(GameState.GameOver);
    }

    private void SetGameState(GameState gameState)
    {
        this.gameState = gameState;
        onGameStatedChanged?.Invoke(gameState);
    }

    public GameState GetGameState()
    {
        return gameState;
    }

    public void SetGameState()
    {
        SetGame();
    }

    public bool IsGameState()
    {
        return gameState == GameState.Game;
    }
    public void SetGameOverState()
    {
        SetGameOver();
    }
    public void PlayButtonCallback()
    {
        GameManager.GameManagerInstance.SetGameState();
        SetGame();
    }

    public void NextButtonCallback()
    {
        SceneManager.LoadScene(0);

    }
}
