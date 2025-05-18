using System;

namespace Client{
    public class Block{
        int chunkX;
        int chunkY;
        int x;
        int y;
        string type;
        string properties;
        
        public Block(int chunkX, int chunkY, int x, int y, string type, string properties){
            this.chunkX = chunkX;
            this.chunkY = chunkY;
            this.x = x;
            this.y = y;
            this.type = type;
        }
    }
}