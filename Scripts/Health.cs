using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]private float health;
    [SerializeField]private float baseHealth;
    public event Action<float> OnHealthPctChanged = delegate { };
    private float modHealthCap;
    private float vitality;
    private float armor;
    private Character character;

    private void OnValidate()
    {
        if(character == null)
        {
            character = GetComponent<Character>();
        }
    }

    private void Start()
    {
        SetModHealth();
        health = modHealthCap;
    }

    private void Update()
    {
        if(health <= 0)
        {
            Debug.Log("Player Died.");
            health = modHealthCap;
        }
    }

    public void StatsUpdate()
    {
        vitality = character.Vitality.Value;
        armor = character.Armor.Value;
        SetModHealth();
    }

    private void SetModHealth()
    {
        modHealthCap = baseHealth + vitality;
        Debug.Log("Health Cap: " + modHealthCap);
    }

    public float Damage(float dmg)
    {
        Debug.Log("Health Left " + health);
        health -= dmg / (armor + 1);
        float currentHealthPct = health / modHealthCap;
        Debug.Log("Health PCT. " + currentHealthPct);
        OnHealthPctChanged(currentHealthPct);

        return health;
    }
    


}
