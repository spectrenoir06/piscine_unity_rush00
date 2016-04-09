using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public GameObject		weapon;

	private Entity			entity;
	private Player			player;

	void OntriggerEnter2D(Collider2D coll) {

	}

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player").GetComponent< Player >();
		entity = GetComponent< Entity >();
		entity.setWeapon(weapon.GetComponent< Weapon >());
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
	//	entity.fireWeapon(transform, Camera.main.ScreenToWorldPoint((Vector2)Input.mousePosition);
	}
}
