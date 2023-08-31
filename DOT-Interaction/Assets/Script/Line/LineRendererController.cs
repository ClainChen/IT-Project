using System;
using DOT.UI;
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

        // The fading related variables 淡出相关变量
        private bool lineIsFading = false;
        [SerializeField] private float fadeTime = 1;
        private float fadeRate;
        private float fadingTime;
        private GradientAlphaKey[] alphaKeys;
        private Gradient gradient;

        private enum FadingPattern
        {
            Decrease,
            Zero,
            One
        };

        // Dot determinations (Record) related variables 连接记录相关变量
        private List<GameObject> dotList;
        private List<GameObject> remainDots = new List<GameObject>();
        private List<GameObject> touchingDots = new List<GameObject>();
        private int numTouchedDots = 0;
        private ConnectTextController text;

        public TextMeshProUGUI t;

        // Start is called before the first frame update
        void Start()
        {
            line = ObjectGetter.lineRight;
            lr = line.GetComponent<LineRenderer>();
            region = ObjectGetter.regionRight;
            text = GetComponent<ConnectTextController>();
            dotList = GameObject.FindGameObjectsWithTag("Matrix2").ToList();

            Debug.Log(lr.transform.parent.gameObject);
            Debug.Log(lr.transform.parent.localPosition);
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
            Vector3 startPosition = InRegion(mousePosition);
            if (!startPosition.Equals(Vector3.negativeInfinity))
            {
                lr.positionCount = 1;
                lr.SetPosition(0, startPosition);
                CancelInvoke("EraseLine");
            }

        }

        // 鼠标左键抬起时的行为
        // Actions after lift up the left-mouse button
        void EndTouch()
        {
            remainDots.Clear();
            text.ResetCoordinates();
            numTouchedDots = 0;
            lr.positionCount--;
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
                    lr.positionCount = numTouchedDots + 1;
                    lr.SetPosition(numTouchedDots,
                        dot.transform.localPosition * dot.transform.parent.localScale.x);
                    remainDots.Remove(dot);
                    touchingDots.Add(dot);
                    text.AddCoordinates(dot.name.Substring(7));
                    numTouchedDots++;
                    Debug.Log("Add Dot!");
                    break;
                }
            }

            lr.positionCount = numTouchedDots + 1;
            Vector3 mousePos = Utils.GetMouseScreenPosition();
            lr.SetPosition(numTouchedDots, mousePos - lr.transform.parent.localPosition);

        }

        // 确认鼠标是否在区域和某一个点的碰撞范围内
        // Check whether the mouse is in the region or any dots
        Vector3 InRegion(Vector3 mousePosition)
        {
            Bounds bounds = region.GetComponent<BoxCollider2D>().bounds;
            if (bounds.Contains(mousePosition))
            {
                Debug.Log("Here!");
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
                        text.AddCoordinates(dot.name.Substring(7));
                        numTouchedDots += 1;
                        return dot.transform.localPosition * dot.transform.parent.localScale.x;
                    }
                }
            }
            return Vector3.negativeInfinity;
        }

        void EraseLine()
        {
            lr.positionCount = 0;
        }
    }

}
