using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	static LevelManager levelManager;

	public float lowerYBounds = -20f; //Reset to active checkpoint when a gameObject goes below this

	private Vector3 respawnLocation;

	public static LevelManager Instance {
		get {
			if (levelManager == null) {
				GameObject g = new GameObject ();
				levelManager = g.AddComponent<LevelManager> ();
			}
			return levelManager;
		}
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public Vector3 GetRespawnLocation(){
		return respawnLocation;
	}
	public void SetRespawnLocation(Vector3 spawnPoint){
		respawnLocation = spawnPoint;
	}
}
