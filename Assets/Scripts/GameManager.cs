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
        StartCoroutine(Wait(2f));
    }

    IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        SceneManager.LoadScene("LoseScene");
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
