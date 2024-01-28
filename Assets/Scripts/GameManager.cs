using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using FMODUnity;
using FMOD.Studio;

public class GameManager : MonoBehaviour
{
    [SerializeField] EventReference gameOverSound;

    public StatsComponent stats;

    public int score;

    public CrowdBehaviour crowdGroup;

    int combo = 0;


    public static GameManager instance{ get; private set; }

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);

        //if (stats == null)
        //    stats = GameObject.FindGameObjectWithTag("Player").GetComponent<StatsComponent>();

        //if (crowdGroup == null)
        //    crowdGroup = GameObject.FindGameObjectWithTag("Crowd").GetComponent<CrowdBehaviour>();
    }

    private void Start()
    {
        stats.died += LoseScene;
    }

    private void Update()
    {
        
        if(Input.GetKeyUp(KeyCode.Escape)) 
        {
            GoMenu();
        }

    }

    void GetRef()
    {
        if (stats == null)
            stats = GameObject.FindGameObjectWithTag("Player").GetComponent<StatsComponent>();

        if (crowdGroup == null)
            crowdGroup = GameObject.FindGameObjectWithTag("Crowd").GetComponent<CrowdBehaviour>();
    }
    // Start is called before the first frame update
    public void LoseScene()
    {
        TransitionManager.instance.PlayCloseAnimation();
        FMODManager.instance.PlayOneShot(gameOverSound, transform.position);
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
