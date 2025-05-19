using System.Net;

namespace Server{
    public class Anticheat
    {
        private static Anticheat instance;
        private ConnectToClient connectToClient;
        private string worldDir;
        private string playerDir;
        private long seed;

        public Anticheat()
        {
            connectToClient = ConnectToClient.getInstance();
        }

        public static Anticheat getInstance()
        {
            if (instance == null)
            {
                instance = new Anticheat();
            }
            return instance;
        }
        public void addPlayer(string playerName)
        {
            while(!Variables.playerData.TryAdd(playerName, new float[3]));
            Variables.playerData[playerName] = Variables.worldSpawn;
        }

        public void checkMovement(string playerName, float x, float y, float z)
        {
            float[] playerData = Variables.playerData[playerName];
            if(playerData == null){
                addPlayer(playerName);
            }
        }
    }
}