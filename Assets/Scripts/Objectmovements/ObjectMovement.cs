
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour {

    public Vector3 destination;
    public float speed;

    Vector3 startposition;

	// Use this for initialization
	void Start () {
        startposition = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
         //transform.Translate(-Vector3.right * speed * Time.deltaTime);
         float time = Mathf.PingPong(Time.time * speed, 1);
         transform.position = Vector3.Lerp(startposition, destination, time);
    }
    
}
