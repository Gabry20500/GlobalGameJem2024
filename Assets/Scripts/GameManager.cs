using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public StatsComponent stats;


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
}
