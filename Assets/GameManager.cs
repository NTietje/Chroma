using System.Collections;
using System.Collections.Generic;

using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public bool debug;

	public float spawnOffset = 0.5f;
	public static GameManager instance;
	//public GameObject rockBottomPrefab;
	//public int rockBottomHeight = -10;

	private GameObject checkPoint;
	private Vector3 spawnPoint;

	private int saveLayer;
	private int level;
	//private int activePlayerLayer;

	//Custom Spawn
	private bool customSpawn;
	private Vector3 customSpawnPoint;
	private int customSpawnLayer;

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
		if (SceneManager.GetActiveScene ().buildIndex != 0) {
			level = SceneManager.GetActiveScene ().buildIndex;
			//spawnPoint = PlayerControls.instance.transform.position;
		}
		//Instantiate (rockBottomPrefab, transform);
		//rockBottomPrefab.transform.position = (new Vector3 (0, rockBottomHeight, 0));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	private void LoadLevel(int levelIndex){
		if (levelIndex < SceneManager.sceneCountInBuildSettings) {
			SceneManager.LoadScene (levelIndex, LoadSceneMode.Single);
			level = levelIndex;

			//SceneManager.SetActiveScene (SceneManager.GetSceneAt(currentLevel+1));

		} else {
			Debug.Log ("no more scenes in build");
		}
	}
	public void NextLevel(){

		LoadLevel (level+1);

		if (level != null) {
			LoadLevel (level + 1);
		} else {
			LoadLevel (1);
		}


	}
	public void Finish(){
		//Load Canvas
	}
	public void NewGame(){
		LoadLevel (1);
	}
	public Vector3 GetSpawn(){
		return spawnPoint;
	}
	public void SetSpawn(Vector3 spawnPoint){
		this.spawnPoint = spawnPoint;
	}
	public void SetCheckPoint(GameObject checkPoint, int layer){
		if (checkPoint.GetComponent<CheckPointBehaviour> () && (this.checkPoint != checkPoint)) {
			if (this.checkPoint != null) {
				this.checkPoint.GetComponent<CheckPointBehaviour> ().Off();
			}
			this.checkPoint = checkPoint;
			checkPoint.GetComponent<CheckPointBehaviour>().On ();
			spawnPoint = new Vector3 (checkPoint.transform.position.x, checkPoint.transform.position.y + spawnOffset, checkPoint.transform.position.z);
			saveLayer = layer;
		} 
	}
	/*public void SetActivePlayerLayer(int layerIndex){
		this.activePlayerLayer = activePlayerLayer;
	}*/
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
	public void Load(){
		if (File.Exists (Application.persistentDataPath + "/playerInfo.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
			PlayerData data = (PlayerData)bf.Deserialize (file);
			file.Close ();
			LoadLevel (data.level);

			if (PlayerControls.instance.isActiveAndEnabled) {
				Debug.Log ("instance not null");
			}
			customSpawnPoint = new Vector3(data.x, data.y, data.z);
			customSpawnLayer = data.layer;
			customSpawn = true;
			//PlayerControls.instance.gameObject.transform.position = new Vector3(data.x, data.y, data.z);
			spawnPoint = new Vector3(data.x, data.y, data.z);
			//activePlayerLayer = data.activePlayerLayer;
			Debug.Log (spawnPoint.ToString());
		}
	}

	public bool CustomSpawn(){
		if (customSpawn) {
			PlayerControls.instance.transform.position = customSpawnPoint;
			PlayerControls.instance.gameObject.layer = customSpawnLayer;
			customSpawn = false;
			return true;
		}
		return false;
	}
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
[Serializable]
class PlayerData {

	public int level;
	public int layer;

	//Spawn Point
	public float x;
	public float y;
	public float z;

}