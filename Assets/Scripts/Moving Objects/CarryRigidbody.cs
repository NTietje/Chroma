using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryRigidbody : MonoBehaviour {

    public bool useTriggerAsSensor = false;

    Vector3 lastPosition;
    Transform _transform;
    Rigidbody movingRigidbody = null;

	// Use this for initialization
	void Start () {
        _transform = transform;
        lastPosition = _transform.position;

        if(useTriggerAsSensor)
        {
            foreach (CarryRigidbodySensor sensor in GetComponentsInChildren<CarryRigidbodySensor>())
            {
                sensor.carrier = this;
            }
        }
	}

    void LateUpdate()
    {
        if (movingRigidbody != null)
        {
            Vector3 velocity = (_transform.position - lastPosition);
            movingRigidbody.transform.Translate(velocity);
            // movingRigidbody.transform.Translate(velocity, _transform); besser?
        }
        lastPosition = _transform.position;
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (useTriggerAsSensor) return;

        if (collision.collider.tag == "Player")
        {
            Rigidbody rigidbody = collision.collider.GetComponent<Rigidbody>();
            SetBody(rigidbody);
        }
    }
    
    void OnCollisionExit(Collision collision)
    {
        if (useTriggerAsSensor) return;

        if (collision.collider.tag == "Player")
        {
            SetBodyNull();
        }
       
    }

    public void SetBody(Rigidbody rigidbody)
    {
        Debug.Log("Collision on");
        movingRigidbody = rigidbody;
    }

    public void SetBodyNull()
    {
        Debug.Log("off");
        movingRigidbody = null;
    }

    
}
