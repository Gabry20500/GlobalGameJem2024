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

    Event e;
	string lastKeyPressed = "";
	bool keydown = false;

    public delegate void CheckKey(string key);
    public CheckKey checkKey;

	void OnGUI()
	{
		e = Event.current;
		if (e.type.Equals(EventType.KeyDown) && !keydown)
		{
			keydown = true;
			lastKeyPressed = e.keyCode.ToString();
		}

		if (e.type.Equals(EventType.KeyUp))
			keydown = false;

        checkKey.Invoke(lastKeyPressed);
        GUILayout.Label("\nLast Key Pressed - " + lastKeyPressed);
	}


	// Start is called before the first frame update
	void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {

    }

}
