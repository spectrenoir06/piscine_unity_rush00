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
		Application.LoadLevel("TitleScreen");
	}

	public void onRestartPressed() { 
		Application.LoadLevel("level1");
	}
}
