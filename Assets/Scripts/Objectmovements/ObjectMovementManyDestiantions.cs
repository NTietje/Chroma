using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovementManyDestiantions : MonoBehaviour {

    public float speed;
    public GameObject platform;
    public Transform[] positions;
    public bool oneWay;
    public bool allowMoving;
   
    private Vector3 nextDestination;
    private int startIndex;
    private int destinationIndex;
    private int direction;
    private float movementTimer;
    private bool move = true;

    // Use this for initialization
    void Start () {

        // default start position
        //transform.position = Vector3.zero;
        //platform.transform.localPosition = Vector3.zero;

        // start values
        direction = 0;
        startIndex = 0;
        transform.position = positions[startIndex].position;
        destinationIndex = 1;
        nextDestination = positions[destinationIndex].position;
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        if (allowMoving && move)
        {
            // if destination reached
            if (transform.position == nextDestination)
            {
                ChangeDirection();
                ChangeDestination();
                movementTimer = 0;
            }

            // move object from startposition to destination
            movementTimer += (Time.deltaTime * speed) / Vector3.Distance(positions[startIndex].position, nextDestination);
            transform.position = Vector3.Lerp(positions[startIndex].position, nextDestination, movementTimer);
        }
    }

    // change direction
    void ChangeDirection()
    {
        if (positions.Length == destinationIndex + 1) // if endposition reached: set direction to move back
        {
            if (oneWay)
            {
                move = false;
            }
            else
            {
                direction = 1;
            } 
        }
        else if (destinationIndex == 0) // if startposition reached: set direction to move forward
        {
            direction = 0;
        }
    }

    // set next destination 
    void ChangeDestination()
    {
        startIndex = destinationIndex; // new startposition

        if (direction == 0) // moving forwars
        {
            destinationIndex++;
        } 
        else if (direction == 1) // moving back
        { 
            destinationIndex--;
        }
        if (!oneWay)
        {
            nextDestination = positions[destinationIndex].position;
        }
    }
    

}
