using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public PlayerMovement _PlayerMovement;
    public LevelLoader _LevelLoader;
    public UIManager _UIManager;

    public GameObject player;
    public GameObject playerArt;
    public enum GameState { MainMenu, GamePlay, PauseMenu, OptionsMenu, WinMenu, LoseMenu }
    public GameState gameState;

    public void Awake()
    {
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) && gameState == GameState.GamePlay)
        {
            PauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && gameState == GameState.PauseMenu)
        {
            ResumeGame();
        }

        if (Input.GetKeyDown(KeyCode.K) && gameState == GameState.GamePlay)
        {
            TriggerLoseMenu();
        }

        if (Input.GetKeyDown(KeyCode.H) && gameState == GameState.GamePlay)
        {
            HouseLevel();
        }

        switch (gameState)
        {
            case GameState.MainMenu: MainMenu();
                break;
            case GameState.GamePlay: GamePlay();
                break;
            case GameState.PauseMenu: PauseMenu();
                break;
            case GameState.OptionsMenu: OptionsMenu();
                break;
            case GameState.WinMenu: WinMenu();
                break;
            case GameState.LoseMenu: LoseMenu();
                break;
        }
    }

    private void MainMenu()
    {
        Cursor.visible = true;
        playerArt.SetActive(false);
        _PlayerMovement.enabled = false;
        _UIManager.UIMainMenu();
    }

    private void GamePlay()
    {
        Cursor.visible = false;
        playerArt.SetActive(true);
        _PlayerMovement.enabled = true;
        _UIManager.UIGamePlay();
    }

    private void PauseMenu()
    {
        Cursor.visible = true;
        _UIManager.UIPauseMenu();
    }

    private void OptionsMenu()
    {
        Cursor.visible = true;
        _UIManager.UIOptionsMenu();
    }

    private void WinMenu()
    {
        Cursor.visible = true;
        _UIManager.UIWinMenu();
    }

    private void LoseMenu()
    {
        Cursor.visible = true;
        _UIManager.UILoseMenu();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PauseGame()
    {
        gameState = GameState.PauseMenu;
        _PlayerMovement.enabled = false;
        Cursor.visible = true;
        Time.timeScale = 0f;
        _UIManager.UIPauseMenu();
    }

    public void ResumeGame()
    {
        gameState = GameState.GamePlay;
        _PlayerMovement.enabled = true;
        Cursor.visible = false;
        Time.timeScale = 1f;
        _UIManager.UIGamePlay();
    }

    public void ShowOptionsMenu()
    {
        gameState = GameState.OptionsMenu;
        _UIManager.UIOptionsMenu();
    }

    public void MainMenuOp()
    {
        gameState = GameState.MainMenu;
        _UIManager.UIMainMenu();
    }

    public void TriggerLoseMenu()
    {
        gameState = GameState.LoseMenu;
        _PlayerMovement.enabled = false;
        playerArt.SetActive(false);
        Cursor.visible = true;
        Time.timeScale = 0f;
        _UIManager.UILoseMenu();
        _LevelLoader.LoadEndScene();
    }

    private void HouseLevel()
    {
        _LevelLoader.LoadHouseLevel();
    }

    public void GoBack()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        if (currentScene.name == "MainMenu")
        {
            MainMenuOp();
        }
        else if (currentScene.name == "GrassLevel" || currentScene.name == "HouseLevel")
        {
            PauseGame();
        }
    }
}
