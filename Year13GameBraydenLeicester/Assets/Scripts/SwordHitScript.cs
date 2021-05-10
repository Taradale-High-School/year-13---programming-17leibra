﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHitScript : MonoBehaviour
{
    //References
    public GameObject hand;
    public GameObject player;
    public PlayerController playerScript;

    //Other Variables
    private Quaternion startRot;
    public bool isSwinging;
   

    // Start is called before the first frame update
    void Start()
    {
        //sets variables
        startRot = hand.transform.localRotation;
        playerScript = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Check if swinging
        if (startRot == hand.transform.localRotation) { isSwinging = false; }else if(startRot != hand.transform.localRotation) { isSwinging = true; }
    }

    private void OnTriggerEnter(Collider other)
    {
        //compares the tags and decides what to print out. 
        if (other.CompareTag("enemy") && isSwinging)
        {
            Debug.Log("Sword hit enemy");
            Destroy(other.gameObject); //destroys the enemies
        }
        if (other.CompareTag("player") && isSwinging && !playerScript.isBlocking)
        {
            Debug.Log("Enemy Sword hit player, not blocking");
        }
        if (other.CompareTag("player") && isSwinging && playerScript.isBlocking)
        {
            Debug.Log("Enemy Sword hit player, is blocking");
        }
    }
}