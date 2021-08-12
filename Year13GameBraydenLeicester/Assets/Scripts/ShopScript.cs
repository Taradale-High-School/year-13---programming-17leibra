using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour
{
    int costSS=10;
    int costLS = 20;
    int costR = 10;
    int costK = 20;

    PlayerController playerScript;

    public Text shopText;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("ShopSmall Shield") && !playerScript.allowSS &&costSS <=int.Parse( playerScript.goldAmmount.text) && Input.GetKeyDown(KeyCode.E))
        {
            playerScript.goldChange(-costSS);
            playerScript.allowSS = true;
        }
        if (other.CompareTag("ShopLarge Shield") && !playerScript.allowLS && costLS <= int.Parse(playerScript.goldAmmount.text) && Input.GetKeyDown(KeyCode.E))
        {
            playerScript.goldChange(-costLS);
            playerScript.allowLS = true;
        }
        if (other.CompareTag("ShopRapier") && !playerScript.allowR && costR <= int.Parse(playerScript.goldAmmount.text) && Input.GetKeyDown(KeyCode.E))
        {
            playerScript.goldChange(-costR);
            playerScript.allowR = true;
        }
        if (other.CompareTag("ShopKatana") && !playerScript.allowK && costK <= int.Parse(playerScript.goldAmmount.text) && Input.GetKeyDown(KeyCode.E))
        {
            playerScript.goldChange(-costK);
            playerScript.allowK = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ShopSmall Shield") || other.CompareTag("ShopLarge Shield") || other.CompareTag("ShopRapier") || other.CompareTag("ShopKatana"))
        {
            string tag = other.tag;
            string toShow = "Press E to buy " + tag.Substring(4);
            shopText.text = toShow;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        shopText.text = "";
    }
}
