using System;
using System.IO;
using UnityEngine;
using UnityEngine.Playables;

namespace JsonSave
{
    public class SaveSystem
    {
        private string _filePath;
        public void SaveGame(JsonData data)
        {

            _filePath = Application.persistentDataPath + "/gamedata.json";
            string json = JsonUtility.ToJson(data, true);
            Debug.Log("Data: " + json);
            File.WriteAllText(_filePath, json);
            Debug.Log("gamedata.json created at: " + _filePath);
        }
    }
}
