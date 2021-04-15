using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Floats for the movements and more
    private float moveSpeed = 5;
    private float xRotation = 0f;
    private float mouseSensitivity = 200f;
    public float gravityAccleration = -9.81f;
    public float jumpHeight = 4f;
    public float swingSpeed = 10f;
    public float sheildMoveSpeed = 10f;
    public float groundDistance = 0.3f;


    //Position or rotaition initilaisers 
    private Vector3 velocity;
    private Quaternion swordTargetRotation;
    private Quaternion swordStartingRotation;
    private Vector3 sheildTargetPosition;
    private Vector3 sheildStartingPosition;

    
    //Bools for logic
    public bool isGronded;
    public bool isBlocking;

    //Object refeferences 
    public Transform cameraTransform;
    public CharacterController controller;
    public Transform groundCheck;
    public LayerMask groundMask;
    public Transform compass;
    public GameObject rightHand;
    public GameObject leftHand;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // locks and hides the mouse cursor
        
        //sets starting positions and rotations
        swordStartingRotation = rightHand.transform.localRotation;
        swordTargetRotation = rightHand.transform.localRotation;
        sheildStartingPosition = leftHand.transform.localPosition;
        sheildTargetPosition = leftHand.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        isGronded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);//check for ground with a sphere 
        
        //Checks for blocking
        if (leftHand.transform.localPosition == sheildStartingPosition)
        {
            isBlocking = false;
        }else
        {
            isBlocking = true;
        }
        
        //resets downward velocity if grounded
        if (isGronded && velocity.y<0)
        {
            velocity.y = -2f;
        }

        //gets mouse position
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX); //rotates player around y axis due to x pos of mouse
        compass.Rotate(Vector3.forward * -mouseX);// roates the comapass 

        //sets and clamps camera rotaion, and rotates the camera
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        
        //Wasd inputs and scaled to speeds
        float xMove = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float zMove = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        
        Vector3 totalMove = transform.right * xMove + transform.forward * zMove; // makes a vector3 to move through

        controller.Move(totalMove); // moves through character controller component

       
        //Jump Controls
        if (Input.GetButtonDown("Jump") && isGronded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravityAccleration);
        }
        velocity.y += gravityAccleration * Time.deltaTime;//makes gravity as using character controller not rigid body
        controller.Move(velocity * Time.deltaTime);// moves character with gravity 

        //Right Click and at ready rot, the target rotation of the sword changes
        if (Input.GetButtonDown("Fire2") && rightHand.transform.localRotation == swordStartingRotation)
        {
            swordTargetRotation *= Quaternion.AngleAxis(90, Vector3.right);
            swordTargetRotation *= Quaternion.AngleAxis(30, Vector3.forward);
        }

        //left click and a ready pos, the target position of the sheild changes
        if (Input.GetButtonDown("Fire1") && leftHand.transform.localPosition == sheildStartingPosition)
        {
            sheildTargetPosition = new Vector3(leftHand.transform.localPosition.x + 0.2f, leftHand.transform.localPosition.y + 0.2f, leftHand.transform.localPosition.z); 
        }


        //resest target rot/pos to starting ones once swing/movement is finished
        if (rightHand.transform.localRotation == swordTargetRotation)
        {
            swordTargetRotation = swordStartingRotation;
        }
        if (leftHand.transform.localPosition == sheildTargetPosition)
        {
            sheildTargetPosition = sheildStartingPosition;
        }

        //rotates the sword smoothly (using lerp) to the target rotation (if the current rotation is the target, nothing changes)
        rightHand.transform.localRotation = Quaternion.Lerp(rightHand.transform.localRotation, swordTargetRotation, swingSpeed * Time.deltaTime);

        //moves the sheild smoothly (using lerp) to the target position (if the current position is the target, nothing changes)
        leftHand.transform.localPosition = Vector3.Lerp(leftHand.transform.localPosition, sheildTargetPosition, sheildMoveSpeed * Time.deltaTime);


    }
}
