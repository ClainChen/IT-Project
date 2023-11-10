using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DOT
{
    public class ObjectGetter : MonoBehaviour
    {
        public static GameObject lineLeft;
        public static GameObject lineRight;

        public static GameObject matrixLeft;
        public static GameObject matrixRight;

        public static GameObject regionLeft;
        public static GameObject regionRight;

        public static List<GameObject> dotsLeft;
        public static List<GameObject> dotsRight;

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

            Debug.Log(regionRight.transform.position);
        }
    }
}
