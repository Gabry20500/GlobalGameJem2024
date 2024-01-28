using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsComponent : MonoBehaviour
{
    [SerializeField] int maxHealth;
    [SerializeField] int health;

    public delegate void Died();

    public Died died;

    private void Awake()
    {
        GameManager.instance.stats = this;
    }
    void Start()
    {
        health = maxHealth;
    }

    public void HealthChange(int val)
    {
        health+=val;

        if (health <= 0)
        {
            GameManager.instance.LoseScene();
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
