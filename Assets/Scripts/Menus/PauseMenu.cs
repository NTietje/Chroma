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
		pauseMenuUI.SetActive(false); //Das Pausenmenü ist nicht sichtbar
		Time.timeScale = 1f; //Somit geht das Spiel wieder weiter
		GameIsPaused = false;
	}
	
	public void Pause (){
		pauseMenuUI.SetActive(true); //Macht das Pausenmenü sichtbar
		Time.timeScale = 0f; //Dient dazu die Zeit in dem Spiel anzuhalten
		GameIsPaused = true; 
	}
	public void LevelCompleted (){
		completedMenuUI.SetActive (true);
	}
	public void LoadMenu(){
		SceneManager.LoadScene("Menu");	//Ruft Szene Meu auf
	}
	public void NextLevel(){
		GameManager.instance.NextLevel ();
	}
	
}
