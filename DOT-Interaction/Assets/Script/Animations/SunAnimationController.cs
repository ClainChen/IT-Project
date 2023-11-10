using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The animation for the sun, and the behaviour after click the sun
/// </summary>
public class SunAnimationController : MonoBehaviour
{
    public Animator animator;
    public float animateTime = 3.0f;

    public void Click()
    {
        StartCoroutine(PlayClickProcess());
    }

    IEnumerator PlayClickProcess()
    {
        animator.SetTrigger("Click");
        yield return new WaitForSeconds(animateTime);
        animator.SetTrigger("Normal");
    }
}
