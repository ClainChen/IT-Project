using System;
using System.Collections;
using System.Collections.Generic;
using DOT.Utilities;
using TMPro;
using UnityEngine;

public class ScreenSizeDetector : MonoBehaviour
{
    public TextMeshProUGUI tmp_screenSize;
    public TextMeshProUGUI tmp_mousePosition;

    // Update is called once per frame
    void Update()
    {
        String s = "Screen Size: " + Screen.width + " * " + Screen.height;
        tmp_screenSize.text = s;

        s = "GetWorldMousePosition: " + Utils.GetMouseWorldPosition();
        s += "\nMouse Screen Position: " + Utils.GetMouseScreenPosition();
        s += "\nInput Mouse Position: " + Input.mousePosition;
        tmp_mousePosition.text = s;
    }
}
