using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour {

	public static bool GameIsPaused = false;
	
	public GameObject pauseMenuUI; 
	
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
	
	public void LoadMenu(){
		SceneManager.LoadScene(0);	//Ruft Szene 0 auf, das ist in unserem Fall die Menu Szene
	}
	
}
