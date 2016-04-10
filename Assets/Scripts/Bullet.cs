using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public float		lifeTime = 2;
	public AudioClip	sound;

	IEnumerator	destroyAmmo() {
		yield return new WaitForSeconds(lifeTime);
		GameObject.Destroy(gameObject);
	}

	// Use this for initialization
	void Start () {
		if (gameObject.tag == "gnomeBullet")
			GetComponent< gnomeGun >().playRandomSounds();
		else
			gameManager.playClip(sound, 1);
		StartCoroutine(destroyAmmo());
	}

	void OnTriggerEnter2D(Collider2D coll) {
		Debug.Log (coll.gameObject.tag);
		if (coll.gameObject.tag == "Player") {
			if (gameObject.layer != LayerMask.NameToLayer("playerBullet"))
				coll.gameObject.GetComponent< Entity >().die();
		} else if (coll.gameObject.tag == "Enemy") {
			if (gameObject.layer != LayerMask.NameToLayer("EnemyBullet"))
				coll.gameObject.GetComponent< Entity >().die();
		} else {
			GameObject.Destroy(gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
	}
}
