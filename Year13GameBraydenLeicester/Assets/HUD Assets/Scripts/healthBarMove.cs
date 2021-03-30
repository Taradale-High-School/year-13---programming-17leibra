using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBarMove : MonoBehaviour
{
    public GameObject healthBar;
    Renderer rend;
    public Vector3 startVal;
    public float newVal;
    public float difference;
    public float numberOfTimes;
    // Start is called before the first frame update
    void Start()
    {
        //rend = healthBar.GetComponent<Renderer>();
        //startVal = rend.bounds.min.x;
        startVal = healthBar.transform.position;

    }


    // Update is called once per frame
    void Update() { 
     
        if (Input.GetKeyDown(KeyCode.A)) {
            //healthBar.transform.localScale = new Vector3((healthBar.transform.localScale.x-0.1f), healthBar.transform.localScale.y, healthBar.transform.localScale.z); 
            //newVal = -0.1f; 
            //numberOfTimes += 1;
            //healthBar.transform.position = new Vector3((healthBar.transform.position.x + 100 * newVal * numberOfTimes), healthBar.transform.position.y, healthBar.transform.position.z);
            healthBar.transform.position = new Vector3((healthBar.transform.position.x - 50), healthBar.transform.position.y, healthBar.transform.position.z);
        }
        if (Input.GetKeyDown(KeyCode.D)) {
            //healthBar.transform.localScale = new Vector3((healthBar.transform.localScale.x + 0.1f), healthBar.transform.localScale.y, healthBar.transform.localScale.z);
            //newVal = 0.1f; 
            //numberOfTimes += 1;
            //healthBar.transform.position = new Vector3((healthBar.transform.position.x + 100 * newVal * numberOfTimes), healthBar.transform.position.y, healthBar.transform.position.z);
            healthBar.transform.position = new Vector3((healthBar.transform.position.x + 50), healthBar.transform.position.y, healthBar.transform.position.z);
        }
        //newVal = rend.bounds.min.x;
        //difference = newVal-startVal;
        //healthBar.transform.Translate(new Vector3(-difference, 0f, 0f));
        


    }
}
