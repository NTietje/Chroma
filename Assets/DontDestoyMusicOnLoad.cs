using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestoyMusicOnLoad : MonoBehaviour {

	void Awake () {
		
		GameObject[] musicObjs = GameObject.FindGameObjectsWithTag("Music"); //Checks objects in the scene with tag: Music
		
		if(musicObjs.Length > 1) 		//If more than 1 object is found, 
			Destroy(this.gameObject);	//it will be destroyed
			
			DontDestroyOnLoad(this.gameObject);
		
	}

}