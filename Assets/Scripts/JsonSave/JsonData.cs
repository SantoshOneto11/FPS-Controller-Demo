namespace JsonSave
{
    [System.Serializable]
    public class JsonData
    {
        public string playerName;
        public int playerId;
        public float playerHealth;
        public float playerSpeed;

        public JsonData(string name, int id, float health, float speed)
        {
            this.playerName = name;
            this.playerId = id;
            this.playerHealth = health;
            this.playerSpeed = speed;
        }

        public JsonData(string name)
        {
            this.playerName = name;
        }
    }
}
