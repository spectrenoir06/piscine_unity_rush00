using UnityEngine;
using System.Collections;

public class gnomeGun : MonoBehaviour {

	private AudioSource[]		ads;

	public void playRandomSounds() {
		ads[Random.Range(0, 4)].Play();
	}

	// Use this for initialization
	void Start () {
		ads = GetComponents< AudioSource >();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
