using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public enum GameState
    {
        Prepare,
        Task1,
        Task2,
        Task3,
        FinishGame,
    }

    [SerializeField] private GameState currentGameState;

    public GameState CurrentGameState
    {
        get { return currentGameState; }
        set
        {
            switch (value)
            {
                case GameState.Prepare:
                    break;
                case GameState.Task1:
                    break;
                case GameState.Task2:
                    break;
                case GameState.Task3:
                    break;
                case GameState.FinishGame:
                    break;
            }

            currentGameState = value;
        }
    }
}