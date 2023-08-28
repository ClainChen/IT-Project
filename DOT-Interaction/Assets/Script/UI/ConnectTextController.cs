using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEditor.Search;
using UnityEngine;
using DOT.Utilities;

namespace DOT.UI
{
    public class ConnectTextController : MonoBehaviour
    {
        private GameObject tmpObjectBottom;
        private GameObject tmpObjectTop;
        private TextMeshProUGUI bottomTexts;
        private TextMeshProUGUI topTexts;
        private List<String> coordinates = new List<String>();
        private List<List<String>> coordinatesHistory = new List<List<String>>();
        private List<String> resultHistory = new List<String>();

        void Awake()
        {
            tmpObjectBottom = GameObject.Find("Text Bottom");
            tmpObjectTop = GameObject.Find("Text Top");
            if (tmpObjectBottom.TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI tmpb))
            {
                bottomTexts = tmpb;
            }
            else
            {
                Debug.LogError("Bottom Text MeshPro not Found!");
            }

            if (tmpObjectTop.TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI tmpu))
            {
                topTexts = tmpu;
            }
            else
            {
                Debug.LogError("Top Text MeshPro not Found!");
            }
        }

        public void AddCoordinates(String s)
        {
            coordinates.Add(s);
            UpdateBottomText();
        }

        public void ResetCoordinates()
        {
            AddNewHistory();
            UpdateUpperText();
            coordinates.Clear();
            UpdateBottomText();
            
        }

        private void AddNewHistory()
        {
            String result = CheckResult();
            String[] coords = new string[coordinates.Count];
            coordinates.CopyTo(coords);
            if (coordinatesHistory.Count > 5)
            {
                coordinatesHistory.RemoveAt(0);
                resultHistory.RemoveAt(0);
            }
            resultHistory.Add(result);
            coordinatesHistory.Add(coords.ToList());
        }

        private String CheckResult()
        {
            if (coordinates.Count == Constants.PRE_LOAD_DOTS.Length)
            {
                for (int i = 0; i < coordinates.Count; i++)
                {
                    if (!coordinates[i].Equals(Constants.PRE_LOAD_DOTS[i]))
                    {
                        return "Wrong";
                    }
                }

                return "Correct";
            }
            else
            {
                return "Wrong";
            }

        }

        void UpdateBottomText()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Connections: ");
            if (coordinates.Count != 0)
            {
                foreach (String s in coordinates)
                {
                    sb.Append(s).Append("; ");
                }
            }
            else
            {
                sb.Append("No Connection");
            }
            bottomTexts.text = sb.ToString();
        }

        void UpdateUpperText()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Connection Histories: \n");
            if (coordinatesHistory.Count != 0)
            {
                for (int i = 0; i < coordinatesHistory.Count; i++)
                {
                    foreach (var coord in coordinatesHistory[i])
                    {
                        sb.Append(coord).Append("; ");
                    }
                    sb.Append("    ").Append(resultHistory[i]).Append('\n');
                }
            }
            else
            {
                sb.Append("No History");
            }
            topTexts.text = sb.ToString();
        }
    }
}

