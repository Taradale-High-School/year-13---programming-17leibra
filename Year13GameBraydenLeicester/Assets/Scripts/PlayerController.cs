using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public float blockingDamage;
    private float baseSwordDamage=1f;
    public float swordDamage;
    public float health;
    float maxHealth = 20;

    //Position or rotaition initilaisers 
    private Vector3 velocity;
    private Quaternion swordTargetRotation;
    private Quaternion swordStartingRotation;
    private Vector3 sheildTargetPosition;
    private Vector3 sheildStartingPosition;
    Vector3 hbStartPos;

    
    //Bools for logic
    public bool isGrounded;
    public bool isBlocking;

    //Object refeferences 
    public Transform cameraTransform;
    public CharacterController controller;
    public Transform groundCheck;
    public LayerMask groundMask;
    public Transform compass;
    public GameObject rightHand;
    public GameObject leftHand;
    public GameObject canvas;
    public NameEnteringScript nameEnteringScript;
    public GameObject healthBar; 


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

    float mouseX;
    float mouseY;
    float xMove;
    float zMove;
    

    //bools for having bought the items
    public bool allowSS;
    public bool allowLS;
    public bool allowR;
    public bool allowK;

    public Text goldAmmount;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        hbStartPos = healthBar.transform.position;
        health = maxHealth;
        nameEnteringScript = canvas.GetComponent<NameEnteringScript>();
        //sets starting positions and rotations
        swordStartingRotation = rightHand.transform.localRotation;
        swordTargetRotation = rightHand.transform.localRotation;
        sheildStartingPosition = leftHand.transform.localPosition;
        sheildTargetPosition = leftHand.transform.localPosition;

        weaponDamage[0] = 1f;
        weaponDamage[1] = 1.5f;
        weaponDamage[2] = 3f;

        sheildDamageMod[0] = 0.8f;
        sheildDamageMod[1] = 1f;
        sheildDamageMod[2] = 0.6f;

        sheildSpeedMod[0] = 1f;
        sheildSpeedMod[1] = 2f;
        sheildSpeedMod[2] = 0.5f;

        goldAmmount.text = "70";

        statChange();
        objChange();
        StartCoroutine(healthRegen());
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            canvas.transform.Find("GameOverForm").gameObject.SetActive(true);
        }else if(health> maxHealth) 
        { 
            health = maxHealth;
            healthBar.transform.position = hbStartPos;
        }
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);//check for ground with a sphere 
        
        //Checks for blocking
        if (leftHand.transform.localPosition == sheildStartingPosition)
        {
            isBlocking = false;
        }else
        {
            isBlocking = true;
        }
        
        //resets downward velocity if grounded
        if (isGrounded && velocity.y<0)
        {
            velocity.y = -2f;
        }
        
       // if (nameEnteringScript.startGame)//to stop movement before name is chosen
        //{
            //gets mouse position
            mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            //Wasd inputs and scaled to speeds
            xMove = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
            zMove = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
            
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
      //  }


        transform.Rotate(Vector3.up * mouseX); //rotates player around y axis due to x pos of mouse
        compass.Rotate(Vector3.forward * -mouseX);// roates the comapass 

        //sets and clamps camera rotaion, and rotates the camera
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        
        
        
        Vector3 totalMove = transform.right * xMove + transform.forward * zMove; // makes a vector3 to move through

        controller.Move(totalMove); // moves through character controller component

       
        //Jump Controls
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravityAccleration);
        }
        velocity.y += gravityAccleration * Time.deltaTime;//makes gravity as using character controller not rigid body
        controller.Move(velocity * Time.deltaTime);// moves character with gravity 

       

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

        bool hasChanged = false;
        //changes index
        if (Input.GetKeyDown(KeyCode.Keypad1)) { sheildIndex = 0; hasChanged = true; }
        if (Input.GetKeyDown(KeyCode.Keypad2) &&allowSS) { sheildIndex = 1; hasChanged = true; }
        if (Input.GetKeyDown(KeyCode.Keypad3) && allowLS) { sheildIndex = 2; hasChanged = true; }
        if (Input.GetKeyDown(KeyCode.Keypad4)) { weaponIndex = 0; hasChanged = true; }
        if (Input.GetKeyDown(KeyCode.Keypad5) && allowR) { weaponIndex = 1; hasChanged = true; }
        if (Input.GetKeyDown(KeyCode.Keypad6) && allowK) { weaponIndex = 2; hasChanged = true; }
        if(hasChanged)
        {
            statChange();
            objChange();
            hasChanged = false;
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
    
    public void goldChange(int toChange)
    {
        int goldNum = int.Parse(goldAmmount.text);
        goldNum += toChange;
        goldAmmount.text = goldNum.ToString();
    }

    public void takeDamage(float toTake)
    {
        health -= toTake;
        healthBar.transform.position +=new Vector3(-16.5f * toTake,0,0);
    }

    IEnumerator healthRegen()
    {
        while (true)
        {
            if (health < maxHealth && health > 0)
            {
                yield return new WaitForSeconds(2);
                health+=1;
                healthBar.transform.position += new Vector3(16.5f, 0, 0);
            }
            else
            {
                yield return null;
            }
        }
    }
}

