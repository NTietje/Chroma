using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

	public int speed;

	private Vector3 rotAxis;
	private Vector3 rotPoint;
	private bool moving;
	private float height;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (moving) {
			//move for 90°
			transform.RotateAround(rotPoint, rotAxis, speed*Time.deltaTime);
			if (transform.position.y <= height) {
				moving = false;
				//TODO position correction
				transform.eulerAngles = Vector3.zero;
				transform.position = new Vector3 (Mathf.Round (transform.position.x), height, Mathf.Round (transform.position.z));

			}
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
			height = Mathf.Round(transform.position.y);
		}
	}
}
