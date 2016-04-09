using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	public Sprite		attachToBodySprite;
	public GameObject	bullet;
	public float		fireRate;
	public Vector2		target;

	private bool		pickable = true;

	// Use this for initialization
	void Start () {
		target = transform.position;
	}

	// Update is called once per frame
	void Update () {
		if (target != (Vector2)transform.position)
		{
			transform.position = Vector2.Lerp((Vector2)transform.position, target, Time.deltaTime * 5);
		}
	}

	public bool	isPickable() {
		return (pickable);
	}

	public void fire(Transform t) {
		GameObject b = GameObject.Instantiate(bullet, t.position, t.rotation) as GameObject;
		b.GetComponent< Rigidbody2D >().AddForce(new Vector2(10, 0));
		Debug.Log("wepon fire to ");
	}

	IEnumerator		pickableWeapon() {
		yield return new WaitForSeconds(0.5F);
		pickable = true;
	}

	public void	drop(Transform t, Vector2 pos) {
		target = pos;
		gameObject.transform.position = t.position;
		gameObject.transform.rotation = t.rotation;
		gameObject.SetActive(true);
		pickable = false;
		StartCoroutine(pickableWeapon());
	}
}
