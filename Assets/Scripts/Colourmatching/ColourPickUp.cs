using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourPickUp : MonoBehaviour {

	public enum ColorLayer{Red, Green, Blue, Yellow};
	public ColorLayer colorLayer;
    public float respawnTime;

    private Color colour;

	// Use this for initialization
	void Start ()
    {
        colour = GetComponent<Renderer>().material.color;
	}

    // Change player colour on trigger-collision
    void OnTriggerEnter(Collider other)
    {
		GameObject gameobject = other.gameObject;
        // Switch player colour
        gameobject.GetComponent<Renderer>().material.color = colour;

        // Switch player layer
		gameobject.layer = ApplyColorLayer (colorLayer);

        // Make ColourItem not visible/ usable
        SetGameObjectActiv(false);

        // After respawnTime again visible/ usable
        Invoke("ShowGameobjectAgain", respawnTime);
    }

    void SetGameObjectActiv(bool boolean)
    {
        gameObject.GetComponent<Renderer>().enabled = boolean;
        gameObject.GetComponent<Collider>().enabled = boolean;
        gameObject.GetComponentInChildren<Light>().enabled = boolean;
    }

    void ShowGameobjectAgain()
    {
        SetGameObjectActiv(true);
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
