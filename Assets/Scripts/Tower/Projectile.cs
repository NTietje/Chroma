using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

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

    // ********* SPIELER COLLISION ABFRAGEN *********
    private void OnTriggerEnter(Collider other) //Keine OnCollisionEnter Methode nehmen! Dann fliegt das Projektil nicht richtig aus dem Turm raus
    {
        if (other.tag == "Player")
        {
            Destroy(gameObject);
            //Hier Spielerfarbe zu Default (erstmal weiß) ändern
            //collision.gameObject.Blabla Funktion
            Debug.Log("Spieler wurde von Projektil getroffen");
        }
    }
   

}
