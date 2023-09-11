using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ZXing;
using ZXing.QrCode;

public class QRCodeScanner : MonoBehaviour
{
    private WebCamTexture webCamTexture = null;
    private RawImage imgRenderer = null;

    private float _readGap = 1f;
    private string QRCodeResult = string.Empty;

    public TextMeshProUGUI textOnGUI;

    // Start is called before the first frame update
    void OnEnable()
    {
        if (webCamTexture != null)
        {
            StartCoroutine(ScanQRCode());
            Debug.Log("Start Scan");
        }
        else
        {
            StartCoroutine(InitializeCam());
        }
        
    }

    void OnDisable()
    {
        if (webCamTexture != null && webCamTexture.isPlaying)
        {
            webCamTexture.Stop();
        }
    }

    IEnumerator InitializeCam()
    {
        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
        if (Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            webCamTexture = new WebCamTexture(250, 280);
            imgRenderer = GetComponent<RawImage>();
            imgRenderer.texture = webCamTexture;
            StartCoroutine(ScanQRCode());
        }
        else
        {
            Debug.Log("WebCam not found");
        }
    }

    IEnumerator ScanQRCode()
    {
        IBarcodeReader reader = new BarcodeReader();
        webCamTexture.Play();
        Debug.Log($"{webCamTexture.width}, {webCamTexture.height}");
        var snap = new Texture2D(250, 280, TextureFormat.ARGB32, false);
        Debug.Log($"{snap.width}, {snap.height}");
        while (string.IsNullOrEmpty(QRCodeResult))
        {
            try
            {
                snap.SetPixels32(webCamTexture.GetPixels32());
                var Result = reader.Decode(snap.GetRawTextureData(), webCamTexture.width, webCamTexture.height, RGBLuminanceSource.BitmapFormat.ARGB32);
                if (Result != null)
                {
                    QRCodeResult = Result.Text;
                    textOnGUI.text = QRCodeResult;
                    if (!string.IsNullOrEmpty(QRCodeResult))
                    {
                        Debug.Log("DECODED TEXT FROM QR: " + QRCodeResult);
                        break;
                    }
                }
                Debug.Log("No Result!");
            }
            catch (Exception ex) { Debug.LogWarning(ex.Message); }
            yield return null;
        }
        webCamTexture.Stop();
    }
}
