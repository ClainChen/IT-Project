using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullScreen : MonoBehaviour
{
    void Start()
    {
        Screen.fullScreen = true;
    }

    void Update()
    {
        if (Screen.fullScreen && Input.GetKeyDown(KeyCode.Escape))
        {
            Screen.fullScreen = false;
        }
    }

    public void Full()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
}
