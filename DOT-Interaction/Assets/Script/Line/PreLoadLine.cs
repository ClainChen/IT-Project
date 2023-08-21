using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DOT.Utilities;

namespace DOT.Line
{
    public class PreLoadLine : MonoBehaviour
    {
        private GameObject line;

        // Start is called before the first frame update
        void Start()
        {
            line = GameObject.Find("LineLeft");
            LineRenderer lr = line.GetComponent<LineRenderer>();
            lr.positionCount = 0;
            List<GameObject> dotList = GameObject.FindGameObjectsWithTag("Matrix1").ToList();
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
