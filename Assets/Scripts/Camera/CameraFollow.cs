using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
* This script enables a camera, if attached, to follow a defined target, e.g. the player.
*
* target : the object to follow
* lerpTo : when this boolean is true, the camera will not instantly follow the target, but interpolate between it's position and the target's position
* lerpTarget : used to save the last position of the target, before a lerp-follow
* offset : the distance between the camera and the target
* 
* by Cevau
*/

public class CameraFollow : MonoBehaviour {

	public GameObject target;

	private bool lerpTo;
	private Vector3 lerpTarget;

	private Vector3 offset; 

	void Awake (){
		//offset will be the initial distance between the target and the camera
		offset = transform.position - target.transform.position;
	}
	void LateUpdate () {
		if (lerpTo) {
			//check if target has been reached already, if so, disables lerpTo mode
			if (transform.position == lerpTarget) {
				lerpTo = false;
			} else {
				transform.position = Vector3.Lerp (transform.position, lerpTarget, 0.3f);
			}

		} else {
			//follow the target (instantly)
			transform.position = target.transform.position + offset;
		}
	}
	/**
	 * enables the lerpTo mode (see lerpTo)  
	 */
	public void LerpToTarget(){
		lerpTarget = target.transform.position + offset;
		lerpTo = true;
	}
}
