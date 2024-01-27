using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using FMODUnity;


public class InputManager : MonoBehaviour
{
    public static InputManager instance { get; private set; }
    private FMOD.Studio.EventInstance FMODEventInstance;

    private void Start()
    {
        FMODEventInstance = FMODUnity.RuntimeManager.CreateInstance(typingSound);
        FMODEventInstance.start();

        FMODEventInstance.setParameterByName("Alphabet", 'b' - 'a');

    }

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

    [SerializeField] EventReference typingSound;

    Event eventKeyPressed;
	string lastKeyPressed = "";
    bool isDragged;

    public delegate void CheckKey(string key);
    public CheckKey checkKey;

	void OnGUI()
	{
        if (isDragged) return;

        eventKeyPressed = Event.current;
		if (eventKeyPressed.type.Equals(EventType.KeyUp))
		{
			lastKeyPressed = eventKeyPressed.keyCode.ToString();
            checkKey.Invoke(lastKeyPressed);

            FMODManager.instance.PlayOneShot(typingSound, transform.position);
        }
	}

    public void setDragged(bool value)
    {
        isDragged = value;
    }

}
