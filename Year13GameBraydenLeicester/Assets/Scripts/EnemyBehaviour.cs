using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    //Object References
    public GameObject body;
    public GameObject hand;
    public GameObject playerObject;
    public GameObject[] markers;
    public Rigidbody rb;

    //Position var
    private Vector3 targetPosition;
    private Vector3 startingPosition;
    private Quaternion swordTargetRotation;
    private Quaternion swordStartingRotation;

    //bools
    public bool playerClose = false;

    //floats
    public float enemyMoveSpeed;
    public float playerRange;
    public float swingSpeed;
    public float health = 5f;
    // Start is called before the first frame update
    void Start()
    {
        rb = body.GetComponent<Rigidbody>();
        startingPosition = body.transform.position;
        targetPosition = body.transform.position;
        swordStartingRotation = hand.transform.localRotation;
        swordTargetRotation = hand.transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("health = " + health);
        if (health < 0)
        {
            Destroy(gameObject);// destorys the whole enemy game object if the body gets destroyed, by a sword
        }
        float playerDist = Vector3.Distance(body.transform.position, playerObject.transform.position); // gets distance to player
        if (playerDist < playerRange) { playerClose = true; } else { playerClose = false; }// decides if the player is in range

        //decides position and movement
        if (Vector3.Distance(body.transform.position, targetPosition) < 1f && !playerClose) //LERP never gets to position, so just needs to be close
        {
            int chosenMaker = Random.Range(0, markers.Length); //gets a random index 
            Debug.Log(chosenMaker);
            targetPosition = markers[chosenMaker].transform.position; // sets target as position of marker
            body.transform.LookAt(markers[chosenMaker].transform);//turns towards marker
           
        }
        else if (playerClose)
        {
            body.transform.LookAt(playerObject.transform);// turns towards player
            targetPosition = playerObject.transform.position;// sets target position to its own position to cancel LERP
        }
        body.transform.position = Vector3.Lerp(body.transform.position, targetPosition, Time.deltaTime * enemyMoveSpeed); // moves the body to the target pos smoothly
        

        //player close and at ready rot, the target rotation of the sword changes
        if (playerClose && hand.transform.localRotation == swordStartingRotation)
        {
            swordTargetRotation *= Quaternion.AngleAxis(90, Vector3.right);
            swordTargetRotation *= Quaternion.AngleAxis(-30, Vector3.forward);
        }

        //resest target rot to starting ones once swing is finished
        if (hand.transform.localRotation == swordTargetRotation)
        {
            swordTargetRotation = swordStartingRotation;
        }

        //rotates the sword smoothly (using lerp) to the target rotation (if the current rotation is the target, nothing changes)
        hand.transform.localRotation = Quaternion.Lerp(hand.transform.localRotation, swordTargetRotation, swingSpeed * Time.deltaTime);


    }
    public void damage(float toTake)
    {
        Debug.Log("have taken damage");
        health -= toTake;
    }

    
}
