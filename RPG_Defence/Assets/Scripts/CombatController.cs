using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour, IMovebale, IRotate
{
    public virtual void ExcuteMove(Vector3 pos)
    {

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