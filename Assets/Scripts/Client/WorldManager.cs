using System;
using System.Collections;
using UnityEngine;

namespace Client
{
    public class WorldManager : MonoBehaviour
    {
        ConnectToHost connectToHost;
        static ArrayList scheduleBlocks = ArrayList.Synchronized(new ArrayList());
        public void Start()
        {
            Server.CreateHost.CreateNewHost(Application.persistentDataPath);
            connectToHost = ConnectToHost.getInstance();
        }

        public void OnDestroy()
        {
            Server.CreateHost.ShutDownServer();
            connectToHost.StopClient();
        }

        public static void ScheduleBlockCreation(string chunkX, string chunkY, string x, string y, string z, string blockType)
        {
            scheduleBlocks.Add(new string[]{chunkX, chunkY, x, y, z, blockType});
        }

        public void Update()
        {
            for(int i = 0; i < scheduleBlocks.Count; i++)
            {
                string[] blockData = scheduleBlocks[i] as string[];
                if(blockData[0].Equals("wg")){
                    Variables.worldGenerated = true;
                    continue;
                }
                int chunkX = int.Parse(blockData[0]);
                int chunkY = int.Parse(blockData[1]);
                int x = int.Parse(blockData[2]);
                int y = int.Parse(blockData[3]);
                int z = int.Parse(blockData[4]);
                string blockType = blockData[5];
                Tuple<int, int> key = new Tuple<int, int>(chunkX, chunkY);
                GameObject[][][] test;
                if(!Variables.ChunkData.TryGetValue(key, out test)){
                    Debug.LogWarning("key not working");
                }
                Variables.ChunkData[key][x][y][z] = new GameObject();
                Variables.ChunkData[key][x][y][z].AddComponent<Block>();
                Variables.ChunkData[key][x][y][z].GetComponent<Block>().SetUp(chunkX, chunkY, x, y, z, blockType);
                Variables.ChunkData[key][x][y][z].layer = LayerMask.NameToLayer("Blocks");
                scheduleBlocks.RemoveAt(i);
                i--;
            }
        }
    }
}