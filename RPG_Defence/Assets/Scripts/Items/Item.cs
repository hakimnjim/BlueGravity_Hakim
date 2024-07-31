using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject
{
    public string itemName;
    [TextArea(0, 5)]
    public string description;
    public Sprite icon;

    public virtual bool UseItem(CombatController combatController)
    {
        return true;
    }
}

