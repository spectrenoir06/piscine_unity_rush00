using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public	float			Speed = 5F;

	private Animator		anim;
	private Rigidbody2D		rbody;
	private Weapon			weapon;
	private GameObject		weaponFloor;
	private SpriteRenderer	attachToBodySprite;
	private Vector2			bulletEmitter;
	private bool			canShoot;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		rbody = GetComponent< Rigidbody2D >();
		attachToBodySprite = GetComponentsInChildren< SpriteRenderer >()[3];
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Weapon")
			weaponFloor = coll.gameObject;
	}

	void OnTriggerExit2D(Collider2D coll) {
		if (coll.gameObject.tag == "Weapon")
			weaponFloor = null;
	}

	void pickWeapon() {
		if (!weaponFloor)
			return ;
		weapon = weaponFloor.GetComponent< Weapon >();
		if (!weapon.isPickable())
		{
			weapon = null;
			return ;
		}
		attachToBodySprite.sprite = weapon.attachToBodySprite;
		weaponFloor.GetComponent< SpriteRenderer >().enabled = false;
		weapon.grab(gameObject);
	}

	void dropWeapon() {
		weapon.drop(transform, Camera.main.ScreenToWorldPoint((Vector2)Input.mousePosition));
		weapon = null;
		attachToBodySprite.sprite = null;
	}

	void fireWeapon() {
		weapon.fire(transform, Camera.main.ScreenToWorldPoint((Vector2)Input.mousePosition));
	}

	public void die() {

	}

	// Update is called once per frame
	void Update () {
		Vector2		mouvement;

		mouvement.x = Input.GetAxis("Horizontal");
		mouvement.y = Input.GetAxis("Vertical");

		if (mouvement != Vector2.zero)
			anim.SetBool("isWalking", true);
		else
			anim.SetBool("isWalking", false);

		rbody.velocity = mouvement * Speed;

		Vector2		mouse;

		mouse = (Vector2)Camera.main.ScreenToWorldPoint((Vector2)Input.mousePosition);
		mouse -= (Vector2)transform.position;

		float	dot = 0 + 1 * mouse.y;
		float	det = 0 - 1 * mouse.x;
		float	angle = Mathf.Atan2(det, dot) * Mathf.Rad2Deg;
		angle += 180;
		transform.rotation = Quaternion.Euler(0, 0, angle);

		if (Input.GetMouseButtonDown(0) && weapon)
			fireWeapon();
		if (Input.GetMouseButtonDown(1) && weapon)
			dropWeapon();

		if (Input.GetKeyDown("e") || Input.GetMouseButtonDown(1))
			pickWeapon();
		Debug.Log(rbody.velocity);
	}
}