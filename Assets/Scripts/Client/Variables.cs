using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;

namespace Client
{
    public class Variables
    {
        //For Z, anything below 100, is -100 to 0, and anything above 100 is 0 to 100. 100 is 0.
        public static ConcurrentDictionary<Tuple<int, int>, Block[][][]> BlockData = new ConcurrentDictionary<Tuple<int, int>, Block[][][]>();
        public static ConcurrentDictionary<Tuple<int, int>, GameObject> ChunkData = new ConcurrentDictionary<Tuple<int, int>, GameObject>();
        public static ConcurrentDictionary<BlockType, ConcurrentDictionary<BlockFace, Vector2[]>> UVs = new ConcurrentDictionary<BlockType, ConcurrentDictionary<BlockFace, Vector2[]>> (
            new Dictionary<BlockType, ConcurrentDictionary<BlockFace, Vector2[]>>
            {
                {BlockType.Grass, new ConcurrentDictionary<BlockFace, Vector2[]>( new Dictionary<BlockFace, Vector2[]>
                    {
                        {BlockFace.Top, new Vector2[]
                            {
                                new Vector2(0.0f, 0.5f),
                                new Vector2(0.5f, 0.5f),
                                new Vector2(0.5f, 1.0f),
                                new Vector2(0.0f, 1.0f)
                            }
                        },
                        {BlockFace.Bottom, new Vector2[]
                            {
                                new Vector2(0.0f, 0.0f),
                                new Vector2(0.5f, 0.0f),
                                new Vector2(0.5f, 0.5f),
                                new Vector2(0.0f, 0.5f)
                            }
                        },
                        {BlockFace.Left, new Vector2[]
                            {
                                new Vector2(0.5f, 0.5f),
                                new Vector2(1.0f, 0.5f),
                                new Vector2(1.0f, 1.0f),
                                new Vector2(0.5f, 1.0f)
                            }
                        },
                        {BlockFace.Right, new Vector2[]
                            {
                                new Vector2(0.5f, 0.5f),
                                new Vector2(1.0f, 0.5f),
                                new Vector2(1.0f, 1.0f),
                                new Vector2(0.5f, 1.0f)
                            }
                        },
                        {BlockFace.Front, new Vector2[]
                            {
                                new Vector2(0.5f, 0.5f),
                                new Vector2(1.0f, 0.5f),
                                new Vector2(1.0f, 1.0f),
                                new Vector2(0.5f, 1.0f)
                            }
                        },
                        {BlockFace.Back, new Vector2[]
                            {
                                new Vector2(0.5f, 0.5f),
                                new Vector2(1.0f, 0.5f),
                                new Vector2(1.0f, 1.0f),
                                new Vector2(0.5f, 1.0f)
                            }
                        },
                    })
                }
            }
        );

        public static ConcurrentDictionary<BlockFace, Vector3[]> Verts = new ConcurrentDictionary<BlockFace, Vector3[]> (
            new Dictionary<BlockFace, Vector3[]>
            {
                {BlockFace.Top, new Vector3[] 
                    { 
                        new Vector3(-0.5f, 0.5f, 0.5f),// 16
                        new Vector3(0.5f, 0.5f, 0.5f),// 17
                        new Vector3(0.5f, 0.5f, -0.5f), // 18
                        new Vector3(-0.5f, 0.5f, -0.5f) // 19
                    }
                },
                {BlockFace.Bottom, new Vector3[] 
                    { 
                        new Vector3(-0.5f, -0.5f, -0.5f),
                        new Vector3(0.5f, -0.5f, -0.5f),
                        new Vector3(0.5f, -0.5f, 0.5f),
                        new Vector3(-0.5f, -0.5f, 0.5f)
                    }
                },
                {BlockFace.Right, new Vector3[] 
                    { 
                        new Vector3(0.5f, -0.5f, 0.5f),
                        new Vector3(0.5f, -0.5f, -0.5f),
                        new Vector3(0.5f, 0.5f, -0.5f),
                        new Vector3(0.5f, 0.5f, 0.5f)
                    }
                },
                {BlockFace.Left, new Vector3[] 
                    { 
                        new Vector3(-0.5f, -0.5f, -0.5f),
                        new Vector3(-0.5f, -0.5f, 0.5f),
                        new Vector3(-0.5f, 0.5f, 0.5f),
                        new Vector3(-0.5f, 0.5f, -0.5f),
                    }
                },
                {BlockFace.Back, new Vector3[] 
                    { 
                        new Vector3(0.5f, -0.5f, -0.5f),
                        new Vector3(-0.5f, -0.5f, -0.5f),
                        new Vector3(-0.5f, 0.5f, -0.5f),
                        new Vector3(.5f, 0.5f, -0.5f)
                    }
                },
                {BlockFace.Front, new Vector3[] 
                    { 
                        new Vector3(-0.5f, -0.5f, 0.5f),
                        new Vector3(0.5f, -0.5f, 0.5f),
                        new Vector3(0.5f, 0.5f, 0.5f),
                        new Vector3(-0.5f, 0.5f, 0.5f)
                    }
                }
            }
        );


        public static bool worldGenerated = false;
        public static bool isGrounded = false;
        public static string seed = "";
    }

    public enum BlockType
    {
        None,
        Grass,
        Dirt,
        Stone,
        Sand,
        Water,
        Lava,
        Wood,
        Leaves,
        Bedrock,
        Cobblestone,
        Planks,
        Glass,
        Brick,
        Clay,
        Snow
    }

    public enum BlockFace{
        Top,
        Bottom,
        Left,
        Right,
        Front,
        Back
    }

    public enum BlockProperties{
        None,
        Transparent,
        Solid,
        Liquid
    }
}