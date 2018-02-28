using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Script for a trigger object to start moving of a platform

public class TriggerStartMovement : MonoBehaviour {

    public GameObject platform;
    public float stoptimer;
	public AudioClip triggerSound;

	AudioSource triggerSource;
	
	// Use this for initialization
    void Start ()
    {
		triggerSource = GetComponent<AudioSource>();
	}
	
    // On Trigger Enter move platform
    void OnTriggerEnter(Collider other)
    {
        // if collision with player, move the platform
        if (other.tag == "Player")
        {
            platform.GetComponent<ObjectMovementManyDestiantions>().allowMoving = true;

            //sound for the trigger
			 triggerSource.PlayOneShot(triggerSound, 1F); //1st parameter: audio clip and 2nd paramenter: volume
            // if stoptimer is set, stop platform after time
            if (stoptimer > 0)
            {
                Invoke("stopPlatform", stoptimer);
            }
        }  
    }

    // Stops platform with boolean to false
    void stopPlatform()
    {
        platform.GetComponent<ObjectMovementManyDestiantions>().allowMoving = false;
    }

}
