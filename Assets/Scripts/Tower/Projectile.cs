using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public Color defaultPlayerColour;
    public AudioClip hitSound;

    private AudioSource hitSource;
    private Vector3 startPosition;

    private float projectileDistance;
    private float projectileSpeed;
    private bool allowUpdate;
    
    private void Start()
    {
        hitSource = GetComponent<AudioSource>();
    }

    // initialize method for projectile
    public void Initialize(Vector3 startPosition, float projectileDistance, float projectileSpeed)
    {
        this.projectileDistance = projectileDistance;
        this.projectileSpeed = projectileSpeed;
        this.startPosition = startPosition;
        allowUpdate = true;	
    }

    void FixedUpdate()
    {
        if(allowUpdate)
        {
            // If the distance is bigger than given projectilDistance, destoy the projectile
            if (Vector3.Distance(startPosition, transform.position) > projectileDistance)
            {
                Destroy(gameObject, 3);
            }
            else
            {
                // Move forward
                GetComponent<Rigidbody>().velocity = transform.forward * projectileSpeed;
            }
        }  
    }

    // Waits for trigger-collision with player
    private void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Player")
        {
            // If player is coloured play the reset-colour-sound
            if (other.gameObject.layer != 0)
            {
                hitSource.PlayOneShot(hitSound);
            }
            // hide gameobject and destory after some seconds
            gameObject.GetComponent<Renderer>().enabled = false;
            gameObject.GetComponentInChildren<Light>().enabled = false;
            Destroy(gameObject, 3);

            //Change player colour
			if (other.gameObject.GetComponent<PlayerControls> ()) {
				other.gameObject.GetComponent<PlayerControls> ().ResetColor ();
				
			}
        }
    }
   

}
