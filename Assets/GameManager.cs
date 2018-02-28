using System.Collections;
using System.Collections.Generic;

using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
/**
 * The GameManager organizes checkpoints, savegames, game progress etc. 
 * This class is initialized as singleton and can be accessed by any other class with GameManager.instance
 * This classes instance will also remain active when changing the scene (level change or main menu)
 */

public class GameManager : MonoBehaviour {

	//Singleton instance, will 
	public static GameManager instance;

	//used for various debugging purposes, deactivate this when building 
	public bool debug = false;
	public bool musicOn;

	//linked pause menu to call the menus functions
	private PauseMenu menu;

	//player spawn related
	public float spawnOffset = 0.5f; //let's the player spawn above the checkpoint
	private GameObject checkPoint; //linked active checkpoint
	private Vector3 spawnPoint; //current spawn position of the player
	private bool customSpawn; //true: load a level with a custom spawn point
	private Vector3 customSpawnPoint; //custom spawn point location (see above)
	private int customSpawnLayer; //custom spawn layer (see above)
	private int saveLayer; //the layer the player was on, when reaching the last checkpoint

	//current active game level
	private int level;

	//Music
	public AudioClip levelMusicSound;
	private AudioSource levelMusicSource;

	void Awake () {
		if (instance == null) {
			DontDestroyOnLoad (gameObject);
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}
				
	}
	void Start () {
		//active level when starting the game in any scene (dev)
		if (SceneManager.GetActiveScene ().buildIndex != 0) {
			level = SceneManager.GetActiveScene ().buildIndex;
			//spawnPoint = PlayerControls.instance.transform.position;
		} else {
			MusicOff ();
		}
		musicOn = true;
	}
	/**
	 * Loads a level. Level indices are managed in the SceneManager (build settings)
	 * make sure the desired level indices are the same as the scene manager build indices
	 * Can also load the main menu with LoadLevel(0)
	 * 
	 * int levelIndex : scene manager index of the scene to open
	 */
	public void LoadLevel(int levelIndex){
		if (levelIndex < SceneManager.sceneCountInBuildSettings) {
			SceneManager.LoadScene (levelIndex, LoadSceneMode.Single);
			level = levelIndex;
		} else {
			Debug.Log ("Level not found.");
		}
	}
	//Load next level after finished
	public void NextLevel(){
		LoadLevel (level);
	}
	//show the level completed screen
	public void Finish(bool lastLevel){
		if (lastLevel) {
			menu.WinScreen ();
			level = 0;
		} else {
			menu.LevelCompleted ();
			level++;
		}
		Save ();
	}
	//starts a new game (level 1)
	public void NewGame(){
		LoadLevel (1);
	}
	public void MusicOn(){
		gameObject.GetComponent<AudioSource> ().enabled = true;
	}
	public void MusicOff(){
		gameObject.GetComponent<AudioSource> ().enabled = false;
	}
	//activate and register a new checkpoint, basically saves a players progress within a level
	public void SetCheckPoint(GameObject checkPoint, int layer){
		if (checkPoint.GetComponent<CheckPointBehaviour> () && (this.checkPoint != checkPoint)) {
			if (this.checkPoint != null) {
				this.checkPoint.GetComponent<CheckPointBehaviour> ().Off();
			}
			this.checkPoint = checkPoint;
			checkPoint.GetComponent<CheckPointBehaviour>().On ();
			spawnPoint = new Vector3 (checkPoint.transform.position.x, checkPoint.transform.position.y + spawnOffset, checkPoint.transform.position.z);
			saveLayer = layer;
			Save ();
		} 
	}
	//Get currently active spawn location
	public Vector3 GetSpawn(){
		return spawnPoint;
	}
	//Set new spawn location
	public void SetSpawn(Vector3 spawnPoint){
		this.spawnPoint = spawnPoint;
	}
	/**
	 * Save the players progress to a dedicated file 
	 */
	public void Save(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/playerInfo.dat");

		PlayerData data = new PlayerData ();
		data.level = level;
		data.x = spawnPoint.x;
		data.y = spawnPoint.y;
		data.z = spawnPoint.z;
		data.layer = saveLayer;

		bf.Serialize (file, data);
		file.Close ();
	}
	/**
	 * load the players progress from a dedicated file
	 * uses custom spawn point to let the player continue from a checkpoint instead of the levelstart
	 */
	public bool Load(){
		if (File.Exists (Application.persistentDataPath + "/playerInfo.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
			PlayerData data = (PlayerData)bf.Deserialize (file);
			file.Close ();
			LoadLevel (data.level);
			customSpawnPoint = new Vector3 (data.x, data.y, data.z);
			customSpawnLayer = data.layer;
			customSpawn = true;
			spawnPoint = new Vector3 (data.x, data.y, data.z);
			if (data.level > 0) {
				return true;
			}
		} 
		return false;
	}
	//register the pause menu
	public void SetMenu(PauseMenu menu){
		this.menu = menu;
	}
	//returns true, if a custom spawn is set, also moves the player to its spawn
	public bool CustomSpawn(){
		if (customSpawn) {
			PlayerControls.instance.transform.position = customSpawnPoint;
			PlayerControls.instance.gameObject.layer = customSpawnLayer;
			customSpawn = false;
			return true;
		}
		return false;
	}
	//for debugging only atm
	void OnGUI(){
		if (debug) {
			if (GUI.Button (new Rect (10, 100, 100, 30), "Save")) {
				Save ();
			}
			if (GUI.Button (new Rect (10, 140, 100, 30), "Load")) {
				Load ();
			}
		}
	}
}
/**
 *this is the save game format. 
 * public in level : current level
 * public int layer : current color layer
 * ints x/y/z define the spawn point
 */
[Serializable]
class PlayerData {

	public int level;
	public int layer;

	//Spawn Point
	public float x;
	public float y;
	public float z;

}