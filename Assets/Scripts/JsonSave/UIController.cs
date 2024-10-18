using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JsonSave
{
    public class UIController : MonoBehaviour
    {
        public TMP_InputField playerName;
        public TMP_InputField id;
        public TMP_InputField health;
        public TMP_InputField speed;

        public TMP_Text dataTxt;
        public Button updateBtn;

        private LoadSystem loadSystem;
        private void Start()
        {
            loadSystem = new LoadSystem();

            //Data Update
            updateBtn.onClick.AddListener(UpdateData);
            dataTxt.text = loadSystem.GetData();
        }

        void UpdateData()
        {
            int value = 0;
            if (!string.IsNullOrWhiteSpace(playerName.text))
            {
                loadSystem.UpdateName(playerName.text);
            }

            if (!string.IsNullOrWhiteSpace(id.text))
            {
                value = Int32.Parse(id.text);
                loadSystem.UpdateId(value);
            }

            if (!string.IsNullOrWhiteSpace(health.text))
            {
                value = Int32.Parse(health.text);
                loadSystem.UpdateHealth(value);
            }

            if (!string.IsNullOrWhiteSpace(speed.text))
            {
                value = Int32.Parse(speed.text);
                loadSystem.UpdateSpeed(value);
            }

            dataTxt.text = loadSystem.GetData();
        }


    }
}
