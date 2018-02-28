using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour {

	public static bool GameIsPaused = false;
	public GameObject pauseMenuUI;
	public GameObject completedMenuUI;

	void Start (){
		GameManager.instance.SetMenu (this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	
	public void Resume (){
		pauseMenuUI.SetActive(false); //Sets the PauseMenu canvas to not visible
		Time.timeScale = 1f; //The game continues
		GameIsPaused = false;
	}
	
	public void Pause (){
		pauseMenuUI.SetActive(true); //Sets the PauseMenu canvas active
		Time.timeScale = 0f; //Stops the game
		GameIsPaused = true; 
	}
	public void LevelCompleted (){
			completedMenuUI.SetActive (true);
			Time.timeScale = 0f; //Stops the game
			GameIsPaused = true; 
	}
	public void LoadMenu(){
		SceneManager.LoadScene("Menu");	//Calls up the Menu Scene
	}
	public void NextLevel(){
		GameManager.instance.NextLevel ();
		Time.timeScale = 1f; //The game continues
		GameIsPaused = false;
	}
	
}
