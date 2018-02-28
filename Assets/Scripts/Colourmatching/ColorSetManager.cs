using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Use this to define which layerColors are used on special color objects and areas
 * these colors are bound to certain physics layers (layers 8-11) and also to the default layer (uncolored)
 * Must be active in any Scene
 * 
 * public Color defaultPlayerColor : default color of the player cube
 * public LayerColor[] colorLayers : set of layerColors (see public struct layerColor in this script)
 * 
 */
public class ColorSetManager : MonoBehaviour {

	public Color defaultPlayerColor;
	public LayerColor[] colorLayers;

	//on awake, apply the layer colors to static variables and the default color to the player
	void Awake () {
		LayerColors.defaultColor = defaultPlayerColor;
		LayerColors.layers = colorLayers;
	}
}
/**
 * This class can be accessed from anywere for colouring behaviour purposes 
 * it is primarily used by the ColorLayer script and the PlayerControls script
 */
public static class LayerColors {

	public static Color defaultColor;
	public static LayerColor[] layers;

	/**
	 * returns the corresponding color to a certain layer
	 * four different layers can be used (layers 8-11)
	 */
	public static Color FindLayerColor(int layerIndex){
		foreach (LayerColor layercolor in layers) {
			if (layercolor.index == layerIndex) {
				return layercolor.color;
			}
		}
		return defaultColor;
	}

}
/**
 * This struct keeps a color and a corresponding layer index
 */
[System.Serializable]
public struct LayerColor{
	
	public int index;
	public Color color;

	public LayerColor(Color color, int layerIndex){
		index = layerIndex;
		this.color = color;
	}
}
