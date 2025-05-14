using UnityEngine;

namespace Client{
    public class Chunk : MonoBehaviour{
        int chunkX;
        int chunkY;
        public void Start()
        {
            gameObject.AddComponent<MeshFilter>();
            gameObject.AddComponent<MeshRenderer>();
        }

        public void SetUp(int chunkX, int chunkY){
            this.chunkX = chunkX;
            this.chunkY = chunkY;
            gameObject.name = "Chunk " + chunkX + " " + chunkY;
            gameObject.transform.position = new Vector3(chunkX * 16, 0, chunkY * 16);
            Mesh mesh = new Mesh();
            GetComponent<MeshFilter>().mesh = mesh;
        }
    }
}