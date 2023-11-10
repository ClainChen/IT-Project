using System;
using System.Collections.Generic;
using DOT.Utilities;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Analytics;

namespace DOT.Line
{
    /// <summary>
    /// The process logic when playing the game,
    /// include in the level change, score counting and success, fail determinations.
    /// </summary>
    public class PlayProcesses : MonoBehaviour
    {
        public LineRendererController lineRendererController;
        public CustomerInfo customerInfo;
        public PageChange pageChange;
        public GameObject ButtonRetry;
        public GameObject ButtonNext;
        public TextMeshProUGUI Title;

        private List<string> levelNames = new()
        {
            "Regular Pattern",
            "Vertical Flip",
            "Horizontal Flip"
        };

        // The control whether the current process is quick test
        public bool IsQuickTest = false;

        private int level = 1;
        private int levelScore = 2;

        // Behaviour after click retry button
        public void ClickRetry()
        {
            lineRendererController.EraseLine();
            levelScore -= 1;
            DeactivateButtons();
        }


        // Behaviour afterclick next button
        // Aiming to check whether the input is correct and continue to the next level
        public void ClickNext()
        {
            CheckResult();
            DeactivateButtons();
        }

        public void ChangeQuickTest()
        {
            IsQuickTest = !IsQuickTest;
        }

        // Initialize the game state and description
        public void Initialize()
        {
            Title.text = levelNames[0];
            level = 1;
            levelScore = 2;
        }

        public void CheckResult()
        {
            
            List<GameObject> touchedLines = lineRendererController.GetTouchingLines();
            string[] checkingLines;
            if (!IsQuickTest)
            {
                // Pharse the target matrix pattern to current checking matrix
                switch (level)
                {
                    case 1: checkingLines = Constants.DOTS_NORMAL; break;
                    case 2: checkingLines = Constants.DOTS_VER_FLIP; break;
                    case 3: checkingLines = Constants.DOTS_HOR_FLIP; break;
                    default: throw new Exception("Level is invalid!");
                }
            }
            else
            {
                checkingLines = Constants.DOTS_NORMAL;
            }
            
            bool pass = true;

            // Check the input pattern and the target pattern
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
                if (!IsQuickTest)
                {
                    Success();
                }
                else
                {
                    Debug.Log("QuickTest Finished!");
                    Play2Result();
                }
                
            }
            
            if (level == 4)
            {
                Play2Result();
            }
        }

        // The behaviour after success the current level
        // Add score and go to next level
        void Success()
        {
            customerInfo.Score += levelScore;
            Debug.Log("Pass!");
            levelScore = 2;
            level++;
            lineRendererController.EraseLine();
            Title.text = levelNames[level - 1];
            Debug.Log($"Score Now: {customerInfo.Score}");
            VGController.instance.PlaySound($"EnterS{level}");
        }

        // Change the page from play page to result page
        void Play2Result()
        {
            level = 1;
            levelScore = 2;
            Title.text = levelNames[0];
            lineRendererController.EraseLine();
            pageChange.Play2Result();
        }

        // Activate next and retry buttons
        public void ActivateButtons()
        {
            ButtonNext.SetActive(true);
            GetComponent<AudioSource>().enabled = false;
            if (levelScore != 2) return;
            ButtonRetry.SetActive(true);
        }

        // Deactivate next and retry buttons
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
