namespace Server
{
    public class CreateHost
    {
        static ConnectToClient connectToClient;
        public static void CreateNewHost(string persistentDataPath)
        {
            Variables.persistentDataPath = persistentDataPath;
            connectToClient = ConnectToClient.getInstance();
        }
        public static void ShutDownServer(){
            connectToClient.StopServer();
        }
    }
}