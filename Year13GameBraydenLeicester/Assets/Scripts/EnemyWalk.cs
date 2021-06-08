using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalk : MonoBehaviour
{
    float speed = 10;
    float armDeclanation = 20;
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
        legLeft.transform.eulerAngles = new Vector3(legLeft.transform.eulerAngles.x + 20, legLeft.transform.eulerAngles.y, legLeft.transform.eulerAngles.z);
        LLForwardRot = legLeft.transform.localRotation;
        legLeft.transform.eulerAngles = new Vector3(legLeft.transform.eulerAngles.x - 40, legLeft.transform.eulerAngles.y, legLeft.transform.eulerAngles.z);
        LLBackRot = legLeft.transform.localRotation;

        legRight.transform.eulerAngles = new Vector3(legRight.transform.eulerAngles.x - 20, legRight.transform.eulerAngles.y, legRight.transform.eulerAngles.z);
        LRBackRot = legRight.transform.localRotation;
        legRight.transform.eulerAngles = new Vector3(legRight.transform.eulerAngles.x +40, legRight.transform.eulerAngles.y, legRight.transform.eulerAngles.z);
        LRForwardRot = legRight.transform.localRotation;

        armLeft.transform.eulerAngles = new Vector3(armLeft.transform.eulerAngles.x , armLeft.transform.eulerAngles.y+20, armLeft.transform.eulerAngles.z+armDeclanation);
        ALBackRot = armLeft.transform.localRotation;
        armLeft.transform.eulerAngles = new Vector3(armLeft.transform.eulerAngles.x, armLeft.transform.eulerAngles.y-40, armLeft.transform.eulerAngles.z);
        ALForwardRot = armLeft.transform.localRotation;

        armRight.transform.eulerAngles = new Vector3(armRight.transform.eulerAngles.x, armRight.transform.eulerAngles.y +20, armRight.transform.eulerAngles.z - armDeclanation);
        ARForwardRot = armRight.transform.localRotation;
        armRight.transform.eulerAngles = new Vector3(armRight.transform.eulerAngles.x, armRight.transform.eulerAngles.y -40, armRight.transform.eulerAngles.z );
        ARBackRot = armRight.transform.localRotation;



    }

    // Update is called once per frame
    void Update()
    {
        if (legLeft.transform.localRotation == LLBackRot) 
        {
            LLTargetRot = LLForwardRot;
        }
        if (legLeft.transform.localRotation == LLForwardRot)
        {
            LLTargetRot = LLBackRot;
        }

        if(legRight.transform.localRotation == LRBackRot)
        {
            LRTargetRot = LRForwardRot;
        }
        if (legRight.transform.localRotation == LRForwardRot)
        {
            LRTargetRot = LRBackRot;
        }

        if(armLeft.transform.localRotation == ALBackRot)
        {
            ALTargetRot = ALForwardRot;
        }
        if(armLeft.transform.localRotation == ALForwardRot)
        {
            ALTargetRot = ALBackRot;
        }

        if(armRight.transform.localRotation == ARBackRot)
        {
            ARTargetRot = ARForwardRot;
        }
        if(armRight.transform.localRotation == ARForwardRot)
        {
            ARTargetRot = ARBackRot;
        }



        legLeft.transform.localRotation = Quaternion.Lerp(legLeft.transform.localRotation, LLTargetRot, Time.deltaTime*speed); 
        legRight.transform.localRotation = Quaternion.Lerp(legRight.transform.localRotation, LRTargetRot, Time.deltaTime * speed);
        armLeft.transform.localRotation = Quaternion.Lerp(armLeft.transform.localRotation, ALTargetRot, Time.deltaTime * speed);
        armRight.transform.localRotation = Quaternion.Lerp(armRight.transform.localRotation, ARTargetRot, Time.deltaTime * speed);

    }
}
