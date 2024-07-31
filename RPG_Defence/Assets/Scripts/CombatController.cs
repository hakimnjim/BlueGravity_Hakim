using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CombatController : MonoBehaviour, IMovebale, IRotate, IDamage
{
    public Action<float> OnTakeDamge;
    public float health;

    public virtual void ExcuteMove(Vector3 pos)
    {

    }

    public virtual void ExecuteDamage(float amount)
    {
        health -= amount;
        OnTakeDamge?.Invoke(health / 100);
    }

    public virtual void ExecuteRotate(Vector3 eurAngle)
    {

    }
}

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