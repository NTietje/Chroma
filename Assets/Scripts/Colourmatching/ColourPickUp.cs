using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourPickUp : MonoBehaviour {

    /*public enum ColorLayer { ColorLayer1, ColorLayer2, ColorLayer3, ColorLayer4 };
    public ColorLayer colorLayer;*/
    public float respawnTime;

    //private Color colour;

	// Use this for initialization
	void Start ()
    {
        //colour = GetComponent<Renderer>().material.color;
	}

    // Change player colour on trigger-collision
    void OnTriggerEnter(Collider other)
    {
        // Switch player colour
		other.gameObject.GetComponent<Renderer>().material.color = gameObject.GetComponent<Renderer>().material.color;

        // Switch player layer
		other.gameObject.layer = gameObject.layer;

		// Make ColourItem not visible/ usable
		SetGameObjectActiv(false);

		// After respawnTime again visible/ usable
		Invoke("ShowGameobjectAgain", respawnTime);
    }

    void SetGameObjectActiv(bool boolean)
    {
        gameObject.GetComponent<Renderer>().enabled = boolean;
        //gameObject.GetComponent<Collider>().enabled = boolean;
        gameObject.GetComponentInChildren<Light>().enabled = boolean;
		gameObject.GetComponentInChildren<BoxCollider> ().enabled = boolean;
    }

    void ShowGameobjectAgain()
    {
        SetGameObjectActiv(true);
    }

	public void PickUp(GameObject player){
		// Switch player colour
		player.GetComponent<Renderer>().material.color = gameObject.GetComponent<Renderer>().material.color;

		// Switch player layer
		player.layer = gameObject.layer;

		// Make ColourItem not visible/ usable
		SetGameObjectActiv(false);

		// After respawnTime again visible/ usable
		Invoke("ShowGameobjectAgain", respawnTime);
	}

    /*int ApplyColorLayer(ColorLayer colorLayer)
    {
        switch (colorLayer)
        {
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
    }*/

}
