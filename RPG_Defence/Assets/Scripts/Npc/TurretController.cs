using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : CombatController
{
    private PlatformController ownerPlatform;
    private CombatController target;
    [SerializeField] private float speedRotation = 0.15f;
    [SerializeField] private float raduis = 5;
    [SerializeField] private float range = 8;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform firePoint;
    private float shootingTime;

    public void Init(PlatformController controller)
    {
        ownerPlatform = controller;
        shootingTime = Time.time;
        OnHealthChaned?.Invoke(health / maxHealth);
    }

    public override void ExecuteDamage(float amount)
    {
        base.ExecuteDamage(amount);
        if (health <= 0)
        {
            ownerPlatform.TurretDestroyed();
            Destroy(gameObject);
        }
    }

    public override void ExecuteRotate(Vector3 eurAngle)
    {
        base.ExecuteRotate(eurAngle);
        if (target)
        {
            Vector3 dir = target.transform.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(-dir);
            Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * speedRotation).eulerAngles;
            transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
            if (Time.time > shootingTime + fireRate)
            {
                Shoot();
            }
        }
    }

    private void LateUpdate()
    {
        if (target == null || (target!= null && Vector3.Distance(transform.position, target.transform.position) > range))
        {
            UpdateTarget();
        }
        else
        {
            ExecuteRotate(target.transform.position);
        }
    }

    private void UpdateTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, raduis);
        float shortestDistance = Mathf.Infinity;
        CombatController nearestEnemy = null;
        foreach (Collider enemy in colliders)
        {
            CombatController combatController = enemy.GetComponent<CombatController>();
            if (combatController && combatController.combatControllerType == CombatControllerType.Npc)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = combatController;
                }
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy;
        }
        else
        {
            target = null;
        }
    }

    private void Shoot()
    {
        Bullet bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        if (bullet != null)
            bullet.Init();

        shootingTime = Time.time;
    }
}
