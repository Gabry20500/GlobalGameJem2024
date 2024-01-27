using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InputManager : MonoBehaviour
{
    public static InputManager instance { get; private set; }

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
        }
	}

    public void setDragged(bool value)
    {
        isDragged = value;
    }

}
