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
	private Player		player;

	private Vector2		target;
	private bool		pickable = true;
	private bool		canShoot = true;

	// Use this for initialization
	void Start () {
		target = transform.position;
		player = GameObject.Find("Player").GetComponent< Player >();
	}

	public void		grab(GameObject c) {
		gameManager.playGetWeapon();
		controller = c.transform.FindChild("bulletEmitter").gameObject;
	}

	// Update is called once per frame
	void Update () {
		if (target != (Vector2)transform.position) {
			transform.position = Vector2.Lerp ((Vector2)transform.position, target, Time.deltaTime * 5);
			float l = Vector2.Distance(target, (Vector2)transform.position);
			transform.Rotate(Vector3.back * Time.deltaTime * l * 500);
		}
	}

	public bool	isPickable() {
		return (pickable);
	}

	IEnumerator		weaponCanShoot() {
		yield return new WaitForSeconds(fireIdle);
		canShoot = true;
	}

	IEnumerator		shoot(Transform t, Vector2 pos, int layer) {
		for (int i = 0; i < raffaleSize; i++)
		{
			GameObject b = GameObject.Instantiate(bullet, controller.transform.position, Quaternion.Euler(t.eulerAngles.x, t.eulerAngles.y, t.eulerAngles.z - 90)) as GameObject;
			b.layer = layer;
			b.GetComponent< Rigidbody2D >().AddForce((pos - (Vector2)t.position).normalized * bulletSpeed);
			if (layer != LayerMask.NameToLayer("enemyBullet"))
				ammoNumber--;
			if (ammoNumber == 0)
				break ;
			yield return new WaitForSeconds(raffaleRate);
		}
	}

	IEnumerator		shoot(Transform t, Entity.Target target, int layer) {
		for (int i = 0; i < raffaleSize; i++)
		{
			Vector2 pos;

			switch (target)
			{
			case Entity.Target.Mouse:
				pos = Camera.main.ScreenToWorldPoint((Vector2)Input.mousePosition);
				break;
			case Entity.Target.Player:
				pos = player.transform.position;
				break;
			}

			GameObject b = GameObject.Instantiate(bullet, controller.transform.position, Quaternion.Euler(t.eulerAngles.x, t.eulerAngles.y, t.eulerAngles.z - 90)) as GameObject;
			b.layer = layer;
			b.GetComponent< Rigidbody2D >().AddForce((pos - (Vector2)t.position).normalized * bulletSpeed);
			if (layer != LayerMask.NameToLayer("enemyBullet"))
				ammoNumber--;
			if (ammoNumber == 0)
				break ;
			yield return new WaitForSeconds(raffaleRate);
		}
	}


	public void fire(Transform t, Vector2 pos, int layer) {
		if (ammoNumber <= 0)
		{
			gameManager.playDryFire();
			return ;
		}
		if (!canShoot)
			return ;
		canShoot = false;
		StartCoroutine(shoot(t, pos, layer));
		StartCoroutine(weaponCanShoot());
	}

	public void fire(Transform t, Entity.Target target, int layer) {
		if (ammoNumber <= 0)
		{
			gameManager.playDryFire();
			return ;
		}
		if (!canShoot)
			return ;
		canShoot = false;
		StartCoroutine(shoot(t, target, layer));
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
		if (gameObject.name == "S-Saber" && coll.gameObject.layer == LayerMask.NameToLayer("entity") && pickable == false && coll.gameObject.tag != "Player")
			coll.gameObject.GetComponent< Entity >().die();
	}
}
