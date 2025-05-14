using System.Collections.Generic;
using System.Data.SqlTypes;

namespace Server
{
    public class Variables
    {
        public static List<float> playerX = new List<float>();
        public static List<float> playerY = new List<float>();
        public static List<float> playerZ = new List<float>();
        public static List<string> playerName = new List<string>();
        public static float[] worldSpawn = { 0f, 0f, 0f };
        public static ServerGenerator serverGenerator;
        public static string persistentDataPath;

    }
}