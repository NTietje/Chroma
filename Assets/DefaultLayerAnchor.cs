using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultLayerAnchor : MonoBehaviour {

	void Awake(){
		//lock this gameObject to the default layer!
		gameObject.layer = 0;
	}
	void OnTriggerEnter (Collider other){
		//Handles the pickup on the default collision layer, no matter what layer the parent object is.
		GetComponentInParent<ColourPickUp> ().PickUp (other.gameObject);
	}
}
