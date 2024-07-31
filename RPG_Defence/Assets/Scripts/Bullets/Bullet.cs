using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float forceDown;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private float damage = 5f;

    public void Init()
    {
        rb.AddForce(transform.forward * moveSpeed);
    }

    private void FixedUpdate()
    {
        rb.AddForce(-transform.up * forceDown * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag != "Player" && other.tag != "Bomb")
        {
            Explode(other);
        }
    }

    private void Explode(Collider other)
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        CombatController controller = other.GetComponent<CombatController>();
        if (controller && controller.combatControllerType == CombatControllerType.Npc)
        {
            controller.ExecuteDamage(damage);
        }
        Destroy(gameObject);
    }
}
