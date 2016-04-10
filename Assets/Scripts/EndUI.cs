using UnityEngine;
using System.Collections;

public class EndUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void onBackToMenuPressed() {
		Debug.Log ("back pressed ");
		Application.LoadLevel("TitleScreen");
	}

	public void onRestartPressed() { 
		Debug.Log ("restart pressed ");
		Application.LoadLevel("level1");
	}
}
