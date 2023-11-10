using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAnimationController : MonoBehaviour
{
    public Animator animator;

    public void Click()
    {
        StartCoroutine(ClickAnimatorProcess());
    }

    IEnumerator ClickAnimatorProcess()
    {
        animator.SetTrigger("Click");
        VGController.instance.PlayCatSound();
        yield return new WaitWhile(() => VGController.instance.voiceSource.isPlaying);
        animator.SetTrigger("Normal");
    }
}
