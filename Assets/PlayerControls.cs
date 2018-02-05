using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

	public int speed = 450;
	public int resetFallingHeight = 50;

	private GameObject movementPivot;

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
		movementPivot = transform.Find ("MovementPivot").gameObject;
		AlignPosition ();
	}
	
	// Update is called once per frame
	void Update () {
		//ground detection
		RaycastHit[] hit;
		hit = Physics.RaycastAll (transform.position, new Vector3 (0, -1, 0), 1f);
		if (hit.Length > 0) {
			falling = false;
		} else {
			falling = true;
		}
		//player is within a movement
		if (moving) {
			//continue 90-degree-rotation for this frame
			transform.RotateAround (movementPivot.transform.position, rotAxis, speed * Time.deltaTime);
			if (transform.position.y <= Mathf.Round (transform.position.y)) {
				moving = false;
				//position correction
				//transform.eulerAngles = Vector3.zero;
				//transform.position = new Vector3 (Mathf.Round (transform.position.x), height, Mathf.Round (transform.position.z));
				AlignPosition();
			}
		// player is free falling
		} else if (falling) {
			if (transform.position.y < lowerBound) {
				//reset to active checkpoint
				transform.position = spawnPoint;
				AlignPosition();
				falling = false;
			}
		//player interaction is not locked by a current falling or moving status
		} else {
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
			movementPivot.transform.localPosition = rotPointOffset;
		}
	}
	//corrects the position of the game object to integers, sets all angles and velocity to zero 
	public void AlignPosition(){
		transform.rotation = Quaternion.identity;
		transform.localPosition = new Vector3 (Mathf.Round (transform.localPosition.x), Mathf.Round (transform.localPosition.y), Mathf.Round (transform.localPosition.z));
		gameObject.GetComponent<Rigidbody> ().velocity = Vector3.zero;
		lowerBound = (int)transform.position.y - resetFallingHeight;
	}
	public void SetSpawnPoint(Vector3 spawnPoint){
		this.spawnPoint = spawnPoint;
	}
}
