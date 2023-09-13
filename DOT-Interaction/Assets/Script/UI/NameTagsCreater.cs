using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DOT.Utilities;
using TMPro;
using Unity.VisualScripting;

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
        int[] xs = { -400, -150, 100, 350 };
        int x = 0;
        int y = 0;

        // Initialize Content Properties
        double row = Math.Ceiling(allInfo.Count / 4.0f);

        Vector2 rect2 = Content.GetComponent<RectTransform>().sizeDelta;
        Content.GetComponent<RectTransform>().sizeDelta = new Vector2(rect2.x, (float)row * 100);
        float topY = (float)(row - 1) * 50.0f;
        foreach (var info in allInfo)
        {
            GameObject nameButton = Instantiate(NameTagPrefab, Vector3.zero, Quaternion.identity, Content.transform);
            nameButton.GetComponent<CustomerInfo>().SetCustomerInfo(info.name, info.id);
            nameButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(xs[x], topY - y * 100);
            x++;
            if (x > 3)
            {
                x = 0;
                y++;
            }
        }
    }

    List<CInfo> CollectInfo(string info)
    {
        // Split the info based on their LF, CR or CRLF
        String[] nameIdPairs = info.Split(new[] {"\r\n", "\r", "\n"}, StringSplitOptions.None);
        List<CInfo> allInfo = new List<CInfo>();
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


}
