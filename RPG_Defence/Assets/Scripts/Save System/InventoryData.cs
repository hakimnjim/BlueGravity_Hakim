using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryData
{
    public List<ItemSlotData> itemSlotDatas;

    public InventoryData(List<ItemSlotController> slots)
    {
        itemSlotDatas = new List<ItemSlotData>();
        foreach (var slot in slots)
        {
            itemSlotDatas.Add(new ItemSlotData(slot.CurrentItem?.itemID ?? -1, slot.ItemSlotStruct.itemCount));
        }
    }
}

[System.Serializable]
public struct ItemSlotData
{
    public int itemID; // Assuming each item has a unique ID
    public int itemCount;

    public ItemSlotData(int id, int count)
    {
        itemID = id;
        itemCount = count;
    }
}
