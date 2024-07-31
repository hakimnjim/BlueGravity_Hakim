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
        // Instantiate the explosion effect
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        // Get nearby objects within the explosion radius
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider nearbyObject in colliders)
        {
            // Apply explosion force
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null && rb.tag != "Bomb")
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }

            // Check for NavMeshAgent and apply damage
            NavMeshAgent agent = nearbyObject.GetComponent<NavMeshAgent>();
            if (agent != null)
            {
                /*Health health = agent.GetComponent<Health>();
                if (health != null)
                {
                    health.TakeDamage(damage);
                }*/
            }
        }

        // Destroy the bomb
        Destroy(gameObject);
    }
}

