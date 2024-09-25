using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Static Instance
    public static PlayerMovement Instance;

    //Private Variables
    CharacterController controller;
    GameObject CameraHolder;

    Vector2 MoveVector;
    Vector2 LookVector;

    float RotationX;
    Vector3 Velocity;
    bool isGrounded;

    [Header("References")]
    public Camera Camera;
    public Transform GroundCheck;

    [Header("Movement Settings")]
    public float CameraSensitivity = 1.5f;
    public float PlayerSpeed = 4f;
    public float Gravity = -25f;
    public float JumpHeight = 1f;

    [Space]
    public bool PauseInput = false;

    [Header("Ground Check Settings")]
    public float GroundDistance = 0.2f;
    public LayerMask GroundMask;

    private void Start()
    {
        //Assign Private References
        Instance = this;
        controller = GetComponent<CharacterController>();
        CameraHolder = Camera.transform.parent.gameObject;

        //Lock Player Mouse
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        //Run Functions
        GetInput();
        MoveCamera();
        MovePlayer();
    }

    void GetInput()
    {
        if (PauseInput)
        {
            LookVector = Vector2.zero; 
            MoveVector = Vector2.zero; 
            return;
        }

        //Get Input From Keyboard and Mouse
        LookVector = new Vector2(Input.GetAxisRaw("Mouse X") * CameraSensitivity, Input.GetAxisRaw("Mouse Y") * CameraSensitivity);
        MoveVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    void MovePlayer()
    {
        //Check if player is grounded
        isGrounded = Physics.CheckSphere(GroundCheck.position, GroundDistance, GroundMask);

        if (isGrounded && Velocity.y < 0)
            Velocity.y = -2f;

        //Calculate Move direction and Move the Player
        Vector3 Direction = transform.right * MoveVector.x + transform.forward * MoveVector.y;
        controller.Move(Direction * PlayerSpeed * Time.deltaTime);

        //Player Jump
        if (Input.GetButtonDown("Jump") && isGrounded && !PauseInput)
            Velocity.y = Mathf.Sqrt(JumpHeight * -2f * Gravity);

        //Gravity
        Velocity.y += Gravity * Time.deltaTime;
        controller.Move(Velocity * Time.deltaTime);
    }

    void MoveCamera()
    {
        //Clamp Neck Rotation
        RotationX -= LookVector.y;
        RotationX = Mathf.Clamp(RotationX, -90f, 90f);

        //Move Cameras
        CameraHolder.transform.localRotation = Quaternion.Euler(RotationX, 0, 0);
        transform.Rotate(Vector3.up, LookVector.x);
    }
}