using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourPickUp : MonoBehaviour {

	private Color colour;

	// Use this for initialization
	void Start () {
        colour = GetComponent<Renderer>().material.color;
	}
	
	void OnCollisionEnter(Collision col){
		GameObject gameobject = col.gameObject;
        gameobject.GetComponent<Renderer>().material.color = colour;
		if (gameobject.GetComponent<Colourise> ()) {
			gameobject.GetComponent<Colourise> ().SetColourLayer (colour);
			Destroy (gameObject);
		}
	}
}
