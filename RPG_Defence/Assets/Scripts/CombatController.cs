using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CombatController : MonoBehaviour, IMovebale, IRotate, IDamage
{
    public Action<float> OnHealthChaned;
    public float health;
    public float maxHealth = 100;
    public CombatControllerType combatControllerType;
    public float fireRate;
    public ParticleSystem effect;

    public virtual void ExcuteMove(Vector3 pos)
    {

    }

    public virtual void ExecuteDamage(float amount)
    {
        health -= amount;
        OnHealthChaned?.Invoke(health / maxHealth);
    }

    public virtual void ExecuteRotate(Vector3 eurAngle)
    {

    }

    public virtual void AddHealth(float amount)
    {
        health += amount;
        health = Mathf.Clamp(health, 0, maxHealth);
        OnHealthChaned?.Invoke(health / maxHealth);
        effect.Play();
    }

    public virtual bool BuildTurret()
    {
        return true;
    }

    public virtual void PowerUpController(float amount)
    {
        fireRate -= amount;
        effect.Play();
    }
}

public enum CombatControllerType { None, Player, Turret, Tower, Npc, Platforme}

public interface IMovebale
{
    public void ExcuteMove(Vector3 pos);
}

public interface IRotate
{
    public void ExecuteRotate(Vector3 eurAngle);
}

public interface IDamage
{
    public void ExecuteDamage(float amount);
}