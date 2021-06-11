using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalk : MonoBehaviour
{
    //floats
    float speed = 5;
    float armDeclanation = 20;
    float legSwing = 10;
    float armSwing = 20;
    float angleClose = 0.0001f;

    //object References
    public GameObject legLeft;
    public GameObject legRight;
    public GameObject armLeft;
    public GameObject armRight;

    //target rots
    private Quaternion LLTargetRot;
    private Quaternion LRTargetRot;
    private Quaternion ALTargetRot;
    private Quaternion ARTargetRot;
    private Quaternion swordTarget;

    //forward rots
    private Quaternion LLForwardRot;
    private Quaternion LRForwardRot;
    private Quaternion ALForwardRot;
    private Quaternion ARForwardRot;
    
    //back rots
    private Quaternion LLBackRot;
    private Quaternion LRBackRot;
    private Quaternion ALBackRot;
    private Quaternion ARBackRot;

    private EnemyBehaviour enemyBehaviour;

    // Start is called before the first frame update
    void Start()
    {
        enemyBehaviour = GetComponentInParent<EnemyBehaviour>();

        //left leg starting rots
        legLeft.transform.eulerAngles = new Vector3(legLeft.transform.eulerAngles.x + legSwing, legLeft.transform.eulerAngles.y, legLeft.transform.eulerAngles.z); //sets rotation to foward
        LLForwardRot = legLeft.transform.localRotation; //sets the forward rot for later
        legLeft.transform.eulerAngles = new Vector3(legLeft.transform.eulerAngles.x - 2*legSwing, legLeft.transform.eulerAngles.y, legLeft.transform.eulerAngles.z); //sets the rotation to backwards (twice the forwards amount)
        LLBackRot = legLeft.transform.localRotation; // sets the back rot for later

        //right leg starting rots
        legRight.transform.eulerAngles = new Vector3(legRight.transform.eulerAngles.x - legSwing, legRight.transform.eulerAngles.y, legRight.transform.eulerAngles.z);
        LRBackRot = legRight.transform.localRotation;
        legRight.transform.eulerAngles = new Vector3(legRight.transform.eulerAngles.x +2*legSwing, legRight.transform.eulerAngles.y, legRight.transform.eulerAngles.z);
        LRForwardRot = legRight.transform.localRotation;

        //left arm starting rots
        armLeft.transform.eulerAngles = new Vector3(armLeft.transform.eulerAngles.x , armLeft.transform.eulerAngles.y+armSwing, armLeft.transform.eulerAngles.z+armDeclanation);
        ALBackRot = armLeft.transform.localRotation;
        armLeft.transform.eulerAngles = new Vector3(armLeft.transform.eulerAngles.x, armLeft.transform.eulerAngles.y-2*armSwing, armLeft.transform.eulerAngles.z);
        ALForwardRot = armLeft.transform.localRotation;

        //right arm starting rots
        armRight.transform.eulerAngles = new Vector3(armRight.transform.eulerAngles.x-10, armRight.transform.eulerAngles.y -70, armRight.transform.eulerAngles.z - 20);
        swordTarget = armRight.transform.localRotation;
        armRight.transform.eulerAngles = new Vector3(armRight.transform.eulerAngles.x + 10, armRight.transform.eulerAngles.y + 70, armRight.transform.eulerAngles.z + 20);
        armRight.transform.eulerAngles = new Vector3(armRight.transform.eulerAngles.x, armRight.transform.eulerAngles.y +armSwing, armRight.transform.eulerAngles.z - armDeclanation);
        ARForwardRot = armRight.transform.localRotation;
        armRight.transform.eulerAngles = new Vector3(armRight.transform.eulerAngles.x, armRight.transform.eulerAngles.y -2*armSwing, armRight.transform.eulerAngles.z );
        ARBackRot = armRight.transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        //left leg rotation changes
        if (Quaternion.Angle(legLeft.transform.localRotation, LLBackRot) < angleClose)//checks for the same rotation as the back rot 
        {
            LLTargetRot = LLForwardRot;//sets target to the forward rot
        }
        if (Quaternion.Angle(legLeft.transform.localRotation, LLForwardRot) < angleClose)//checks for the same rotaition as the forward rot
        {
            LLTargetRot = LLBackRot;//sets it to the forward rot
        }
       
        //right leg changes
        if (Quaternion.Angle(legRight.transform.localRotation, LRForwardRot) < angleClose)
        {
            LRTargetRot = LRBackRot;
        }
        if (Quaternion.Angle(legRight.transform.localRotation, LRBackRot) < angleClose)
        {
            LRTargetRot = LRForwardRot;
        }
       
        //left arm changes
        if(Quaternion.Angle(armLeft.transform.localRotation, ALBackRot) < angleClose)
        {
            ALTargetRot = ALForwardRot;
        }
        if(Quaternion.Angle(armLeft.transform.localRotation, ALForwardRot) < angleClose)
        {
            ALTargetRot = ALBackRot;
        }

        //right arm changes
        if (!enemyBehaviour.playerClose)
        {
            if (Quaternion.Angle(armRight.transform.localRotation, ARBackRot) < angleClose)
            {
                ARTargetRot = ARForwardRot;
            }
            if (Quaternion.Angle(armRight.transform.localRotation, ARForwardRot) < angleClose)
            {
                ARTargetRot = ARBackRot;
            }
            if (Quaternion.Angle(armRight.transform.localRotation, swordTarget) < angleClose)
            {
                ARTargetRot = ARBackRot;
            }
        }
        else 
        {
            ARTargetRot = swordTarget;
        }

        legLeft.transform.localRotation = Quaternion.Lerp(legLeft.transform.localRotation, LLTargetRot, Time.deltaTime*speed); //smoothly rotates the left leg beteween where it is and the target
        legRight.transform.localRotation = Quaternion.Lerp(legRight.transform.localRotation, LRTargetRot, Time.deltaTime * speed);//right leg
        armLeft.transform.localRotation = Quaternion.Lerp(armLeft.transform.localRotation, ALTargetRot, Time.deltaTime * speed);//left arm
        armRight.transform.localRotation = Quaternion.Lerp(armRight.transform.localRotation, ARTargetRot, Time.deltaTime * speed);//right arm

    }
}
