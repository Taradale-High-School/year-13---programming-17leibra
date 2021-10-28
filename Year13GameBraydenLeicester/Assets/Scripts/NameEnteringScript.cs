using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class NameEnteringScript : MonoBehaviour
{
    private string playerName; // for the player name
    //all these variables are public as they have the objects droped in in the editor
    public InputField nameEnterField;   // for the text field for the name 
    public Button submitNameButton; // for the button
    public GameObject nameForm; // for the form the field and the button are in
    public GameObject gameOverForm; // for the game over gameobject
    public bool startGame = false; // startGame is false and is accessed by other scripts to tell them to start
    public Text nameWrongText; //text box for prompting user for a new name
    public GameObject imageCursor; //object of the cursor
    // Start is called before the first frame update
    void Start()
    {
        submitNameButton.interactable = true; //makes the button interactable
        submitNameButton.onClick.AddListener(NameSet); // adds a listener to the button
    }   

    // Update is called once per frame
    void Update()
    {
           //if the name is correct
            if (startGame) 
            {
                nameForm.SetActive(false);/*gets rid of the name enter form*/
                Cursor.lockState = CursorLockMode.Locked; // locks and hides the mouse cursor
                imageCursor.SetActive(true);// shows the crosshairs
            }
    }

    //called when the button is clicked
    void NameSet()
    {
        Debug.Log("button clicked");
        playerName = nameEnterField.text; // sets player name to the value of the field
        print("name is " + playerName);
        startGame = NameCheck(playerName); ; // sets start game to true if the name is valid so the other scripts can start running
    }

    //called by nameset to check the name for requirements
    bool NameCheck(string nameToCheck)
    {
        //Initialising variables for the method
        bool nameGood = false; // to return
        int lenght = nameToCheck.Length; //setting the lenght of the name
        bool lenghtGood = false; //to set by checking against it
        bool allNum = false; //to set by a check

        if (lenght >= 2 && lenght <= 10) //check to make sure the lenght of the name is allowable
        {
            lenghtGood = true;
        }
        else
        {
            lenghtGood = false;
        }

        allNum = nameToCheck.All(char.IsDigit);
        /*One line check for the enitre string of name being digit
         it returns a True only if all the characters are numbers*/

        //set of if/elseif to check for an allowable name or ones that have one or both things wrong with them. and then tells the user what is wrong with the name if it isn't valid
        if (lenghtGood && !allNum)
        {
            nameGood = true;
        }
        else if (lenghtGood && allNum)
        {
            nameWrongText.text = "The Name can't be just numbers";
        }
        else if (!lenghtGood && !allNum)
        {
            nameWrongText.text = "The Name be between 2 and 10 characters(inclusive)";
        }
        else if (!lenghtGood && allNum)
        {
            nameWrongText.text = "The Name can't be just numbers \r\n and must be between 2 and 10 characters(inclusive)";
        }

        return nameGood; //returns this bool to where the method was called
    }
}

