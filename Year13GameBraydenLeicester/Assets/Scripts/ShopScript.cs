using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour
{
    //cost ints
    int costSS=10;
    int costLS = 20;
    int costR = 10;
    int costK = 20;

    //refereences
    private PlayerController playerScript;
    public Text shopText;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<PlayerController>();//sets the player script var
    }

    //is called while the collider is touching an object
    private void OnTriggerStay(Collider other)
    {
        //each if statment does the same thing for a differnent object
        //Small shield
        if (other.CompareTag("ShopSmall Shield") && !playerScript.allowSS &&costSS <=int.Parse( playerScript.goldAmmount.text) && Input.GetKeyDown(KeyCode.E))
        { //^comapres the tag of the shop, if it has already be bought, if the player has enough money and the buy key is getting pressed  
            playerScript.goldChange(-costSS); //subtracts the ammount of money
            playerScript.allowSS = true; //allows the it to be used
        }
        // Large Shield
        if (other.CompareTag("ShopLarge Shield") && !playerScript.allowLS && costLS <= int.Parse(playerScript.goldAmmount.text) && Input.GetKeyDown(KeyCode.E))
        {
            playerScript.goldChange(-costLS);
            playerScript.allowLS = true;
        }
        //Rapier
        if (other.CompareTag("ShopRapier") && !playerScript.allowR && costR <= int.Parse(playerScript.goldAmmount.text) && Input.GetKeyDown(KeyCode.E))
        {
            playerScript.goldChange(-costR);
            playerScript.allowR = true;
        }
        //Katana
        if (other.CompareTag("ShopKatana") && !playerScript.allowK && costK <= int.Parse(playerScript.goldAmmount.text) && Input.GetKeyDown(KeyCode.E))
        {
            playerScript.goldChange(-costK);
            playerScript.allowK = true;
        }
    }
    
    //called when collider first touches the object
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ShopSmall Shield") || other.CompareTag("ShopLarge Shield") || other.CompareTag("ShopRapier") || other.CompareTag("ShopKatana"))
        {//^if what is being colided with is a shop
            string tag = other.tag; //gets the tag
            string toShow = "Press E to buy " + tag.Substring(4);//sets a var to a set text and then the name of the object, which is the tag without "Shop" on it
            shopText.gameObject.SetActive(true); //shows the textbox
            shopText.text = toShow; //adds the text to the textbox
        }
    }
   
    //called when collision stops
    private void OnTriggerExit(Collider other)
    {
        shopText.gameObject.SetActive(false); // hides the textbox
        shopText.text = ""; //makes the message blank
    }
}
