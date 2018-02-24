using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public Color defaultPlayerColour;

    bool allowUpdate;
    float projectileDistance;
    float projectileSpeed;
    Vector3 startPosition;
	
	

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
            if (Vector3.Distance(startPosition, transform.position) > projectileDistance)
            {
                Destroy(gameObject);
               //Debug.Log("Projectile destroyed");
            }
            else
            {
                GetComponent<Rigidbody>().velocity = transform.forward * projectileSpeed;
            }
        }  
    }

    // Waits for trigger-collision with player
    private void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Player")
        {
            Destroy(gameObject); //Destroys projetile

            //Change player colour
			if (other.gameObject.GetComponent<PlayerControls> ()) {
				other.gameObject.GetComponent<PlayerControls> ().ResetColor ();
			}
            //other.gameObject.GetComponent<Renderer>().material.color = defaultPlayerColour;
            //Debug.Log("Spieler wurde von Projektil getroffen");
        }
    }
   

}
