using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public void PlayGame (){
		if (GameManager.instance.musicOn){
			GameManager.instance.MusicOn ();
		}
		GameManager.instance.NewGame();
        Time.timeScale = 1f;
	}

}
