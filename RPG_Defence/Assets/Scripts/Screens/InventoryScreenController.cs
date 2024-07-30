using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScreenController : ScreenUIController
{
    [SerializeField] private Transform content;
    [SerializeField] private ItemSlotController slotController;
    private List<ItemSlotController> slotControllers = new List<ItemSlotController>();
    private int slotCount;
    private ItemSlotController itemHover;

    public override void Init(InventoryStruct inventoryStruct)
    {
        base.Init();
        slotCount = inventoryStruct.slotCount;
        SpawnItems();
        GlobalEventManager.OnPickupItem += HandleOnPickupItem;
    }

    private void SpawnItems()
    {
        for (int i = 0; i < slotCount; i++)
        {
            ItemSlotController itemSlotController = Instantiate(slotController, content);
            itemSlotController.Init(new ItemSlotStruct
            { 
                item = null,  
                OnPointerEnterSlot = OnPointerEnter,
                OnPointerExitSlot = OnPointerExit
            });
            slotControllers.Add(itemSlotController);
        }
    }

    private void HandleOnPickupItem(Item item)
    {
        int index = slotControllers.FindIndex(x => x.CurrentItem == item);
        if (index != -1)
        {
            slotControllers[index].UpdateCount();
        }
        else
        {
            int nullIndex = slotControllers.FindIndex(x => x.CurrentItem == null);
            if (nullIndex != -1)
            {
                slotControllers[nullIndex].UpdateSlotItem(new ItemSlotStruct
                {
                    itemCount = 1,
                    item = item,
                    OnSelectSlot = OnSelectSlot,
                    OnDeselectSlot = OnDeSelectSlot,
                    OnBeginDragSlot = OnBeginDragSlot,
                    OnDraggingSlot = OnDraggingSlot,
                    OnEndDragSlot = OnEndDragSlot,
                    OnDropSlot = OnDropSlot,
                    OnPointerEnterSlot = OnPointerEnter,
                    OnPointerExitSlot = OnPointerExit
                });
            }
        }
    }

    private void OnSelectSlot(ItemSlotController itemSeleted)
    {
        var infoPanel = (PopUpInfoController)GlobalEventManager.TriggerOnSpawnEvent(ScreenType.InfoPanel);
        infoPanel.Init(new PopUpInfoStruct { item = itemSeleted.CurrentItem });
    }

    private void OnDeSelectSlot(ItemSlotController itemDeselected)
    {
        GlobalEventManager.TriggerDestroyScreenController(ScreenType.InfoPanel);
    }

    private void OnBeginDragSlot(ItemSlotController item)
    {
        SwitchItemController switchItemController = (SwitchItemController)GlobalEventManager.TriggerOnSpawnEvent(ScreenType.SwitchItem);
        switchItemController.Init(item.ItemSlotStruct);
    }

    private void OnEndDragSlot(ItemSlotController currentItem)
    {
        GlobalEventManager.TriggerDestroyScreenController(ScreenType.SwitchItem);
        if (itemHover)
        {
            ItemSlotStruct currentItemSlotStruct = currentItem.ItemSlotStruct;
            ItemSlotStruct itemHoverStruct = itemHover.ItemSlotStruct;
            currentItem.UpdateSlotItem(itemHoverStruct);
            itemHover.UpdateSlotItem(currentItemSlotStruct);
        }
        GlobalEventManager.TriggerDestroyScreenController(ScreenType.InfoPanel);
    }

    private void OnDropSlot(ItemSlotController currentItem)
    {
       
    }

    private void OnDraggingSlot(ItemSlotController item)
    {

    }

    public void OnPointerEnter(ItemSlotController item)
    {
        itemHover = item;
    }

    public void OnPointerExit(ItemSlotController item)
    {
        itemHover = null;
    }

    private void OnDisable()
    {
        GlobalEventManager.OnPickupItem -= HandleOnPickupItem;
    }
}

public struct InventoryStruct: IViewElement
{
    public int slotCount;
}
