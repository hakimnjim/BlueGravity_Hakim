using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class PauseGameController : ScreenUIController
{
    [SerializeField] private Button homeButton;
    [SerializeField] private Button closePauseScreen;

    public override void Init(PauseGameStruct pauseGameStruct)
    {
        base.Init(pauseGameStruct);
        homeButton.onClick.AddListener(() => pauseGameStruct.OnBackHome?.Invoke());
        closePauseScreen.onClick.AddListener(() => pauseGameStruct.OnClosePausePanel?.Invoke());
    }
}

public struct PauseGameStruct
{
    public Action OnClosePausePanel;
    public Action OnBackHome;
}
