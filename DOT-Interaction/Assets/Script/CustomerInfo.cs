using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CustomerInfo : MonoBehaviour
{
    private string childName;
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
    public string ID
    {
        get => id.ToString();
        set => id = int.Parse(value);
    }

    public int Score{ get; set; }

    public void SetCustomerInfo(string name, string id)
    {
        Name = name;
        ID = id;
        Score = 0;
    }

    public void SetCustomerInfo(CustomerInfo info)
    {
        Name = info.Name;
        ID = info.ID;
        Score = 0;
    }

}
