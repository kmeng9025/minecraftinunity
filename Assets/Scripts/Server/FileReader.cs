using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using NUnit.Framework;
using UnityEngine.Rendering;

namespace Server{
    public class FileReader{
        private static string worldDir;
        private static string playerDir;
        private static long seed;
        public bool canRead = false;

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void SetUp(string worldID){
            worldDir = Variables.persistentDataPath + "\\Scripts\\Server\\Worlds\\" + worldID;
            playerDir = Variables.persistentDataPath + "\\Scripts\\Server\\Worlds\\" + worldID + "\\PlayerData";
            if(!Directory.Exists(worldDir)){
                Directory.CreateDirectory(worldDir);
            }
            if(!Directory.Exists(playerDir)){
                Directory.CreateDirectory(playerDir);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static string readChunk(int chunkX, int chunkY){
            StreamReader sr = new StreamReader(worldDir + "\\" + chunkX + " " + chunkY);
            return sr.ReadToEnd();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void writeBlocks(int chunkX, int chunkY, Dictionary<int[], string> blocks){
            StreamReader sr = new StreamReader(worldDir + "\\" + chunkX + " " + chunkY);
            List<string> lines = new List<string>(sr.ReadToEnd().Split('\n'));
            List<int[]> keys = new List<int[]>(blocks.Keys);
            for(int i = 0; i < lines.Count; i++){
                if((int.Parse(lines[i].Split(" ")[0]) >= keys[0][0] && int.Parse(lines[i].Split(" ")[1]) > keys[0][1]) || int.Parse(lines[i].Split(" ")[0]) > keys[0][0]){
                    lines.Insert(i, blocks[keys[0]]);
                    keys.RemoveAt(0);
                } else if(int.Parse(lines[i].Split(" ")[0]) == keys[0][0] && int.Parse(lines[i].Split(" ")[1]) == keys[0][1]){
                    lines[i] = blocks[keys[0]];
                    keys.RemoveAt(0);
                }
            }
            writeChunks(chunkX, chunkY, string.Join("\n", lines));
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void writeChunks(int chunkX, int chunkY, string blocks){
            StreamWriter sw = new StreamWriter(worldDir + "\\" + chunkX + " " + chunkY);
            sw.WriteAsync(blocks);
            sw.Close();
        }
    }
}