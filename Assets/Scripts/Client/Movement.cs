// using System;
// using Client;
// using Unity.Collections;
// using UnityEngine;
// using UnityEngine.InputSystem;
// using UnityEngine.UIElements;
// using UnityEngine.XR;
// namespace Clien
// {
//     public class Movement : MonoBehaviour
//     {
//         // Start is called once before the first execution of Update after the MonoBehaviour is created
//         bool first = true;
//         ConnectToHost connectToHost;
//         float xSpeed = 5f;
//         float ySpeed = 5f;
//         float xSpeedModifier = 1f;
//         float ySpeedModifier = 1f;
//         Rigidbody rb;
//         [SerializeField] private LayerMask collisionLayerMask;
//         bool jumping = false;
//         public bool isGrounded = true;
//         float jumpHeight = 1.25f;
//         float jumpTimeout = 0f;
//         public float verticalVelocity = 0f;
//         float Gravity = -10.31f;
//         float xVelocity = 0f;
//         float yVelocity = 0f;
//         float acceleration = 0.6f;
//         float deceleration = 0.8f;
//         public bool moving = false;
//         float collisionOffset = 0.1f;
//         float sensitivity = 2f;
//         CharacterController controller;
//         float currentRotationX;
//         float currentRotationY;
//         float maxYAngle = 80f;
//         // float groundedOffset = 0.15f
//         void Start()
//         {
//             connectToHost = ConnectToHost.getInstance();
//             rb = GetComponent<Rigidbody>();
//             UnityEngine.Cursor.lockState = CursorLockMode.Locked;
//             UnityEngine.Cursor.visible = false;
//             transform.Find("Visual").transform.Find("Chest").transform.GetComponent<MeshRenderer>().enabled = false;
//             transform.Find("Visual").transform.Find("Left Leg").transform.GetComponent<MeshRenderer>().enabled = false;
//             transform.Find("Visual").transform.Find("Right Leg").transform.GetComponent<MeshRenderer>().enabled = false;
//             transform.Find("Visual").transform.Find("Left Arm").transform.GetComponent<MeshRenderer>().enabled = false;
//             transform.Find("Visual").transform.Find("Right Arm").transform.GetComponent<MeshRenderer>().enabled = false;
//             transform.Find("Visual").transform.Find("Head").transform.GetComponent<MeshRenderer>().enabled = false;
//         }

//         // Update is called once per frame
//         void Update()
//         {
//             SetUpPlayerInitialPosition();
//             if (Variables.worldGenerated)
//             {
//                 calculateCameraAngle();
//                 checkGrounded();
//                 gravity();
//                 jump();
//                 Move();
//             }
//         }
//         void calculateCameraAngle()
//         {
//             if (Variables.worldGenerated)
//             {
//                 float mouseX = Input.GetAxis("Mouse X");
//                 float mouseY = Input.GetAxis("Mouse Y");

//                 // Calculate rotation
//                 currentRotationX += mouseX * sensitivity;
//                 currentRotationY -= mouseY * sensitivity;

//                 // Clamp the vertical rotation
//                 currentRotationY = Mathf.Clamp(currentRotationY, -maxYAngle, maxYAngle);

//                 // Apply rotation to the camera
//                 transform.Find("Visual").transform.Find("Head").rotation = Quaternion.Euler(0, currentRotationX, -currentRotationY);
//                 transform.rotation = Quaternion.Euler(0, currentRotationX, 0);
//             }
//         }
//         void jump()
//         {
//             if (jumping && isGrounded && jumpTimeout <= 0f)
//             {
//                 verticalVelocity += Mathf.Sqrt(12.5f);
//             }
//         }
//         void Move()
//         {
//             bool limitX = false;
//             bool limitY = false;
//             bool limitZ = false;
//             if (!jumping && isGrounded)
//             {
//                 verticalVelocity = 0f;
//             }

//             Vector3 worldVelocity = transform.TransformDirection(new Vector3(xVelocity, verticalVelocity, yVelocity));

//             Vector3 halfExtents = GetComponent<BoxCollider>().size / 2f;
//             Quaternion rotation = transform.rotation;
//             Vector3 pos = transform.position;
//             Vector3[] yRayPositions = {new(0.3f, 1 , 0.3f), new(0.3f, 1, -0.3f), new(-0.3f, 1, 0.3f), new(-0.3f, 1, -0.3f)};
//             Vector3[] xRayPositions = {new(-0.3f, -1, 0.3f), new(0.3f, -1, 0.3f), new(-0.3f, 1, 0.3f), new(0.3f, 1, 0.3f)};
//             Vector3[] zRayPositions = {new(0.3f,-1 , 0.3f), new(0.3f, -1, -0.3f), new(0.3f, 1, 0.3f), new(0.3f, 1, -0.3f)};
//             float lowestX = float.MaxValue;
//             float lowestY = float.MaxValue;
//             float lowestZ = float.MaxValue;
//             for (int i = 0; i < 4; i++)
//             {
//                 RaycastHit hit;
//                 xRayPositions[i].x = Math.Sign(worldVelocity.x) * xRayPositions[i].x;
//                 Vector3 direction = new Vector3(Mathf.Sign(worldVelocity.x), 0, 0);
//                 if (Physics.Raycast(pos+xRayPositions[i], direction, out hit, worldVelocity.x, collisionLayerMask, QueryTriggerInteraction.Ignore))
//                 {
//                     if(lowestX > (pos+xRayPositions[i]).x - hit.transform.position.x){
//                         lowestX = (pos+xRayPositions[i]).x - hit.transform.position.x;
//                     }
//                 }
//             }
//             if(!(lowestX == float.MaxValue))
//             {
//                 worldVelocity.x = lowestX;
//             }
//             for (int i = 0; i < 4; i++)
//             {
//                 RaycastHit hit;
//                 zRayPositions[i].z = Math.Sign(worldVelocity.z) * zRayPositions[i].z;
//                 Vector3 direction = new Vector3(Mathf.Sign(worldVelocity.z), 0, 0);
//                 if (Physics.Raycast(pos+zRayPositions[i], direction, out hit, worldVelocity.z, collisionLayerMask, QueryTriggerInteraction.Ignore))
//                 {
//                     if(lowestZ > (pos+zRayPositions[i]).z - hit.transform.position.z){
//                         lowestZ = (pos+zRayPositions[i]).z - hit.transform.position.z;
//                     }
//                 }
//             }
//             if(!(lowestZ == float.MaxValue))
//             {
//                 worldVelocity.z = lowestZ;
//             }
//             for (int i = 0; i < 4; i++)
//             {
//                 RaycastHit hit;
//                 yRayPositions[i].y = Math.Sign(worldVelocity.y) * yRayPositions[i].y;
//                 Vector3 direction = new Vector3(Mathf.Sign(worldVelocity.y), 0, 0);
//                 if (Physics.Raycast(pos+yRayPositions[i], direction, out hit, worldVelocity.y, collisionLayerMask, QueryTriggerInteraction.Ignore))
//                 {
//                     if(lowestY > (pos+yRayPositions[i]).y - hit.transform.position.y){
//                         lowestY = (pos+yRayPositions[i]).y - hit.transform.position.y + 0.1f;
//                     }
//                 }
//             }
//             if(!(lowestY == float.MaxValue))
//             {
//                 worldVelocity.y = lowestY;
//             }
//             // if (limitX)
//             // {
//             //     worldVelocity.x = 0;
//             // }
//             // if (limitY)
//             // {
//             //     worldVelocity.y = 0;
//             // }
//             // if (limitZ)
//             // {
//             //     worldVelocity.z = 0;
//             // }
//             transform.position += worldVelocity;
//             // // X-axis collision
//             // if (Mathf.Abs(xVelocity) > 0)
//             // {
//             //     Vector3 world = transform.TransformDirection(new Vector3(Mathf.Sign(xVelocity), 0, 0));
//             //     world.z = 0;
//             //     world.y = 0;
//             //     Vector3 direction = transform.TransformDirection(new Vector3(Mathf.Sign(xVelocity), 0, 0));
//             //     Vector3 checkPos = pos + direction * (Mathf.Abs(xVelocity) + collisionOffset);
//             //     if (Physics.CheckBox(checkPos, halfExtents, rotation, collisionLayerMask, QueryTriggerInteraction.Ignore))
//             //     {
//             //         limitX = true;
//             //     }
//             // }

//             // // Y-axis (vertical) collision
//             // if (Mathf.Abs(verticalVelocity) > 0)
//             // {
//             //     Vector3 world = transform.TransformDirection(new Vector3(0, Mathf.Sign(verticalVelocity), 0));
//             //     world.x = 0;
//             //     world.z = 0;
//             //     Vector3 direction = world;
//             //     Vector3 checkPos = pos + direction * (Mathf.Abs(verticalVelocity) + collisionOffset);
//             //     if (Physics.CheckBox(checkPos, halfExtents, rotation, collisionLayerMask, QueryTriggerInteraction.Ignore))
//             //     {
//             //         limitY = true;
//             //     }
//             // }

//             // // Z-axis collision
//             // if (Mathf.Abs(yVelocity) > 0)
//             // {
//             //     Vector3 world = transform.TransformDirection(new Vector3(0, 0, Mathf.Sign(yVelocity)));
//             //     world.x = 0;
//             //     world.y = 0;
//             //     Vector3 direction = world;
//             //     Vector3 checkPos = pos + direction * (Mathf.Abs(yVelocity) + collisionOffset);
//             //     if (Physics.CheckBox(checkPos, halfExtents, rotation, collisionLayerMask, QueryTriggerInteraction.Ignore))
//             //     {
//             //         limitZ = true;
//             //     }
//             // }

//         }
//         void checkGrounded()
//         {
//             RaycastHit hit;
//             if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.1f, LayerMask.GetMask("Blocks"), QueryTriggerInteraction.Ignore))
//             {
//                 isGrounded = true;
//             }
//             else
//             {
//                 isGrounded = false;
//             }
//         }
//         void SetUpPlayerInitialPosition()
//         {
//             if (Variables.worldGenerated && first)
//             {
//                 for (int i = 0; i < 100; i++)
//                 {
//                     try
//                     {
//                         if (Variables.BlockData[new Tuple<int, int>(0, 0)][0][0][i] == null)
//                         {
//                             transform.position = new Vector3(0, i + 2.5f, 0);
//                             break;
//                         }
//                     }
//                     catch (Exception)
//                     {
//                         transform.position = new Vector3(0, i + 2.5f, 0);
//                         break;
//                     }
//                 }
//                 first = false;
//             }
//             if (Variables.worldGenerated)
//             {
//                 connectToHost.SendMessageToHost("pp " + transform.position.x + " " + transform.position.y + " " + transform.position.z);
//             }
//         }
//         void gravity()
//         {
//             if (!isGrounded)
//             {
//                 verticalVelocity += Gravity * Time.deltaTime;
//             }
//         }


//         public void OnMovement(InputValue inputValue)
//         {
//             if (Variables.worldGenerated)
//             {
//                 Vector2 input = inputValue.Get<Vector2>();
//                 if (input.x == 0 && input.y == 0)
//                 {
//                     xVelocity = Mathf.Lerp(xVelocity, 0, deceleration);
//                     yVelocity = Mathf.Lerp(yVelocity, 0, deceleration);
//                     moving = false;
//                 }
//                 else
//                 {
//                     moving = true;
//                 }
//                 float xMovement = input.y * Time.deltaTime * xSpeed * xSpeedModifier;
//                 float yMovement = -input.x * Time.deltaTime * ySpeed * ySpeedModifier;
//                 xVelocity = Mathf.Lerp(xVelocity, xMovement, acceleration);
//                 yVelocity = Mathf.Lerp(yVelocity, yMovement, acceleration);
//             }
//         }

//         public void OnJump(InputValue inputValue)
//         {
//             if (Variables.worldGenerated)
//             {
//                 if (inputValue.isPressed)
//                 {
//                     jumping = true;
//                 }
//                 else
//                 {
//                     jumping = false;
//                     jumpTimeout = 0f;
//                 }
//             }
//         }

//         // void OnCollisionExit(Collision collision)
//         // {
//         //     if(collision.gameObject.tag == "b"){
//         //         if(Variables.Blocks[(int) Math.Floor(transform.position.x/16)][ (int) Math.Floor(transform.position.y/16)][(int) Math.Floor(transform.position.x)%16][(int) Math.Floor(transform.position.y)%16][(int) Math.Floor(transform.position.z-2)] == collision.gameObject){
//         //             isGrounded = false;
//         //         }
//         //     }

//         // }

//     }

//     // Vector3[] vertices = new Vector3[]
//     //             {
//     //             // Front face
//     //             new Vector3(-1f, -1f,  1f), // 0
//     //             new Vector3( 1f, -1f,  1f), // 1
//     //             new Vector3( 1f,  1f,  1f), // 2
//     //             new Vector3(-1f,  1f,  1f), // 3

//     //             // Back face
//     //             new Vector3( 1f, -1f, -1f), // 4
//     //             new Vector3(-1f, -1f, -1f), // 5
//     //             new Vector3(-1f,  1f, -1f), // 6
//     //             new Vector3( 1f,  1f, -1f), // 7

//     //             // Left face
//     //             new Vector3(-1f, -1f, -1f), // 8
//     //             new Vector3(-1f, -1f,  1f), // 9
//     //             new Vector3(-1f,  1f,  1f), // 10
//     //             new Vector3(-1f,  1f, -1f), // 11

//     //             // Right face
//     //             new Vector3( 1f, -1f,  1f), // 12
//     //             new Vector3( 1f, -1f, -1f), // 13
//     //             new Vector3( 1f,  1f, -1f), // 14
//     //             new Vector3( 1f,  1f,  1f), // 15

//     //             // Top face
//     //             new Vector3(-1f,  1f,  1f), // 16
//     //             new Vector3( 1f,  1f,  1f), // 17
//     //             new Vector3( 1f,  1f, -1f), // 18
//     //             new Vector3(-1f,  1f, -1f), // 19

//     //             // Bottom face
//     //             new Vector3(-1f, -1f, -1f), // 20
//     //             new Vector3( 1f, -1f, -1f), // 21
//     //             new Vector3( 1f, -1f,  1f), // 22
//     //             new Vector3(-1f, -1f,  1f), // 23
//     //             };
// }
// using Client;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
namespace Client
{
    public class Movement : MonoBehaviour
    {
        public float xMoveSpeed = 4.317f;
        public float yMoveSpeed = 4.317f;
        public float xSpeedModifier = 1;
        public float ySpeedModifier = 1;
        public float jumpHeight = 1.25f;
        public float gravity = -32.656f;
        public float mouseSensitivity = 2f;
        bool first;
        ConnectToHost connectToHost;
        private float cameraPitch = 0f;


        private CharacterController controller;
        private Transform cam;
        private Vector3 velocity;
        private Vector2 moveInput = Vector2.zero;
        private bool jumpPressed = false;
        private bool isGrounded;
        bool jumping;
        float jumpTimeout = 0f;
        bool sprinting = false;
        bool lastJump;

        void Start()
        {
            connectToHost = ConnectToHost.getInstance();
            first = true;
            controller = GetComponent<CharacterController>();
            cam = transform.Find("Visual").transform.Find("Head");
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            controller.enabled = false;
            lastJump = false;
            transform.Find("Visual").transform.Find("Chest").transform.GetComponent<MeshRenderer>().enabled = false;
            transform.Find("Visual").transform.Find("Left Leg").transform.GetComponent<MeshRenderer>().enabled = false;
            transform.Find("Visual").transform.Find("Right Leg").transform.GetComponent<MeshRenderer>().enabled = false;
            transform.Find("Visual").transform.Find("Left Arm").transform.GetComponent<MeshRenderer>().enabled = false;
            transform.Find("Visual").transform.Find("Right Arm").transform.GetComponent<MeshRenderer>().enabled = false;
            transform.Find("Visual").transform.Find("Head").transform.GetComponent<MeshRenderer>().enabled = false;
        }

        void Update()
        {
            SetUpPlayerInitialPosition();
            if(!first){
            CheckJump();
            HandleMouseLook();
            HandleMovement();
            }
        }

        void HandleMovement()
        {
            isGrounded = controller.isGrounded;
            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            Vector3 move = transform.right * moveInput.x * xMoveSpeed * xSpeedModifier + transform.forward * moveInput.y * yMoveSpeed * ySpeedModifier;
            controller.Move(move * Time.deltaTime);

            if (jumping && isGrounded && jumpTimeout <= 0f)
            {
                lastJump = true;
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
            else if (jumpTimeout > 0f)
            {
                jumpTimeout -= Time.deltaTime;
            }

            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }

        // void HandleMouseLook()
        // {
        //     Vector2 mouse = Mouse.current.delta.ReadValue();
        //     float mouseX = mouse.x * mouseSensitivity * Time.deltaTime;
        //     float mouseY = mouse.y * mouseSensitivity * Time.deltaTime;

        //     transform.Rotate(Vector3.up * mouseX);

        //     float pitch = cam.localEulerAngles.z - mouseY;
        //     if (pitch > 180) pitch -= 360;
        //     pitch = Mathf.Clamp(pitch, -80f, 80f);
        //     cam.localEulerAngles = new Vector3(0f, 0f, -pitch);
        // }
        void CheckJump(){
            if(isGrounded && lastJump){
                jumpTimeout = 0.1f;
                lastJump = false;
            }
        }
        void OnSprint(InputValue inputValue)
        {
            if (inputValue.isPressed)
            {
                sprinting = true;
                xSpeedModifier = 1.3f;
            }
            else
            {
                sprinting = false;
                xSpeedModifier = 1f;
            }
        }
        void HandleMouseLook()
        {
            Vector2 mouse = Mouse.current.delta.ReadValue();
            float mouseX = mouse.x * mouseSensitivity * Time.deltaTime;
            float mouseY = mouse.y * mouseSensitivity * Time.deltaTime;

            // Rotate the player horizontally (yaw)
            transform.Rotate(Vector3.up * mouseX);

            // Update and clamp pitch
            cameraPitch -= mouseY;
            cameraPitch = Mathf.Clamp(cameraPitch, -80f, 80f);

            // Apply pitch to camera only
            cam.localRotation = Quaternion.Euler(0f, 0f, -cameraPitch);
        }

        public void OnMovement(InputValue inputValue)
        {
            moveInput = inputValue.Get<Vector2>();
        }

        public void OnJump(InputValue inputValue)
        {
            if (inputValue.isPressed){
                jumping = true;
            }
            else{
                jumping = false;
                jumpTimeout = 0f;
            }
        }
        void SetUpPlayerInitialPosition()
        {
            if (Variables.worldGenerated && first)
            {
                for (int i = 0; i < 100; i++)
                {
                    try
                    {
                        if (Variables.BlockData[new Tuple<int, int>(0, 0)][0][0][i] == null)
                        {
                            transform.position = new Vector3(0, i - 95, 0);
                            controller.enabled = true;
                            break;
                        }
                    }
                    catch (Exception)
                    {
                        transform.position = new Vector3(0, i - 95, 0);
                        controller.enabled = true;
                        break;
                    }
                }
                first = false;
            }
            if (Variables.worldGenerated)
            {
                connectToHost.SendMessageToHost("pp " + transform.position.x + " " + transform.position.y + " " + transform.position.z);
            }
        }
    }
}