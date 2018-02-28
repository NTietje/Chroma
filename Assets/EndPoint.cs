using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour {

	public AudioClip endPointSound;
	private AudioSource endPointSource;

	// Use this for initialization
	void Start () {
		
		//Looks for the AudioSource
		endPointSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter(Collider other){
		
		if (other.tag == "Player") {
			
		//Sound for the EndPoint	
		endPointSource.PlayOneShot(endPointSound, .5F); //1st parameter: audio clip and 2nd paramenter: volume
		
		//Calls up the Finish() methode in GameManager
		GameManager.instance.Finish ();
		
		}
	}
}
