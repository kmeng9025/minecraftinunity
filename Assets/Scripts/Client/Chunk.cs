using System;
using System.Collections.Generic;
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
        public void Start()
        {
            gameObject.AddComponent<MeshFilter>();
            gameObject.AddComponent<MeshRenderer>();
            meshUpdated = false;
            vert = new List<Vector3>();
            uvlist = new List<Vector2>();
            tri = new List<int>();
        }

        public void SetUp(int chunkX, int chunkY)
        {
            this.chunkX = chunkX;
            this.chunkY = chunkY;
            gameObject.name = "Chunk " + chunkX + " " + chunkY;
            gameObject.transform.position = new Vector3(chunkX * 16, 0, chunkY * 16);
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

        public void updateMesh()
        {
            Tuple<int, int> key = new Tuple<int, int>(chunkX, chunkY);
            for (int x = 0; x < 16; x++)
            {
                for (int y = 0; y < 16; y++)
                {
                    for (int z = 0; z < 200; z++)
                    {
                        if (Variables.BlockData[key][x][y][z] == null) continue;
                        // Debug.LogError("RUNNING block");
                        try
                        {
                            if (Variables.BlockData[key][x][y][z + 1] == null)
                            {
                                AddTri();
                                AddUvs()
                                vert.Add(new Vector3(x - 0.5f, z + 0.5f, y + 0.5f)); // 16
                                vert.Add(new Vector3(x + 0.5f, z + 0.5f, y + 0.5f)); // 17
                                vert.Add(new Vector3(x + 0.5f, z + 0.5f, y - 0.5f)); // 18
                                vert.Add(new Vector3(x - 0.5f, z + 0.5f, y - 0.5f)); // 19
                            }
                        }
                        catch (Exception)
                        {
                            int count = vert.Count;
                            tri.Add(count);
                            tri.Add(count + 1);
                            tri.Add(count + 2);
                            tri.Add(count);
                            tri.Add(count + 2);
                            tri.Add(count + 3);
                            
                            vert.Add(new Vector3(x - 0.5f, z + 0.5f, y + 0.5f)); // 16
                            vert.Add(new Vector3(x + 0.5f, z + 0.5f, y + 0.5f)); // 17
                            vert.Add(new Vector3(x + 0.5f, z + 0.5f, y - 0.5f)); // 18
                            vert.Add(new Vector3(x - 0.5f, z + 0.5f, y - 0.5f)); // 19
                        }
                        try
                        {
                            if (Variables.BlockData[key][x][y][z - 1] == null)
                            {
                                int count = vert.Count;
                                tri.Add(count);
                                tri.Add(count + 1);
                                tri.Add(count + 2);
                                tri.Add(count);
                                tri.Add(count + 2);
                                tri.Add(count + 3);
                                
                                vert.Add(new Vector3(x - 0.5f, z - 0.5f, -0.5f));
                                vert.Add(new Vector3(x + 0.5f, z - 0.5f, -0.5f));
                                vert.Add(new Vector3(x + 0.5f, z - 0.5f, 0.5f));
                                vert.Add(new Vector3(x - 0.5f, z - 0.5f, 0.5f));
                            }
                        }
                        catch (Exception)
                        {
                            int count = vert.Count;
                            
                            uvlist.Add(new Vector2(0.0f, 0.0f));
                            uvlist.Add(new Vector2(0.5f, 0.0f));
                            uvlist.Add(new Vector2(0.5f, 0.5f));
                            uvlist.Add(new Vector2(0.0f, 0.5f));
                            vert.Add(new Vector3(x - 0.5f, z - 0.5f, -0.5f));
                            vert.Add(new Vector3(x + 0.5f, z - 0.5f, -0.5f));
                            vert.Add(new Vector3(x + 0.5f, z - 0.5f, 0.5f));
                            vert.Add(new Vector3(x - 0.5f, z - 0.5f, 0.5f));
                        }
                        try
                        {
                            if (Variables.BlockData[key][x + 1][y][z] == null)
                            {
                                int count = vert.Count;
                                tri.Add(count);
                                tri.Add(count + 1);
                                tri.Add(count + 2);
                                tri.Add(count);
                                tri.Add(count + 2);
                                tri.Add(count + 3);
                                
                                vert.Add(new Vector3(x + 0.5f, z - 0.5f, y + 0.5f)); // 12
                                vert.Add(new Vector3(x + 0.5f, z - 0.5f, y - 0.5f)); // 13
                                vert.Add(new Vector3(x + 0.5f, z + 0.5f, y - 0.5f)); // 14
                                vert.Add(new Vector3(x + 0.5f, z + 0.5f, y + 0.5f)); // 15
                            }
                        }
                        catch (Exception)
                        {
                            try
                            {
                                if (Variables.BlockData[new Tuple<int, int>(chunkX + 1, chunkY)][0][y][z] == null)
                                {
                                    int count = vert.Count;
                                    tri.Add(count);
                                    tri.Add(count + 1);
                                    tri.Add(count + 2);
                                    tri.Add(count);
                                    tri.Add(count + 2);
                                    tri.Add(count + 3);
                                    uvlist.Add(new Vector2(0.5f, 0.5f));
                                    uvlist.Add(new Vector2(1.0f, 0.5f));
                                    uvlist.Add(new Vector2(1.0f, 1.0f));
                                    uvlist.Add(new Vector2(0.5f, 1.0f));
                                    vert.Add(new Vector3(x + 0.5f, z - 0.5f, y + 0.5f)); // 12
                                    vert.Add(new Vector3(x + 0.5f, z - 0.5f, y - 0.5f)); // 13
                                    vert.Add(new Vector3(x + 0.5f, z + 0.5f, y - 0.5f)); // 14
                                    vert.Add(new Vector3(x + 0.5f, z + 0.5f, y + 0.5f)); // 15
                                }
                            }
                            catch (Exception)
                            {
                                int count = vert.Count;
                                tri.Add(count);
                                tri.Add(count + 1);
                                tri.Add(count + 2);
                                tri.Add(count);
                                tri.Add(count + 2);
                                tri.Add(count + 3);
                                uvlist.Add(new Vector2(0.5f, 0.5f));
                                uvlist.Add(new Vector2(1.0f, 0.5f));
                                uvlist.Add(new Vector2(1.0f, 1.0f));
                                uvlist.Add(new Vector2(0.5f, 1.0f));
                                vert.Add(new Vector3(x + 0.5f, z - 0.5f, y + 0.5f)); // 12
                                vert.Add(new Vector3(x + 0.5f, z - 0.5f, y - 0.5f)); // 13
                                vert.Add(new Vector3(x + 0.5f, z + 0.5f, y - 0.5f)); // 14
                                vert.Add(new Vector3(x + 0.5f, z + 0.5f, y + 0.5f)); // 15
                            }
                        }

                        try
                        {
                            if (Variables.BlockData[key][x - 1][y][z] == null)
                            {
                                int count = vert.Count;
                                tri.Add(count);
                                tri.Add(count + 1);
                                tri.Add(count + 2);
                                tri.Add(count);
                                tri.Add(count + 2);
                                tri.Add(count + 3);
                                vert.Add(new Vector3(x - 0.5f, z - 0.5f, y - 0.5f)); // 8
                                vert.Add(new Vector3(x - 0.5f, z - 0.5f, y + 0.5f)); // 9
                                vert.Add(new Vector3(x - 0.5f, z + 0.5f, y + 0.5f)); // 10
                                vert.Add(new Vector3(x - 0.5f, z + 0.5f, y - 0.5f)); // 11
                                uvlist.Add(new Vector2(0.5f, 0.5f));
                                uvlist.Add(new Vector2(1.0f, 0.5f));
                                uvlist.Add(new Vector2(1.0f, 1.0f));
                                uvlist.Add(new Vector2(0.5f, 1.0f));
                            }
                        }
                        catch (Exception)
                        {
                            try
                            {
                                if (Variables.BlockData[new Tuple<int, int>(chunkX - 1, chunkY)][15][y][z] == null)
                                {
                                    int count = vert.Count;
                                    tri.Add(count);
                                    tri.Add(count + 1);
                                    tri.Add(count + 2);
                                    tri.Add(count);
                                    tri.Add(count + 2);
                                    tri.Add(count + 3);
                                    vert.Add(new Vector3(x - 0.5f, z - 0.5f, y - 0.5f)); // 8
                                    vert.Add(new Vector3(x - 0.5f, z - 0.5f, y + 0.5f)); // 9
                                    vert.Add(new Vector3(x - 0.5f, z + 0.5f, y + 0.5f)); // 10
                                    vert.Add(new Vector3(x - 0.5f, z + 0.5f, y - 0.5f)); // 11
                                    uvlist.Add(new Vector2(0.5f, 0.5f));
                                    uvlist.Add(new Vector2(1.0f, 0.5f));
                                    uvlist.Add(new Vector2(1.0f, 1.0f));
                                    uvlist.Add(new Vector2(0.5f, 1.0f));
                                }
                            }
                            catch (Exception)
                            {
                                int count = vert.Count;
                                tri.Add(count);
                                tri.Add(count + 1);
                                tri.Add(count + 2);
                                tri.Add(count);
                                tri.Add(count + 2);
                                tri.Add(count + 3);
                                vert.Add(new Vector3(x - 0.5f, z - 0.5f, y - 0.5f)); // 8
                                vert.Add(new Vector3(x - 0.5f, z - 0.5f, y + 0.5f)); // 9
                                vert.Add(new Vector3(x - 0.5f, z + 0.5f, y + 0.5f)); // 10
                                vert.Add(new Vector3(x - 0.5f, z + 0.5f, y - 0.5f)); // 11
                                uvlist.Add(new Vector2(0.5f, 0.5f));
                                uvlist.Add(new Vector2(1.0f, 0.5f));
                                uvlist.Add(new Vector2(1.0f, 1.0f));
                                uvlist.Add(new Vector2(0.5f, 1.0f));
                            }
                        }

                        try
                        {
                            if (Variables.BlockData[key][x][y - 1][z] == null)
                            {
                                int count = vert.Count;
                                tri.Add(count);
                                tri.Add(count + 1);
                                tri.Add(count + 2);
                                tri.Add(count);
                                tri.Add(count + 2);
                                tri.Add(count + 3);
                                uvlist.Add(new Vector2(0.5f, 0.5f));
                                uvlist.Add(new Vector2(1.0f, 0.5f));
                                uvlist.Add(new Vector2(1.0f, 1.0f));
                                uvlist.Add(new Vector2(0.5f, 1.0f));
                                vert.Add(new Vector3(x + 0.5f, z - 0.5f, y - 0.5f)); // 4
                                vert.Add(new Vector3(x - 0.5f, z - 0.5f, y - 0.5f)); // 5
                                vert.Add(new Vector3(x - 0.5f, z + 0.5f, y - 0.5f)); // 6
                                vert.Add(new Vector3(x + 0.5f, z + 0.5f, y - 0.5f)); // 7
                            }
                        }
                        catch (Exception)
                        {
                            try
                            {
                                if (Variables.BlockData[new Tuple<int, int>(chunkX, chunkY - 1)][x][15][z] == null)
                                {
                                    int count = vert.Count;
                                    tri.Add(count);
                                    tri.Add(count + 1);
                                    tri.Add(count + 2);
                                    tri.Add(count);
                                    tri.Add(count + 2);
                                    tri.Add(count + 3);
                                    uvlist.Add(new Vector2(0.5f, 0.5f));
                                    uvlist.Add(new Vector2(1.0f, 0.5f));
                                    uvlist.Add(new Vector2(1.0f, 1.0f));
                                    uvlist.Add(new Vector2(0.5f, 1.0f));
                                    vert.Add(new Vector3(x + 0.5f, z - 0.5f, y - 0.5f)); // 4
                                    vert.Add(new Vector3(x - 0.5f, z - 0.5f, y - 0.5f)); // 5
                                    vert.Add(new Vector3(x - 0.5f, z + 0.5f, y - 0.5f)); // 6
                                    vert.Add(new Vector3(x + 0.5f, z + 0.5f, y - 0.5f)); // 7
                                }
                            }
                            catch (Exception)
                            {
                                int count = vert.Count;
                                tri.Add(count);
                                tri.Add(count + 1);
                                tri.Add(count + 2);
                                tri.Add(count);
                                tri.Add(count + 2);
                                tri.Add(count + 3);
                                uvlist.Add(new Vector2(0.5f, 0.5f));
                                uvlist.Add(new Vector2(1.0f, 0.5f));
                                uvlist.Add(new Vector2(1.0f, 1.0f));
                                uvlist.Add(new Vector2(0.5f, 1.0f));
                                vert.Add(new Vector3(x + 0.5f, z - 0.5f, y - 0.5f)); // 4
                                vert.Add(new Vector3(x - 0.5f, z - 0.5f, y - 0.5f)); // 5
                                vert.Add(new Vector3(x - 0.5f, z + 0.5f, y - 0.5f)); // 6
                                vert.Add(new Vector3(x + 0.5f, z + 0.5f, y - 0.5f)); // 7
                            }
                        }
                        try
                        {
                            if (Variables.BlockData[key][x][y + 1][z] == null)
                            {
                                int count = vert.Count;
                                tri.Add(count);
                                tri.Add(count + 1);
                                tri.Add(count + 2);
                                tri.Add(count);
                                tri.Add(count + 2);
                                tri.Add(count + 3);
                                uvlist.Add(new Vector2(0.5f, 0.5f));
                                uvlist.Add(new Vector2(1.0f, 0.5f));
                                uvlist.Add(new Vector2(1.0f, 1.0f));
                                uvlist.Add(new Vector2(0.5f, 1.0f));
                                vert.Add(new Vector3(x - 0.5f, z - 0.5f, y + 0.5f)); // 0
                                vert.Add(new Vector3(x + 0.5f, z - 0.5f, y + 0.5f)); // 1
                                vert.Add(new Vector3(x + 0.5f, z + 0.5f, y + 0.5f)); // 2
                                vert.Add(new Vector3(x - 0.5f, z + 0.5f, y + 0.5f)); // 3

                            }
                        }
                        catch (Exception)
                        {
                            try
                            {
                                if (Variables.BlockData[new Tuple<int, int>(chunkX, chunkY + 1)][x][0][z] == null)
                                {
                                    int count = vert.Count;
                                    tri.Add(count);
                                    tri.Add(count + 1);
                                    tri.Add(count + 2);
                                    tri.Add(count);
                                    tri.Add(count + 2);
                                    tri.Add(count + 3);
                                    uvlist.Add(new Vector2(0.5f, 0.5f));
                                    uvlist.Add(new Vector2(1.0f, 0.5f));
                                    uvlist.Add(new Vector2(1.0f, 1.0f));
                                    uvlist.Add(new Vector2(0.5f, 1.0f));
                                    vert.Add(new Vector3(x - 0.5f, z - 0.5f, y + 0.5f)); // 0
                                    vert.Add(new Vector3(x + 0.5f, z - 0.5f, y + 0.5f)); // 1
                                    vert.Add(new Vector3(x + 0.5f, z + 0.5f, y + 0.5f)); // 2
                                    vert.Add(new Vector3(x - 0.5f, z + 0.5f, y + 0.5f)); // 3
                                }
                            }
                            catch (Exception)
                            {
                                int count = vert.Count;
                                tri.Add(count);
                                tri.Add(count + 1);
                                tri.Add(count + 2);
                                tri.Add(count);
                                tri.Add(count + 2);
                                tri.Add(count + 3);
                                uvlist.Add(new Vector2(0.5f, 0.5f));
                                uvlist.Add(new Vector2(1.0f, 0.5f));
                                uvlist.Add(new Vector2(1.0f, 1.0f));
                                uvlist.Add(new Vector2(0.5f, 1.0f));
                                vert.Add(new Vector3(x - 0.5f, z - 0.5f, y + 0.5f)); // 0
                                vert.Add(new Vector3(x + 0.5f, z - 0.5f, y + 0.5f)); // 1
                                vert.Add(new Vector3(x + 0.5f, z + 0.5f, y + 0.5f)); // 2
                                vert.Add(new Vector3(x - 0.5f, z + 0.5f, y + 0.5f)); // 3
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
            if (textureAtlas == null)
            {
                Debug.LogError("Texture atlas not found! Make sure it's in Resources and named correctly.");
            }
            Mesh mesh = new Mesh();
            mesh.vertices = vertices;
            mesh.triangles = triangles;
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
        }
    }
}