using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public float		lifeTime = 2;

	IEnumerator	destroyAmmo() {
		yield return new WaitForSeconds(lifeTime);
		GameObject.Destroy(gameObject);
	}

	// Use this for initialization
	void Start () {
		StartCoroutine(destroyAmmo());
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Player" || coll.gameObject.tag == "Enemy") {
			if (gameObject.layer != LayerMask.NameToLayer("playerBullet"))
				coll.gameObject.GetComponent< Entity >().die();
		} else {
			GameObject.Destroy(gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
	}
}
