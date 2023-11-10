using System.Collections;
using System.Collections.Generic;
using DOT.Animations;
using DOT.Line;
using Unity.VisualScripting;
using UnityEngine;

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
    
    void PlayToSim()
    {
        Debug.Log("Change To Simulation");
        animator.SetBool("isPlayState", false);
        playState = false;
        GetComponent<DotBubbleAnimation>().enabled = false;
    }

    void SimToPlay()
    {
        Debug.Log("Change To Play");
        animator.SetBool("isPlayState", true);
        playState = true;
        GetComponent<DotBubbleAnimation>().enabled = true;
    }

}
