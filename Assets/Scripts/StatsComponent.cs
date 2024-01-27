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

    public void TakeDamage()
    {
        health--;
        if (health == 0)
        {
            died.Invoke();
        }
    }
}
