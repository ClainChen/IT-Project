using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clear all name button after exit the select page
/// </summary>
public class ClearNameTag : MonoBehaviour
{
    public GameObject Content;

    public void ClearContent()
    {
        int length = Content.transform.childCount;
        if (length > 0)
        {
            int i = 0;
            while (i < length)
            {
                Destroy(Content.transform.GetChild(i).gameObject);
                i++;
            }
            Debug.Log("Content Cleared!");
        }
    }
}
