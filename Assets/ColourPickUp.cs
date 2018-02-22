using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourPickUp : MonoBehaviour {

	public enum ColorLayer{Red, Green, Blue, Yellow};

	public ColorLayer colorLayer;

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
			//gameobject.GetComponent<Colourise> ().SetColourLayer (colour);
			//gameobject.GetComponent<Colourise> ().SetColourLayer (colorLayer);
			gameobject.layer = ApplyColorLayer (colorLayer);
            // Make ColourItem inactiv
            controller.SetColourItemInactiv();
		}
	}
	int ApplyColorLayer(ColorLayer colorLayer){
		switch (colorLayer) {
		case ColorLayer.Red:
			return 8;
		case ColorLayer.Green:
			return 9;
		case ColorLayer.Blue:
			return 10;
		case ColorLayer.Yellow:
			return 11;
		default: 
			return 0;
		}
	}

}
