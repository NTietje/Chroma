using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadNextLevel : MonoBehaviour {

	public void LoadLevel (){
		GameManager.instance.NextLevel ();
        Time.timeScale = 1f;		
	}
}
