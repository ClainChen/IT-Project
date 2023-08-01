using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        [SerializeField] private LineRenderer line;
        [SerializeField] private GameObject region;

        private bool lineIsOn = false;
        private List<GameObject> dotList;
        private List<GameObject> remainDots = new List<GameObject>();
        private int numTouchedDots = 0;

        // Start is called before the first frame update
        void Start()
        {
            Instantiate(line, Vector3.zero, Quaternion.identity);
            Debug.Log("The line is at: " + line.gameObject.transform.position);
            // FadeRegion();
            dotList = GameObject.FindGameObjectsWithTag("Matrix1").ToList();
            Debug.Log(dotList);

        }

        // Update is called once per frame
        void Update()
        {
            if (!lineIsOn && Input.GetButtonDown("Touch"))
            {
                OnTouch();
            }
            if (lineIsOn && Input.GetButtonUp("Touch"))
            {
                EndTouch();
            }
            if (lineIsOn && Input.GetButton("Touch"))
            {
                Touching();
            }
        }

        // 鼠标左键按下后的行为
        void OnTouch()
        {
            Vector3 mousePosition = IUtils.GetMouseWorldPosition();
            Vector3 startPosition = InRegion(mousePosition);
            if (!startPosition.Equals(Vector3.negativeInfinity))
            {
                lineIsOn = true;
                line.positionCount = 1;
                line.SetPosition(0, startPosition);
            }

        }

        // 鼠标左键抬起时的行为
        void EndTouch()
        {
            lineIsOn = false;
            line.positionCount = 0;
            remainDots.Clear();
            numTouchedDots = 0;
        }

        // 保持鼠标左键按下时的行为
        void Touching()
        {
            Vector3 mousePosition = IUtils.GetMouseWorldPosition();
            foreach (GameObject dot in remainDots)
            {
                Bounds bounds = dot.GetComponent<CircleCollider2D>().bounds;
                if (bounds.Contains(mousePosition))
                {
                    line.positionCount = numTouchedDots + 1;
                    line.SetPosition(numTouchedDots, dot.transform.position);
                    remainDots.Remove(dot);
                    numTouchedDots++;
                    break;
                }
            }
            // Debug.Log(mousePosition);
            line.positionCount = numTouchedDots + 1;
            line.SetPosition(numTouchedDots, mousePosition);

        }

        // 确认鼠标是否在区域和某一个点的碰撞范围内
        Vector3 InRegion(Vector3 mousePosition)
        {
            Bounds bounds = region.GetComponent<BoxCollider2D>().bounds;
            if (bounds.Contains(mousePosition))
            {
                foreach (GameObject dot in dotList)
                {
                    bounds = dot.GetComponent<CircleCollider2D>().bounds;
                    if (bounds.Contains(mousePosition))
                    {
                        Debug.Log(bounds);
                        Debug.Log(dot.transform.position);
                        foreach (GameObject d in dotList)
                        {
                            remainDots.Add(d);
                        }
                        numTouchedDots += 1;
                        return dot.transform.position;
                    }
                }
            }
            return Vector3.negativeInfinity;
        }

        // 区域背景关闭
        void FadeRegion()
        {
            if (region.TryGetComponent(out SpriteRenderer render))
            {
                Color c = render.color;
                render.color = new Color(c.r, c.g, c.b, 0);
            }
            else
            {
                Debug.Log("Didn't found sprite renderer!");
            }
            if (region.TryGetComponent(out Collider2D c2d))
            {
                Debug.Log(region.GetComponent<Collider2D>().bounds);
            }
        }
    }

}
