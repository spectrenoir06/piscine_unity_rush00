using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public	float		Speed = 5F;

	private Animator	anim;
	private Rigidbody2D	rbody;
	private GameObject	weapon;


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		rbody = GetComponent< Rigidbody2D >();
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
	}
}