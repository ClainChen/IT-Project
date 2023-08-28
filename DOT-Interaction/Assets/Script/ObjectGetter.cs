using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DOT
{
    public class ObjectGetter : MonoBehaviour
    {
        [HideInInspector] public static GameObject lineLeft;
        [HideInInspector] public static GameObject lineRight;

        [HideInInspector] public static GameObject matrixLeft;
        [HideInInspector] public static GameObject matrixRight;

        [HideInInspector] public static GameObject regionLeft;
        [HideInInspector] public static GameObject regionRight;

        [HideInInspector] public static List<GameObject> dotsLeft;
        [HideInInspector] public static List<GameObject> dotsRight;

        // Start is called before the first frame update
        void Awake()
        {
            lineLeft = GameObject.Find("LineLeft");
            lineRight = GameObject.Find("LineRight");
            matrixLeft = GameObject.Find("Left");
            matrixRight = GameObject.Find("Right");
            regionLeft = GameObject.Find("RegionLeft");
            regionRight = GameObject.Find("RegionRight");
            dotsLeft = GameObject.FindGameObjectsWithTag("Matrix1").ToList();
            dotsRight = GameObject.FindGameObjectsWithTag("Matrix2").ToList();
        }
    }
}
