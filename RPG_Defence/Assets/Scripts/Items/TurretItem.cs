using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTurret", menuName = "Inventory/NewTurret")]
public class TurretItem : Item
{
    public override bool UseItem(CombatController combatController)
    {
        if (combatController.combatControllerType != CombatControllerType.Platforme)
        {
            return false;
        }
        bool canBuild = combatController.BuildTurret();
        return canBuild;
    }

}
