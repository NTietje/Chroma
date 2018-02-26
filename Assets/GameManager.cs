using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {


	public static GameManager instance;
	public GameObject rockBottomPrefab;
	public int rockBottomHeight = -10;

	private Vector3 spawnPoint;


	void Awake() {
		if (instance == null) {
			DontDestroyOnLoad (gameObject);
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}
			
	}
	// Use this for initialization
	void Start () {
		Instantiate (rockBottomPrefab, transform);
		rockBottomPrefab.transform.position = (new Vector3 (0, rockBottomHeight, 0));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void NextLevel(){
		int currentLevel = SceneManager.GetActiveScene ().buildIndex;
		if (currentLevel+1 < SceneManager.sceneCountInBuildSettings) {

			//Unload the current scene	
			SceneManager.UnloadSceneAsync (currentLevel);

			//Load the next scene and set it as active
			SceneManager.LoadScene (currentLevel + 1);
			SceneManager.SetActiveScene (SceneManager.GetSceneAt(currentLevel+1));

		} else {
			Debug.Log ("no more scenes in build");
		}
	}
	public Vector3 GetSpawn(){
		return spawnPoint;
	}
	public void SetSpawn(Vector3 spawnPoint){
		this.spawnPoint = spawnPoint;
	}
}