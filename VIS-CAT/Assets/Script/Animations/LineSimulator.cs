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
    List<GameObject> currentDots;

    private bool isPlaying = false;
    public float speed = 140.0f;
    private Vector3 currentPosition;
    private Vector3 direction;

    void FixedUpdate()
    {
        if (!isPlaying) return;
        //Start Playing
        int i = lr.positionCount - 1;
        currentPosition += direction * speed * Time.deltaTime;
        lr.SetPosition(i, currentPosition);
        if ((currentDots[i].transform.position - currentPosition).magnitude <= 30.0f)
        {
            if (lr.positionCount == 7)
            {
                isPlaying = false;
                Debug.Log("Simulation End");
                return;
            }
            lr.positionCount++;
            lr.SetPosition(i, currentDots[i].transform.position);
            currentPosition = currentDots[i].transform.position;
            lr.SetPosition(i+1, currentPosition);
            direction = (currentDots[i+1].transform.position - currentDots[i].transform.position).normalized;
        }
    }

    public void StopPlaying()
    {
        isPlaying = false;
    }

    public void Play()
    {
        // Pre Initialize Process
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

        // Initialize the Start Point of the Line
        lr.positionCount = 1;
        lr.SetPosition(0, currentDots[0].transform.position);
        direction = (currentDots[1].transform.position - currentDots[0].transform.position).normalized;
        currentPosition = currentDots[0].transform.position;
        lr.positionCount = 2;
        lr.SetPosition(1, currentPosition);

        isPlaying = true;
        Debug.Log($"Simulation Start, now in level {game.GetLevel()}");
        VGController.instance.PlaySound($"S{transform.Find("Global Effect").GetComponent<PlayProcesses>().GetLevel()}Sim");
    }
}
