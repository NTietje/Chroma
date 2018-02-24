using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager gm;

	public Color colorLayer1 = Color.red;
	public Color colorLayer2 = Color.green;
	public Color colorLayer3 = Color.blue;
	public Color colorLayer4 = Color.yellow;


	void Awake() {
		if (gm == null) {
			DontDestroyOnLoad (gameObject);
			gm = this;
		} else if (gm != this) {
			Destroy (gameObject);
		}
			
	}
	// Use this for initialization
	void Start () {
		LayerColors.layer1 = colorLayer1;
		LayerColors.layer2 = colorLayer1;
		LayerColors.layer3 = colorLayer1;
		LayerColors.layer4 = colorLayer1;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
public static class LayerColors {

	public static Color layer1;
	public static Color layer2;
	public static Color layer3;
	public static Color layer4;

}