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

    //bools
    public bool playerClose = false;

    //floats
    public float enemyMoveSpeed = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = body.transform.position;
        targetPosition = body.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (body.transform.localPosition == targetPosition)
        {
            int chosenMaker = Random.Range(0, markers.Length);
            Debug.Log(chosenMaker);
            targetPosition = markers[chosenMaker].transform.localPosition;
        }
        body.transform.localPosition = Vector3.Lerp(body.transform.localPosition, targetPosition, Time.deltaTime * enemyMoveSpeed);
    }

    void DecidePosition()
    {
        
        
    }
}
