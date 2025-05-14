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
            Variables.playerX.Add(Variables.worldSpawn[0]);
            Variables.playerY.Add(Variables.worldSpawn[1]);
            Variables.playerZ.Add(Variables.worldSpawn[2]);
            Variables.playerName.Add(playerName);
        }

        public void checkMovement(string playerName, float x, float y, float z)
        {
            int index = Variables.playerName.FindIndex(x => x == playerName);
            Variables.playerX[index] = x;
            Variables.playerY[index] = y;
            Variables.playerZ[index] = z;
        }
    }
}