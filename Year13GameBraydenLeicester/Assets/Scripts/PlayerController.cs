using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //speed vars for the movements
    private float moveSpeed = 5;
    private float xRotation = 0f;
    public float mouseSensitivity = 200f;
    public float gravityAccleration = -9.81f;
    public float jumpHeight = 4f;

    private Vector3 velocity;

    public float groundDistance = 0.3f;

    public bool isGronded;

    //Object refeferences 
    public Transform cameraTransform;
    public CharacterController controller;
    public Transform groundCheck;
    public LayerMask groundMask;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        isGronded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGronded && velocity.y<0)
        {
            velocity.y = -2f;
        }

        //gets mouse position
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX); //rotates player

        //sets and clamps camera rotaion, and rotates the camera
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        
        //Wasd inputs and scaled to speeds
        float xMove = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float zMove = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        
        Vector3 totalMove = transform.right * xMove + transform.forward * zMove; // makes a vector3 to move through

        controller.Move(totalMove); // moves through character controller component

       

        if (Input.GetButtonDown("Jump") && isGronded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravityAccleration);
        }
        velocity.y += gravityAccleration * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        

    }
}
