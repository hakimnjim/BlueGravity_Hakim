using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public List<Item> justforNows;
    private GameInput playerInputActions;

    private void Awake()
    {
        playerInputActions = new GameInput();
    }

    private void OnEnable()
    {
        playerInputActions.Player.Enable();
        playerInputActions.Player.Mouse.performed += OnMouseClick;
    }

    private void OnDisable()
    {
        playerInputActions.Player.Mouse.performed -= OnMouseClick;
        playerInputActions.Player.Disable();
    }

    private void OnMouseClick(InputAction.CallbackContext context)
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Debug.Log("Mouse clicked at: " + mousePosition);
        int index = Random.Range(0, justforNows.Count);
        GlobalEventManager.TriggerPickupItem(justforNows[index]);
    }

    private void Update()
    {
        GlobalEventManager.OnSendMousePosition?.Invoke(playerInputActions.Player.MousePosition.ReadValue<Vector2>());
    }
}
