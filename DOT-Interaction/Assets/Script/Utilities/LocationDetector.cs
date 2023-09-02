using System;
using System.Collections;
using System.Collections.Generic;
using DOT.Utilities;
using TMPro;
using UnityEngine;

public class LocationDetector : MonoBehaviour
{
    public TextMeshProUGUI tmp_screenSize;
    public TextMeshProUGUI tmp_mousePosition;
    public TextMeshProUGUI tmp_objectPosition;

    public GameObject MatrixRight;
    public GameObject LeftUpDot;

    // Update is called once per frame
    void Update()
    {
        String s = "Screen Size: " + Screen.width + " * " + Screen.height;
        tmp_screenSize.text = s;

        s = "GetWorldMousePosition: " + Utils.GetMouseWorldPosition();
        s += "\nMouse Screen Position: " + Utils.GetMouseScreenPosition();
        s += "\nInput Mouse Position: " + Input.mousePosition;
        tmp_mousePosition.text = s;

        s = "Matrix Position: \n" + MatrixRight.transform.position;
        s += "\n Left Up Dot Position: \n" + LeftUpDot.transform.position;
        tmp_objectPosition.text = s;

    }
}
