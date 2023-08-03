using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DOT.Utilities;

public class PreLoadLine : MonoBehaviour
{
    [SerializeField] private LineRenderer line;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(line, Vector3.zero, Quaternion.identity);
        line.positionCount = 0;
        List<GameObject> dotList = GameObject.FindGameObjectsWithTag("Matrix1").ToList();
        for (int i = 0; i < Constants.PRE_LOAD_DOTS.Length; i++)
        {
            foreach (GameObject go in dotList)
            {
                if (go.name.EndsWith(Constants.PRE_LOAD_DOTS[i]))
                {
                    line.positionCount += 1;
                    line.SetPosition(i, go.transform.position);
                    Debug.Log("Add dot [" + go.name + "] to the line's position");
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
