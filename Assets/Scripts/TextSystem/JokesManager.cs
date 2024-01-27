using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


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
                currentJoke++;
                currentJokeEnded.Invoke(currentJoke);
            }

            if (jokesCollection.Count == currentJoke)
            {
                // Fine delle battute
                //jokesEnded.Invoke();
            }
        }
        else
        {
            mistakeMade.Invoke();
            currentCharacter = Mathf.Clamp(currentCharacter - 1, 0, jokesCollection[currentJoke].getTextSize());
            errorCount++;
        }

        UpdateUiText();
    }

    bool checkCurrentKey(string key)
    {
        return currentJoke < jokesCollection.Count && key.ToLower().Equals(jokesCollection[currentJoke].getCurrentCharacter(currentCharacter).ToString().ToLower());
    }

    void UpdateUiText()
    {
        string toPrint = "<color=#E42F00>";

        if (currentJoke < jokesCollection.Count)
        {
            toPrint += jokesCollection[currentJoke].getText().Insert(currentCharacter, "</color>");
        } else
        {
            toPrint += jokesCollection[currentJoke - 1].getText();
        }

        UiText.text = toPrint;

    }

}
