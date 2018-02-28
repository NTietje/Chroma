using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour {

	public static PauseMenu instance;
	public static bool GameIsPaused = false;
	public GameObject pauseMenuUI;
	public GameObject completedMenuUI;
	public GameObject winScreenUI;

	void Awake() {
		if (instance == null) {
			DontDestroyOnLoad (gameObject);
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}

	}

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
		winScreenUI.SetActive(false);
		completedMenuUI.SetActive (false);
	}

	public void Pause (){
		pauseMenuUI.SetActive(true); //Sets the PauseMenu canvas active
		Stop ();
	}
	public void LevelCompleted (){
		completedMenuUI.SetActive (true);
		Stop ();
	}
	public void LoadMenu(){
		SceneManager.LoadScene("Menu");	//Calls up the Menu Scene
		MusicOff();
		Resume();
	}
	public void NextLevel(){
		GameManager.instance.NextLevel ();
		Resume ();
	}
	public void MusicOff(){
		GameManager.instance.MusicOff ();
		GameManager.instance.musicOn = false;
	}
	public void MusicOn(){
		GameManager.instance.MusicOn ();
		GameManager.instance.musicOn = true;
	}
	public void WinScreen(){
		winScreenUI.SetActive(true);
		Stop ();
	}
	private void Stop(){
		Time.timeScale = 0f; //Stops the game
		GameIsPaused = true;
	}
}
