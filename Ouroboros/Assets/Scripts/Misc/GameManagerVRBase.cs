using UnityEngine;
using System.Collections.Generic;

public class GameManagerVRBase : MonoBehaviour
{
    // Event handlers for game states
    public delegate void GameStateChangeHandler();
    public static event GameStateChangeHandler OnGameStart;
    public static event GameStateChangeHandler OnGamePause;
    public static event GameStateChangeHandler OnGameResume;
    public static event GameStateChangeHandler OnGameEnd;
    public static event GameStateChangeHandler OnGameOver;

    // List of game components to manage
    protected List<IGameComponent> gameComponents = new List<IGameComponent>();

    protected virtual void Start()
    {
        // Custom initialization
        InitializeGame();
    }

    protected virtual void Update()
    {
        
        HandleGameUpdate();
    }

    protected virtual void InitializeGame()
    {
        // Trigger game start event
        OnGameStart?.Invoke();
        foreach (var component in gameComponents)
        {
            component.Initialize();
        }
    }

    protected virtual void HandleGameUpdate()
    {
        foreach (var component in gameComponents)
        {
            component.UpdateComponent();
        }
    }

    public virtual void PauseGame()
    {
        // Trigger game pause event
        OnGamePause?.Invoke();
        foreach (var component in gameComponents)
        {
            component.Pause();
        }
    }

    public virtual void ResumeGame()
    {
        // Trigger game resume event
        OnGameResume?.Invoke();
        foreach (var component in gameComponents)
        {
            component.Resume();
        }
    }

    public virtual void EndGame()
    {
        // Trigger game end event
        OnGameEnd?.Invoke();
        foreach (var component in gameComponents)
        {
            component.End();

        }
    }

    public virtual void GameOver()
    {
        // Trigger game pause event
        OnGameOver?.Invoke();
        foreach (var component in gameComponents)
        {
            component.GameOver();
        }
    }

    // Method to add game components
    public void AddGameComponent(IGameComponent component)
    {
        gameComponents.Add(component);
    }
}
