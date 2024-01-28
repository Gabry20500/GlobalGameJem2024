using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public StatsComponent stats;

    public int score;

    [SerializeField] CrowdBehaviour crowdGroup;

    int combo = 0;


    public static GameManager instance{ get; private set; }

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        stats.died += LoseScene;
    }

    // Start is called before the first frame update
    void LoseScene()
    {
        TransitionManager.instance.PlayCloseAnimation();
        StartCoroutine(Wait(.5f, "LoseScene"));
    }


    public void GoMenu()
    {
        TransitionManager.instance.PlayCloseAnimation();
        StartCoroutine(Wait(.5f, "MainMenu"));
    }
    IEnumerator Wait(float seconds, string name)
    {
        yield return new WaitForSeconds(seconds);

        TransitionManager.instance.EndCloseAnimation();

        SceneManager.LoadScene(name);
    }

    public void GiveReward(int strlength, int mistakesmade)
    {
        int scoretoadd = (strlength * 100 - mistakesmade*100);

        if (scoretoadd < 0)
        {
            scoretoadd = 0;
        }

        
        if (stats.Checkbonus())
        {
            combo++;
            scoretoadd += (200 * combo);

        }
        else
        {
            combo = 0;
            stats.HealthChange(+3);
        }

        score += scoretoadd;



    }


    public int GetScore()
    {
        return score;
    }
}
