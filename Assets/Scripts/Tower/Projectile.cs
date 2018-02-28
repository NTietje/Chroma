using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script for projectiles, move the gameobject forwards depending on the projectilegenerator

public class Projectile : MonoBehaviour {

    public Color defaultPlayerColour;
    public AudioClip hitSound;

    AudioSource hitSource;
    Vector3 startPosition;

    float projectileDistance;
    float projectileSpeed;
    bool allowUpdate;
    
    void Start()
    {
        hitSource = GetComponent<AudioSource>();
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

    // initialize method for projectile
    public void Initialize(Vector3 startPosition, float projectileDistance, float projectileSpeed)
    {
        this.projectileDistance = projectileDistance;
        this.projectileSpeed = projectileSpeed;
        this.startPosition = startPosition;
        allowUpdate = true;
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
			other.gameObject.layer = 0;
        }
    }
   

}
