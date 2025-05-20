using System;
using UnityEngine;
using UnityEngine.InputSystem;
namespace Client
{
    public class Movement : MonoBehaviour
    {
        
        private float xMoveSpeed = 4.317f;
        private float yMoveSpeed = 4.317f;
        private float xSpeedModifier = 1;
        private float forwardSpeedModifier = 1;
        private float ySpeedModifier = 1;
        private float jumpHeight = 5f;
        private float gravity = -30f;
        private float mouseSensitivity = 10f;
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
        bool hitting;
        bool interacting;
        GameObject head;
        [SerializeField] private LayerMask chunkLayer;

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
            hitting = false;
            interacting = false;
            transform.Find("Visual").transform.Find("Chest").transform.GetComponent<MeshRenderer>().enabled = false;
            transform.Find("Visual").transform.Find("Left Leg").transform.GetComponent<MeshRenderer>().enabled = false;
            transform.Find("Visual").transform.Find("Right Leg").transform.GetComponent<MeshRenderer>().enabled = false;
            transform.Find("Visual").transform.Find("Left Arm").transform.GetComponent<MeshRenderer>().enabled = false;
            transform.Find("Visual").transform.Find("Right Arm").transform.GetComponent<MeshRenderer>().enabled = false;
            transform.Find("Visual").transform.Find("Head").transform.GetComponent<MeshRenderer>().enabled = false;
            head = transform.Find("Visual").transform.Find("Head").gameObject;
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
        
        public bool isHitting(){
            return hitting;
        }
        public bool isInteracting(){
            return interacting;
        }
        public Vector3 getCameraForward(){
            return cam.forward;
        }

        public Vector3 getPlayerPosition(){
            return transform.position;
        }
        void HandleMovement()
        {
            isGrounded = controller.isGrounded;
            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }
            Vector3 move;
            if(moveInput.y > 0){
                move = transform.right * moveInput.x * xMoveSpeed * xSpeedModifier + transform.forward * moveInput.y * yMoveSpeed * ySpeedModifier * forwardSpeedModifier;
            }else{
                move = transform.right * moveInput.x * xMoveSpeed * xSpeedModifier + transform.forward * moveInput.y * yMoveSpeed * ySpeedModifier;
            }
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
        // public void OnSprint(InputValue inputValue)
        // {
        //     if (inputValue.isPressed)
        //     {
        //         sprinting = true;
        //         xSpeedModifier = 5f;
        //     }
        //     else
        //     {
        //         sprinting = false;
        //         xSpeedModifier = 1f;
        //     }
        // }
        public void OnSprint(InputValue inputValue)
        {
            if (inputValue.isPressed){
                sprinting = true;
                forwardSpeedModifier = 5f;
            }
            else{
                sprinting = false;
                forwardSpeedModifier = 1f;
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
            cameraPitch = Mathf.Clamp(cameraPitch, -90f, 90f);

            // Apply pitch to camera only
            cam.localRotation = Quaternion.Euler(0f, 0f, -cameraPitch);
        }

        public void OnMovement(InputValue inputValue)
        {
            moveInput = inputValue.Get<Vector2>();
        }
        public void OnHit(InputValue inputValue)
        {

            if (inputValue.isPressed)
            {

                hitting = true;
            } else {
                hitting = false;
            }
        }
        public void OnInteract(InputValue inputValue)
        {
            if (inputValue.isPressed)
            {
                interacting = true;
                RaycastHit hit;
                if(Physics.Raycast(cam.position, cam.right, out hit, 10f, chunkLayer))
                {

                    Vector3 hitPoint = hit.point;
                    
                    if(Mathf.Approximately((float) (Math.Abs(hitPoint.y) - Math.Abs(Math.Truncate(hitPoint.y))), 0.5f)){
                        float rawX = hitPoint.x + 0.5f;
                        float rawZ = hitPoint.z + 0.5f;
                        float rawY = hitPoint.y;
                        int chunkX = (int)Math.Floor((hitPoint.x + 0.5f) / 16f);
                        int chunkZ = (int)Math.Floor((hitPoint.z + 0.5f) / 16f);
                        int blockX = (int)Math.Round(hitPoint.x % 16);
                        int blockZ = (int)Math.Round(hitPoint.z % 16);
                        blockZ = blockZ < 0 ? blockZ + 16 : blockZ;
                        blockX = blockX < 0 ? blockX + 16 : blockX;
                        int blockY = (int)Math.Floor(hitPoint.y) + 101;

                        print(chunkX + " " + chunkZ + " " + blockX + " " + blockZ + " " + blockY + "% " + rawX + " " + rawZ + " " + rawY);
                        Vector3 comparable = new Vector3((float)Math.Floor(transform.position.x), (float)Math.Floor(transform.position.y), (float)Math.Floor(transform.position.z));
                        if(comparable != new Vector3(chunkX * 16 + blockX, blockY-101, chunkZ * 16 + blockZ)){
                            Variables.BlockData[new Tuple<int, int>(chunkX, chunkZ)][blockX][blockZ][blockY] = new Block(BlockType.Grass);
                            Variables.ChunkData[new Tuple<int, int>(chunkX, chunkZ)].GetComponent<Chunk>().updateMesh();
                        }
                    } else {
                        print(hitPoint.y);
                    }
                }
            } else {
                interacting = false;
            }
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

        void OnCrouch(InputValue inputValue)
        {
            if (inputValue.isPressed)
            {
                ySpeedModifier = 0.25f;
                xSpeedModifier = 0.25f;
            }
            else
            {
                ySpeedModifier = 1f;
                xSpeedModifier = 1f;
            }
            
        }
        void SetUpPlayerInitialPosition()
        {
            if (Variables.worldGenerated && first)
            {
                for (int i = 0; i < 200; i++)
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
        }
    }
}