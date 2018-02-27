using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public GameObject target; //object to follow

	public Vector3 offset; 

	void Awake (){
		offset = transform.position - target.transform.position;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
		transform.position = target.transform.position + offset;
		//TODO Lerp!
	}
	public void ForceUpdate() {
		transform.position = target.transform.position + offset;
	}
}
