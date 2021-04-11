using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class compassRotate : MonoBehaviour
{
    public GameObject compass;
    private Quaternion compassStartRot;

    // Start is called before the first frame update
    void Start()
    {
        compassStartRot = compass.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        { 
            float currentRot = compass.transform.rotation.eulerAngles.z;
            compass.transform.eulerAngles= new Vector3(0,0,(currentRot+20)); 
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            float currentRot = compass.transform.rotation.eulerAngles.z;
            compass.transform.eulerAngles = new Vector3(0, 0, (currentRot - 20)); 
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            compass.transform.rotation = compassStartRot; 
        }
    }
}
