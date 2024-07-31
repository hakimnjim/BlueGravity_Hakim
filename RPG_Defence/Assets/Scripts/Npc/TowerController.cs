using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : CombatController
{
    private void OnEnable()
    {
        OnHealthChaned?.Invoke(health / maxHealth);
    }
    public override void ExecuteDamage(float amount)
    {
        base.ExecuteDamage(amount);
        if (health <= 0)
        {
            GlobalEventManager.OnGameOver?.Invoke();
        }
    }
}
