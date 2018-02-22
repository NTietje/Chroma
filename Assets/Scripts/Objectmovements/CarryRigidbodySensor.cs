using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryRigidbodySensor : MonoBehaviour {

    public CarryRigidbody carrier;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Rigidbody rigidbody = other.GetComponent<Rigidbody>();
            carrier.SetBody(rigidbody);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            carrier.SetBodyNull();
        }
    }
}
