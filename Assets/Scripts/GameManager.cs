using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public enum GameState 
    {
        Prepare,
        MainGame,
        Jumping,
        FinishGame,
    }
    [SerializeField] private GameState _currentGameState;
    public GameState CurrentGameState
    {
        get { return _currentGameState;}
        set
        {
            switch (value)
            {
                case GameState.Prepare:
                    break;
                case GameState.MainGame:                       
                    break;
                case GameState.Jumping:
                    break;
                case GameState.FinishGame:                        
                    break;
            }
            _currentGameState = value;
        }           
    }
}
