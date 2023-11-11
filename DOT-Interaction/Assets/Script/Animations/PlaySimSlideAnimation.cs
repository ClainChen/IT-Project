using System.Collections;
using System.Collections.Generic;
using DOT.Animations;
using DOT.Line;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// The animation controller for the simulation matrix slides in or out the canvas.
/// </summary>
public class PlaySimSlideAnimation : MonoBehaviour
{
    public Animator animator;
    private bool playState = true;

    public void Transition()
    {
        if (playState)
        {
            PlayToSim();
        }
        else
        {
            SimToPlay();
            VGController.instance.StopSound();
        }
    }
    
    // Slides in the simulation matrix
    void PlayToSim()
    {
        Debug.Log("Change To Simulation");
        animator.SetBool("isPlayState", false);
        playState = false;
        GetComponent<DotBubbleAnimation>().enabled = false;
    }

    // Slides out the simulationo matrix
    void SimToPlay()
    {
        Debug.Log("Change To Play");
        animator.SetBool("isPlayState", true);
        playState = true;
        GetComponent<DotBubbleAnimation>().enabled = true;
    }

}
