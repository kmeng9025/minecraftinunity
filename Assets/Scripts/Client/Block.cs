using System;

namespace Client{
    public class Block{
        int chunkX;
        int chunkY;
        int x;
        int y;
        string properties;
        BlockType blockType;
        public Block(int chunkX, int chunkY, int x, int y, BlockType blockType, string properties){
            this.chunkX = chunkX;
            this.chunkY = chunkY;
            this.x = x;
            this.y = y;
            this.blockType = blockType;
        }

        public Block(BlockType blockType){
            this.blockType = blockType;
        }

        public BlockType GetBlockType(){
            return blockType;
        }
    }
}