using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdBehaviour : MonoBehaviour
{
    [SerializeField] JokesManager jokesManager;

    int rage = 0;
    int difficulty = 0;

    float timeToRage = 20f;
    float rageTime = 0f;

    private void Start()
    {
        jokesManager.mistakeMade += UpdateRage;
        jokesManager.currentJokeEnded += OnNewJoke;
    }

    private void FixedUpdate()
    {
        rageTime += Time.deltaTime;

        if (rageTime >= timeToRage)
        {
            UpdateRage();
        }
    }

    private void UpdateRage()
    {
        rageTime = 0;
        rage++;
    }

    private void OnNewJoke(int currentJoke)
    {
        difficulty++;
        rage = 0;
        rageTime = 0;
    }

    public int getRage()
    {
        return rage;
    }

    public int getDifficulty()
    {
        return difficulty;
    }
}
