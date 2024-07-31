using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcMovement : CombatController
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator anim;
    private Transform target;
    [SerializeField] private float damage;

    private void OnEnable()
    {
        target = GameObject.FindGameObjectWithTag("Base").transform;
        ExcuteMove(target.position);
    }

    public override void ExcuteMove(Vector3 pos)
    {
        base.ExcuteMove(pos);
        agent.SetDestination(pos);
        
        anim.SetBool("walk", true);
    }

    public override void ExecuteDamage(float amount)
    {
        base.ExecuteDamage(amount);
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, agent.destination) < 0.5f)
        {
            if (target != null)
            {
                CombatController controller = target.GetComponent<CombatController>();
                if (controller != null)
                {
                    controller.ExecuteDamage(damage);
                }
            }
            Destroy(gameObject);
        }
    }

}
