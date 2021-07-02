using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]private float maxHealth;
    private float hp;
    [SerializeField]private float armour;
    public event Action<float> OnHealthPctChanged = delegate { };
    private int spawnerNum;
    public int SpawnerNum { get { return spawnerNum; } set { spawnerNum = value; } }
    EnemySpawner spawner;
    public EnemySpawner Spawner { get { return spawner; } set { spawner = value; } }

    private void OnEnable()
    {
        hp = maxHealth;
    }
    
    void Update()
    {
        if(hp <= 0)
        {
            DeathReturn();
            this.gameObject.GetComponent<Drops>().RollDrop();
            Destroy(this.gameObject);
        }
    }

    public float TakeDamage(float damage)
    {
        hp -= damage / (armour / 100);
        float currentHealthPct = hp / maxHealth;
        OnHealthPctChanged(currentHealthPct);

        return hp;
    }

    private void DeathReturn()
    {
        if(Spawner != null)
        {
            Spawner.RemoveEnemy(this);
        }
    }


}
