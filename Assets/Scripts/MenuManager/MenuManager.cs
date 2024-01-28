using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using FMODUnity;
using FMOD.Studio;

public class MenuManager : MonoBehaviour
{
    [SerializeField] EventReference soundTrackSound;
    [SerializeField] EventReference uiSound;

    private void Start()
    {
        FMODManager.instance.PlayOneShot(soundTrackSound, new Vector3(0f, 0f, 0f));
    }

    public void PlayGame()
    {
        FMODManager.instance.PlayOneShot(uiSound, new Vector3(0f, 0f, 0f));
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
        FMODManager.instance.PlayOneShot(uiSound, new Vector3(0f, 0f, 0f));
        Application.Quit();
    }
}
