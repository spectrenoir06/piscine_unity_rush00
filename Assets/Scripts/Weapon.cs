using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	public Sprite		attachToBodySprite;
	public GameObject	bullet;
	public float		fireIdle = 0.4F;
	public int			raffaleSize = 1;
	public float		raffaleRate = 0F;
	public float		bulletSpeed = 300;
	public int			ammoNumber = 0;
	private GameObject	controller;

	private Vector2		target;
	private bool		pickable = true;
	private bool		canShoot = true;

	// Use this for initialization
	void Start () {
		target = transform.position;
	}

	public void		grab(GameObject c) {
		gameManager.playGetWeapon();
		controller = c.transform.FindChild("bulletEmitter").gameObject;
	}

	// Update is called once per frame
	void Update () {
		if (target != (Vector2)transform.position)
			transform.position = Vector2.Lerp((Vector2)transform.position, target, Time.deltaTime * 5);
	}

	public bool	isPickable() {
		return (pickable);
	}

	IEnumerator		weaponCanShoot() {
		yield return new WaitForSeconds(fireIdle);
		canShoot = true;
	}

	IEnumerator		shoot(Transform t, Vector2 pos) {
		for (int i = 0; i < raffaleSize; i++)
		{
			GameObject b = GameObject.Instantiate(bullet, controller.transform.position, Quaternion.Euler(t.eulerAngles.x, t.eulerAngles.y, t.eulerAngles.z - 90)) as GameObject;
			b.GetComponent< Rigidbody2D >().AddForce((pos - (Vector2)t.position).normalized * bulletSpeed);
			ammoNumber--;
			if (ammoNumber == 0)
				break ;
			yield return new WaitForSeconds(raffaleRate);
		}
	}


	public void fire(Transform t, Vector2 pos) {
		if (ammoNumber == 0)
		{
			gameManager.playDryFire();
			return ;
		}
		if (!canShoot)
			return ;
		canShoot = false;
		StartCoroutine(shoot(t, pos));
		StartCoroutine(weaponCanShoot());
	}

	IEnumerator		pickableWeapon() {
		yield return new WaitForSeconds(0.5F);
		pickable = true;
	}

	public void	drop(Transform t, Vector2 pos) {
		target = pos;
		gameObject.transform.position = t.position;
		gameObject.transform.rotation = t.rotation;
		gameObject.GetComponent< SpriteRenderer >().enabled = true;
		pickable = false;
		StartCoroutine(pickableWeapon());
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.layer == LayerMask.NameToLayer("wall"))
			target = transform.position;
	}
}
