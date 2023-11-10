using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Page controller, use to change pages.
/// </summary>
public class PageChange : MonoBehaviour
{
    public GameObject StartPage;
    public GameObject ScanPage;
    public GameObject SelectPage;
    public GameObject TypeInPage;
    public GameObject MenuPage;
    public GameObject TryPage;
    public GameObject PlayPage;
    public GameObject ResultPage;

    private Coroutine currentCoroutine;

    void Start()
    {
        StartCoroutine(PreprocessPages());
    }

    IEnumerator PreprocessPages()
    {
        yield return null;

        StartPage.SetActive(true);
        ScanPage.SetActive(false);
        SelectPage.SetActive(false);
        TypeInPage.SetActive(false);
        MenuPage.SetActive(false);
        TryPage.SetActive(false);
        PlayPage.SetActive(false);
        ResultPage.SetActive(false);
        PlaySound("StartPage");
    }

    public void Start2Scan()
    {
        StartPage.SetActive(false);
        ScanPage.SetActive(true);
        GetComponent<ClearNameTag>().ClearContent();
        PlaySound("ScanPage");
    }

    public void Scan2Select()
    {
        ScanPage.SetActive(false);
        SelectPage.SetActive(true);
        PlaySound("SelectPage");
    }

    public void Scan2TypeIn()
    {
        ScanPage.SetActive(false);
        TypeInPage.SetActive(true);
        PlaySound("TypeInPage");
    }

    public void Select2Menu()
    {
        SelectPage.SetActive(false);
        MenuPage.SetActive(true);
        GetComponent<ClearNameTag>().ClearContent();
        PlaySound("MenuPage");
        StartCoroutine(PlaySoundProcesses("MenuPage"));
    }

    public void Select2TypeIn()
    {
        SelectPage.SetActive(false);
        TypeInPage.SetActive(true);
        GetComponent<ClearNameTag>().ClearContent();
        PlaySound("TypeInPage");
    }

    public void Select2Scan()
    {
        SelectPage.SetActive(false);
        GetComponent<ClearNameTag>().ClearContent();
        ScanPage.SetActive(true);
        PlaySound("ScanPage");
    }

    public void TypeIn2Menu()
    {
        TypeInPage.SetActive(false);
        MenuPage.SetActive(true);
        PlaySound("MenuPage");
    }

    public void Menu2Try()
    {
        MenuPage.SetActive(false);
        TryPage.SetActive(true);
    }

    public void PlaySound(string name)
    {
        if (currentCoroutine != null) { StopCoroutine(currentCoroutine); }
        currentCoroutine = StartCoroutine(PlaySoundProcesses(name));
    }

    IEnumerator PlaySoundProcesses(string name)
    {
        VGController.instance.SetCatCanSpeak(false);
        VGController.instance.PlaySound(name);
        yield return new WaitWhile(() => VGController.instance.voiceSource.isPlaying);
        VGController.instance.SetCatCanSpeak(true);
    }

    public void Menu2Play()
    {
        MenuPage.SetActive(false);
        PlayPage.SetActive(true);
        if (currentCoroutine != null) { StopCoroutine(currentCoroutine); }
        currentCoroutine = StartCoroutine(MenuToPlay());
    }

    IEnumerator MenuToPlay()
    {
        MenuPage.SetActive(false);
        PlayPage.SetActive(true);
        VGController.instance.PlaySound("EnterPlayPage");
        yield return new WaitWhile(() => VGController.instance.voiceSource.isPlaying);
        VGController.instance.PlaySound("Reminder Finger");
    }

    public void Try2Menu()
    {
        TryPage.SetActive(false);
        MenuPage.SetActive(true);
    }

    public void Play2Result()
    {
        PlayPage.SetActive(false);
        ResultPage.SetActive(true);

        PlaySound("ResultPage");

        GameObject ci = GameObject.Find("SelectedInfo");
        if (ci == null) return;
        SendData sd = ci.GetComponent<SendData>();
        if (sd != null)
        {
            sd.SendJSON();
        }
    }

    public void Result2Start()
    {
        ResultPage.SetActive(false);
        StartPage.SetActive(true);
        PlaySound("StartPage");
    }
}
