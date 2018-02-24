using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerStartMovement : MonoBehaviour {

    public GameObject platform;
    public float stoptimer;

    // On Trigger Enter move platform
    private void OnTriggerEnter(Collider other)
    {
        // if collision with player, move the platform
        if (other.tag == "Player")
        {
            platform.GetComponent<ObjectMovementManyDestiantions>().allowMoving = true;

            //************************ Hier Sound für Trigger rein ****************************

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
