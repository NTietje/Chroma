using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorLayer : MonoBehaviour {
	/**
	* with this script attached, the gameObject will change its color to a certain layercolor, which is managed in the ColorSetManager.cs 
	* A ColorSetManager must be active in the Scene
	* 
	* public float alphaFactor : reduces the alpha of the material
	*
	*/
	[Range(0.0f, 1.0f)]
	public float alphaFactor = 1;


	// Use this for initialization
	void Start () {
		Color layerColor = Color.white;
		foreach (LayerColor layer in LayerColors.layers) {
			if (layer.index == gameObject.layer) {
				layerColor = new Color (layer.color.r, layer.color.g, layer.color.b, layer.color.a * alphaFactor);
				break;
			}
		}
		//colorize object's material
		Renderer rend = gameObject.GetComponent<Renderer> ();
		rend.material.color = layerColor;
		rend.material.SetColor ("_EmissionColor", layerColor);
		DynamicGI.SetEmissive (gameObject.GetComponent<Renderer> (), layerColor);
		//colorize children materials
		Component[] renderers = gameObject.GetComponentsInChildren(typeof(Renderer));
		//Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer>();
		foreach (Renderer renderer in renderers) {
			renderer.material.color = layerColor;
		}
		//colorize lights
		Component[] lights = gameObject.GetComponentsInChildren (typeof(Light));
		foreach (Light light in lights) {
			light.color = layerColor;
		}
	}
}
