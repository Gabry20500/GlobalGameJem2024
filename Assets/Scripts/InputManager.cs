using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using FMODUnity;
using FMOD.Studio;


public class InputManager : MonoBehaviour
{

    [SerializeField] EventReference typingSound; 
    [SerializeField] PlayerAnimationManager plyAnimationManager;
    
    Event eventKeyPressed;
    string lastKeyPressed = "";
    bool isDragged;
    float timer = 0f;

    public delegate void CheckKey(string key);
    public CheckKey checkKey;

    public static InputManager instance { get; private set; }

    private void Update()
    {
        if (plyAnimationManager.GetKeyPressed())
        {
            timer += Time.deltaTime;

            if (timer >= 0.25f)
            {
                ResetParameter();
                timer = 0.0f;
            }
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

	void OnGUI()
	{
        if (isDragged) return;

        eventKeyPressed = Event.current;
		if (eventKeyPressed.type.Equals(EventType.KeyUp))
		{
			lastKeyPressed = eventKeyPressed.keyCode.ToString();
            checkKey.Invoke(lastKeyPressed);

            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Alphabet", lastKeyPressed.ToLower()[0] - 'a');
            FMODManager.instance.PlayOneShot(typingSound, transform.position);

            plyAnimationManager.SetKeyPressed(true);
            
        }
	}

    void ResetParameter()
    {
        plyAnimationManager.SetKeyPressed(false);
    }

    public void setDragged(bool value)
    {
        isDragged = value;
    }

}
