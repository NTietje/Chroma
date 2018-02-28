using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * This class manages the colour-pick-up behaviour. Items where this is attached can be picked up and will also define the picking object's layer and color 
 * 
 * !!! ColourPickUp objects themselves should not have an active collider anymore, only the child of type DefaultLayerAnchor
 * !!! This is a workaround to make the PickUps be pickable from anylayer, although there is no physical collision between the player and the ColourPickUp-object itself
 */
public class ColourPickUp : MonoBehaviour {
	
    public float respawnTime; //respawn of the attached gameObject after pick up
	public AudioClip colorItemSound;
	private AudioSource colorItemSource;
	void Start(){
		//Looks for the Audio Source
		colorItemSource = GetComponent<AudioSource>();
	}

    // Change player colour on trigger-collision
    void OnTriggerEnter(Collider other)
    {
		//!!! This collision behaviours only works, if the collecting object is not on the same colourlayer as the item itself
		//!!! if it is desired to make an object be pickable from any layer, use a child with the DefaultLayerAnchor.cs attached and disable this objects collider

		// Switch player colour
		//other.gameObject.GetComponent<Renderer>().material.color = gameObject.GetComponent<Renderer>().material.color;

        // Switch player layer
		other.gameObject.layer = gameObject.layer;

		// Make ColourItem not visible/ usable
		SetGameObjectActiv(false);

		// After respawnTime again visible/ usable
		Invoke("ShowGameobjectAgain", respawnTime);
		
    }
	/**
	 * reenable all visibility and collision
	 */
    void SetGameObjectActiv(bool boolean)
    {
		gameObject.GetComponent<Renderer>().enabled = boolean;
        gameObject.GetComponentInChildren<Light>().enabled = boolean;
		gameObject.GetComponentInChildren<BoxCollider> ().enabled = boolean;
    }
	/**
	 * reactivates the gameObject, can be invoked
	 */
    void ShowGameobjectAgain()
    {
        SetGameObjectActiv(true);
    }
	/**
	 * This method lets the attached gameObject be picked up by another one.
	 * When picked up, it will change the targets layer and color.
	 * The attached gameObject will also disappear for the defined respawnTime
	 */
	public void PickUp(GameObject player){
		// Switch player colour
		//player.GetComponent<Renderer>().material.color = gameObject.GetComponent<Renderer>().material.color;

		// Switch player layer
		player.layer = gameObject.layer;

		// Make ColourItem not visible/ usable
		SetGameObjectActiv(false);

		// After respawnTime again visible/ usable
		Invoke("ShowGameobjectAgain", respawnTime);
		
		//Sound for when the player picks up a color	
		colorItemSource.PlayOneShot(colorItemSound);
		
	}

}
