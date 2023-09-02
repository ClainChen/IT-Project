using System;
using DOT.Utilities;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace DOT.Line
{
    public class LineRendererController : MonoBehaviour
    {
        // Pre-initialize 预初始化组件
        private GameObject line;
        private LineRenderer lr;
        private GameObject region;

        // Dot determinations (Record) related variables 连接记录相关变量
        private List<GameObject> dotList;
        private List<GameObject> remainDots = new List<GameObject>();
        private List<GameObject> touchingDots = new List<GameObject>();
        private int numTouchedDots = 0;
        private bool mouseDown = true;

        // Start is called before the first frame update
        void Start()
        {
            line = ObjectGetter.lineRight;
            lr = line.GetComponent<LineRenderer>();
            region = ObjectGetter.regionRight;
            dotList = ObjectGetter.dotsRight;
        }

        // Update is called once per frame
        void Update()
        {
            UpdateLine();

            if (Input.GetButtonDown("Touch"))
            {
                OnTouch();
            }
            if (Input.GetButtonUp("Touch"))
            {
                EndTouch();
            }
            if (Input.GetButton("Touch"))
            {
                Touching();
            }
            

        }

        // 鼠标左键按下后的行为
        // Actions after press down the left-mouse button
        void OnTouch()
        {
            Vector3 mousePosition = Utils.GetMouseWorldPosition();
            EraseLine();
            bool inRegion = InRegion(mousePosition);
            if (inRegion)
            {
                mouseDown = true;
                Debug.Log("OnTouch");
                CancelInvoke("EraseLine");
            }

        }

        // 鼠标左键抬起时的行为
        // Actions after lift up the left-mouse button
        void EndTouch()
        {
            mouseDown = false;
            lr.positionCount = numTouchedDots;
            Invoke("EraseLine", 5f);
        }

        // 保持鼠标左键按下时的行为
        // Actions when the left-mouse button is holding
        void Touching()
        {
            Vector3 mousePosition = Utils.GetMouseWorldPosition();

            foreach (GameObject dot in remainDots)
            {
                Bounds bounds = dot.GetComponent<CircleCollider2D>().bounds;
                if (bounds.Contains(mousePosition))
                {
                    remainDots.Remove(dot);
                    touchingDots.Add(dot);
                    numTouchedDots++;
                    break;
                }
            }

        }

        // 确认鼠标是否在区域和某一个点的碰撞范围内
        // Check whether the mouse is in the region or any dots
        bool InRegion(Vector3 mousePosition)
        {
            Bounds bounds = region.GetComponent<BoxCollider2D>().bounds;
            if (bounds.Contains(mousePosition))
            {
                foreach (GameObject dot in dotList)
                {
                    bounds = dot.GetComponent<CircleCollider2D>().bounds;
                    if (bounds.Contains(mousePosition))
                    {
                        foreach (GameObject d in dotList)
                        {
                            remainDots.Add(d);
                        }
                        remainDots.Remove(dot);
                        touchingDots.Add(dot);
                        numTouchedDots++;
                        return true;
                    }
                }
            }

            return false;
        }

        void EraseLine()
        {
            numTouchedDots = 0;
            remainDots.Clear();
            touchingDots.Clear();
            lr.positionCount = 0;
        }

        void UpdateLine()
        {
            lr.positionCount = touchingDots.Count;
            int i = 0;
            if (lr.positionCount > 0)
            {
                Debug.Log("Touching");
                foreach (GameObject dot in touchingDots)
                {
                    lr.SetPosition(i, dot.transform.position);
                    i++;
                }
            }

            if (mouseDown)
            {
                lr.positionCount = numTouchedDots + 1;
                lr.SetPosition(numTouchedDots, Utils.GetMouseWorldPosition());
            }
        }
    }

}
