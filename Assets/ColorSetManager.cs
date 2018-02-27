using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSetManager : MonoBehaviour {

	public Color defaultPlayerColor;
	public LayerColor[] colorLayers;

	// Use this for initialization
	void Awake () {

		LayerColors.defaultColor = defaultPlayerColor;
		LayerColors.layers = colorLayers;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

public static class LayerColors {

	public static Color defaultColor;
	public static LayerColor[] layers;

	public static Color FindLayerColor(int layerIndex){
		foreach (LayerColor layercolor in layers) {
			if (layercolor.index == layerIndex) {
				return layercolor.color;
			}
		}
		return defaultColor;
	}

}
[System.Serializable]
public struct LayerColor{
	
	public int index;
	public Color color;

	public LayerColor(Color color, int layerIndex){
		index = layerIndex;
		this.color = color;
	}
}
