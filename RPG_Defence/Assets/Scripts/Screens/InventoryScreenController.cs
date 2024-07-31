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
    private Vector2 mousePos;

    public override void Init(InventoryStruct inventoryStruct)
    {
        base.Init();
        slotCount = inventoryStruct.slotCount;
        SpawnItems();
        GlobalEventManager.OnPickupItem += HandleOnPickupItem;
        GlobalEventManager.OnSendMousePosition += HandleGetMousePos;
        if (GameData.Instance.isLoadingGame)
        {
            LoadInventory();
        }
        else
        {
            CleanInventory();
        }

    }

    private void HandleGetMousePos(Vector2 vector2)
    {
        mousePos = vector2;
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
            slotControllers[index].UpdateCount(1);
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
                    OnPointerExitSlot = OnPointerExit,
                    OnRemoveItem = OnRemoveItem
                });
            }
        }
        SaveInventory();
    }

    private void OnRemoveItem(ItemSlotController ItemSlot)
    {
        ItemSlot.Init(new ItemSlotStruct
        {
            item = null,
            OnPointerEnterSlot = OnPointerEnter,
            OnPointerExitSlot = OnPointerExit
        });
        SaveInventory();
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
        else
        {
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                CombatController combatController = hit.collider.GetComponent<CombatController>();
                if (combatController != null)
                {
                    Debug.Log("we found this combat");
                    bool isUsed = currentItem.ItemSlotStruct.item.UseItem(combatController);
                    if (isUsed)
                    {
                        currentItem.UpdateCount(-1);
                    }
                    // Perform actions with the CombatController here
                }
                else
                {
                    Debug.Log("No CombatController found at hit location.");
                }
            }
        }
        GlobalEventManager.TriggerDestroyScreenController(ScreenType.InfoPanel);
        SaveInventory();
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

    private void SaveInventory()
    {
        SaveSystem.SaveInventory(slotControllers);
    }

    private void CleanInventory()
    {
        foreach (var slot in slotControllers)
        {
            OnRemoveItem(slot);
        }
        SaveInventory();
    }

    private void LoadInventory()
    {
        InventoryData data = SaveSystem.LoadInventory();
        if (data != null)
        {
            for (int i = 0; i < slotControllers.Count; i++)
            {
                if (i < data.itemSlotDatas.Count)
                {
                    Item item = GetItemById(data.itemSlotDatas[i].itemID);
                    slotControllers[i].UpdateSlotItem(new ItemSlotStruct
                    {
                        item = item,
                        itemCount = data.itemSlotDatas[i].itemCount,
                        OnSelectSlot = OnSelectSlot,
                        OnDeselectSlot = OnDeSelectSlot,
                        OnBeginDragSlot = OnBeginDragSlot,
                        OnDraggingSlot = OnDraggingSlot,
                        OnEndDragSlot = OnEndDragSlot,
                        OnDropSlot = OnDropSlot,
                        OnPointerEnterSlot = OnPointerEnter,
                        OnPointerExitSlot = OnPointerExit,
                        OnRemoveItem = OnRemoveItem
                    });
                }
            }
        }
    }

    private Item GetItemById(int id)
    {
        return GameData.Instance.GetItemById(id);
    }

    private void OnDisable()
    {
        GlobalEventManager.OnPickupItem -= HandleOnPickupItem;
        GlobalEventManager.OnSendMousePosition -= HandleGetMousePos;
    }
}

public struct InventoryStruct: IViewElement
{
    public int slotCount;
}

