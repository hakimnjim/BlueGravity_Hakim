using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISpawner : MonoBehaviour
{
    [SerializeField] private Transform _parentTransform;
    [SerializeField] private List<ScreenView> screens;

    private List<ScreenView> _currentScreentControllers = new List<ScreenView>();

    private void OnEnable()
    {
        GlobalEventManager.OnSpawnScreenEvent += OnSpawnScreen;
        GlobalEventManager.OnDestroyScreenController += OnDestroyScreenController;
    }

    private void OnDisable()
    {
        GlobalEventManager.OnSpawnScreenEvent -= OnSpawnScreen;
        GlobalEventManager.OnDestroyScreenController -= OnDestroyScreenController;
    }

    private ScreenUIController OnSpawnScreen(ScreenType screenType)
    {
        ScreenUIController screenController = screens.Find(x => x.screenType == screenType).screenController;
        int index = _currentScreentControllers.FindIndex(x => x.screenType == screenType);
        ScreenUIController controller = null;

        // if already have this controller
        if (index != -1)
        {
            controller = _currentScreentControllers[index].screenController;
            return controller;
        }

        // Still not have this game object => let's instantiate again

        if (screenController)
        {
            controller = Instantiate(screenController, _parentTransform);
        }
        _currentScreentControllers.Add(new ScreenView { screenType = screenType, screenController = controller });
        return controller;
    }

    private void OnDestroyScreenController(ScreenType screenType)
    {
        int index = _currentScreentControllers.FindIndex(x => x.screenType == screenType);
        if (index != -1)
        {
            ScreenUIController controller = _currentScreentControllers[index].screenController;
            _currentScreentControllers.RemoveAt(index);
            Destroy(controller.gameObject);
        }
    }

    public void PauseAction()
    {
        Time.timeScale = 0;
        GlobalEventManager.OnGamePause?.Invoke();
    }
}

public enum ScreenType { None, Inventory, SwitchItem, InfoPanel, GameOver, PauseGame}

[System.Serializable]
public struct ScreenView
{
    public ScreenType screenType;
    public ScreenUIController screenController;
}