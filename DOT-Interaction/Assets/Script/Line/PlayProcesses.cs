using System;
using System.Collections.Generic;
using DOT.Utilities;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Analytics;

namespace DOT.Line
{
    public class PlayProcesses : MonoBehaviour
    {
        public LineRendererController lineRendererController;
        public CustomerInfo customerInfo;
        public PageChange pageChange;
        public GameObject ButtonRetry;
        public GameObject ButtonNext;
        public TextMeshProUGUI Title;

        private int level = 1;
        private int levelScore = 2;
        public void ClickRetry()
        {
            lineRendererController.EraseLine();
            levelScore -= 1;
            DeactivateButtons();
        }

        public void ClickNext()
        {
            CheckResult();
            DeactivateButtons();
        }

        public void CheckResult()
        {
            List<GameObject> touchedLines = lineRendererController.GetTouchingLines();
            string[] checkingLines;
            switch (level)
            {
                case 1: checkingLines = Constants.DOTS_NORMAL; break;
                case 2: checkingLines = Constants.DOTS_VER_FLIP; break;
                case 3: checkingLines = Constants.DOTS_HOR_FLIP; break;
                default: throw new Exception("Level is invalid!");
            }

            bool pass = true;


            if (touchedLines.Count == checkingLines.Length)
            {
                for (int i = 0; i < touchedLines.Count; i++)
                {
                    string[] goName = touchedLines[i].name.Split(' ');
                    Debug.Log($"DOT: {goName[1]}, CheckLine: {checkingLines[i]}");
                    if (goName[1] != checkingLines[i])
                    {
                        pass = false;
                        Debug.Log("Fail!");
                        Play2Result();
                        return;
                    }
                }
            }
            else
            {
                pass = false;
                Debug.Log("Fail!");
                Play2Result();
                return;
            }

            if (pass)
            {
                Success();
            }
            
            if (level == 4)
            {
                Play2Result();
            }
        }

        void Success()
        {
            customerInfo.Score += levelScore;
            Debug.Log("Pass!");
            levelScore = 2;
            level++;
            lineRendererController.EraseLine();
            Title.text = $"Stage {level}";
            Debug.Log($"Score Now: {customerInfo.Score}");
            VGController.instance.PlaySound($"EnterS{level}");
        }

        void Play2Result()
        {
            level = 1;
            levelScore = 2;
            Title.text = $"Stage {level}";
            lineRendererController.EraseLine();
            pageChange.Play2Result();
        }

        public void ActivateButtons()
        {
            ButtonNext.SetActive(true);
            GetComponent<AudioSource>().enabled = false;
            if (levelScore != 2) return;
            ButtonRetry.SetActive(true);
        }

        public void DeactivateButtons()
        {
            ButtonNext.SetActive(false);
            ButtonRetry.SetActive(false);
            GetComponent<AudioSource>().enabled = true;
        }

        public int GetLevel()
        {
            return level;
        }
    }

}
