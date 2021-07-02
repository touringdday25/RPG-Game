using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EntityMovement : MonoBehaviour
{
    [SerializeField] Animator anim;
    public float lookRadius = 10f;
    public float minRadius = 3f;

    [SerializeField]Transform target;

    NavMeshAgent agent;

    private void OnValidate()
    {
        target = FindObjectOfType<Character>().transform;
        agent = GetComponent<NavMeshAgent>();
        if (anim == null)
        {
            anim = GetComponentInChildren<Animator>();
        }
    }

    private void Start()
    {
        if(target == null)
        {
            target = FindObjectOfType<Character>().transform;
        }
        if(agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }
        if (anim == null)
        {
            anim = GetComponentInChildren<Animator>();
        }
    }


    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        //Debug.Log("Distance " + distance);
        if (distance <= lookRadius)
        {
            if (distance >= minRadius)
            {
                //Debug.Log("Walk");
                anim.SetInteger("moving", 1);

            }
            else
            {
                //Debug.Log("Idle");
                if (!GetComponent<EnemyCombat>().GetAttack)
                {
                    
                    anim.SetInteger("moving", 0);
                    anim.SetInteger("battle", 1);

                }
            }
            agent.SetDestination(target.position);
        }
        else
        {
            anim.SetInteger("battle", 0);
            anim.SetInteger("moving", 0);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
        
        Gizmos.DrawLine(transform.position, target.position);
    }
}
