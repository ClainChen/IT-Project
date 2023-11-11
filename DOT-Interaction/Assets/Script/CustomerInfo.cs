using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// The information about the customer
/// </summary>
public class CustomerInfo : MonoBehaviour
{
    private string childName;

    /// <summary>
    /// The property of childName,
    /// when change th childName, the NameTag of this current gameObject will be changed simultaneously
    /// </summary>
    public string Name
    {
        get => childName;
        set
        {
            childName = value;
            if (transform.Find("NameTag") != null)
            {
                if (transform.Find("NameTag").TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI tmp))
                {
                    tmp.text = childName;
                }
            }
        }
    }

    private int id;

    /// <summary>
    /// The property of id,
    /// when change ID, it will check whether the input id is integer
    /// </summary>
    public string ID
    {
        get => id.ToString();
        set
        {
            try
            {
                id = int.Parse(value);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Debug.LogException(e);
            }
            
        }
    }

    public int Score{ get; set; }

    /// <summary>
    /// Setup the customer Info based on name and id
    /// </summary>
    /// <param name="name"></param>
    /// <param name="id"></param>
    public void SetCustomerInfo(string name, string id)
    {
        Name = name;
        ID = id;
        Score = 0;
    }

    /// <summary>
    /// Setup the customer Info based on another info
    /// </summary>
    /// <param name="info"></param>
    public void SetCustomerInfo(CustomerInfo info)
    {
        Name = info.Name;
        ID = info.ID;
        Score = 0;
    }

}
