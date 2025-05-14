// using System;
// using UnityEngine;
// namespace Client
// {
//     public class Gravity : MonoBehaviour
//     {
//         // Start is called once before the first execution of Update after the MonoBehaviour is created
//         void Start()
//         {

//         }

//         // Update is called once per frame
//         void OnCollisionEnter(Collision collision)
//         {
//             checkGravity(collision);
//         }

//         void OnCollisionStay(Collision collision)
//         {
//             checkGravity(collision);
//         }
//         void OnCollisionExit(Collision collision)
//         {
//             if (collision.gameObject.tag == "b")
//             {
//                 Vector3 playerPos = transform.parent.parent.position;
//                 if (collision.gameObject == Variables.ChunkData[new Tuple<int, int>((int) Math.Floor(playerPos.x/16), (int) Math.Floor(playerPos.y/16))][(int)Math.Floor(playerPos.x)][(int)Math.Floor(playerPos.z)][(int)Math.Floor(playerPos.y-1)]){
//                     Variables.isGrounded = false;
//                 }
//             }
//         }

//         void checkGravity(Collision collision){
//             if (collision.gameObject.tag == "b")
//             {
//                 Vector3 playerPos = transform.parent.parent.position;
//                 if (collision.gameObject == Variables.ChunkData[new Tuple<int, int>((int) Math.Floor(playerPos.x/16), (int) Math.Floor(playerPos.y/16))][(int)Math.Floor(playerPos.x)][(int)Math.Floor(playerPos.z)][(int)Math.Floor(playerPos.y-1)]){
//                     Variables.isGrounded = true;
//                 } else {
//                     Variables.isGrounded = false;
//                 }
//             }
//         }
//     }
// }
