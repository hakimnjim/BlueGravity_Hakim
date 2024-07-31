using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalEventManager : MonoBehaviour
{
    public delegate ScreenUIController SpawnScreenEvent(ScreenType screenType);

    public static event SpawnScreenEvent OnSpawnScreenEvent;

    public static ScreenUIController TriggerOnSpawnEvent(ScreenType screenType)
    {
        return OnSpawnScreenEvent?.Invoke(screenType);
    }

    public delegate void DestroyScreenController(ScreenType screenType);
    public static event DestroyScreenController OnDestroyScreenController;

    public static void TriggerDestroyScreenController(ScreenType screenType)
    {
        OnDestroyScreenController?.Invoke(screenType);
    }

    public delegate void PickupItem(Item item);
    public static event PickupItem OnPickupItem;

    public static void TriggerPickupItem(Item item)
    {
        OnPickupItem?.Invoke(item);
    }

    public delegate void SendMousePosition(Vector2 vector2);
    public static SendMousePosition OnSendMousePosition;

    public delegate void SendMoveInput(Vector3 vector);
    public static SendMoveInput OnSendMoveInput;

    public delegate void SendRightClickMouse(Vector2 vector);
    public static SendRightClickMouse OnSendRightClickMouse;

    public delegate void GameOver();
    public static GameOver OnGameOver;

    public delegate void GamePause();
    public static GameOver OnGamePause;
}
