using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHitScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Into the on trigger and tag is: " + other.tag);
        if (other.CompareTag("enemy"))
        {
            Debug.Log("Sword hit enemy");
            Destroy(other);
        }
    }
}
