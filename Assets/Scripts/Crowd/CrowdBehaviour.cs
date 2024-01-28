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
    bool isdisabled;
    List<CrowdGroupBehaviour> crowdGroupBehaviours;

    private void Start()
    {
        GameManager.instance.stats.died += DisableCrowd;
        jokesManager.mistakeMade += UpdateRage;
        jokesManager.currentJokeEnded += OnNewJoke;

        crowdGroupBehaviours = new List<CrowdGroupBehaviour>();

        crowdGroupBehaviours.AddRange(gameObject.GetComponentsInChildren<CrowdGroupBehaviour>());
    }

    private void FixedUpdate()
    {
        if (isdisabled)
        {
            rageTime += Time.deltaTime;

            if (rageTime >= timeToRage)
            {
                UpdateRage();
            }
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

        foreach(var  c in crowdGroupBehaviours)
        {
            c.GetDifficulty(difficulty);
        }
    }

    public int getRage()
    {
        return rage;
    }

    public int getDifficulty()
    {
        return difficulty;
    }

    public void DisableCrowd()
    {
        isdisabled = true;
    }
}
