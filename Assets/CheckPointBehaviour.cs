using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointBehaviour : MonoBehaviour {

	public Color colorUnchecked = Color.yellow;
	public Color colorChecked = Color.green;
	public bool active;
	public float spawnOffset = 0.5f;
	private Renderer rend;

	// Use this for initialization
	void Start () {
		active = false;
		rend = GetComponent<Renderer> ();
		rend.material.color = colorUnchecked;
	}
	void OnTriggerEnter(){
		if (!active) {
			active = true;
			Vector3 spawnPoint = new Vector3 (transform.position.x, transform.position.y + spawnOffset, transform.position.z);
			LevelManager.Instance.SetRespawnLocation(spawnPoint);
		}

	}
	
	// Update is called once per frame
	void Update () {
		if (active) {
			rend.material.color = Color.Lerp (rend.material.color, colorChecked, Time.deltaTime);
		} else {
			rend.material.color = Color.Lerp (rend.material.color, colorUnchecked, Time.deltaTime);
		}
	}
}

