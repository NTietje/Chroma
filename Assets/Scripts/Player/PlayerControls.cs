using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

	public int speed = 450;
	public int maxFallingHeight = 50;
	public float maxClimb = 0.2f;

	private GameObject pivot;

	private Vector3 spawnPoint;
	private Vector3 rotAxis;
	//private Vector3 rotPoint;
	//private Vector3 rotPointOffset;
	private Vector3 direction;

	private float cubeRadius;
	private bool moving;
	private bool falling;
	private int lowerBound;

	// Use this for initialization
	void Start () {
		cubeRadius = transform.lossyScale.x*0.5f;
		spawnPoint = transform.position;
		pivot = new GameObject("Pivot");
		pivot.transform.SetParent (transform);
		AlignPosition ();
	}
	void Update (){
		direction = Vector3.zero;
		if (!moving && !falling) {
			AlignPosition ();
			//Bottom Left
			if (Input.GetKeyDown (KeyCode.S)) {
				//x-0.5, y-0.5 BottomLeft
				direction = Vector3.left;
				//rotPointOffset = new Vector3 (-0.5f, -0.5f, 0f);
				rotAxis = Vector3.forward;
			}
			//Bottom Right
			if (Input.GetKeyDown (KeyCode.D)) {
				direction = Vector3.back;
				//rotPointOffset = new Vector3 (0f, -0.5f, -0.5f);
				rotAxis = Vector3.left;
			}
			//Top Left
			if (Input.GetKeyDown (KeyCode.W)) {
				direction = Vector3.forward;
				//rotPointOffset = new Vector3 (0f, -0.5f, 0.5f);
				rotAxis = Vector3.right;
			}
			//Top Right
			if (Input.GetKeyDown (KeyCode.E)) {
				direction = Vector3.right;
				//rotPointOffset = new Vector3 (0.5f, -0.5f, 0f);
				rotAxis = Vector3.back;

			}

			//pivot.transform.localPosition = rotPointOffset;
			if (direction != Vector3.zero) {
				pivot.transform.localPosition = new Vector3 (cubeRadius * direction.x, -cubeRadius, cubeRadius * direction.z);
				if (TargetIsNegotiable(direction)){
					moving = true;
				}
			}
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
	private bool TargetIsNegotiable(Vector3 direction){
		Vector3 origin = new Vector3 (transform.position.x, (transform.position.y - cubeRadius) + maxClimb, transform.position.z);
		if (Physics.Raycast (origin, direction*1.4f, 1f)) {
				return false;
		}
		return true;
	}
}
