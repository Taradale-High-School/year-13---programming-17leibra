using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Floats for the movements and more
    private float baseMoveSpeed = 5;
    private float moveSpeed;
    private float xRotation = 0f;
    private float mouseSensitivity = 200f;
    public float gravityAccleration = -9.81f;
    public float jumpHeight = 4f;
    public float swingSpeed = 10f;
    public float sheildMoveSpeed = 10f;
    public float groundDistance = 0.3f;
    private float baseBlockingDamage = 1;
    private float blockingDamage;
    private float baseSwordDamage;
    private float swordDamage;

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
    //Sword/sheild objects
    public GameObject[] weapons;
    public GameObject[] sheilds;

    int weaponIndex = 0;
    int oldWeaponIndex = 0;
    float[] weaponDamage = new float[3];
    int sheildIndex = 0;
    int oldSheildIndex = 0;
    float[] sheildDamageMod = new float[3];
    float[] sheildSpeedMod = new float[3];

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // locks and hides the mouse cursor
        
        //sets starting positions and rotations
        swordStartingRotation = rightHand.transform.localRotation;
        swordTargetRotation = rightHand.transform.localRotation;
        sheildStartingPosition = leftHand.transform.localPosition;
        sheildTargetPosition = leftHand.transform.localPosition;

        weaponDamage[0] = 1;
        weaponDamage[1] = 0.8f;
        weaponDamage[2] = 1.2f;

        sheildDamageMod[0] = 1;
        sheildDamageMod[1] = 0.8f;
        sheildDamageMod[2] = 1.2f;

        sheildSpeedMod[0] = 1;
        sheildSpeedMod[1] = 2f;
        sheildSpeedMod[2] = 0.5f;

        statChange();
        objChange();
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

        //changes index
        if (Input.GetKeyDown(KeyCode.Keypad1)) { sheildIndex = 0; }
        if (Input.GetKeyDown(KeyCode.Keypad2)) { sheildIndex = 1; }
        if (Input.GetKeyDown(KeyCode.Keypad3)) { sheildIndex = 2; }
        if (Input.GetKeyDown(KeyCode.Keypad4)) { weaponIndex = 0; }
        if (Input.GetKeyDown(KeyCode.Keypad5)) { weaponIndex = 1; }
        if (Input.GetKeyDown(KeyCode.Keypad6)) { weaponIndex = 2; }
        if( Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.Keypad4) || Input.GetKeyDown(KeyCode.Keypad5) || Input.GetKeyDown(KeyCode.Keypad6))
        {
            statChange();
            objChange();
        }

    }

    void statChange()
    {
        moveSpeed = baseMoveSpeed * sheildSpeedMod[sheildIndex];
        blockingDamage = baseBlockingDamage * sheildDamageMod[sheildIndex];
        swordDamage = baseSwordDamage * weaponDamage[weaponIndex];
    }
    void objChange()
    {
        weapons[oldWeaponIndex].SetActive(false);
        weapons[weaponIndex].SetActive(true);
        oldWeaponIndex = weaponIndex;
        sheilds[oldSheildIndex].SetActive(false);
        sheilds[sheildIndex].SetActive(true);
        oldSheildIndex = sheildIndex;
    }
    
}
