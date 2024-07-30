using UnityEngine;

[CreateAssetMenu(fileName = "NewHealthPotion", menuName = "Inventory/HealthPotion")]
public class HealthPotion : Item
{
    public int healthRestoreAmount;
}