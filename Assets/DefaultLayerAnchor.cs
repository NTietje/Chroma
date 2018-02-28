using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * This script is used on the child object DefaultLayerAnchor on PickUp items (see prefab)
 * It is used to link the colour pickup also to the default layer, so it can trigger collisions on that layer
 * Only useful when the parent object has the ColourPickUp script
 */
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
