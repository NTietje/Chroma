using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {


	public void PlayGame (){
        //SceneManager.LoadScene(1); //Loads scene 1 
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //Opens the next scene in build (File -> Build Settings.. -> Scenes in Build) 
        //SceneManager.LoadScene("Dev Nina");
		GameManager.instance.NewGame();
        Time.timeScale = 1f;
	}
	
}
