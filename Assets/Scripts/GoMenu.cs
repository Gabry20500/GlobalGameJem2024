using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoMenu : MonoBehaviour
{
    public void GoToMenu()
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
}
