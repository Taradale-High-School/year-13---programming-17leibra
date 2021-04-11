using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //speed vars for the movements
    private float moveSpeed = 10;
    private float rotSpeed = 2;
    public Camera mainCam;
    public GameObject cursor;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Input.GetAxis("Horizontal")*moveSpeed*Time.deltaTime, 0, Input.GetAxis("Vertical")*moveSpeed*Time.deltaTime); // moves player on wasd input (or arrows)
                                                                                                                                          // for rotation on mouse posittion
        /* Plane playerPlane = new Plane(Vector3.up, transform.position); //plane of the player
         Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition); // ray from camera to mouse pos
         float hitDistance = 0.0f; //the distacnce that the two rays need to be from each other, 0
         if (playerPlane.Raycast(cameraRay, out hitDistance))// to check if they are parallel
         {
             Vector3 targetPoint = cameraRay.GetPoint(hitDistance);//the point that is 0 from the camera ray
             Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position); // the rotation thats needed
             transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotSpeed * Time.deltaTime); // rotates smoothly to the point
         }*/
        Vector3 mousePos = Input.mousePosition;
        Vector3 pos = mainCam.ScreenToWorldPoint(mousePos);
        cursor.transform.position = pos;
    }
}
