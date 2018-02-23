using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourItemController : MonoBehaviour {

    public float respawnTime;
    public GameObject colourItem;

    public void SetColourItemInactiv()
    {
        Debug.Log("OFF");
        colourItem.SetActive(false);
        Invoke("ShowGameobjectAfterSeconds", respawnTime);
    }

    void ShowGameobjectAfterSeconds()
    { 
        colourItem.SetActive(true);
    }

}
