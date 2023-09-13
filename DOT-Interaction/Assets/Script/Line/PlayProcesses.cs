using System;
using System.Collections.Generic;
using DOT.Utilities;
using UnityEditor;
using UnityEngine;

namespace DOT.Line
{
    public class PlayProcesses : MonoBehaviour
    {
        public LineRendererController lineRendererController;
        public CustomerInfo customerInfo;
        public PageChange pageChange;
        public GameObject ButtonRetry;
        public GameObject ButtonNext;

        private int level = 1;
        private int levelScore = 2;
        public void ClickRetry()
        {
            lineRendererController.EraseLine();
            levelScore = levelScore - 1 < 0 ? 0 : levelScore - 1;
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
                        break;
                    }
                }
            }
            else
            {
                pass = false;
                Debug.Log("Fail!");
            }

            if (pass)
            {
                customerInfo.Score += levelScore;
                Debug.Log("Pass!");
            }
            levelScore = 2;
            level++;
            lineRendererController.EraseLine();
            Debug.Log($"Score Now: {customerInfo.Score}");
            if (level == 4)
            {
                pageChange.Play2Result();
            }
        }

        public void ActivateButtons()
        {
            ButtonNext.SetActive(true);
            ButtonRetry.SetActive(true);
        }

        public void DeactivateButtons()
        {
            ButtonNext.SetActive(false);
            ButtonRetry.SetActive(false);
        }
    }

}
