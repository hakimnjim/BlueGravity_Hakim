using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopUpInfoController : ScreenUIController
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text description;


    public override void Init(PopUpInfoStruct popUpInfoStruct)
    {
        base.Init(popUpInfoStruct);
        icon.sprite = popUpInfoStruct.item.icon;
        title.text = popUpInfoStruct.item.itemName;
    }
}

public struct PopUpInfoStruct
{
    public Item item;
}