using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveInventory(List<ItemSlotController> slots)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/inventoryData.data";
        FileStream stream = new FileStream(path, FileMode.Create);
        InventoryData data = new InventoryData(slots);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static InventoryData LoadInventory()
    {
        string path = Application.persistentDataPath + "/inventoryData.data";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            InventoryData data = formatter.Deserialize(stream) as InventoryData;
            stream.Close();
            return data;
        }
        else
        {
            return null;
        }
    }

    public static bool HasSavedData()
    {
        string path = Application.persistentDataPath + "/inventoryData.data";
        Debug.Log(path);
        return File.Exists(path);
    }
}
