using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
   //object references
    public PlayerController playerScript;
    public GameObject enA1; //(ENemy Array #)
    public GameObject enA2;
    
    public int numEn; // int for number of enemies
    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<PlayerController>();//sets the player script
    }

    // Update is called once per frame
    void Update()
    {
        numEn = enA1.transform.childCount + enA2.transform.childCount; //always seting the number of enemies
        if (numEn==0) { playerScript.gameOver("You killed all the enemies, You Win"); } // calls gameover in the player script when all enemies are dead
    }
}
