using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSetManager : MonoBehaviour {

	public Color defaultColor;
	public LayerColor[] colorLayers;

	// Use this for initialization
	void Awake () {

		LayerColors.defaultColor = defaultColor;
		LayerColors.layers = colorLayers;

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

public static class LayerColors {

	public static Color defaultColor;
	public static LayerColor[] layers;

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
