using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Client
{
    public class Chunk : MonoBehaviour
    {
        int chunkX;
        int chunkY;
        bool meshUpdated;
        public Texture2D textureAtlas;
        List<int> tri;
        List<Vector3> vert;
        List<Vector2> uvlist;
        bool meshReady = false;
        public void Start()
        {
            gameObject.AddComponent<MeshFilter>();
            gameObject.AddComponent<MeshRenderer>();
            gameObject.AddComponent<MeshCollider>();
            meshUpdated = false;
        }

        public bool MeshReady(){
            return meshReady;
        }

        public void SetUp(int chunkX, int chunkY)
        {
            this.chunkX = chunkX;
            this.chunkY = chunkY;
            gameObject.name = "Chunk " + chunkX + " " + chunkY;
            gameObject.transform.position = new Vector3(chunkX * 16, -100, chunkY * 16);
        }

        public void Update()
        {
            if (Variables.worldGenerated && !meshUpdated)
            {
                updateMesh();
            }
        }

        private void AddTri(){
            int count = vert.Count; 
            tri.Add(count);
            tri.Add(count + 1);
            tri.Add(count + 2);
            tri.Add(count);
            tri.Add(count + 2);
            tri.Add(count + 3);
        }

        private void AddVert(BlockFace blockFace, int x, int y, int z){
            for(int i = 0; i < 4; i++) vert.Add(Variables.Verts[blockFace][i] + new Vector3(x, z, y));
        }

        private void AddUvs(BlockType blockType, BlockFace blockFace)
        {
            for (int i = 0; i < 4; i++) uvlist.Add(Variables.UVs[blockType][blockFace][i]);
        }

        private void AddT(BlockType blockType, BlockFace blockFace, int x, int y, int z){
            AddTri();
            AddUvs(blockType, blockFace);
            AddVert(blockFace, x, y, z);
        }

        public void updateMesh()
        {
            vert = new List<Vector3>();
            uvlist = new List<Vector2>();
            tri = new List<int>();
            Tuple<int, int> key = new Tuple<int, int>(chunkX, chunkY);
            for (int x = 0; x < 16; x++)
            {
                for (int y = 0; y < 16; y++)
                {
                    for (int z = 0; z < 200; z++)
                    {
                        if (Variables.BlockData[key][x][y][z] == null) continue;
                        Block currentBlock = Variables.BlockData[key][x][y][z];
                        try
                        {
                            if (Variables.BlockData[key][x][y][z + 1] == null)
                            {
                                AddT(currentBlock.GetBlockType(), BlockFace.Top, x, y, z);
                            }
                        }
                        catch (Exception)
                        {
                            AddT(currentBlock.GetBlockType(), BlockFace.Top, x, y, z);
                        }


                        try
                        {
                            if (Variables.BlockData[key][x][y][z - 1] == null)
                            {
                                AddT(currentBlock.GetBlockType(), BlockFace.Bottom, x, y, z);
                            }
                        }
                        catch (Exception)
                        {
                                AddT(currentBlock.GetBlockType(), BlockFace.Bottom, x, y, z);
                        }


                        try
                        {
                            if (Variables.BlockData[key][x + 1][y][z] == null)
                            {
                                AddT(currentBlock.GetBlockType(), BlockFace.Right, x, y, z);
                            }
                        }
                        catch (Exception)
                        {
                            try
                            {
                                if (Variables.BlockData[new Tuple<int, int>(chunkX + 1, chunkY)][0][y][z] == null)
                                {
                                    AddT(currentBlock.GetBlockType(), BlockFace.Right, x, y, z);
                                }
                            }
                            catch (Exception)
                            {
                                AddT(currentBlock.GetBlockType(), BlockFace.Right, x, y, z);
                            }
                        }


                        try
                        {
                            if (Variables.BlockData[key][x - 1][y][z] == null)
                            {
                                AddT(currentBlock.GetBlockType(), BlockFace.Left, x, y, z);
                            }
                        }
                        catch (Exception)
                        {
                            try
                            {
                                if (Variables.BlockData[new Tuple<int, int>(chunkX - 1, chunkY)][15][y][z] == null)
                                {
                                    AddT(currentBlock.GetBlockType(), BlockFace.Left, x, y, z);
                                }
                            }
                            catch (Exception)
                            {
                                AddT(currentBlock.GetBlockType(), BlockFace.Left, x, y, z);
                            }
                        }


                        try
                        {
                            if (Variables.BlockData[key][x][y + 1][z] == null)
                            {
                                AddT(currentBlock.GetBlockType(), BlockFace.Front, x, y, z);
                            }
                        }
                        catch (Exception)
                        {
                            try
                            {
                                if (Variables.BlockData[new Tuple<int, int>(chunkX, chunkY + 1)][x][0][z] == null)
                                {
                                    AddT(currentBlock.GetBlockType(), BlockFace.Front, x, y, z);
                                }
                            }
                            catch (Exception)
                            {
                                AddT(currentBlock.GetBlockType(), BlockFace.Front, x, y, z);
                            }
                        }

                        try
                        {
                            if (Variables.BlockData[key][x][y - 1][z] == null)
                            {
                                AddT(currentBlock.GetBlockType(), BlockFace.Back, x, y, z);
                            }
                        }
                        catch (Exception)
                        {
                            try
                            {
                                if (Variables.BlockData[new Tuple<int, int>(chunkX, chunkY - 1)][x][15][z] == null)
                                {
                                    AddT(currentBlock.GetBlockType(), BlockFace.Back, x, y, z);
                                }
                            }
                            catch (Exception)
                            {
                                AddT(currentBlock.GetBlockType(), BlockFace.Back, x, y, z);
                            }
                        }
                    }
                }
            }
            textureAtlas = Resources.Load<Texture2D>("Material/gr");
            int[] triangles = tri.ToArray();
            Vector3[] vertices = vert.ToArray();
            Vector2[] uv = uvlist.ToArray();
            MeshFilter mf = GetComponent<MeshFilter>();
            MeshRenderer mr = GetComponent<MeshRenderer>();
            MeshCollider mc = GetComponent<MeshCollider>();
            if (textureAtlas == null)
            {
                Debug.LogError("Texture atlas not found! Make sure it's in Resources and named correctly.");
            }
            Mesh mesh = new Mesh();
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mc.sharedMesh = mesh;
            mesh.uv = uv;
            mesh.RecalculateNormals();
            // Apply material
            Material mat = new Material(Shader.Find("Universal Render Pipeline/Lit"));
            mat.mainTexture = textureAtlas;
            mat.SetFloat("_Smoothness", 0f); // For glossiness
            mat.SetFloat("_Metallic", 0f);   // For reflectiveness
            mr.material = mat;
            mf.mesh = mesh;
            meshUpdated = true;
            meshReady = true;
        }
    }
}