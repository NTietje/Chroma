using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation) as GameObject;
        projectile.GetComponent<Projectile>().Initialize(transform.position, projectileDistance, projectileSpeed);
		
    }

}
