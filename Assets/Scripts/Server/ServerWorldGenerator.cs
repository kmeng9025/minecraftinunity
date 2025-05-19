using System.IO;
using System.Threading;
using UnityEngine;
using System;
using System.Text;
using NUnit.Framework.Interfaces;
using Unity.Mathematics;
namespace Server
{
    public class ServerGenerator
    {
        string worldDir;
        string playerDir;
        long seed;
        string persistentDataPath;
        ConnectToClient connectToClient;
        public ServerGenerator(string worldDir)
        {
            connectToClient = ConnectToClient.getInstance();
            loadWorld(worldDir);
            // Thread thread = new Thread(new ThreadStart(listenPlayer));
            // thread.Start();
        }
        public ServerGenerator(long seed)
        {
            Debug.Log("server generator exist");
            connectToClient = ConnectToClient.getInstance();
            this.seed = seed;
            newWorld();
            // Thread thread = new Thread(new ThreadStart(listenPlayer));
            // thread.Start();
        }
        private void listenPlayer()
        {
            // while (true)
            // {

            // }
        }
        public void generateNewChunks(int chunkX, int chunkY)
        {
            generateBlocks(chunkX, chunkY);
        }
        private void generateBlocks(int chunkX, int chunkY)
        {
            if(File.Exists(worldDir + "\\" + chunkX + " " + chunkY)){
                File.Delete(worldDir + "\\" + chunkX + " " + chunkY);
            }
            FileStream fs = File.Create(worldDir + "\\" + chunkX + " " + chunkY);
            string results = "cd " + chunkX + " " + chunkY;
            for (int x = 0; x < 16; x++)
            {
                for (int y = 0; y < 16; y++)
                {
                    int blockx = chunkX*16+x;
                    int blocky = chunkY*16+y;
                    // float baseAmptitude = 2f;
                    // float baseFrequency = 0.5f;
                    float noise1 = Mathf.PerlinNoise(blockx*0.1f, blocky*0.1f);
                    // float noise2 = (float)(Mathf.PerlinNoise(blockx*0.2f, blocky*0.2f) * 0.5);
                    // float noise3 = (float)(Mathf.PerlinNoise(blockx*0.4f, blocky*0.4f) * 0.25);
                    // float finalNoise = noise1 + noise2 + noise3;
                    int finalZ = (int)Math.Round(noise1 * 5);
                    results += ";" + x + " " + y + " " + finalZ + " gr";
                    // for(int i = 0; i < finalZ; i++){
                    //     results += ";" + x + " " + y + " " + i + " gr";
                    // }
                    
                }
            }
            // results += ";" + 0 + " " + 0 + " " + 0 + " gr";
            byte[] info = new UTF8Encoding(true).GetBytes(results);
            fs.Write(info, 0, info.Length);
            fs.Flush();
            connectToClient.SendMessageToClient(results);
        }
        public void loadChunks(int chunkX, int chunkY) { }
        public void loadWorld(string worldDir)
        {
        }

        public void newWorld()
        {
            long worldId;
            do{
                System.Random random = new System.Random();
                worldId = random.Next(0, int.MaxValue);
            } while(Directory.Exists(Variables.persistentDataPath + "\\Scripts\\Server\\Worlds\\" + worldId));
            Directory.CreateDirectory(Variables.persistentDataPath + "\\Scripts\\Server\\Worlds\\" + worldId);
            Directory.CreateDirectory(Variables.persistentDataPath + "\\Scripts\\Server\\Worlds\\" + worldId + "\\PlayerData");
            worldDir = Variables.persistentDataPath + "\\Scripts\\Server\\Worlds\\" + worldId;
            playerDir = Variables.persistentDataPath + "\\Scripts\\Server\\Worlds\\" + worldId + "\\PlayerData";
            for(int x = -2; x < 3; x++){
                for (int y = -2; y < 3; y++){
                    generateNewChunks(x, y);
                    connectToClient.SendMessageToClient("cg");
                }
            }
            // generateNewChunks(0, 0);
            connectToClient.SendMessageToClient("wg");
        }

        public void saveWorld(string worldDir)
        {
        }
    }
}