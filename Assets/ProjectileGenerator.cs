using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileGenerator : MonoBehaviour {

    public GameObject projectilePrefab;
    public float projectileSpeed;
    public float projectileDistance;
    public float respawnTime;

    List<GameObject> projectiles;
    Vector3 startpoint;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        respawnTime -= Time.deltaTime;
        if(respawnTime <= 0)
        {
            instantiateNewProjetile();
        }
        foreach (var projectile in projectiles)
        {
            if(transform.position.x + projectileDistance < projectile.transform.position.x)
            {
                projectiles.Remove(projectile);
                Destroy(projectile);
            }
        }
        //projectiles.RemoveAll(item => item == null);
    }

    // Move all projectiles
    void LateUpdate ()
    {
        // Vector wird nur hier gebraucht, trotzdem lieber in Start initialisieren?
        Vector3 destination = new Vector3((transform.position.x + projectileDistance), transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, destination, Time.deltaTime);
    }

    // Instantiate new projectile and add to the projectiles list
    void instantiateNewProjetile ()
    {
        projectiles.Add(Instantiate(projectilePrefab, transform.position, transform.rotation) as GameObject);
    }
}
