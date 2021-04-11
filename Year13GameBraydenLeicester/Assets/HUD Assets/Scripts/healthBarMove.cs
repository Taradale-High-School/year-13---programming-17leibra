using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBarMove : MonoBehaviour
{
    public GameObject healthBar;
    public Vector3 startVal;
    public float newVal;
    public float difference;
    public float numberOfTimes;
    // Start is called before the first frame update
    void Start()
    {
        startVal = healthBar.transform.position;

    }


    // Update is called once per frame
    void Update() { 
     
        if (Input.GetKeyDown(KeyCode.J)) {
            healthBar.transform.position = new Vector3((healthBar.transform.position.x - 50), healthBar.transform.position.y, healthBar.transform.position.z);
        }
        if (Input.GetKeyDown(KeyCode.L)) {
            healthBar.transform.position = new Vector3((healthBar.transform.position.x + 50), healthBar.transform.position.y, healthBar.transform.position.z);
        }


    }
}
