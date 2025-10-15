using System.Collections;
using System.Collections.Generic;
using UnityEngine;



    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Movement Settings")]
        public float walkSpeed = 5f;
        public float sprintSpeed = 10f;
        public float jumpHeight = 2f;
        public float gravity = -9.81f;

        [Header("Ground Check")]
        public Transform groundCheck;
        public float groundDistance = 0.4f;
        public LayerMask groundMask;

        [Header("Camera Settings")]
        public Transform cameraTransform;        // Assign Main Camera transform here
        public Vector3 cameraOffset = new Vector3(0, 3, -5);
        public float cameraRotationSpeed = 5f;
        public float cameraSmoothSpeed = 0.1f;
        public float cameraLookHeight = 1.5f;    // Height above player to look at

        private CharacterController controller;
        private Vector3 velocity;
        private bool isGrounded;

        // Camera rotation state
        private float currentYaw = 0f;
        private float currentPitch = 10f;

        void Start()
        {
            controller = GetComponent<CharacterController>();

            // Optionally lock and hide cursor
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        void Update()
        {
            GroundCheck();
            Move();
            HandleJump();
            ApplyGravity();

            UpdateCamera();
        }

        void GroundCheck()
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f; // small negative to keep grounded
            }
        }

        void Move()
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;

            float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;
            controller.Move(move * currentSpeed * Time.deltaTime);
        }

        void HandleJump()
        {
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }

        void ApplyGravity()
        {
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }

        void UpdateCamera()
        {
            if (cameraTransform == null) return;

            // Get mouse input
            currentYaw += Input.GetAxis("Mouse X") * cameraRotationSpeed;
            currentPitch -= Input.GetAxis("Mouse Y") * cameraRotationSpeed;
            currentPitch = Mathf.Clamp(currentPitch, -20f, 45f);

            // Calculate rotation from pitch and yaw
            Quaternion rotation = Quaternion.Euler(currentPitch, currentYaw, 0);

            // Calculate desired camera position based on player position + rotated offset
            Vector3 desiredPosition = transform.position + rotation * cameraOffset;

            // Smoothly move camera to desired position
            cameraTransform.position = Vector3.Slerp(cameraTransform.position, desiredPosition, cameraSmoothSpeed);

            // Make camera look at player + look height offset
            cameraTransform.LookAt(transform.position + Vector3.up * cameraLookHeight);
        }
    }






