using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    public void Start2Scan()
    {
        StartPage.SetActive(false);
        ScanPage.SetActive(true);
    }

    public void Scan2Select()
    {
        ScanPage.SetActive(false);
        SelectPage.SetActive(true);
    }

    public void Scan2TypeIn()
    {
        ScanPage.SetActive(false);
        TypeInPage.SetActive(true);
    }

    public void Select2Menu()
    {
        SelectPage.SetActive(false);
        MenuPage.SetActive(true);
    }

    public void TypeIn2Menu()
    {
        TypeInPage.SetActive(false);
        MenuPage.SetActive(true);
    }

    public void Menu2Try()
    {
        MenuPage.SetActive(false);
        TryPage.SetActive(true);
    }

    public void Menu2Play()
    {
        MenuPage.SetActive(false);
        PlayPage.SetActive(true);
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
    }

    public void Result2Start()
    {
        ResultPage.SetActive(false);
        StartPage.SetActive(true);
    }
}
