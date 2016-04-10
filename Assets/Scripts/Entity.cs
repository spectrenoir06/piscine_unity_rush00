using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour {

	public	float			Speed = 5F;
	[HideInInspector]public Animator		anim;
	[HideInInspector]public Rigidbody2D		rbody;
	[HideInInspector]public Weapon			weapon;

	private GameObject		weaponFloor;
	private SpriteRenderer	attachToBodySprite;
	private Vector2			bulletEmitter;
	private bool			canShoot;

	public enum Target{
		Mouse = 1,
		Player = 2
	};

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		rbody = GetComponent< Rigidbody2D >();
		attachToBodySprite = GetComponentsInChildren< SpriteRenderer >()[3];
	}

	void FixedUpdate() {
		rbody.velocity = Vector2.zero;
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Weapon")
			weaponFloor = coll.gameObject;
		if (coll.gameObject.tag == "Player")
			gameObject.GetComponent< Enemy >().folow = true;
	}

	void OnTriggerExit2D(Collider2D coll) {
		if (coll.gameObject.tag == "Weapon")
			weaponFloor = null;
	}

	public void setWeapon(Weapon w) {
		weapon = w;
		if (!attachToBodySprite)
			attachToBodySprite = GetComponentsInChildren< SpriteRenderer >()[3];
		attachToBodySprite.sprite = weapon.attachToBodySprite;
		w.GetComponent< SpriteRenderer >().enabled = false;
		w.grab(gameObject);
	}

	public void pickWeapon() {
		if (!weaponFloor)
			return ;
		weapon = weaponFloor.GetComponent< Weapon >();
		if (!weapon.isPickable())
		{
			weapon = null;
			return ;
		}
		setWeapon(weapon);
	}

	public void dropWeapon(Transform t, Vector2 pos) {
		weapon.drop(t, pos);
		weapon = null;
		attachToBodySprite.sprite = null;
	}

	public void fireWeapon(Transform t, Entity.Target target) {
		if (GetComponent< Enemy >())
			weapon.fire(t, target, LayerMask.NameToLayer("enemyBullet"));
		else
			weapon.fire (t, target, LayerMask.NameToLayer("playerBullet"));
	}

	public void fireWeapon(Transform t, Vector2 pos) {
		if (GetComponent< Enemy >())
			weapon.fire(t, pos, LayerMask.NameToLayer("enemyBullet"));
		else
			weapon.fire (t, pos, LayerMask.NameToLayer("playerBullet"));
	}

	public void die() {
		gameManager.playDeath();
		GameObject.Destroy(gameObject);
	}
}
