using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateMachine : GlobalStateMachine
{
    private void Start()
    {
        GlobalEventManager.OnGameOver += HandleGameOver;
        GlobalEventManager.OnGamePause += HandleGamePause;
    }

    private void HandleGamePause()
    {
        Time.timeScale = 0;
        PauseGameController pauseGameController = (PauseGameController)GlobalEventManager.TriggerOnSpawnEvent(ScreenType.PauseGame);
        pauseGameController.Init(new PauseGameStruct { OnBackHome = BackHome, OnClosePausePanel = ClosePausePanel });
    }

    private void ClosePausePanel()
    {
        GlobalEventManager.TriggerDestroyScreenController(ScreenType.PauseGame);
        Time.timeScale = 1;
    }

    private void HandleGameOver()
    {
        Time.timeScale = 0;
        GameOverController gameOverController = (GameOverController)GlobalEventManager.TriggerOnSpawnEvent(ScreenType.GameOver);
        gameOverController.Init(new GameOverStruct { OnBackHome = BackHome });
    }

    private void BackHome()
    {
        GameData.Instance.LoadScene(0);
    }

    private void OnDisable()
    {
        GlobalEventManager.OnGameOver -= HandleGameOver;
        GlobalEventManager.OnGamePause -= HandleGamePause;
    }
}
