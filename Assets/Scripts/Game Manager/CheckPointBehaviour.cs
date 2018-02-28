using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This script enables a gameObject to register itself as checkpoint in the game manager
 * When the player is reset or a game is continued, the last activated checkpoint is the place where the player starts
 * checkpoints become active when the player triggers it's collider
 * checkpoints become inactive, when a different checkPoint is activated. checkpoints are also inactive by default
 * 
 */
public class CheckPointBehaviour : MonoBehaviour {

	public Color colorUnchecked = Color.yellow; //inactive color
	public Color colorChecked = Color.green; //active color

	bool active; //checkpoint is active, only one will be active at once (handled by game manager)
	bool switching; //checkpoint is currently changeing it's color
	Renderer rend; //attached gameobject's renderer
	Component[] renderers; //gameobjects' childs renderes


	// Use this for initialization
	void Start () {
		Off ();
		rend = GetComponent<Renderer> ();
		rend.material.color = colorUnchecked;
		renderers = gameObject.GetComponentsInChildren(typeof(Renderer));
	}
	void OnTriggerEnter(Collider other){
		//registeres the attached gameObject as active spawnPoint in the gameManagers
		if (other.gameObject.tag == "Player") {
			GameManager.instance.SetCheckPoint (gameObject, other.gameObject.layer);
		}
	}

	// Update is called once per frame
	void Update () {
		//manages the color change from active to inactive and vice versa
		if (switching) {
			Color lerpColor = rend.material.color;
			if (active) {
				lerpColor = Color.Lerp (rend.material.color, colorChecked, 0.07f);
				//color switch completed
				if (rend.material.color == colorChecked) {
					switching = false;
				}
			} else {
				lerpColor = Color.Lerp (rend.material.color, colorUnchecked, Time.deltaTime);
				//color switch completed
				if (rend.material.color == colorUnchecked) {
					switching = false;
				}
			}
			//apply color to gameObject's renderer and children
			rend.material.color = lerpColor;
			foreach (Renderer childRenderer in renderers) {
				childRenderer.material.color = lerpColor;
			}
		}
	}
	// deactivates
	public void Off(){
		active = false;
		switching = true;
	}
	//activates
	public void On(){
		active = true;
		switching = true;
	}
}

