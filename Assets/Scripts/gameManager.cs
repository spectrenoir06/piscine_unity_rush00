using UnityEngine;
using System.Collections;

public class gameManager : MonoBehaviour {

	public AudioClip				death;
	public AudioClip				getWeapon;
	public AudioClip				dryFire;

	static public Weapon			currentPlayerWeapon = null;
	static public Vector2			playerDoorPosition;
	static public bool				finished = false;
	static private AudioClip		deathClip;
	static private AudioClip		dryFireClip;
	static private AudioClip		getWeaponClip;
	static private AudioSource		ads;

	public static void	playClip(AudioClip ac, float level) {
		if (ads)
			ads.PlayOneShot(ac, level);
	}

	public static void	playDryFire() {
		if (ads && dryFireClip)
			ads.PlayOneShot(dryFireClip, 0.7F);
	}

	public static void	playGetWeapon() {
		if (ads && getWeaponClip)
			ads.PlayOneShot(getWeaponClip, 0.7F);
	}

	public static void	playDeath() {
		if (ads && deathClip)
			ads.PlayOneShot(deathClip, 0.7F);
	}

	// Use this for initialization
	void Start () {
		deathClip = death;
		getWeaponClip = getWeapon;
		dryFireClip = dryFire;
		ads = GetComponent< AudioSource >();
	}
	
	// Update is called once per frame
	void Update () {
	}
}
