using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public GameObject target; //object to follow

	private bool lerpTo;
	private Vector3 lerpTarget;

	private Vector3 offset; 

	void Awake (){
		offset = transform.position - target.transform.position;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if (lerpTo) {
			if (transform.position == lerpTarget) {
				lerpTo = false;
			} else {
				transform.position = Vector3.Lerp (transform.position, lerpTarget, 0.3f);
			}

		} else {
			transform.position = target.transform.position + offset;
		}
	}
	public void LerpToTarget(){
		lerpTarget = target.transform.position + offset;
		lerpTo = true;
	}
}
