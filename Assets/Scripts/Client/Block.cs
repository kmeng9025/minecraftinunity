using System;
using System.Collections.Generic;
using UnityEngine;
namespace Client
{
    public class Block : MonoBehaviour
    {
        public int chunkX;
        public int chunkY;
        public int x;
        public int y;
        public int z;
        public string blockType;
        public Texture2D textureAtlas;
        Tuple<int, int> key;
        BoxCollider boxCollider;

        public void SetUp(int chunkX, int chunkY, int x, int y, int z, string blockType)
        {
            this.chunkX = chunkX;
            this.chunkY = chunkY;
            this.x = x;
            this.y = y;
            this.z = z;
            this.blockType = blockType;
            gameObject.transform.position = new Vector3(chunkX * 16 + x, z-100, chunkY * 16 + y);
            // gameObject.GetComponent<Renderer>().material = Resources.Load<Material>("Material/" + blockType);
            textureAtlas = Resources.Load<Texture2D>("Material/" + blockType);
            gameObject.tag = "b";
            key = new Tuple<int, int>(chunkX, chunkY);
            gameObject.AddComponent<MeshFilter>();
            gameObject.AddComponent<MeshRenderer>();
            gameObject.AddComponent<BoxCollider>();
            boxCollider = gameObject.GetComponent<BoxCollider>();
            boxCollider.size = new Vector3(1f, 1f, 1f);
            // if (Variables.worldGenerated)
            // {
            //     List<int> tri = new List<int>();
            //     try
            //     {
            //         if (Variables.ChunkData[key][x][y + 1][z] == null)
            //         {
            //             tri.Add(0);
            //             tri.Add(1);
            //             tri.Add(2);
            //             tri.Add(0);
            //             tri.Add(2);
            //             tri.Add(3);
            //         }
            //     }
            //     catch (IndexOutOfRangeException)
            //     {
            //         tri.Add(0);
            //         tri.Add(1);
            //         tri.Add(2);
            //         tri.Add(0);
            //         tri.Add(2);
            //         tri.Add(3);
            //     }
            //     try
            //     {
            //         if (Variables.ChunkData[key][x][y - 1][z] == null)
            //         {
            //             tri.Add(4);
            //             tri.Add(5);
            //             tri.Add(6);
            //             tri.Add(4);
            //             tri.Add(6);
            //             tri.Add(7);
            //         }
            //     }
            //     catch (IndexOutOfRangeException)
            //     {
            //         tri.Add(4);
            //         tri.Add(5);
            //         tri.Add(6);
            //         tri.Add(4);
            //         tri.Add(6);
            //         tri.Add(7);
            //     }
            //     try
            //     {
            //         if (Variables.ChunkData[key][x - 1][y][z] == null)
            //         {
            //             tri.Add(8);
            //             tri.Add(9);
            //             tri.Add(10);
            //             tri.Add(8);
            //             tri.Add(10);
            //             tri.Add(11);
            //         }
            //     }
            //     catch (IndexOutOfRangeException)
            //     {
            //         tri.Add(8);
            //         tri.Add(9);
            //         tri.Add(10);
            //         tri.Add(8);
            //         tri.Add(10);
            //         tri.Add(11);
            //     }
            //     try
            //     {
            //         if (Variables.ChunkData[key][x + 1][y][z] == null)
            //         {
            //             tri.Add(12);
            //             tri.Add(13);
            //             tri.Add(14);
            //             tri.Add(12);
            //             tri.Add(14);
            //             tri.Add(15);
            //         }
            //     }
            //     catch (IndexOutOfRangeException)
            //     {
            //         tri.Add(12);
            //         tri.Add(13);
            //         tri.Add(14);
            //         tri.Add(12);
            //         tri.Add(14);
            //         tri.Add(15);
            //     }
            //     try
            //     {
            //         if (Variables.ChunkData[key][x][y][z + 1] == null)
            //         {
            //             tri.Add(16);
            //             tri.Add(17);
            //             tri.Add(18);
            //             tri.Add(16);
            //             tri.Add(18);
            //             tri.Add(19);
            //         }
            //     }
            //     catch (IndexOutOfRangeException)
            //     {
            //         tri.Add(16);
            //         tri.Add(17);
            //         tri.Add(18);
            //         tri.Add(16);
            //         tri.Add(18);
            //         tri.Add(19);
            //     }
            //     try
            //     {
            //         if (Variables.ChunkData[key][x][y][z - 1] == null)
            //         {
            //             tri.Add(20);
            //             tri.Add(21);
            //             tri.Add(22);
            //             tri.Add(20);
            //             tri.Add(22);
            //             tri.Add(23);
            //         }
            //     }
            //     catch (IndexOutOfRangeException)
            //     {
            //         tri.Add(20);
            //         tri.Add(21);
            //         tri.Add(22);
            //         tri.Add(20);
            //         tri.Add(22);
            //         tri.Add(23);
            //     }
            meshUpdated = false;

        }
        // Vector3[] vertices = new Vector3[]
        // {
        // // Front face
        // new Vector3(-0.5f, -0.5f,  0.5f), // 0
        // new Vector3( 0.5f, -0.5f,  0.5f), // 1
        // new Vector3( 0.5f,  0.5f,  0.5f), // 2
        // new Vector3(-0.5f,  0.5f,  0.5f), // 3

        // // Back face
        // new Vector3( 0.5f, -0.5f, -0.5f), // 4
        // new Vector3(-0.5f, -0.5f, -0.5f), // 5
        // new Vector3(-0.5f,  0.5f, -0.5f), // 6
        // new Vector3( 0.5f,  0.5f, -0.5f), // 7

        // // Left face
        // new Vector3(-0.5f, -0.5f, -0.5f), // 8
        // new Vector3(-0.5f, -0.5f,  0.5f), // 9
        // new Vector3(-0.5f,  0.5f,  0.5f), // 10
        // new Vector3(-0.5f,  0.5f, -0.5f), // 11

        // // Right face
        // new Vector3( 0.5f, -0.5f,  0.5f), // 12
        // new Vector3( 0.5f, -0.5f, -0.5f), // 13
        // new Vector3( 0.5f,  0.5f, -0.5f), // 14
        // new Vector3( 0.5f,  0.5f,  0.5f), // 15

        // // Top face
        // new Vector3(-0.5f,  0.5f,  0.5f), // 16
        // new Vector3( 0.5f,  0.5f,  0.5f), // 17
        // new Vector3( 0.5f,  0.5f, -0.5f), // 18
        // new Vector3(-0.5f,  0.5f, -0.5f), // 19

        // // Bottom face
        // new Vector3(-0.5f, -0.5f, -0.5f), // 20
        // new Vector3( 0.5f, -0.5f, -0.5f), // 21
        // new Vector3( 0.5f, -0.5f,  0.5f), // 22
        // new Vector3(-0.5f, -0.5f,  0.5f), // 23
        // };








        // Mesh mesh = new Mesh();

        // Vector3[] vertices = new Vector3[]
        // {
        //     // Front (+Z)
        //     new Vector3(0, 0, 1),
        //     new Vector3(1, 0, 1),
        //     new Vector3(1, 1, 1),
        //     new Vector3(0, 1, 1),

        //     // Back (-Z)
        //     new Vector3(1, 0, 0),
        //     new Vector3(0, 0, 0),
        //     new Vector3(0, 1, 0),
        //     new Vector3(1, 1, 0),

        //     // Left (-X)
        //     new Vector3(0, 0, 0),
        //     new Vector3(0, 0, 1),
        //     new Vector3(0, 1, 1),
        //     new Vector3(0, 1, 0),

        //     // Right (+X)
        //     new Vector3(1, 0, 1),
        //     new Vector3(1, 0, 0),
        //     new Vector3(1, 1, 0),
        //     new Vector3(1, 1, 1),

        //     // Top (+Y)
        //     new Vector3(0, 1, 1),
        //     new Vector3(1, 1, 1),
        //     new Vector3(1, 1, 0),
        //     new Vector3(0, 1, 0),

        //     // Bottom (-Y)
        //     new Vector3(0, 0, 0),
        //     new Vector3(1, 0, 0),
        //     new Vector3(1, 0, 1),
        //     new Vector3(0, 0, 1),
        // };

        // // UVs based on 2x3 texture atlas (tile size = 0.5 x 1/3)
        // float tx = 0.5f;
        // float ty = 1f / 3f;

        // // UV for each face, in this order: Front, Back, Left, Right, Top, Bottom
        // Vector2[] uvs = new Vector2[]
        // {
        //     // Front (0,2)
        //     new Vector2(0f, 2 * ty),
        //     new Vector2(tx, 2 * ty),
        //     new Vector2(tx, 3 * ty),
        //     new Vector2(0f, 3 * ty),

        //     // Back (1,2)
        //     new Vector2(tx, 2 * ty),
        //     new Vector2(2 * tx, 2 * ty),
        //     new Vector2(2 * tx, 3 * ty),
        //     new Vector2(tx, 3 * ty),

        //     // Left (0,1)
        //     new Vector2(0f, 1 * ty),
        //     new Vector2(tx, 1 * ty),
        //     new Vector2(tx, 2 * ty),
        //     new Vector2(0f, 2 * ty),

        //     // Right (1,1)
        //     new Vector2(tx, 1 * ty),
        //     new Vector2(2 * tx, 1 * ty),
        //     new Vector2(2 * tx, 2 * ty),
        //     new Vector2(tx, 2 * ty),

        //     // Top (0,0)
        //     new Vector2(0f, 0f),
        //     new Vector2(tx, 0f),
        //     new Vector2(tx, ty),
        //     new Vector2(0f, ty),

        //     // Bottom (1,0)
        //     new Vector2(tx, 0f),
        //     new Vector2(2 * tx, 0f),
        //     new Vector2(2 * tx, ty),
        //     new Vector2(tx, ty),
        // };

        // int[] triangles = new int[]
        // {
        //     // Front
        //     0, 1, 2,
        //     0, 2, 3,
        //     // Back
        //     4, 5, 6,
        //     4, 6, 7,
        //     // Left
        //     8, 9, 10,
        //     8, 10, 11,
        //     // Right
        //     12, 13, 14,
        //     12, 14, 15,
        //     // Top
        //     16, 17, 18,
        //     16, 18, 19,
        //     // Bottom
        //     20, 21, 22,
        //     20, 22, 23
        // };

        //     mesh.vertices = vertices;
        //     mesh.uv = uvs;
        //     mesh.triangles = triangles;
        //     mesh.RecalculateNormals();
        //     mesh.RecalculateBounds();

        //     MeshFilter mf = GetComponent<MeshFilter>();
        //     mf.mesh = mesh;

        //     MeshRenderer mr = GetComponent<MeshRenderer>();
        //     Material mat = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        //     mat.mainTexture = textureAtlas;
        //     mat.SetFloat("_Smoothness", 0f); // For glossiness
        //     mat.SetFloat("_Metallic", 0f);   // For reflectiveness
        //     mr.material = mat;
        // }
        public void destroy()
        {

        }

        public void alertNeighbors()
        {
            UpdateMesh();
        }
        bool meshUpdated = false;
        public void Update()
        {
            if (Variables.worldGenerated && !meshUpdated)
            {
                UpdateMesh();
                meshUpdated = true;
            }
            // 
            //     if (Variables.worldGenerated)
            //     {
            //         List<int> tri = new List<int>();
            //         try
            //         {
            //             if (Variables.ChunkData[key][x][y + 1][z] == null)
            //             {
            //                 tri.Add(0);
            //                 tri.Add(1);
            //                 tri.Add(2);
            //                 tri.Add(0);
            //                 tri.Add(2);
            //                 tri.Add(3);
            //             }
            //         }
            //         catch (IndexOutOfRangeException e)
            //         {
            //             tri.Add(0);
            //             tri.Add(1);
            //             tri.Add(2);
            //             tri.Add(0);
            //             tri.Add(2);
            //             tri.Add(3);
            //         }
            //         try
            //         {
            //             if (Variables.ChunkData[key][x][y - 1][z] == null)
            //             {
            //                 tri.Add(4);
            //                 tri.Add(5);
            //                 tri.Add(6);
            //                 tri.Add(4);
            //                 tri.Add(6);
            //                 tri.Add(7);
            //             }
            //         }
            //         catch (IndexOutOfRangeException e)
            //         {
            //             tri.Add(4);
            //             tri.Add(5);
            //             tri.Add(6);
            //             tri.Add(4);
            //             tri.Add(6);
            //             tri.Add(7);
            //         }
            //         try
            //         {
            //             if (Variables.ChunkData[key][x + 1][y][z] == null)
            //             {
            //                 tri.Add(8);
            //                 tri.Add(9);
            //                 tri.Add(10);
            //                 tri.Add(8);
            //                 tri.Add(10);
            //                 tri.Add(11);
            //             }
            //         }
            //         catch (IndexOutOfRangeException e)
            //         {
            //             tri.Add(8);
            //             tri.Add(9);
            //             tri.Add(10);
            //             tri.Add(8);
            //             tri.Add(10);
            //             tri.Add(11);
            //         }
            //         try
            //         {
            //             if (Variables.ChunkData[key][x - 1][y][z] == null)
            //             {
            //                 tri.Add(12);
            //                 tri.Add(13);
            //                 tri.Add(14);
            //                 tri.Add(12);
            //                 tri.Add(14);
            //                 tri.Add(15);
            //             }
            //         }
            //         catch (IndexOutOfRangeException e)
            //         {
            //             tri.Add(12);
            //             tri.Add(13);
            //             tri.Add(14);
            //             tri.Add(12);
            //             tri.Add(14);
            //             tri.Add(15);
            //         }
            //         try
            //         {
            //             if (Variables.ChunkData[key][x][y][z + 1] == null)
            //             {
            //                 tri.Add(16);
            //                 tri.Add(17);
            //                 tri.Add(18);
            //                 tri.Add(16);
            //                 tri.Add(18);
            //                 tri.Add(19);
            //             }
            //         }
            //         catch (IndexOutOfRangeException e)
            //         {
            //             tri.Add(16);
            //             tri.Add(17);
            //             tri.Add(18);
            //             tri.Add(16);
            //             tri.Add(18);
            //             tri.Add(19);
            //         }
            //         try
            //         {
            //             if (Variables.ChunkData[key][x][y][z - 1] == null)
            //             {
            //                 tri.Add(20);
            //                 tri.Add(21);
            //                 tri.Add(22);
            //                 tri.Add(20);
            //                 tri.Add(22);
            //                 tri.Add(23);
            //             }
            //         }
            //         catch (IndexOutOfRangeException e)
            //         {
            //             tri.Add(20);
            //             tri.Add(21);
            //             tri.Add(22);
            //             tri.Add(20);
            //             tri.Add(22);
            //             tri.Add(23);
            //         }

            //         int[] triangles = tri.ToArray();
            //         MeshFilter mf = GetComponent<MeshFilter>();
            //         MeshRenderer mr = GetComponent<MeshRenderer>();
            //         if (textureAtlas == null)
            //         {
            //             Debug.LogError("Texture atlas not found! Make sure it's in Resources and named correctly.");
            //         }
            //         Mesh mesh = new Mesh();
            //         mf.mesh = mesh;



            //         Vector2[] uv = new Vector2[24];

            //         // Top-left (top face): U 0–0.5, V 0.5–1.0
            //         uv[16] = new Vector2(0.0f, 0.5f);
            //         uv[17] = new Vector2(0.5f, 0.5f);
            //         uv[18] = new Vector2(0.5f, 1.0f);
            //         uv[19] = new Vector2(0.0f, 1.0f);

            //         // Top-right (sides): U 0.5–1.0, V 0.5–1.0
            //         for (int i = 0; i < 16; i += 4)
            //         {
            //             uv[i + 0] = new Vector2(0.5f, 0.5f);
            //             uv[i + 1] = new Vector2(1.0f, 0.5f);
            //             uv[i + 2] = new Vector2(1.0f, 1.0f);
            //             uv[i + 3] = new Vector2(0.5f, 1.0f);
            //         }


            //         // Bottom-left (bottom face): U 0–0.5, V 0–0.5
            //         uv[20] = new Vector2(0.0f, 0.0f);
            //         uv[21] = new Vector2(0.5f, 0.0f);
            //         uv[22] = new Vector2(0.5f, 0.5f);
            //         uv[23] = new Vector2(0.0f, 0.5f);
            //         Vector3[] vertices = new Vector3[]
            //                     {
            //     // Front face
            //     new Vector3(-0.5f, -0.5f,  0.5f), // 0
            //     new Vector3( 0.5f, -0.5f,  0.5f), // 1
            //     new Vector3( 0.5f,  0.5f,  0.5f), // 2
            //     new Vector3(-0.5f,  0.5f,  0.5f), // 3

            //     // Back face
            //     new Vector3( 0.5f, -0.5f, -0.5f), // 4
            //     new Vector3(-0.5f, -0.5f, -0.5f), // 5
            //     new Vector3(-0.5f,  0.5f, -0.5f), // 6
            //     new Vector3( 0.5f,  0.5f, -0.5f), // 7

            //     // Left face
            //     new Vector3(-0.5f, -0.5f, -0.5f), // 8
            //     new Vector3(-0.5f, -0.5f,  0.5f), // 9
            //     new Vector3(-0.5f,  0.5f,  0.5f), // 10
            //     new Vector3(-0.5f,  0.5f, -0.5f), // 11

            //     // Right face
            //     new Vector3( 0.5f, -0.5f,  0.5f), // 12
            //     new Vector3( 0.5f, -0.5f, -0.5f), // 13
            //     new Vector3( 0.5f,  0.5f, -0.5f), // 14
            //     new Vector3( 0.5f,  0.5f,  0.5f), // 15

            //     // Top face
            //     new Vector3(-0.5f,  0.5f,  0.5f), // 16
            //     new Vector3( 0.5f,  0.5f,  0.5f), // 17
            //     new Vector3( 0.5f,  0.5f, -0.5f), // 18
            //     new Vector3(-0.5f,  0.5f, -0.5f), // 19

            //     // Bottom face
            //     new Vector3(-0.5f, -0.5f, -0.5f), // 20
            //     new Vector3( 0.5f, -0.5f, -0.5f), // 21
            //     new Vector3( 0.5f, -0.5f,  0.5f), // 22
            //     new Vector3(-0.5f, -0.5f,  0.5f), // 23
            //                     };

            //         mesh.vertices = vertices;
            //         mesh.triangles = triangles;
            //         mesh.uv = uv;
            //         mesh.RecalculateNormals();

            //         // Apply material
            //         Material mat = new Material(Shader.Find("Universal Render Pipeline/Lit"));
            //         mat.mainTexture = textureAtlas;
            //         mat.SetFloat("_Smoothness", 0f); // For glossiness
            //         mat.SetFloat("_Metallic", 0f);   // For reflectiveness
            //         mr.material = mat;
            //     }
        }
        public void UpdateMesh()
        {
            if (Variables.worldGenerated)
            {
                // int[] triangles = {
                // 0, 1, 2, 0, 2, 3, 4, 5, 6, 4, 6, 7, 8, 9, 10, 8, 10, 11, 12, 13, 14, 12, 14, 15, 16, 17, 18, 16, 18, 19, 20, 21, 22, 20, 22, 23
                // };
                List<int> tri = new List<int>();
                List<Vector3> vert = new List<Vector3>();
                List<Vector2> uvlist = new List<Vector2>();
                // Vector3[] vertices = new Vector3[24];
                try
                {
                    // Debug.LogWarning(Variables.ChunkData[key]);
                    if (Variables.ChunkData[key][x][y + 1][z] == null)
                    {
                        tri.Add(0);
                        tri.Add(1);
                        tri.Add(2);
                        tri.Add(0);
                        tri.Add(2);
                        tri.Add(3);
                        uvlist.Add(new Vector2(0.5f, 0.5f));
                        uvlist.Add(new Vector2(1.0f, 0.5f));
                        uvlist.Add(new Vector2(1.0f, 1.0f));
                        uvlist.Add(new Vector2(0.5f, 1.0f));
                        vert.Add(new Vector3(-0.5f, -0.5f, 0.5f)); // 0
                        vert.Add(new Vector3(0.5f, -0.5f, 0.5f)); // 1
                        vert.Add(new Vector3(0.5f, 0.5f, 0.5f)); // 2
                        vert.Add(new Vector3(-0.5f, 0.5f, 0.5f)); // 3

                    }
                }
                catch (IndexOutOfRangeException)
                {
                    tri.Add(0);
                    tri.Add(1);
                    tri.Add(2);
                    tri.Add(0);
                    tri.Add(2);
                    tri.Add(3);
                    uvlist.Add(new Vector2(0.5f, 0.5f));
                    uvlist.Add(new Vector2(1.0f, 0.5f));
                    uvlist.Add(new Vector2(1.0f, 1.0f));
                    uvlist.Add(new Vector2(0.5f, 1.0f));
                    vert.Add(new Vector3(-0.5f, -0.5f, 0.5f)); // 0
                    vert.Add(new Vector3(0.5f, -0.5f, 0.5f)); // 1
                    vert.Add(new Vector3(0.5f, 0.5f, 0.5f)); // 2
                    vert.Add(new Vector3(-0.5f, 0.5f, 0.5f)); // 3
                }
                try
                {
                    if (Variables.ChunkData[key][x][y - 1][z] == null)
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
                        vert.Add(new Vector3(0.5f, -0.5f, -0.5f)); // 4
                        vert.Add(new Vector3(-0.5f, -0.5f, -0.5f)); // 5
                        vert.Add(new Vector3(-0.5f, 0.5f, -0.5f)); // 6
                        vert.Add(new Vector3(0.5f, 0.5f, -0.5f)); // 7
                    }
                }
                catch (IndexOutOfRangeException)
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
                    vert.Add(new Vector3(0.5f, -0.5f, -0.5f)); // 4
                    vert.Add(new Vector3(-0.5f, -0.5f, -0.5f)); // 5
                    vert.Add(new Vector3(-0.5f, 0.5f, -0.5f)); // 6
                    vert.Add(new Vector3(0.5f, 0.5f, -0.5f)); // 7
                }
                try
                {
                    if (Variables.ChunkData[key][x - 1][y][z] == null)
                    {
                        int count = vert.Count;
                        tri.Add(count);
                        tri.Add(count + 1);
                        tri.Add(count + 2);
                        tri.Add(count);
                        tri.Add(count + 2);
                        tri.Add(count + 3);
                        vert.Add(new Vector3(-0.5f, -0.5f, -0.5f)); // 8
                        vert.Add(new Vector3(-0.5f, -0.5f, 0.5f)); // 9
                        vert.Add(new Vector3(-0.5f, 0.5f, 0.5f)); // 10
                        vert.Add(new Vector3(-0.5f, 0.5f, -0.5f)); // 11
                        uvlist.Add(new Vector2(0.5f, 0.5f));
                        uvlist.Add(new Vector2(1.0f, 0.5f));
                        uvlist.Add(new Vector2(1.0f, 1.0f));
                        uvlist.Add(new Vector2(0.5f, 1.0f));

                    }
                }
                catch (IndexOutOfRangeException)
                {
                    int count = vert.Count;
                    tri.Add(count);
                    tri.Add(count + 1);
                    tri.Add(count + 2);
                    tri.Add(count);
                    tri.Add(count + 2);
                    tri.Add(count + 3);
                    vert.Add(new Vector3(-0.5f, -0.5f, -0.5f)); // 8
                    vert.Add(new Vector3(-0.5f, -0.5f, 0.5f)); // 9
                    vert.Add(new Vector3(-0.5f, 0.5f, 0.5f)); // 10
                    vert.Add(new Vector3(-0.5f, 0.5f, -0.5f)); // 11
                    uvlist.Add(new Vector2(0.5f, 0.5f));
                    uvlist.Add(new Vector2(1.0f, 0.5f));
                    uvlist.Add(new Vector2(1.0f, 1.0f));
                    uvlist.Add(new Vector2(0.5f, 1.0f));
                }
                try
                {
                    if (Variables.ChunkData[key][x + 1][y][z] == null)
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
                        vert.Add(new Vector3(0.5f, -0.5f, 0.5f)); // 12
                        vert.Add(new Vector3(0.5f, -0.5f, -0.5f)); // 13
                        vert.Add(new Vector3(0.5f, 0.5f, -0.5f)); // 14
                        vert.Add(new Vector3(0.5f, 0.5f, 0.5f)); // 15
                    }
                }
                catch (IndexOutOfRangeException)
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
                    vert.Add(new Vector3(0.5f, -0.5f, 0.5f)); // 12
                    vert.Add(new Vector3(0.5f, -0.5f, -0.5f)); // 13
                    vert.Add(new Vector3(0.5f, 0.5f, -0.5f)); // 14
                    vert.Add(new Vector3(0.5f, 0.5f, 0.5f)); // 15
                }
                try
                {
                    if (Variables.ChunkData[key][x][y][z + 1] == null)
                    {
                        int count = vert.Count;
                        tri.Add(count);
                        tri.Add(count + 1);
                        tri.Add(count + 2);
                        tri.Add(count);
                        tri.Add(count + 2);
                        tri.Add(count + 3);
                        // Top-left (top face): U 0–0.5, V 0.5–1.0
                        uvlist.Add(new Vector2(0.0f, 0.5f));
                        uvlist.Add(new Vector2(0.5f, 0.5f));
                        uvlist.Add(new Vector2(0.5f, 1.0f));
                        uvlist.Add(new Vector2(0.0f, 1.0f));
                        vert.Add(new Vector3(-0.5f, 0.5f, 0.5f)); // 16
                        vert.Add(new Vector3(0.5f, 0.5f, 0.5f)); // 17
                        vert.Add(new Vector3(0.5f, 0.5f, -0.5f)); // 18
                        vert.Add(new Vector3(-0.5f, 0.5f, -0.5f)); // 19
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    int count = vert.Count;
                    tri.Add(count);
                    tri.Add(count + 1);
                    tri.Add(count + 2);
                    tri.Add(count);
                    tri.Add(count + 2);
                    tri.Add(count + 3);
                    // Top-left (top face): U 0–0.5, V 0.5–1.0
                    uvlist.Add(new Vector2(0.0f, 0.5f));
                    uvlist.Add(new Vector2(0.5f, 0.5f));
                    uvlist.Add(new Vector2(0.5f, 1.0f));
                    uvlist.Add(new Vector2(0.0f, 1.0f));
                    vert.Add(new Vector3(-0.5f, 0.5f, 0.5f)); // 16
                    vert.Add(new Vector3(0.5f, 0.5f, 0.5f)); // 17
                    vert.Add(new Vector3(0.5f, 0.5f, -0.5f)); // 18
                    vert.Add(new Vector3(-0.5f, 0.5f, -0.5f)); // 19
                }
                try
                {
                    if (Variables.ChunkData[key][x][y][z - 1] == null)
                    {
                        int count = vert.Count;
                        tri.Add(count);
                        tri.Add(count + 1);
                        tri.Add(count + 2);
                        tri.Add(count);
                        tri.Add(count + 2);
                        tri.Add(count + 3);
                        uvlist.Add(new Vector2(0.0f, 0.0f));
                        uvlist.Add(new Vector2(0.5f, 0.0f));
                        uvlist.Add(new Vector2(0.5f, 0.5f));
                        uvlist.Add(new Vector2(0.0f, 0.5f));
                        vert.Add(new Vector3(-0.5f, -0.5f, -0.5f));
                        vert.Add(new Vector3(0.5f, -0.5f, -0.5f));
                        vert.Add(new Vector3(0.5f, -0.5f, 0.5f));
                        vert.Add(new Vector3(-0.5f, -0.5f, 0.5f));
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    int count = vert.Count;
                    tri.Add(count);
                    tri.Add(count + 1);
                    tri.Add(count + 2);
                    tri.Add(count);
                    tri.Add(count + 2);
                    tri.Add(count + 3);
                    uvlist.Add(new Vector2(0.0f, 0.0f));
                    uvlist.Add(new Vector2(0.5f, 0.0f));
                    uvlist.Add(new Vector2(0.5f, 0.5f));
                    uvlist.Add(new Vector2(0.0f, 0.5f));
                    vert.Add(new Vector3(-0.5f, -0.5f, -0.5f));
                    vert.Add(new Vector3(0.5f, -0.5f, -0.5f));
                    vert.Add(new Vector3(0.5f, -0.5f, 0.5f));
                    vert.Add(new Vector3(-0.5f, -0.5f, 0.5f));
                }

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
                mf.mesh = mesh;




                // Top-left (top face): U 0–0.5, V 0.5–1.0
                // uv[16] = new Vector2(0.0f, 0.5f);
                // uv[17] = new Vector2(0.5f, 0.5f);
                // uv[18] = new Vector2(0.5f, 1.0f);
                // uv[19] = new Vector2(0.0f, 1.0f);

                // Top-right (sides): U 0.5–1.0, V 0.5–1.0
                // for (int i = 0; i < 16; i += 4)
                // {
                //     uv[i + 0] = new Vector2(0.5f, 0.5f);
                //     uv[i + 1] = new Vector2(1.0f, 0.5f);
                //     uv[i + 2] = new Vector2(1.0f, 1.0f);
                //     uv[i + 3] = new Vector2(0.5f, 1.0f);
                // }


                // Bottom-left (bottom face): U 0–0.5, V 0–0.5
                // uv[20] = new Vector2(0.0f, 0.0f);
                // uv[21] = new Vector2(0.5f, 0.0f);
                // uv[22] = new Vector2(0.5f, 0.5f);
                // uv[23] = new Vector2(0.0f, 0.5f);
                // Vector3[] vertices = new Vector3[]
                //             {
                // // Front face
                // new Vector3(-0.5f, -0.5f,  0.5f), // 0
                // new Vector3( 0.5f, -0.5f,  0.5f), // 1
                // new Vector3( 0.5f,  0.5f,  0.5f), // 2
                // new Vector3(-0.5f,  0.5f,  0.5f), // 3

                // // Back face
                // new Vector3( 0.5f, -0.5f, -0.5f), // 4
                // new Vector3(-0.5f, -0.5f, -0.5f), // 5
                // new Vector3(-0.5f,  0.5f, -0.5f), // 6
                // new Vector3( 0.5f,  0.5f, -0.5f), // 7

                // // Left face
                // new Vector3(-0.5f, -0.5f, -0.5f), // 8
                // new Vector3(-0.5f, -0.5f,  0.5f), // 9
                // new Vector3(-0.5f,  0.5f,  0.5f), // 10
                // new Vector3(-0.5f,  0.5f, -0.5f), // 11

                // // Right face
                // new Vector3( 0.5f, -0.5f,  0.5f), // 12
                // new Vector3( 0.5f, -0.5f, -0.5f), // 13
                // new Vector3( 0.5f,  0.5f, -0.5f), // 14
                // new Vector3( 0.5f,  0.5f,  0.5f), // 15

                // // Top face
                // new Vector3(-0.5f,  0.5f,  0.5f), // 16
                // new Vector3( 0.5f,  0.5f,  0.5f), // 17
                // new Vector3( 0.5f,  0.5f, -0.5f), // 18
                // new Vector3(-0.5f,  0.5f, -0.5f), // 19

                // // Bottom face
                // new Vector3(-0.5f, -0.5f, -0.5f), // 20
                // new Vector3( 0.5f, -0.5f, -0.5f), // 21
                // new Vector3( 0.5f, -0.5f,  0.5f), // 22
                // new Vector3(-0.5f, -0.5f,  0.5f), // 23
                //             };

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
            }
        }
    }
}