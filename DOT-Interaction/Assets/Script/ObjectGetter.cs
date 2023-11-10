using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DOT
{
    /// <summary>
    /// The static getter for some common game objects
    /// </summary>
    public class ObjectGetter : MonoBehaviour
    {
        /// <summary>
        /// The line in the left matrix
        /// </summary>
        public static GameObject lineLeft;

        /// <summary>
        /// The line in the right matrix
        /// </summary>
        public static GameObject lineRight;
        
        /// <summary>
        /// The matrix on the left
        /// </summary>
        public static GameObject matrixLeft;

        /// <summary>
        /// The matrix on the right
        /// </summary>
        public static GameObject matrixRight;

        /// <summary>
        /// The region of the matrix on the left
        /// </summary>
        public static GameObject regionLeft;

        /// <summary>
        /// The region of the matrix on the right
        /// </summary>
        public static GameObject regionRight;

        /// <summary>
        ///  All dots on the left
        /// </summary>
        public static List<GameObject> dotsLeft;

        /// <summary>
        /// All dots on the right
        /// </summary>
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
