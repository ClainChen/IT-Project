using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using DOT.UI;
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
        // Pre-initialize 预初始化组件
        [SerializeField] private LineRenderer line;
        [SerializeField] private GameObject region;

        // Check the state of Operation 操作条件
        private bool lineIsOn = false;

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

        

        // Start is called before the first frame update
        void Start()
        {
            text = GetComponent<ConnectTextController>();
            Instantiate(line, Vector3.zero, Quaternion.identity);
            Debug.Log("The line is at: " + line.gameObject.transform.position);
            // CloseBackgroundRegion();
            dotList = GameObject.FindGameObjectsWithTag("Matrix2").ToList();
            Debug.Log(dotList);

            fadeRate = Time.deltaTime / fadeTime;
            alphaKeys = line.colorGradient.alphaKeys;
            gradient = line.colorGradient;

        }

        // Update is called once per frame
        void Update()
        {
            if (!lineIsFading)
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

            if (lineIsFading)
            {
                FadeLine();
            }
        }

        // 鼠标左键按下后的行为
        // Actions after press down the left-mouse button
        void OnTouch()
        {
            SetLineAlpha(FadingPattern.One);
            Vector3 mousePosition = Utils.GetMouseWorldPosition();
            Vector3 startPosition = InRegion(mousePosition);
            if (!startPosition.Equals(Vector3.negativeInfinity))
            {
                lineIsOn = true;
                line.positionCount = 1;
                line.SetPosition(0, startPosition);
            }

        }

        // 鼠标左键抬起时的行为
        // Actions after lift up the left-mouse button
        void EndTouch()
        {
            lineIsOn = false;
            remainDots.Clear();
            text.ResetCoordinates();
            numTouchedDots = 0;

            // Line Fade Behaviour pre-settings
            fadingTime = fadeTime;
            lineIsFading = true;
            alphaKeys = line.colorGradient.alphaKeys;
            gradient = line.colorGradient;
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
                    line.positionCount = numTouchedDots + 1;
                    line.SetPosition(numTouchedDots, dot.transform.position);
                    remainDots.Remove(dot);
                    touchingDots.Add(dot);
                    text.AddCoordinates(dot.name.Substring(7));
                    numTouchedDots++;
                    break;
                }
            }
            // Debug.Log(mousePosition);
            line.positionCount = numTouchedDots + 1;
            line.SetPosition(numTouchedDots, mousePosition);

        }

        // 确认鼠标是否在区域和某一个点的碰撞范围内
        // Check whether the mouse is in the region or any dots
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
                        remainDots.Remove(dot);
                        touchingDots.Add(dot);
                        text.AddCoordinates(dot.name.Substring(7));
                        numTouchedDots += 1;
                        return dot.transform.position;
                    }
                }
            }
            return Vector3.negativeInfinity;
        }

        // 区域背景关闭
        // Close the region background
        void CloseBackgroundRegion()
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

        // 线的淡出
        // Fade out the line
        void FadeLine()
        {
            Debug.Log("Line is Fading!!");
            if (fadingTime > 0)
            {
                fadingTime -= Time.deltaTime;
                SetLineAlpha(FadingPattern.Decrease);
            }
            else
            {
                fadingTime = 0;
                line.positionCount = 0;
                SetLineAlpha(FadingPattern.Zero);
                lineIsFading = false;
            }
            
        }

        // 设置线的透明度
        // Set all of the alpha values of the line
        void SetLineAlpha(FadingPattern pattern)
        {
            float nextAlpha;
            switch (pattern)
            {
                case FadingPattern.Decrease: nextAlpha = alphaKeys[0].alpha - fadeRate; break;
                case FadingPattern.One: nextAlpha = 1.0f; break;
                case FadingPattern.Zero: nextAlpha = 0.0f; break;
                default: nextAlpha = 1.0f; break;
            }
            for (int i = 0; i < alphaKeys.Length; i++)
            {
                alphaKeys[i] = new GradientAlphaKey(nextAlpha, alphaKeys[i].time);
            }
            gradient.SetKeys(gradient.colorKeys, alphaKeys);
            line.colorGradient = gradient;
        }
    }

}
