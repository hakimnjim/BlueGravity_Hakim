using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bomb : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private float explosionRadius = 5f;
    [SerializeField] private float explosionForce = 700f;
    [SerializeField] private float damage = 50f;


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag != "Player" && other.tag != "Bomb")
        {
            Explode();
        }
    }

    private void Explode()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider nearbyObject in colliders)
        {
 
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null && rb.tag != "Bomb")
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }

            NavMeshAgent agent = nearbyObject.GetComponent<NavMeshAgent>();
            if (agent != null)
            {
                CombatController controller = agent.GetComponent<CombatController>();
                if (controller != null)
                {
                    controller.ExecuteDamage(damage);
                }
            }
        }

        Destroy(gameObject);
    }
}

