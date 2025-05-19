using System.Collections.Concurrent;
using UnityEngine;

namespace Server
{
    public class Variables
    {
        // public static List<float> playerX = new List<float>();
        // public static List<float> playerY = new List<float>();
        // public static List<float> playerZ = new List<float>();
        // public static List<string> playerName = new List<string>();
        public static ConcurrentDictionary<string, Player> playerData = new ConcurrentDictionary<string, Player>();
        public static Vector3 worldSpawn = new Vector3(0, 0, 0);
        public static ServerGenerator serverGenerator;
        public static string persistentDataPath;

    }
}