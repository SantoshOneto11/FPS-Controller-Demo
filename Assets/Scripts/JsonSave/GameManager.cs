using UnityEngine;

namespace JsonSave
{
    public class GameManager : MonoBehaviour
    {
        public SaveSystem saveSystem;
        public LoadSystem loadSystem;

        private void Start()
        {
            saveSystem = new SaveSystem();
            loadSystem = new LoadSystem();

            JsonData data = new JsonData(name: "Rambo", id: 007, health: 100f, speed: 10f);

            saveSystem.SaveGame(data);
            JsonData loadedData = loadSystem.LoadGame();
            if (loadedData != null)
            {
                Debug.Log("PlayerData " + loadedData.playerName);
            }

            loadSystem.UpdateName("Das");
            loadSystem.UpdateHealth(99);
            loadSystem.UpdateSpeed(15f);
        }
    }
}
