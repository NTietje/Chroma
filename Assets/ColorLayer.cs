using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorLayer : MonoBehaviour {




	// Use this for initialization
	void Start () {
		Color layerColor = Color.white;
		foreach (LayerColor layer in LayerColors.layers) {
			if (layer.index == gameObject.layer) {
				layerColor = layer.color;
				break;
			}
		}
		gameObject.GetComponent<Renderer> ().material.color = layerColor;
		Component[] renderers = gameObject.GetComponentsInChildren(typeof(Renderer));
		//Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer>();
		foreach (Renderer renderer in renderers) {
			renderer.material.color = layerColor;
		}
		Component[] lights = gameObject.GetComponentsInChildren (typeof(Light));
		foreach (Light light in lights) {
			light.color = layerColor;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
