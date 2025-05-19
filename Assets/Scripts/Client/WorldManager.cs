using System;
using System.Collections;
using UnityEngine;

namespace Client
{
    public class WorldManager : MonoBehaviour
    {
        ConnectToHost connectToHost;
        static ArrayList scheduleBlocks = ArrayList.Synchronized(new ArrayList());
        static ArrayList scheduleChunks = ArrayList.Synchronized(new ArrayList());
        
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

        public static void ScheduleBlockCreation(string chunkX, string chunkY, string x, string y, string z, string blockType, string properties)
        {
            scheduleBlocks.Add(new string[]{chunkX, chunkY, x, y, z, blockType, properties});
        }

        public static void ScheduleChunkCreation(string chunkX, string chunkY){
            scheduleChunks.Add(new string[]{chunkX, chunkY});
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
                BlockType blockType = blockData[5] switch{
                    "st" => BlockType.Stone,
                    "gr" => BlockType.Grass,
                    "dr" => BlockType.Dirt,
                    _ => BlockType.None
                };
                string properties = blockData[6];
                Tuple<int, int> key = new Tuple<int, int>(chunkX, chunkY);
                Variables.BlockData[key][x][y][z] = new Block(chunkX, chunkY, x, y, blockType, properties);
                scheduleBlocks.RemoveAt(0);
                i--;
            }
            for(int i = 0; i < scheduleChunks.Count; i++)
            {
                string[] chunkData = scheduleChunks[i] as string[];
                int chunkX = int.Parse(chunkData[0]);
                int chunkY = int.Parse(chunkData[1]);
                Tuple<int, int> key = new Tuple<int, int>(chunkX, chunkY);
                Variables.ChunkData[key] = new GameObject();
                Variables.ChunkData[key].AddComponent<Chunk>();
                Variables.ChunkData[key].GetComponent<Chunk>().SetUp(chunkX, chunkY);
                scheduleChunks.RemoveAt(0);
                i --;
            }
        }
    }
}