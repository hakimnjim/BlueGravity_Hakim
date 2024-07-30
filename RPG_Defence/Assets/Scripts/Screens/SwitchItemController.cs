using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SwitchItemController : ScreenUIController
{
    [SerializeField] private Image icon;

    public override void Init(ItemSlotStruct viewElement)
    {
        base.Init(viewElement);
        icon.sprite = viewElement.item.icon;
        GlobalEventManager.OnSendMousePosition += HandleOnSendMousePosition;
    }

    private void HandleOnSendMousePosition(Vector2 vector2)
    {
        transform.position = vector2;
    }


    private void OnDisable()
    {
        GlobalEventManager.OnSendMousePosition -= HandleOnSendMousePosition;

    }
}
