using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

namespace JsonSave
{
    public class LoadSystem
    {
        public string _filePath = Application.persistentDataPath + "/gameData.json";

        public JsonData LoadGame()
        {
            if (File.Exists(_filePath))
            {
                string json = File.ReadAllText(_filePath);
                JsonData data = JsonUtility.FromJson<JsonData>(json);
                return data;
            }
            else
            {
                Debug.Log("No Data Found!!");
                return null;
            }
        }

        public string GetData()
        {
            string json = File.ReadAllText(_filePath);
            JsonData data = JsonUtility.FromJson<JsonData>(json);
            string updatedJson = JsonUtility.ToJson(data, true);

            Debug.Log(updatedJson);
            return updatedJson;
        }
        public void UpdateName(string name)
        {
            UpdateData(data => data.playerName = name);
        }

        public void UpdateId(int id)
        {
            UpdateData(data => data.playerId = id);
        }

        public void UpdateHealth(int health)
        {
            UpdateData(data => data.playerHealth = health);
        }

        public void UpdateSpeed(float speed)
        {
            UpdateData(data => data.playerSpeed = speed);
        }


        private void UpdateData(System.Action<JsonData> action)
        {
            if (File.Exists(_filePath))
            {
                string json = File.ReadAllText(_filePath);
                JsonData data = JsonUtility.FromJson<JsonData>(json);

                action?.Invoke(data);

                string updatedJson = JsonUtility.ToJson(data, true);
                File.WriteAllText(_filePath, updatedJson);                
            }
            else
            {
                Debug.LogError("File can't be found!");
            }
        }
    }
}
