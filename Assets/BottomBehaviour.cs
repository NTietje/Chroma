using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter(Collider other){
		if (other.gameObject.GetComponent<Movement> ()) {
			StartCoroutine (Reset (other.gameObject, other.gameObject.GetComponent<Movement> ().spawnPoint));
		} 
	}
	IEnumerator Reset(GameObject g, Vector3 location){
		Camera.main.GetComponent<CameraFollow> ().enabled = false;
		yield return new WaitForSeconds(1);
		g.transform.position = location;
		g.GetComponent<Movement> ().AlignPosition();
		Camera.main.GetComponent<CameraFollow> ().enabled = true;
	}
}
