using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class GameManager : MonoBehaviour
{

    //this script manage the coordinations between all actions
    //in the game
    public static GameManager instance;
    public int totalScore;
 

    //what is your intent
    public enum GameState { Menu, Game, LevelComplete, Gameover }

    private GameState gameState;

    public static Action<GameState> onGameStateChanged;

    //create only a single object of the gameManager
    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }


    void Start()
    {
         
       
    }

  
    void Update()
    {
        totalScore = PlayerPrefs.GetInt("Score");
        PlayerPrefs.SetInt("Score",totalScore);
    }

    public void SetGameState(GameState gameState)
    {
        this.gameState = gameState;
        onGameStateChanged?.Invoke(gameState);

        Debug.Log("Game State changed to : " + gameState);
    }

    public bool IsGameState()
    {
        return gameState == GameState.Game;
    }
}
