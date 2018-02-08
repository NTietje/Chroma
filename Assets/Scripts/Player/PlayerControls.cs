using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

	public int speed = 450;
	public int maxFallingHeight = 50;

	private GameObject pivot;

	private Vector3 spawnPoint;
	private Vector3 rotAxis;
	private Vector3 rotPoint;
	private Vector3 rotPointOffset;

	private bool moving;
	private bool falling;
	private int lowerBound;

	// Use this for initialization
	void Start () {
		spawnPoint = transform.position;
		pivot = new GameObject("Pivot");
		pivot.transform.SetParent (transform);
		AlignPosition ();
	}
	void Update (){
		if (!moving && !falling) {
			AlignPosition ();
			if (Input.GetKeyDown (KeyCode.Keypad1)) {
				//x-0.5, y-0.5 BottomLeft
				rotPointOffset = new Vector3 (-0.5f, -0.5f, 0f);
				rotAxis = Vector3.forward;
				moving = true;
			}
			if (Input.GetKeyDown (KeyCode.Keypad3)) {
				rotPointOffset = new Vector3 (0f, -0.5f, -0.5f);
				rotAxis = Vector3.left;
				moving = true;
			}
			if (Input.GetKeyDown (KeyCode.Keypad7)) {
				rotPointOffset = new Vector3 (0f, -0.5f, 0.5f);
				rotAxis = Vector3.right;	
				moving = true;
			}
			if (Input.GetKeyDown (KeyCode.Keypad9)) {
				rotPointOffset = new Vector3 (0.5f, -0.5f, 0f);
				rotAxis = Vector3.back;
				moving = true;
			}
			pivot.transform.localPosition = rotPointOffset;
		}
	}

	void FixedUpdate () {
		//ground detection
		RaycastHit hit;
		if (Physics.Raycast (transform.position, Vector3.down, out hit, 0.51f)) {
			falling = false;
			if (hit.transform.tag == "Platform") {
				transform.SetParent (hit.transform.parent);
			}
		} else {
			falling = true;
		}
		
		//player is within a movement
		if (moving) {
			//continue 90-degree-rotation for this frame
			transform.RotateAround (pivot.transform.position, rotAxis, speed * Time.deltaTime);
			if (transform.localPosition.y <= Mathf.Round (transform.localPosition.y)) {
				moving = false;
				AlignPosition ();
			}
			// player is free falling
		} else {
			
		}
		if (falling) {
			if (transform.position.y < lowerBound) {
				//reset to active checkpoint
				transform.position = spawnPoint;
				AlignPosition();
				falling = false;
			}
		//player interaction is not locked by a current falling or moving status
		} 
	}
	//corrects the position of the game object to integers, sets all angles and velocity to zero 
	public void AlignPosition(){
		transform.localRotation = Quaternion.identity;
		transform.localPosition = new Vector3 (Mathf.Round (transform.localPosition.x), transform.localPosition.y, Mathf.Round (transform.localPosition.z));
		gameObject.GetComponent<Rigidbody> ().velocity = Vector3.zero;
		lowerBound = (int)transform.position.y - maxFallingHeight;
	}
	public void SetSpawnPoint(Vector3 spawnPoint){
		this.spawnPoint = spawnPoint;
	}
}
