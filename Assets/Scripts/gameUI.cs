using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class gameUI : MonoBehaviour {

	public AudioClip	win_ac;
	public AudioClip	loose_ac;
	public Text			endText;
	public GameObject	endPanel;

	public static gameUI	gui;

	private AudioSource		ads;

	public void loose() {
		if (gameManager.finished)
			return ;
		endText.text = "Game Over";
		gameManager.finished = true;
		ads.PlayOneShot(loose_ac);
	}

	public void win() {
		if (gameManager.finished)
			return ;
		endText.text = "You Win";
		gameManager.finished = true;
		ads.PlayOneShot(win_ac);
	}

	void Awake() {
		gui = this;
	}

	// Use this for initialization
	void Start () {
		ads = GetComponent< AudioSource >();
	}
	
	// Update is called once per frame
	void Update () {
		if (gameManager.finished)
			endPanel.SetActive(true);
	}
}
