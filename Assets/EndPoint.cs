using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * This script finishes the active level when a collider is triggered by a player
 */
public class EndPoint : MonoBehaviour {

	public bool lastLevel = false;
	public AudioClip endPointSound;
	private AudioSource endPointSource;

	void Start ()
    {		
		//Looks for the AudioSource
		endPointSource = GetComponent<AudioSource>();
	}
	// Use this for initialization
	
	void OnTriggerEnter(Collider other){
		if (other.tag == "Player")
        {	
		//Sound for the EndPoint	
		endPointSource.PlayOneShot(endPointSound, .5F); //1st parameter: audio clip and 2nd paramenter: volume
		
		//Calls up the Finish() methode in GameManager
			GameManager.instance.Finish (lastLevel);
		}
	}
}
