using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using UnityEngine;
namespace Client{

    public class BlockManager{
        static int chunkX;
        static int chunkY;

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void LoadChunks(string chunkData){
            if(chunkData.Equals("wg")) {
                WorldManager.ScheduleBlockCreation("wg", "", "", "", "", "", "");
                return;
            }
            if(chunkData.Equals("cg")) {
                WorldManager.ScheduleChunkCreation(chunkX.ToString(), chunkY.ToString());
                return;
            }
            string[] blocks = chunkData.Split(";");
            chunkX = int.Parse(blocks[0].Split(" ")[1]);
            chunkY = int.Parse(blocks[0].Split(" ")[2]);
            Tuple<int, int> key = new Tuple<int, int>(chunkX, chunkY);
            // Debug.LogWarning(string.Join("/", key));
            if(!Variables.BlockData.ContainsKey(key)){
                // Debug.LogWarning("i am making thing");
                Variables.BlockData[key] = new Block[16][][];
                for(int i = 0; i < 16; i ++){
                    Variables.BlockData[key][i] = new Block[16][];
                    for(int j = 0; j < 16; j ++){
                        Variables.BlockData[key][i][j] = new Block[200];
                    }
                }
            }
            for(int i = 1; i < blocks.Length; i++){
                string[] blockData = blocks[i].Split(" ");
                string x = blockData[0];
                string y = blockData[1];
                string z = blockData[2];
                string blockType = blockData[3];
                WorldManager.ScheduleBlockCreation(blocks[0].Split(" ")[1], blocks[0].Split(" ")[2], x, y, z, blockType, "");
                // new GameObject();
            }
        }
    }
}