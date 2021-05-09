using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHitScript : MonoBehaviour
{
    public GameObject hand;
    private Quaternion startRot;
    public bool isSwinging;
    // Start is called before the first frame update
    void Start()
    {
        startRot = hand.transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (startRot == hand.transform.localRotation) { isSwinging = false; }else if(startRot != hand.transform.localRotation) { isSwinging = true; }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Into the on trigger and tag is: " + other.tag);
        if (other.CompareTag("enemy") && isSwinging)
        {
            Debug.Log("Sword hit enemy");
            Destroy(other.gameObject);
        }
    }
}
