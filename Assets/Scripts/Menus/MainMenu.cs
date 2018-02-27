using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {


	public void PlayGame (){
        //	SceneManager.LoadScene(1); //Lädt eine bestimmte Szene
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //Lädt die nächste Szene in der Warteschlange, dafür müssen wir sicher gehen das diese sich dort befindet (File -> Build Settings.. -> Scenes in Build) 
        //SceneManager.LoadScene("Dev Nina");
		GameManager.instance.NewGame();
        Time.timeScale = 1f;
	}
	
}
