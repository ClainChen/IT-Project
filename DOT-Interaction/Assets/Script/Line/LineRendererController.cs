using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using DOT.Utilities;
using UnityEngine.InputSystem;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

namespace DOT.Line
{
    public class LineRendererController : MonoBehaviour
    {
        [SerializeField]
        private LineRenderer line;
        [SerializeField]
        private GameObject region;

        private bool lineIsOn = false;


        // Start is called before the first frame update
        void Start()
        {
            Instantiate(line, Vector3.zero, Quaternion.identity);
            Debug.Log("The line is at: " + line.gameObject.transform.position);
            if (region.TryGetComponent(out SpriteRenderer render))
            {
                Color c = render.color;
                render.color = new Color(c.r, c.g, c.b, 0);
            }
            else
            {
                Debug.Log("Didn't found sprite renderer!");
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown("Touch"))
            {
                OnTouch();
            }
            if (Input.GetButtonUp("Touch"))
            {
                EndTouch();
            }
            if (lineIsOn && Input.GetButton("Touch"))
            {
                Vector3 mousePosition = IUtils.GetMouseWorldPosition();
                // Debug.Log(mousePosition);
                line.positionCount = 2;
                line.SetPosition(1, mousePosition);
            }
        }

        void OnTouch()
        {
            Vector3 mousePosition = IUtils.GetMouseWorldPosition();
            if (InRegion(mousePosition))
            {

            }
            lineIsOn = true;
            
            Debug.Log(mousePosition);
            line.positionCount = 1;
            line.SetPosition(0, mousePosition);
            
        }

        void EndTouch()
        {
            lineIsOn = false;
            line.positionCount = 0;

        }

        bool InRegion(Vector3 mousePosition)
        {

        }
    }

}
