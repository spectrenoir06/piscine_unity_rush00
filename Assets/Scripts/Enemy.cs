﻿using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public GameObject		weapon;
	public float			fireIdle = 1F;
	public Vector2[]		path;

	private GameObject		instanciatedWeapon;
	private Entity			entity;
	private Player			player;
	public bool				folow = false;
	private bool			canShoot;
	private bool			seePlayer;
	private EnemyManager	em;

	// Use this for initialization
	void Start () {
		instanciatedWeapon = GameObject.Instantiate(weapon, transform.position, transform.rotation) as GameObject;
		instanciatedWeapon.GetComponent< SpriteRenderer >().enabled = false;
		player = GameObject.Find("Player").GetComponent< Player >();
		entity = GetComponent< Entity >();
		entity.setWeapon(instanciatedWeapon.GetComponent< Weapon >());
	}

	IEnumerator	 enemyCanShoot() {
		yield return new WaitForSeconds(fireIdle);
		canShoot = true;
	}

	// Update is called once per frame
	void Update () {
		if (!player)
			return ;
		//Debug.DrawLine(transform.position, player.gameObject.transform.position);

		Debug.DrawRay(transform.position, player.gameObject.transform.position - transform.position);
		RaycastHit2D[] hit =  Physics2D.LinecastAll(transform.position, player.gameObject.transform.position, 1 << LayerMask.NameToLayer("entity") | 1 << LayerMask.NameToLayer("wall"));

		foreach (var h in hit) {
			if (h.collider.tag == "Player")
			{
				seePlayer = true;
				break ;
			}
			if (h.collider.gameObject.tag == "Untagged") {
				seePlayer = false;
				break ;
			}
		}
/*		if (hit[0].collider.IsTouchingLayers(LayerMask.NameToLayer("entity")))
			seePlayer = true;
		else
			seePlayer = false;*/
/*		if (hit.collider.gameObject.tag == "player")
			seePlayer = true;
		else
			seePlayer = false;*/

//		transform.rotation = Quaternion.Euler(0, 0, -angle);
//		Debug.Log(transform.eulerAngles.z - angle);
//		if (transform.eulerAngles.z - angle > 60 && transform.eulerAngles.z < -60)
//			Debug.Log ("View !");

		if (folow && canShoot && seePlayer)
		{
			entity.fireWeapon(transform, player.transform.position);
			canShoot = false;
		}
		StartCoroutine(enemyCanShoot());
	//	entity.fireWeapon(transform, Camera.main.ScreenToWorldPoint((Vector2)Input.mousePosition);
	}

	void OnTriggerStay2D(Collider2D coll) {
		if (coll.tag == "Player" && seePlayer)
			folow = true;
		else if (coll.tag == "Player")
			folow = false;
	}

	void FixedUpdate() {
		if (!folow || !player)
			return ;
		Vector2		mouvement;

		mouvement = player.transform.position - transform.position;
		mouvement = mouvement / mouvement.magnitude;

		if (mouvement != Vector2.zero)
			entity.anim.SetBool("isWalking", true);
		else
			entity.anim.SetBool("isWalking", false);

		entity.rbody.transform.position += (Vector3)(mouvement * entity.Speed * Time.deltaTime);

		Vector2		playerPos = (transform.position - player.gameObject.transform.position).normalized;
		float angle = Vector2.Angle(Vector2.up, playerPos);
		Vector3 cross = Vector3.Cross(Vector3.up, (Vector3)playerPos);
		
		if (cross.z > 0)
			angle = 360 - angle;
		transform.rotation = Quaternion.Euler(0, 0, -angle);
	}

	void PlayerFireListener() {
		if ((transform.position - player.gameObject.transform.position).magnitude < 6)
			folow = true;
	}

	void OnEnable() {
		if (!em)
			em = GameObject.Find("enemyManager").GetComponent< EnemyManager >();
		em.OnPlayerFire += PlayerFireListener;
	}

	void OnDisable() {
		em.OnPlayerFire -= PlayerFireListener;
	}
}
