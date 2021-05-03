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

    //Position var
    private Vector3 targetPosition;
    private Vector3 startingPosition;
    private Quaternion swordTargetRotation;
    private Quaternion swordStartingRotation;

    //bools
    public bool playerClose = false;

    //floats
    public float enemyMoveSpeed = 1;
    public float playerRange = 10;
    public float swingSpeed = 10;
    
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = body.transform.position;
        targetPosition = body.transform.localPosition;
        swordStartingRotation = hand.transform.localRotation;
        swordTargetRotation = hand.transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        float playerDist = Vector3.Distance(body.transform.position, playerObject.transform.position); // gets distance to player
        if (playerDist < playerRange) { playerClose = true; } else { playerClose = false; }// decides if the player is in range
        
        //decides position and movement
        if (Vector3.Distance(body.transform.localPosition, targetPosition) < 0.1f && !playerClose)
        {
            int chosenMaker = Random.Range(0, markers.Length);
            Debug.Log(chosenMaker);
            targetPosition = markers[chosenMaker].transform.localPosition;
            body.transform.LookAt(markers[chosenMaker].transform);
        } else if (playerClose)
        {
            body.transform.LookAt(playerObject.transform);
            targetPosition = body.transform.localPosition;
            body.transform.localPosition = Vector3.MoveTowards(body.transform.localPosition, playerObject.transform.position, 1 * enemyMoveSpeed * Time.deltaTime);  
        }
        body.transform.localPosition = Vector3.Lerp(body.transform.localPosition, targetPosition, Time.deltaTime * enemyMoveSpeed);

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

}
