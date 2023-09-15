using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ZXing;


public class QRCodeScanner : MonoBehaviour
{
    private WebCamTexture webCamTexture = null;
    private RawImage imgRenderer = null;

    private float _readGap = 0.4f;
    private string QRCodeResult = string.Empty;

    public GameObject PageController;

    private int pixels = 512;

    // Start is called before the first frame update
    void OnEnable()
    {
        if (webCamTexture != null)
        {
            StartCoroutine(ScanQRCode());
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
            StopAllCoroutines();
        }
    }

    IEnumerator InitializeCam()
    {
        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
        
        if (Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            bool find = false;
            string cam = "OYT 8M AF USB Camera";
            string backFacingCamera = String.Empty;
            WebCamDevice[] devices = WebCamTexture.devices;
            foreach (var device in devices)
            {
                if (device.name == cam)
                {
                    find = true;
                    break;
                }

                if (!device.isFrontFacing)
                {
                    backFacingCamera = device.name;
                }
            }
            
#if UNITY_EDITOR
            webCamTexture = find ? new WebCamTexture(cam, pixels, pixels) : new WebCamTexture(pixels, pixels);
#else
            if (!string.IsNullOrEmpty(backFacingCamera))
            {
                webCamTexture = new WebCamTexture(backFacingCamera, pixels, pixels);
            }
            else
            {
                webCamTexture = new WebCamTexture(pixels, pixels);
            }
#endif
            imgRenderer = GetComponent<RawImage>();
            imgRenderer.texture = webCamTexture;
            webCamTexture.Play();
            while (webCamTexture.width < 200)
            {
                yield return null;
            }
            StartCoroutine(ScanQRCode());
        }
        else
        {
            Debug.Log("WebCam not found");
        }
    }

    IEnumerator ScanQRCode()
    {
        BarcodeReader reader = new BarcodeReader();
        webCamTexture.Play();
        Texture tex = imgRenderer.texture;
        Texture2D tex2d = new Texture2D(tex.width, tex.height, TextureFormat.RGBA32, false);
        RenderTexture currentRT = RenderTexture.active;
        while (string.IsNullOrEmpty(QRCodeResult))
        {
            try
            {
                RenderTexture renderTexture = new RenderTexture(tex.width, tex.height, 32);
                Graphics.Blit(tex, renderTexture);
                RenderTexture.active = renderTexture;
                tex2d.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
                tex2d.Apply();
                var Result = reader.Decode(tex2d.GetPixels32(), tex2d.width, tex2d.height);
                RenderTexture.active = currentRT;
                if (Result != null)
                {
                    QRCodeResult = Result.Text;
                    if (!string.IsNullOrEmpty(QRCodeResult))
                    {
                        Debug.Log("DECODED TEXT FROM QR: " + QRCodeResult);
                        PageController.GetComponent<NameTagsCreater>().CreateButtons(QRCodeResult);
                        PageController.GetComponent<PageChange>().Scan2Select();
                        break;
                    }
                    
                }
                Debug.Log("No Result!");
                Destroy(renderTexture);
            }
            catch (Exception ex) { Debug.LogWarning(ex.Message); }
            yield return new WaitForSeconds(_readGap);
        }
        webCamTexture.Stop();
    }
}
