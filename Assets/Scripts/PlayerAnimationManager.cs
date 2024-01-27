using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{

    public Animator playerAnimator;

    public void SetKeyPressed(bool value)
    {
        playerAnimator.SetBool("KeyPressed", value);
    }

    public void SetDrugged(bool value)
    {
        playerAnimator.SetBool("isDrugged", value);
    }

    public bool GetKeyPressed() { return playerAnimator.GetBool("KeyPressed"); }
}
