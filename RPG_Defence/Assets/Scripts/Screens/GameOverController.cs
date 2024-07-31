using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameOverController : ScreenUIController
{
    public Button homeButton;

    public override void Init(GameOverStruct gameOverStruct)
    {
        base.Init(gameOverStruct);
        homeButton.onClick.AddListener(() => gameOverStruct.OnBackHome?.Invoke());
    }
}

public struct GameOverStruct : IViewElement
{
    public Action OnBackHome;
}
