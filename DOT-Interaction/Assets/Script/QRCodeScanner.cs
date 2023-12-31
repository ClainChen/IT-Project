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
        if (webCamTexture != null)
        {
            webCamTexture.Stop();
            StopAllCoroutines();
        }
    }

    IEnumerator InitializeCam()
    {
        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
        
        // Only continue scanning if get the authorization
        if (Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            bool find = false;
            string cam = "OYT 8M AF USB Camera"; // The camera use for testing, do not needs to care about this
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

            // Only find the decided camera when in the unity editor
#if UNITY_EDITOR
            webCamTexture = find ? new WebCamTexture(cam, pixels, pixels) : new WebCamTexture(pixels, pixels);
#else
            // Get the backFacingCamera
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

    /// <summary>
    /// Scan the QR Code by recognize the webCamTexture before it render on the canvas.
    /// The WEBGL of Unity will cover a pure black background on the canvas at the first frame it rendering,
    /// so the scanning will be invalid if we just use reader.Decode().
    /// </summary>
    /// <returns></returns>
    IEnumerator ScanQRCode()
    {
        if (!webCamTexture.isPlaying)
        {
            webCamTexture.Play();
            imgRenderer.texture = webCamTexture;
        }
        BarcodeReader reader = new BarcodeReader();
        Texture tex = imgRenderer.texture; // Create a texture which store the webCamTexture
        Texture2D tex2d = new Texture2D(tex.width, tex.height, TextureFormat.RGBA32, false); // Create a texture2D use to determine the size of final texture
        RenderTexture currentRT = RenderTexture.active; // Create a RenderTexture and apply the current texture to it
        while (string.IsNullOrEmpty(QRCodeResult))
        {
            try
            {
                // The texture transition phases
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
                        QRCodeResult = string.Empty;
                        imgRenderer.texture = new Texture2D(tex.width, tex.height);
                        PageController.GetComponent<PageChange>().Scan2Select();
                        break;
                    }
                    
                }
                Destroy(renderTexture);
            }
            catch (Exception ex) { Debug.LogWarning(ex.Message); }
            yield return new WaitForSeconds(_readGap);
        }
        webCamTexture.Stop();
        
    }
}
