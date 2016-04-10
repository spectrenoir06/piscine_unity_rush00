using UnityEngine;
using System.Collections;

public class EndUI : MonoBehaviour {

	public GameObject	endPanel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void onBackToMenuPressed() {
		endPanel.SetActive(false);
		gameManager.finished = false;
		Application.LoadLevel("TitleScreen");
	}

	public void onRestartPressed() { 
		endPanel.SetActive(false);
		gameManager.finished = false;
		Application.UnloadLevel(Application.loadedLevelName);
		Application.LoadLevel(Application.loadedLevelName);
	}
}
