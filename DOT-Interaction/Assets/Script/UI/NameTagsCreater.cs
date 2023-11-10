using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DOT.Utilities;
using TMPro;
using Unity.VisualScripting;

/// <summary>
/// Create the name buttons after successfully scan the QR Code
/// </summary>
public class NameTagsCreater : MonoBehaviour
{
    public GameObject Content;
    public GameObject NameTagPrefab;

    private string _testInfo = Constants.EXAMPLE_QR2;

    struct CInfo
    {
        public string name { get; set; }
        public string id { get; set; }
    }

    public void CreateButtons(string scanInfo = null)
    {
        string actualInfo = string.IsNullOrEmpty(scanInfo) ? _testInfo : scanInfo;
        List<CInfo> allInfo = CollectInfo(actualInfo);
        if (allInfo.Count == 0) return; 
        int[] xs = { 180, 515, 850};
        int x = 0;
        int y = 0;

        // Initialize Content Properties
        double row = Math.Ceiling(allInfo.Count / 3.0f);

        Vector2 rect2 = Content.GetComponent<RectTransform>().sizeDelta;
        Content.GetComponent<RectTransform>().sizeDelta = new Vector2(rect2.x, (float)row * 180 + 10);
        float topY = -80.0f;
        foreach (var info in allInfo)
        {
            GameObject nameButton = Instantiate(NameTagPrefab, Vector3.zero, Quaternion.identity, Content.transform);
            nameButton.GetComponent<CustomerInfo>().SetCustomerInfo(info.name, info.id);
            nameButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(xs[x], topY - y * 180);
            x++;
            if (x > 2)
            {
                x = 0;
                y++;
            }
        }
    }

    List<CInfo> CollectInfo(string info)
    {
        try
        {
            // Split the info based on their LF, CR or CRLF
            String[] nameIdPairs = info.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            List<CInfo> allInfo = new List<CInfo>();

            List<String> nip = nameIdPairs.ToList();
            nip.RemoveAt(0);
            nameIdPairs = nip.ToArray();

            // Start Here!
            foreach (String pair in nameIdPairs)
            {
                if (pair.Length >= 3)
                {
                    String[] nameAndID = pair.Split(',');
                    CInfo newInfo = new CInfo()
                    {
                        name = nameAndID[0],
                        id = nameAndID[1]
                    };
                    allInfo.Add(newInfo);
                }
            }
            return allInfo;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            Debug.LogException(e);
            return new List<CInfo>();
        }
        
    }


}
