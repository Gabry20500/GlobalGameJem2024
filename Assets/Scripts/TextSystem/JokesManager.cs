using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using FMODUnity;
using FMOD.Studio;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;

[System.Serializable]
class Joke
{
    [SerializeField] string text;
    string unifiedText;
    int textSize = 0;
    

    public void UpdateTextSize()
    {
        textSize = text.Length;
    }

    public char getCurrentCharacter(int currentCharacter)
    {
        return text[currentCharacter];
    }

    public int getTextSize()
    {
        return textSize;
    }

    public string getText()
    {
        return text;
    }
}

public class JokesManager : MonoBehaviour
{

    [SerializeField] TMP_Text UiText;
    [SerializeField] List<Joke> jokesCollection = new List<Joke>();
    [SerializeField] EventReference fartSound;

    private int currentCharacter = 0;
    private int currentJoke = 0;
    private int errorCount = 0;

    public delegate void JokesEnded();
    public delegate void CurrentJokeEnded(int newJoke);
    public delegate void MistakeMade();

    public JokesEnded jokesEnded;
    public CurrentJokeEnded currentJokeEnded;
    public MistakeMade mistakeMade;


    // Start is called before the first frame update
    void Start()
    {
        foreach(Joke joke in jokesCollection)
        {
            joke.UpdateTextSize();
        }

        UpdateUiText();
        InputManager.instance.checkKey += CheckCorrectInput;
        jokesEnded += WinScene;
    }


    void CheckCorrectInput(string key)
    {
        if (checkCurrentKey(key))
        {
            currentCharacter++;

            if (currentCharacter < jokesCollection[currentJoke].getTextSize() && char.IsWhiteSpace(jokesCollection[currentJoke].getText()[currentCharacter]))
            {
                currentCharacter++;
            }

            if (jokesCollection[currentJoke].getTextSize() == currentCharacter)
            {
                // Fine della battuta
                currentCharacter = 0;
                //

                GameManager.instance.GiveReward(jokesCollection[currentJoke].getTextSize(),errorCount);
                //
                currentJoke++;

                errorCount = 0;

                currentJokeEnded.Invoke(currentJoke);
            }

            if (jokesCollection.Count == currentJoke)
            {
                // Fine delle battute
                jokesEnded.Invoke();
            }
        }
        else
        {
            mistakeMade.Invoke();
            currentCharacter = Mathf.Clamp(currentCharacter - 1, 0, jokesCollection[currentJoke].getTextSize());
        

            errorCount++;
            FMODManager.instance.PlayOneShot(fartSound, transform.position);
        }

        UpdateUiText();
    }

    bool checkCurrentKey(string key)
    {
        return currentJoke < jokesCollection.Count && key.ToLower().Equals(jokesCollection[currentJoke].getCurrentCharacter(currentCharacter).ToString().ToLower());
    }

    void UpdateUiText()
    {
        string toPrint = "<color=#FFC53D>";

        if (currentJoke < jokesCollection.Count)
        {
            toPrint += jokesCollection[currentJoke].getText().Insert(currentCharacter, "</color>");
        } else
        {
            toPrint += jokesCollection[currentJoke - 1].getText();
        }

        UiText.text = toPrint;

    }

    void WinScene()
    {
        TransitionManager.instance.PlayCloseAnimation();
        StartCoroutine(Wait(.5f));
    }

    IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        TransitionManager.instance.EndCloseAnimation();
        SceneManager.LoadScene("WinScene");

    }

}
