using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public List<Item> justforNows;
    private GameInput playerInputActions;
    Vector2 moveInput;

    private void Awake()
    {
        playerInputActions = new GameInput();
    }

    private void OnEnable()
    {
        playerInputActions.Player.Enable();
        playerInputActions.Player.Mouse.performed += OnMouseClick;
        playerInputActions.Player.Move.performed += OnMove;
        playerInputActions.Player.Move.canceled += OnMoveCanceled;
    }

    private void OnMoveCanceled(InputAction.CallbackContext obj)
    {
        moveInput = Vector2.zero;
        GlobalEventManager.OnSendMoveInput?.Invoke(moveInput);
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        GlobalEventManager.OnSendMoveInput?.Invoke(new Vector3(-moveInput.x, 0, -moveInput.y));
    }

    private void OnDisable()
    {
        playerInputActions.Player.Mouse.performed -= OnMouseClick;
        playerInputActions.Player.Move.performed -= OnMove;
        playerInputActions.Player.Move.canceled -= OnMoveCanceled;
        playerInputActions.Player.Disable();
    }

    private void OnMouseClick(InputAction.CallbackContext context)
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Debug.Log("Mouse clicked at: " + mousePosition);
        int index = Random.Range(0, justforNows.Count);
        GlobalEventManager.OnSendRightClickMouse?.Invoke(playerInputActions.Player.MousePosition.ReadValue<Vector2>());
    }

    private void Update()
    {
        GlobalEventManager.OnSendMousePosition?.Invoke(playerInputActions.Player.MousePosition.ReadValue<Vector2>());
    }
}
