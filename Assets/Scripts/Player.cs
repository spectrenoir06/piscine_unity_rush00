using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public EnemyManager		em;

	private Entity			entity;

	void	Start() {
		entity = GetComponent< Entity >();
	}

	void FixedUpdate() {
		if (gameManager.finished)
			return ;
		Vector2		mouvement;

		mouvement.x = Input.GetAxis("Horizontal");
		mouvement.y = Input.GetAxis("Vertical");

		if (mouvement != Vector2.zero)
			entity.anim.SetBool("isWalking", true);
		else
			entity.anim.SetBool("isWalking", false);

//		entity.rbody.transform.position += (Vector3)(mouvement * entity.Speed / 100);

	// Update is called once per frame
	void Update () {
		if (gameManager.finished)
			return ;

		entity.rbody.MovePosition (entity.rbody.transform.position + (Vector3)(mouvement * entity.Speed / 100));

		Vector2		mouse;
		
		mouse = (Vector2)Camera.main.ScreenToWorldPoint((Vector2)Input.mousePosition);
		mouse -= (Vector2)transform.position;
		
		float	dot = 0 + 1 * mouse.y;
		float	det = 0 - 1 * mouse.x;
		float	angle = Mathf.Atan2(det, dot) * Mathf.Rad2Deg;
		angle += 180;
		
//		entity.transform.rotation = Quaternion.Euler(0, 0, angle);//MoveRotation(angle);
		entity.rbody.MoveRotation(angle);
		//		transform.rotation = Quaternion.Euler(0, 0, angle);

	}

	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown(0) && entity.weapon)
		{
			em.playerFire();
			entity.fireWeapon(transform, Entity.Target.Mouse);
		}
		if (Input.GetMouseButtonDown(1) && entity.weapon)
			entity.dropWeapon(transform, Camera.main.ScreenToWorldPoint((Vector2)Input.mousePosition));

		if (Input.GetKeyDown("e") || Input.GetMouseButtonDown(1))
			entity.pickWeapon();
//		entity.rbody.velocity = Vector2.zero;
	}
}