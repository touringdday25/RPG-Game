using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [SerializeField] Animator anim;
    //[SerializeField]Animation AnimController;
    Transform myTarget;
    [SerializeField] float attackRange;
    [SerializeField] int attackDamage;
    [SerializeField] float attackSpeed;
    float lastAttack;
    float animationTime = 0;
    float animationDur = 0.38f;
    bool attack;

    private void OnValidate()
    {
        myTarget = FindObjectOfType<Health>().transform;
        if (anim == null)
        {
            anim = GetComponentInChildren<Animator>();
        }
    }

    private void Start()
    {
        if(myTarget == null)
        {
            myTarget = FindObjectOfType<Health>().transform;

        }
        if (anim == null)
        {
            anim = GetComponentInChildren<Animator>();
        }
    }

    private void Update()
    {
        float distance = Vector3.Distance(myTarget.position, transform.position);

        if (attack)
        {
            animationTime += Time.deltaTime;
            if (animationDur < animationTime)
            {

                attack = false;
                animationTime = 0;
            }
        }
        if (distance <= attackRange)
        {
            lastAttack += Time.deltaTime;
            if (CanAttack())
            {
                Attack(myTarget.GetComponent<Health>());

            }
        }
    }

    private bool CanAttack()
    {
        if (lastAttack >= attackSpeed)
        {
            lastAttack = 0;
            return true;
        }
        return false;
    }


    private void Attack(Health target)
    {
        attack = true;
        anim.SetInteger("moving", 3);
        target.Damage(attackDamage);

    }

    public bool GetAttack { get { return attack; } }
}
