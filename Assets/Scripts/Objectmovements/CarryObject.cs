using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryObject : MonoBehaviour {

    public GameObject carrier;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("is on");
            other.transform.parent = carrier.transform;
        }
    }

    void OnTriggerExit(Collider other)
    {   
        if (other.tag == "Player")
        {
            Debug.Log("off");
            other.transform.parent = null;
        }    
    }
}
