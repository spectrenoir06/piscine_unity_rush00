using UnityEngine;
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

	private	Vector2			pathTarget;
	private int				pathIndex;

	void OnDrawGizmos() {
		Gizmos.color = Color.blue;
		if (path.Length > 1)
			for (int i = 0; i < path.Length - 1; i++)
				Gizmos.DrawLine(path[i], path[i + 1]);
	}

	// Use this for initialization
	void Start () {
		instanciatedWeapon = GameObject.Instantiate(weapon, transform.position, transform.rotation) as GameObject;
		instanciatedWeapon.GetComponent< SpriteRenderer >().enabled = false;
		player = GameObject.Find("Player").GetComponent< Player >();
		entity = GetComponent< Entity >();
		entity.setWeapon(instanciatedWeapon.GetComponent< Weapon >());
		pathIndex = 1;
		if (path.Length > 1)
			pathTarget = path[0];
	}

	IEnumerator	 enemyCanShoot() {
		yield return new WaitForSeconds(fireIdle);
		canShoot = true;
	}

	// Update is called once per frame
	void Update () {
		if (!player || gameManager.finished)
			return ;

	//	Debug.DrawRay(transform.position, player.gameObject.transform.position - transform.position);
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

		if (folow && canShoot && seePlayer)
		{
			entity.fireWeapon(transform, Entity.Target.Player);
			canShoot = false;
		}
		StartCoroutine(enemyCanShoot());
	}

	void OnTriggerStay2D(Collider2D coll) {
		if (coll.tag == "Player" && seePlayer)
			folow = true;
		else if (coll.tag == "Player")
			folow = false;
	}

	bool	cmpRoundPosition(Vector2 p1, Vector2 p2) {
		Vector2	_p1;
		Vector2	_p2;

		_p1.x = Mathf.Round(p1.x * 5f) / 5f;
		_p1.y = Mathf.Round(p1.y * 5f) / 5f;
		_p2.x = Mathf.Round(p2.x * 5f) / 5f;
		_p2.y = Mathf.Round(p2.y * 5f) / 5f;
		return (_p1 == _p2);
	}

	void	moveToTarget() {
		Vector2 mouvement = pathTarget - (Vector2)transform.position;
		mouvement /= mouvement.magnitude;

		if (cmpRoundPosition((Vector2)transform.position, pathTarget)) {
			pathTarget = path[pathIndex++ % path.Length];
		}

		entity.anim.SetBool("isWalking", true);

		entity.rbody.transform.position += (Vector3)(mouvement * entity.Speed * Time.deltaTime);

		Vector2		playerPos = ((Vector2)transform.position - pathTarget).normalized;
		float angle = Vector2.Angle(Vector2.up, playerPos);
		Vector3 cross = Vector3.Cross(Vector3.up, (Vector3)playerPos);
		
		if (cross.z > 0)
			angle = 360 - angle;
		transform.rotation = Quaternion.Euler(0, 0, -angle);
	}

	void FixedUpdate() {
		if (path.Length > 1 && !folow)
			moveToTarget();
		if (!folow || !player || gameManager.finished)
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
		Debug.Log ("removed event listener");
		em.OnPlayerFire -= PlayerFireListener;
	}

	void OnDestroy() {
		em.enemies.Remove(gameObject);
	}
}
