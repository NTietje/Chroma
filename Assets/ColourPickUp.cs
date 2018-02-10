using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourPickUp : MonoBehaviour {

	public Color colour = Color.green;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnCollisionEnter(Collision col){
		GameObject g = col.gameObject;
		if (g.GetComponent<Colourise> ()) {
			g.GetComponent<Colourise> ().SetColourLayer (colour);
			Destroy (gameObject);
		}
	}
}
