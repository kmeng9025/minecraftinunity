using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
namespace Client{

    public class BlockManager{

        public static void LoadChunks(string chunkData){
            if(chunkData.Equals("wg")) {
                WorldManager.ScheduleBlockCreation("wg", "", "", "", "", "");
                return;
            }
            string[] blocks = chunkData.Split(";");
            int chunkX = int.Parse(blocks[0].Split(" ")[1]);
            int chunkY = int.Parse(blocks[0].Split(" ")[2]);
            Tuple<int, int> key = new Tuple<int, int>(chunkX, chunkY);
            // Debug.LogWarning(string.Join("/", key));
            if(!Variables.ChunkData.ContainsKey(key)){
                // Debug.LogWarning("i am making thing");
                Variables.ChunkData[key] = new GameObject[16][][];
                for(int i = 0; i < 16; i ++){
                    Variables.ChunkData[key][i] = new GameObject[16][];
                    for(int j = 0; j < 16; j ++){
                        Variables.ChunkData[key][i][j] = new GameObject[200];
                    }
                }
            }
            for(int i = 1; i < blocks.Length; i++){
                string[] blockData = blocks[i].Split(" ");
                string x = blockData[0];
                string y = blockData[1];
                string z = blockData[2];
                string blockType = blockData[3];
                WorldManager.ScheduleBlockCreation(blocks[0].Split(" ")[1], blocks[0].Split(" ")[2], x, y, z, blockType);
                // new GameObject();
            }
        }
    }
}