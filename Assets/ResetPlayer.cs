using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlayer : MonoBehaviour {

	public float wait;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter(Collider other){
		if (other.tag == "Player") {
			StartCoroutine (Reset (other.gameObject));
		}
	}
	private IEnumerator Reset(GameObject player){
		//immediately stop the camera follow
		Camera.main.GetComponent<CameraFollow> ().enabled = false;

		//reset player and reenable camera follow
		yield return new WaitForSeconds (wait);
		player.transform.position = GameManager.instance.GetSpawn ();
			if (player.GetComponent<PlayerControls>()){
				player.GetComponent<PlayerControls> ().Reset ();
			}
		Camera.main.GetComponent<CameraFollow> ().enabled = true;

	}
		
}
