using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public GameObject		weapon;

	private Entity			entity;
	private Player			player;
	public bool				folow = false;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player").GetComponent< Player >();
		entity = GetComponent< Entity >();
		entity.setWeapon(weapon.GetComponent< Weapon >());
	}

	bool playerIsVisible() {
		return (true);
	}

	// Update is called once per frame
	void Update () {
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

		if (folow && playerIsVisible())
			entity.fireWeapon(transform, player.transform.position);
	//	entity.fireWeapon(transform, Camera.main.ScreenToWorldPoint((Vector2)Input.mousePosition);
	}

	void FixedUpdate() {
		if (!folow)
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
