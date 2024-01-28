using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public void PlayGame()
    {
        TransitionManager.instance.PlayCloseAnimation();

        StartCoroutine(Wait(0.5f));
    }

    IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        TransitionManager.instance.EndCloseAnimation();
        SceneManager.LoadScene("GameScene");

    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
