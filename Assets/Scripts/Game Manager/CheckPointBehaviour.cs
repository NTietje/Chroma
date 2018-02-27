using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointBehaviour : MonoBehaviour {

	public Color colorUnchecked = Color.yellow;
	public Color colorChecked = Color.green;
	public bool active;
	public float spawnOffset = 0.5f;

	private Renderer rend;
	private Component[] renderers;


	// Use this for initialization
	void Start () {
		active = false;
		rend = GetComponent<Renderer> ();
		rend.material.color = colorUnchecked;
		renderers = gameObject.GetComponentsInChildren(typeof(Renderer));
	}
	void OnTriggerEnter(Collider other){
		if (!active) {
			active = true;
			Vector3 spawnPoint = new Vector3 (transform.position.x, transform.position.y + spawnOffset, transform.position.z);
			//other.gameObject.GetComponent<PlayerControls> ().SetSpawnPoint(spawnPoint);
			GameManager.instance.SetSpawn(spawnPoint);
		}

	}
	
	// Update is called once per frame
	void Update () {
		Color lerpColor = rend.material.color;
		if (active) {
			lerpColor = Color.Lerp (rend.material.color, colorChecked, Time.deltaTime);
			
		} else {
			lerpColor = Color.Lerp (rend.material.color, colorUnchecked, Time.deltaTime);
		}
		rend.material.color = lerpColor;
		foreach (Renderer childRenderer in renderers){
			childRenderer.material.color = lerpColor;
		}
	}
}

