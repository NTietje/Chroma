using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourPickUp : MonoBehaviour {

	public enum ColorLayer{ColorLayer1, ColorLayer2, ColorLayer3, ColorLayer4};

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
		gameobject.layer = ApplyColorLayer (colorLayer);
		// Make ColourItem inactiv
		controller.SetColourItemInactiv();
	}
	int ApplyColorLayer(ColorLayer colorLayer){
		switch (colorLayer) {
		case ColorLayer.ColorLayer1:
			return 8;
		case ColorLayer.ColorLayer2:
			return 9;
		case ColorLayer.ColorLayer3:
			return 10;
		case ColorLayer.ColorLayer4:
			return 11;
		default: 
			return 0;
		}
	}

}
