using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourPickUp : MonoBehaviour {

    /*public enum ColorLayer { ColorLayer1, ColorLayer2, ColorLayer3, ColorLayer4 };
    public ColorLayer colorLayer;*/
    public float respawnTime;
	
	
	public AudioClip colorItemSound;
	private AudioSource colorItemSource;

    //private Color colour;

	// Use this for initialization
	void Start ()
    {
        //colour = GetComponent<Renderer>().material.color;
		
		//Looks for the AudioSource
		colorItemSource = GetComponent<AudioSource>();
		
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
		
		//Sound for when the player picks up a color	
<<<<<<< HEAD
		colorItemSource.PlayOneShot(colorItemSound, .5F); //1st parameter: audio clip and 2nd paramenter: volume
=======
		colorItemSource.PlayOneShot(colorItemSound);
>>>>>>> c36f7d83cfa0e2617907fe5afe258183928a6310
		
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
