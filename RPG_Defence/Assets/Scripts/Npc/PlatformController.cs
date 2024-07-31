using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : CombatController
{
    [SerializeField] private TurretController turret;
    [SerializeField] private Transform turretPos;
    private bool isBuilded;

    public override bool BuildTurret()
    {
        if (isBuilded)
        {
            return false;
        }
        
        TurretController newTurret = Instantiate(turret, turretPos.position, turretPos.rotation);
        newTurret.Init(this);
        isBuilded = true;
        return base.BuildTurret();
    }

    public void TurretDestroyed()
    {
        isBuilded = false;
    }
}
