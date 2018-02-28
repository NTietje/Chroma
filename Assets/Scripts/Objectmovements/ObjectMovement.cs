
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script to move platform from A to B in loop

public class ObjectMovement : MonoBehaviour {

    public Vector3 destination;
    public float speed;
    public bool allowMoving;

    Vector3 startposition;

	// Use this for initialization
	void Start ()
    {
        startposition = transform.position;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (allowMoving)
        {
            // Move object between 2 points forward and back
            float time = Mathf.PingPong(Time.time * speed, 1);
            transform.position = Vector3.Lerp(startposition, destination, time);
        }
    }
    
}
