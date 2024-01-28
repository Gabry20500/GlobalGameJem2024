using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsComponent : MonoBehaviour
{
    [SerializeField] int maxHealth;
    [SerializeField] int health;

    public delegate void Died();

    public Died died;


    void Start()
    {
        health = maxHealth;
    }

    public void HealthChange(int val)
    {
        health+=val;

        if (health <= 0)
        {
            died.Invoke();
        }

        if(health>maxHealth) 
        { 
            health = maxHealth; 
        }
    }

    public bool Checkbonus()
    {
        return health == maxHealth;
    }
}
