using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovementSmooth : MonoBehaviour
{
    public Vector3 destination;
    public float smooth;
    public float resetTime;

    Vector3 startPosition;
    Vector3 newPosition;
    int currentState;
    
    // Use this for initialization
    void Start()
    {
        currentState = 0;
        startPosition = transform.position;
        ChangeTarget();
    }

    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, newPosition, smooth * Time.deltaTime);
    }

    void ChangeTarget()
    {
        if(currentState == 0)
        {
            newPosition = destination;
            currentState = 1;
        }
        else if(currentState == 1)
        {
            newPosition = startPosition;
            currentState = 0;
        }
        Invoke("ChangeTarget", resetTime);
    }
}