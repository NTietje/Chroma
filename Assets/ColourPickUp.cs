using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourPickUp : MonoBehaviour {

	private Color colour;
    private ColourItemController controller;

	// Use this for initialization
	void Start ()
    {
        colour = GetComponent<Renderer>().material.color;
        controller = gameObject.GetComponentInParent<ColourItemController>();
	}

    // Change player colour on trigger-collision
    void OnTriggerEnter(Collider other)
    {
		GameObject gameobject = other.gameObject;
        gameobject.GetComponent<Renderer>().material.color = colour;
        // Switch layer
		if (gameobject.GetComponent<Colourise> ()) {
			gameobject.GetComponent<Colourise> ().SetColourLayer (colour);
            // Make ColourItem inactiv
            controller.SetColourItemInactiv();
		}
	}

}
