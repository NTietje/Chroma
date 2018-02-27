using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorLayer : MonoBehaviour {

	[Range(0.0f, 1.0f)]
	public float alphaFactor = 1;


	// Use this for initialization
	void Start () {
		Color layerColor = Color.white;
		foreach (LayerColor layer in LayerColors.layers) {
			if (layer.index == gameObject.layer) {
				layerColor = new Color(layer.color.r, layer.color.g, layer.color.b, layer.color.a * alphaFactor);
				break;
			}
		}
		Renderer rend = gameObject.GetComponent<Renderer> ();
		rend.material.color = layerColor;
		rend.material.SetColor ("_EmissionColor", layerColor);
		DynamicGI.SetEmissive (gameObject.GetComponent<Renderer> (), layerColor);
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
