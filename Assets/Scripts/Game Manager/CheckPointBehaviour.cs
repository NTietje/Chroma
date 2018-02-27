using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointBehaviour : MonoBehaviour {

	public Color colorUnchecked = Color.yellow;
	public Color colorChecked = Color.green;
	private bool active;
	private bool switching;
	//public float spawnOffset = 0.5f;

	private Renderer rend;
	private Component[] renderers;


	// Use this for initialization
	void Start () {
		Off ();
		rend = GetComponent<Renderer> ();
		rend.material.color = colorUnchecked;
		renderers = gameObject.GetComponentsInChildren(typeof(Renderer));
	}
	void OnTriggerEnter(Collider other){
		//Vector3 spawnPoint = new Vector3 (transform.position.x, transform.position.y + spawnOffset, transform.position.z);
		//other.gameObject.GetComponent<PlayerControls> ().SetSpawnPoint(spawnPoint);
		GameManager.instance.SetCheckPoint(gameObject, other.gameObject.layer);
	}

	// Update is called once per frame
	void Update () {
		if (switching) {
			Color lerpColor = rend.material.color;
			if (active) {
				lerpColor = Color.Lerp (rend.material.color, colorChecked, 0.07f);
				if (rend.material.color == colorChecked) {
					switching = false;
				}
			
			} else {
				lerpColor = Color.Lerp (rend.material.color, colorUnchecked, Time.deltaTime);
				if (rend.material.color == colorUnchecked) {
					switching = false;
				}
			}
			rend.material.color = lerpColor;
			foreach (Renderer childRenderer in renderers) {
				childRenderer.material.color = lerpColor;
			}
		}
	}
	public void Off(){
		active = false;
		switching = true;
	}
	public void On(){
		active = true;
		switching = true;
	}
}

