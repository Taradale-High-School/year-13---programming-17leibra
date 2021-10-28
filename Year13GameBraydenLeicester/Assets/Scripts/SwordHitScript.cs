using System.Collections;
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
    float baseDmg = 2f;

    // Start is called before the first frame update
    void Start()
    {
        //sets variables
        //hand = transform.parent.gameObject;
        startRot = hand.transform.localRotation;
        playerScript = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Check if swinging
        if (startRot == hand.transform.localRotation) { isSwinging = false; }else if(startRot != hand.transform.localRotation) { isSwinging = true; }
    }

    //called when the sword collider hits something
    private void OnTriggerEnter(Collider other)
    {
        //compares the tags and decides what is being hit. 
        if (other.CompareTag("enemy") && isSwinging)
        { //^hitting enemy
            Debug.Log("Sword hit enemy"); 
            EnemyBehaviour script = other.GetComponentInParent<EnemyBehaviour>(); // gets that enemies behaviour script
            float toTake = 2f * playerScript.swordDamage; //sets the damage based on the sword in use
            script.damage(toTake); //makes the enemy health decrease (+ as no health regen on them)
        }
        if (other.CompareTag("player") && isSwinging && !playerScript.isBlocking)
        {//^ hit the player and player is not blocking
            Debug.Log("Enemy Sword hit player, not blocking");
            playerScript.takeDamage(baseDmg);//makes the player take damage
        }
        if (other.CompareTag("player") && isSwinging && playerScript.isBlocking)
        {//^ hit the player and player is blocking
            Debug.Log("Enemy Sword hit player, is blocking");
            float dmg = baseDmg * playerScript.blockingDamage; //changes the damge based on the shield
            playerScript.takeDamage(dmg);// makes the player take the changed amount of damage
        }
    }
}