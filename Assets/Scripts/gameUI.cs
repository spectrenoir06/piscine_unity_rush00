using UnityEngine;
using System.Collections;

public class gameUI : MonoBehaviour {

	public AudioClip	win_ac;
	public AudioClip	loose_ac;

	public static gameUI	gui;

	private AudioSource		ads;

	public void loose() {
		if (gameManager.finished)
			return ;
		gameManager.finished = true;
		ads.PlayOneShot(loose_ac);
	}

	public void win() {
		if (gameManager.finished)
			return ;
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
	
	}
}
