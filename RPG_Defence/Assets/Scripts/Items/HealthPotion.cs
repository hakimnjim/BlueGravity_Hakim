using UnityEngine;

[CreateAssetMenu(fileName = "NewHealthPotion", menuName = "Inventory/HealthPotion")]
public class HealthPotion : Item
{
    public int healthRestoreAmount;

    public override bool UseItem(CombatController combatController)
    {
        switch (combatController.combatControllerType)
        {
            case CombatControllerType.None:
                return false;
                break;
            case CombatControllerType.Player:
                return false;
                break;
            case CombatControllerType.Turret:
                combatController.AddHealth(healthRestoreAmount);
                break;
            case CombatControllerType.Tower:
                combatController.AddHealth(healthRestoreAmount);
                break;
            case CombatControllerType.Npc:
                return false;
                break;
            case CombatControllerType.Platforme:
                return false;
                break;
            default:
                break;
        }
        return base.UseItem(combatController);
    }
}