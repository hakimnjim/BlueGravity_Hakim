using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPowerUP", menuName = "Inventory/PowerUP")]
public class PowerUP : Item
{
    [SerializeField] private float amount = 0.1f;
    public override bool UseItem(CombatController combatController)
    {
        switch (combatController.combatControllerType)
        {
            case CombatControllerType.None:
                return false;
                break;
            case CombatControllerType.Player:
                combatController.PowerUpController(amount);
                break;
            case CombatControllerType.Turret:
                combatController.PowerUpController(amount);
                break;
            case CombatControllerType.Tower:
                return false;
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
