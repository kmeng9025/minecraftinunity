using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;

namespace Client{
    public class Variables{
        //For Z, anything below 100, is -100 to 0, and anything above 100 is 0 to 100. 100 is 0.
        public static ConcurrentDictionary<Tuple<int, int>, GameObject[][][]> ChunkData = new ConcurrentDictionary<Tuple<int, int>, GameObject[][][]>();
        public static bool worldGenerated = false;
        public static bool isGrounded = false;
    }
}