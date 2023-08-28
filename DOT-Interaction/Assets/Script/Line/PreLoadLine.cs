using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DOT.Utilities;
using DOT;

namespace DOT.Line
{
    public class PreLoadLine : MonoBehaviour
    {
        private GameObject line;

        void Awake()
        {
            line = ObjectGetter.lineLeft;
        }

        // Start is called before the first frame update
        void Start()
        {
            LineRenderer lr = line.GetComponent<LineRenderer>();
            lr.positionCount = 0;
            List<GameObject> dotList = ObjectGetter.dotsLeft;
            for (int i = 0; i < Constants.PRE_LOAD_DOTS.Length; i++)
            {
                foreach (GameObject go in dotList)
                {
                    if (go.name.EndsWith(Constants.PRE_LOAD_DOTS[i]))
                    {
                        lr.positionCount += 1;
                        lr.SetPosition(i, go.transform.position);
                    }
                }
            }
        }
    }

}
