using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Send the data to the backend server.
/// </summary>
public class SendData : MonoBehaviour
{
    private List<string> serverUrls = new List<string>();

    void Start()
    {
        serverUrls.Add("http://viscat.shop:5002/api/auth/score");
    }

    [Serializable]
    private class Data
    {
        public string studentId;
        public string testScore;
    }

    public void SendJSON()
    {
        StartCoroutine(SendJSONData());
    }

    public IEnumerator SendJSONData()
    {

        CustomerInfo ci = GetComponent<CustomerInfo>();

        Data data = new Data()
        {
            studentId = ci.ID,
            testScore = ci.Score.ToString()
        };

        string json = JsonUtility.ToJson(data);

        UnityWebRequest request = new UnityWebRequest(serverUrls[0], "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || 
            request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + request.error);
        }
        else
        {
            Debug.Log("Server response: " + request.downloadHandler.text);
        }
    }
}
