using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEditor.Search;
using UnityEngine;

namespace DOT.UI
{
    public class ConnectTextController : MonoBehaviour
    {
        [SerializeField] private GameObject tmpObject;
        private TextMeshProUGUI texts;
        private List<String> coordinates = new List<String>();

        void Start()
        {
            if (tmpObject.TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI tmp))
            {
                texts = tmp;
            }
            else
            {
                Debug.LogError("Text MeshPro not Found!");
            }
        }

        public void AddCoordinates(String s)
        {
            coordinates.Add(s);
            UpdateText();
        }

        public void ResetCoordinates()
        {
            coordinates.Clear();
            UpdateText();
        }

        void UpdateText()
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
            texts.text = sb.ToString();
        }
    }
}

