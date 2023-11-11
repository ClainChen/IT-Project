using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The behaviour after click the name button
/// </summary>
public class SelectButtonClick : MonoBehaviour
{
    public void Click()
    {
        try
        {
            GameObject.Find("SelectedInfo").GetComponent<CustomerInfo>().SetCustomerInfo(GetComponent<CustomerInfo>());
            GameObject.Find("PageController").GetComponent<PageChange>().Select2Menu();
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
        
    }
}
