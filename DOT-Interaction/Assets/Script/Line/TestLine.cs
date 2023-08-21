// This is just a script us for do some simple testing, you do not need to pay your attention in this file.
// 这只是用来做一些简单测试的脚本，用在TestScene里面，你不用花精力在这个文件上面。

using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace DOT.Line
{
    public class TestLine : MonoBehaviour
    {
        public LineRenderer line;
        public float fadeTime = 5.0f;
        private float going;

        // Start is called before the first frame update
        void Start()
        {
            going = fadeTime;
        }

        // Update is called once per frame
        void Update()
        {
            Gradient gradient = line.colorGradient;
            GradientAlphaKey[] alphas = line.colorGradient.alphaKeys;
            if (going > 0)
            {
                for (int i = 0; i < gradient.alphaKeys.Length; i++)
                {
                    alphas[i] = new GradientAlphaKey(alphas[i].alpha - (Time.deltaTime / fadeTime), alphas[i].time);
                }
                gradient.SetKeys(gradient.colorKeys, alphas);
                going -= Time.deltaTime;
                line.colorGradient = gradient;
            }
            else
            {
                for (int i = 0; i < gradient.alphaKeys.Length; i++)
                {
                    alphas[i] = new GradientAlphaKey(0.0f, alphas[i].time);
                }
                gradient.SetKeys(gradient.colorKeys, alphas);
                line.colorGradient = gradient;
            }

        }
    }

}
