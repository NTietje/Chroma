using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

	public int speed;
	public Vector3 spawnPoint;

	private Vector3 rotAxis;
	private Vector3 rotPoint;
	private bool moving;
	private bool falling;

	// Use this for initialization
	void Start () {
		spawnPoint = transform.position;
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
			//move for 90°
			transform.RotateAround (rotPoint, rotAxis, speed * Time.deltaTime);
			if (transform.position.y <= Mathf.Round (transform.position.y)) {
				moving = false;
				//position correction
				//transform.eulerAngles = Vector3.zero;
				//transform.position = new Vector3 (Mathf.Round (transform.position.x), height, Mathf.Round (transform.position.z));
				AlignPosition();
			}
		// player is free falling
		} else if (falling) {
			/**if (transform.position.y < LevelManager.Instance.lowerYBounds) {
				//reset to active checkpoint
				transform.position = LevelManager.Instance.GetRespawnLocation ();
				//gameObject.GetComponent<Rigidbody> ().velocity = Vector3.zero;
				AlignPosition();
				falling = false;
			}*/
		//player interaction is not locked by a current falling or moving status
		} else {
			if (Input.GetKeyDown (KeyCode.Keypad1)) {
				//x-0.5, y-0.5 BottomLeft
				rotPoint = transform.position + new Vector3 (-0.5f, -0.5f, 0f);
				rotAxis = Vector3.forward;
				moving = true;
			}
			if (Input.GetKeyDown (KeyCode.Keypad3)) {
				//z-0.5, y-0.5 Bottom Right
				rotPoint = transform.position + new Vector3 (0f, -0.5f, -0.5f);
				rotAxis = Vector3.left;
				moving = true;
			}
			if (Input.GetKeyDown (KeyCode.Keypad7)) {
				//z+0.5, y-0.5 TopLeft
				rotPoint = transform.position + new Vector3 (0f, -0.5f, 0.5f);
				rotAxis = Vector3.right;
				moving = true;
			}
			if (Input.GetKeyDown (KeyCode.Keypad9)) {
				//x+0.5, y-0.5 TopRight
				rotPoint = transform.position + new Vector3 (0.5f, -0.5f, 0f);
				rotAxis = Vector3.back;
				moving = true;
			}
		}
	}
	//corrects the position of the game object to integers, sets all angles and velocity to zero 
	public void AlignPosition(){
		transform.rotation = Quaternion.identity;
		transform.position = new Vector3 (Mathf.Round (transform.position.x), Mathf.Round (transform.position.y), Mathf.Round (transform.position.z));
		gameObject.GetComponent<Rigidbody> ().velocity = Vector3.zero;
	}
}
