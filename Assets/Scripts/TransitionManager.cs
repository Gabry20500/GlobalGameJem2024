using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    bool isClosing;
    Animator animator;
    public static TransitionManager instance { get; private set; }

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

        DontDestroyOnLoad(this.gameObject);
        animator = GetComponent<Animator>();
    }


    public void PlayOpenAnimation()
    {
       
        animator.SetBool("openAnimation", true);
    }
    
    public void EndOpenAnimation()
    {
        animator.SetBool("openAnimation", false);
    }
    
    public void PlayCloseAnimation()
    {
        animator.SetBool("closeAnimation", true);
    }

    public void EndCloseAnimation()
    {
        animator.SetBool("closeAnimation", false);
    }
}
