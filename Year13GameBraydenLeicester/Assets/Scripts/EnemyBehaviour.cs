using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    //Object References
    public GameObject hand;
    public GameObject playerObject;

    //Position var
    private Vector3 targetPosition;
    private Vector3 startingPosition;

    //bools
    public bool playerClose = false;

    //floats
    public float enemyMoveSpeed = 10;
    
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        targetPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (targetPosition == transform.position)
        {
            Debug.Log("positions same");
            if (Vector3.Distance(playerObject.transform.position, transform.position)< 10)
            {
                playerClose = true;
                DecidePosition(0,0);
            }
            else
            {
                playerClose = false;
                DecidePosition(-10, 10);
            }
        }
        transform.position = Vector3.Lerp(transform.position, targetPosition, enemyMoveSpeed * Time.deltaTime);
        //transform.position = Vector3.RotateTowards(transform.position, targetPosition,2*Mathf.PI, 10f);

    }

    void DecidePosition( int minDist, int maxDist)
    {
        if (playerClose)
        {
            targetPosition = new Vector3(playerObject.transform.position.x, transform.position.y, playerObject.transform.position.z);
        }
        else
        {
            targetPosition = new Vector3(transform.position.x + Random.Range(minDist, maxDist), transform.position.y, transform.position.z + Random.Range(minDist, maxDist));
        }
        
    }
}
