using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager gm;

	void Awake() {
		if (gm == null) {
			DontDestroyOnLoad (gameObject);
			gm = this;
		} else if (gm != this) {
			Destroy (gameObject);
		}
			
	}
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}