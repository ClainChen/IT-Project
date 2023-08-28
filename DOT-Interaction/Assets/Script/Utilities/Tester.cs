using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DOT.Utilities
{
    public class Tester : MonoBehaviour
    {
        private List<GameObject> dotList;
        public bool activateTest = false;

        // Start is called before the first frame update
        void Start()
        {
            if (activateTest)
            {
                dotList = GameObject.FindGameObjectsWithTag("Matrix1").ToList();
                foreach (GameObject go in dotList)
                {
                    Debug.Log(go.name);
                }
            }

        }
    }

}
