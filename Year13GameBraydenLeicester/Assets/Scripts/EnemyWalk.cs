using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalk : MonoBehaviour
{
    //floats
    float speed = 10;
    float armDeclanation = 20;
    float legSwing = 20;
    float armSwing = 20;

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

    // Start is called before the first frame update
    void Start()
    {
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
        armRight.transform.eulerAngles = new Vector3(armRight.transform.eulerAngles.x, armRight.transform.eulerAngles.y +armSwing, armRight.transform.eulerAngles.z - armDeclanation);
        ARForwardRot = armRight.transform.localRotation;
        armRight.transform.eulerAngles = new Vector3(armRight.transform.eulerAngles.x, armRight.transform.eulerAngles.y -2*armSwing, armRight.transform.eulerAngles.z );
        ARBackRot = armRight.transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        //left leg rotation changes
        if (legLeft.transform.localRotation == LLBackRot)//checks for the same rotation as the back rot 
        {
            LLTargetRot = LLForwardRot;//sets target to the forward rot
        }
        if (legLeft.transform.localRotation == LLForwardRot)//checks for the same rotaition as the forward rot
        {
            LLTargetRot = LLBackRot;//sets it to the forward rot
        }
       
        //right leg changes
        if (legRight.transform.localRotation == LRForwardRot)
        {
            LRTargetRot = LRBackRot;
        }
        if (legRight.transform.localRotation == LRBackRot)
        {
            LRTargetRot = LRForwardRot;
        }
       
        //left arm changes
        if(armLeft.transform.localRotation == ALBackRot)
        {
            ALTargetRot = ALForwardRot;
        }
        if(armLeft.transform.localRotation == ALForwardRot)
        {
            ALTargetRot = ALBackRot;
        }

        //right arm changes
        if(armRight.transform.localRotation == ARBackRot)
        {
            ARTargetRot = ARForwardRot;
        }
        if(armRight.transform.localRotation == ARForwardRot)
        {
            ARTargetRot = ARBackRot;
        }

        legLeft.transform.localRotation = Quaternion.Lerp(legLeft.transform.localRotation, LLTargetRot, Time.deltaTime*speed); //smoothly rotates the left leg beteween where it is and the target
        legRight.transform.localRotation = Quaternion.Lerp(legRight.transform.localRotation, LRTargetRot, Time.deltaTime * speed);//right leg
        armLeft.transform.localRotation = Quaternion.Lerp(armLeft.transform.localRotation, ALTargetRot, Time.deltaTime * speed);//left arm
        armRight.transform.localRotation = Quaternion.Lerp(armRight.transform.localRotation, ARTargetRot, Time.deltaTime * speed);//right arm

    }
}
