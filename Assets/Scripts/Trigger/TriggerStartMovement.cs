using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerStartMovement : MonoBehaviour {

    public GameObject platform;
    public float stoptimer;
	public AudioClip triggerSound;
	private AudioSource triggerSource;
	
	// Use this for initialization
    void Start ()
    {
		triggerSource = GetComponent<AudioSource>();
	}
	
    // On Trigger Enter move platform
    private void OnTriggerEnter(Collider other)
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

    void stopPlatform()
    {
        platform.GetComponent<ObjectMovementManyDestiantions>().allowMoving = false;
    }

}
