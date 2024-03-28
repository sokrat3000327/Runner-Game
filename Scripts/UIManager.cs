using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{

    // this gameObjects controlls the buttons
    [Header(" Elements ")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gamePanel;
    //this is related to game over UI
    [SerializeField] private GameObject gameoverPanel;
    [SerializeField] private GameObject levelCompletePanel;

    [SerializeField] private Slider progressBar;
  


  

    void Start()
    {
        progressBar.value = 0;

        gamePanel.SetActive(false);
        gameoverPanel.SetActive(false);

        
     

        GameManager.onGameStateChanged += GameStateChangedCallback;
    }

    private void OnDestroy()
    {
        GameManager.onGameStateChanged -= GameStateChangedCallback;
    }

    
    void Update()
    {
        UpdateProgressBar();
    }

    private void GameStateChangedCallback(GameManager.GameState gameState)
    {
        if (gameState == GameManager.GameState.Gameover)
            ShowGameover();
        else if (gameState == GameManager.GameState.LevelComplete)
            ShowLevelComplete();
    }
    //to detect the presses on the button set the gameState to Game
    public void PlayButtonPressed()
    {

        GameManager.instance.SetGameState(GameManager.GameState.Game);

        menuPanel.SetActive(false);
        gamePanel.SetActive(true);
    }

    // retry button 
    public void RetryButtonPressed()
    {
        SceneManager.LoadScene(0);
        gamePanel.SetActive(true);

    }


    //show the gameOver buttons
    public void ShowGameover()
    {
        gamePanel.SetActive(false);
        gameoverPanel.SetActive(true);
    }

    private void ShowLevelComplete()
    {
        gamePanel.SetActive(false);
        levelCompletePanel.SetActive(true);
    }

    public void UpdateProgressBar()
    {
        if (!GameManager.instance.IsGameState())
            return;

        float progress = PlayerController.instance.transform.position.z / ChunkManager.instance.GetFinishZ();
        progressBar.value = progress;
    }
}
