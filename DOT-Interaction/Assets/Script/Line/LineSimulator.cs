using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DOT.Line;
using UnityEngine;

public class LineSimulator : MonoBehaviour
{
    public LineRenderer lr;
    public PlayProcesses game;

    public List<GameObject> stage1Dots;
    public List<GameObject> stage2Dots;
    public List<GameObject> stage3Dots;
    private bool isPlaying = false;
    public float playDuration = 3.0f;
    private float playTime = 0.0f;
    
    void OnEnable()
    {
        
    }

    void OnDisable()
    {
        
    }

    IEnumerator PlayAnimation()
    {
        isPlaying = true;
        Vector3[] dotsPosition;
        List<GameObject> currentDots;
        lr.positionCount = 0;
        switch (game.GetLevel())
        {
            case 1: currentDots = stage1Dots; break;
            case 2: currentDots = stage2Dots; break;
            case 3: currentDots = stage3Dots; break;
            default: currentDots = null; break;
        }
        if (currentDots == null)
        {
            Debug.LogError("Level value is wrong!");
            throw new Exception("Level value is wrong!");
        }

        while (isPlaying)
        {
            yield return null;
        }
    }
}
