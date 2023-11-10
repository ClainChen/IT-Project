using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
