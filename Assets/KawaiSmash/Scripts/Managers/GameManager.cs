using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [Header("Settings")] 
    private GameState gameState;

    public static Action<GameState> onGameStatedChanged;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
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
    
    private void SetMenu()
    {
        SetState(GameState.Menu);
    }
    private void SetGame()
    {
        SetState(GameState.Game);
    }
    private void SetGameOver()
    {
        SetState(GameState.GameOver);
    }
    
    private void SetState(GameState gameState)
    {
        this.gameState = gameState;
        onGameStatedChanged?.Invoke(gameState);
    }
    
    public void SetGameState()
    {
        SetGame();
    }

    public void SetGameOverState()
    {
        SetGameOver();
    }
    public void SetMenuState()
    {
        SetMenu();
    }
    public void PlayButtonCallback()
    {
        SetGameState();
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(0);
    }
    
    public bool IsGameState()
    {
        return gameState == GameState.Game;
    }
}
