using System.Net;
using System.Runtime.CompilerServices;
using UnityEngine;

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

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static Anticheat getInstance()
        {
            if (instance == null)
            {
                instance = new Anticheat();
            }
            return instance;
        }

        // [MethodImpl(MethodImplOptions.Synchronized)]
        // public void addPlayer(string playerName)
        // {
        //     while(!Variables.playerData.TryAdd(playerName, new Player()));
        //     Variables.playerData[playerName] = Variables.worldSpawn;
        // }

        // [MethodImpl(MethodImplOptions.Synchronized)]
        // public void checkMovement(string playerName, float x, float y, float z)
        // {
        //     float[] playerData = Variables.playerData[playerName];
        //     if(playerData == null){
        //         addPlayer(playerName);
        //     }
        // }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void gotData(string playerData){
            string[] data = playerData.Split("%");
            string[] playerp = data[0].Split(" ");
            string[] cam = data[1].Split(" ");
            Vector3 playerPos = new Vector3(float.Parse(playerp[0]), float.Parse(playerp[1]), float.Parse(playerp[2]));
            Vector3 camFor = new Vector3(float.Parse(cam[0]), float.Parse(cam[1]), float.Parse(cam[2]));
        }
    }
}