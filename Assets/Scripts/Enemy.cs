using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public GameObject		weapon;
	public float			fireIdle = 1F;

	private GameObject		instanciatedWeapon;
	private Entity			entity;
	private Player			player;
	public bool				folow = false;
	private bool			canShoot;

	// Use this for initialization
	void Start () {
		instanciatedWeapon = GameObject.Instantiate(weapon, transform.position, transform.rotation) as GameObject;
		instanciatedWeapon.GetComponent< SpriteRenderer >().enabled = false;
		player = GameObject.Find("Player").GetComponent< Player >();
		entity = GetComponent< Entity >();
		entity.setWeapon(instanciatedWeapon.GetComponent< Weapon >());
	}

	bool playerIsVisible() {
		return (true);
	}

	IEnumerator	 enemyCanShoot() {
		yield return new WaitForSeconds(fireIdle);
		canShoot = true;
	}

	// Update is called once per frame
	void Update () {
		if (!player)
			return ;
		Debug.DrawLine(transform.position, player.gameObject.transform.position);

		Vector2		playerPos = (transform.position - player.gameObject.transform.position).normalized;
		float angle = Vector2.Angle(Vector2.up, playerPos);
		Vector3 cross = Vector3.Cross(Vector3.up, (Vector3)playerPos);
		
		if (cross.z > 0)
			angle = 360 - angle;
		transform.rotation = Quaternion.Euler(0, 0, -angle);
//		Debug.Log(transform.eulerAngles.z - angle);
//		if (transform.eulerAngles.z - angle > 60 && transform.eulerAngles.z < -60)
//			Debug.Log ("View !");

		if (folow && playerIsVisible() && canShoot)
		{
			entity.fireWeapon(transform, player.transform.position);
			canShoot = false;
		}
		StartCoroutine(enemyCanShoot());
	//	entity.fireWeapon(transform, Camera.main.ScreenToWorldPoint((Vector2)Input.mousePosition);
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
	}
}
