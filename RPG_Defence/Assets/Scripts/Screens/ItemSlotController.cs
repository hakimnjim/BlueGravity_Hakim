using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ItemSlotController : ScreenUIController, ISelectHandler, IDeselectHandler, IBeginDragHandler, IEndDragHandler, IDropHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler
{

    private Item item;
    private ItemSlotStruct _itemSlotStruct;


    public ItemSlotStruct ItemSlotStruct
    {
        get { return _itemSlotStruct; }
        set { _itemSlotStruct = value; }
    }

    [SerializeField] private GameObject child;
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text textCount;

    public Item CurrentItem
    {
        get { return item; }
        set { item = value; }
    }


    public override void Init(ItemSlotStruct itemSlotStruct)
    {
        base.Init();
        CurrentItem = itemSlotStruct.item;
        ItemSlotStruct = itemSlotStruct;
    }

    public void UpdateSlotItem(ItemSlotStruct itemSlotStruct)
    {
        CurrentItem = itemSlotStruct.item;
        ItemSlotStruct = itemSlotStruct;
        if (CurrentItem)
        {
            child.SetActive(true);
            icon.sprite = _itemSlotStruct.item.icon;
            textCount.text = _itemSlotStruct.itemCount + "";
        }
        else
        {
            child.SetActive(false);
        }

    }

    public void UpdateCount()
    {
        _itemSlotStruct.itemCount++;
        int count = int.Parse(textCount.text);
        textCount.text = _itemSlotStruct.itemCount.ToString();
    }

    public void OnDeselect(BaseEventData eventData)
    {
        _itemSlotStruct.OnDeselectSlot?.Invoke(this);
    }

    public void OnSelect(BaseEventData eventData)
    {
        _itemSlotStruct.OnSelectSlot?.Invoke(this);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _itemSlotStruct.OnBeginDragSlot?.Invoke(this);

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _itemSlotStruct.OnEndDragSlot?.Invoke(this);

    }

    public void OnDrop(PointerEventData eventData)
    {
        _itemSlotStruct.OnDropSlot?.Invoke(this);

    }

    public void OnDrag(PointerEventData eventData)
    {
        _itemSlotStruct.OnDraggingSlot?.Invoke(this);

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _itemSlotStruct.OnPointerEnterSlot?.Invoke(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _itemSlotStruct.OnPointerExitSlot?.Invoke(this);
    }
}

public struct ItemSlotStruct: IViewElement
{
    public int itemCount;
    public Item item;
    public UnityAction<ItemSlotController> OnSelectSlot;
    public UnityAction <ItemSlotController> OnDeselectSlot;
    public UnityAction<ItemSlotController> OnBeginDragSlot;
    public UnityAction<ItemSlotController> OnEndDragSlot;
    public UnityAction<ItemSlotController> OnDraggingSlot;
    public UnityAction<ItemSlotController> OnDropSlot;
    public UnityAction<ItemSlotController> OnPointerEnterSlot;
    public UnityAction<ItemSlotController> OnPointerExitSlot;
}
