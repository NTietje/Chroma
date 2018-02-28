using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script for a gameobject as origin of projectiles

public class ProjectileGenerator : MonoBehaviour {

    public GameObject projectilePrefab;
    public float projectileSpeed;
    public float projectileDistance;
    public float respawnTime;

    float time;

    // Use this for initialization
    void Start()
    {
        instantiateNewProjetile();
        time = respawnTime;
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if (time < 0)
        {
            instantiateNewProjetile();
            //Debug.Log("generate new Projectile");
            time = respawnTime;
			
        }
    }


    // Instantiate new projectile and add to the projectiles list
    void instantiateNewProjetile()
    {
        Vector3 position = new Vector3(transform.position.x, transform.position.y +0.4f, transform.position.z);
        GameObject projectile = Instantiate(projectilePrefab, position, transform.rotation) as GameObject;
        projectile.GetComponent<Projectile>().Initialize(position, projectileDistance, projectileSpeed);
    }

}
